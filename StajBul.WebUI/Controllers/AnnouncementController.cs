﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmailService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using StajBul.Data.Abstract;
using StajBul.Entity;
using StajBul.Service;

namespace StajBul.WebUI.Controllers
{
    public class AnnouncementController : Controller
    {
        private IAnnouncementService announcementService;
        private ICategoryService categoryService;
        private IAddressService addressService;
        private ICityService cityService;
        private UserManager<User> userManager;
        private readonly IEmailSender emailSender;
        public AnnouncementController(IAnnouncementService announcementService, ICategoryService categoryService, IAddressService addressService,ICityService cityService, UserManager<User> userManager,IEmailSender emailSender)
        {
            this.announcementService = announcementService;
            this.categoryService = categoryService;
            this.addressService = addressService;
            this.cityService = cityService;
            this.userManager = userManager;
            this.emailSender = emailSender;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult List()
        {
            IQueryable<InternshipAnnouncement> announcements = announcementService.getAll();
            return View(announcements);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Create()
        {
            ViewBag.categories = new SelectList(categoryService.getAll(), "Id", "CategoryName"); //kullaniciya CategoryName'ler gozukcek ama hangisini sectiyse onun Id'si formdan arka tarafa gelcek.
            ViewBag.cities = new SelectList(cityService.getAll(), "Id", "CityName"); //kullaniciya CategoryName'ler gozukcek ama hangisini sectiyse onun Id'si formdan arka tarafa gelcek.
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(InternshipAnnouncement announcement)
        {
            var user = await userManager.FindByNameAsync(User?.Identity?.Name);
            announcement.UserId = user.Id;
            DateTime dateTime = DateTime.Now;
            string createdDate = dateTime.ToString();
            announcement.CreatedDate = createdDate;
            if (ModelState.IsValid)//Validasyonlar tamamsa demek oluyor yani validasyonlari ekledigim zaman anlamli olacak.
            {
                //announcement.Address.UserId = announcement.UserId; //Her adres bir user'a ait olcak diye bir sey yok bu ilanin adresi
                ///announcement.AddressId = addressService.getNextId(); //Bu sql serverda hata veriyor
                addressService.addAddress(announcement.Address);

                // announcement.Id = announcementService.getNextId();//Bu sql serverda hata veriyor
                announcementService.addInternshipAnnouncement(announcement);
                Category category = categoryService.getById(announcement.CategoryId);
                var message = new Message(new string[] { "yamankutay1@gmail.com" }, "Yeni İlan Eklendi","Kullanıcı Adı : " + announcement.User.UserName + "\nAdı : "+ user.UserRealName + "\nİlan Başlığı : " + announcement.Title + "\nİlan İçeriği : " + announcement.Explanation + "\nKategorisi : " + category.CategoryName + "\nİlan Tipi : " +announcement.AnnouncementType);
                emailSender.SendEmail(message);
                return RedirectToAction("Details","Home", new { id = announcement.Id });
            }

            ViewBag.categories = new SelectList(categoryService.getAll(), "Id", "CategoryName"); //kullaniciya CategoryName'ler gozukcek ama hangisini sectiyse onun Id'si formdan arka tarafa gelcek.
            return View(announcement);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            User user = await userManager.FindByNameAsync(User?.Identity?.Name);
            InternshipAnnouncement announcement = announcementService.getById(id);
            if(announcement == null)
            {
                return NotFound();
            }
            if (announcement.UserId != user.Id && !User.IsInRole("Admin"))
            {
                return NotFound();
            }
            else
            {
                ViewBag.categories = new SelectList(categoryService.getAll(), "Id", "CategoryName"); //kullaniciya CategoryName'ler gozukcek ama hangisini sectiyse onun Id'si formdan arka tarafa gelcek.
                ViewBag.cities = new SelectList(cityService.getAll(), "Id", "CityName");
                return View(announcementService.getById(id));
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> EditPost(InternshipAnnouncement announcement)
        {
            User user = await userManager.FindByNameAsync(User?.Identity?.Name);
            if (announcement.UserId != user.Id && !User.IsInRole("Admin"))
            {
                return NotFound();
            }
            else
            {
                if (ModelState.IsValid)
                {
                    announcement.ModifiedDate = DateTime.Now.ToString();
                    announcementService.updateInternshipAnnouncement(announcement);
                    TempData["message"] = $"{announcement.Title} Güncellendi.";
                    return RedirectToAction("Index","Home");
                }

                ViewBag.categories = new SelectList(categoryService.getAll(), "Id", "CategoryName"); //kullaniciya CategoryName'ler gozukcek ama hangisini sectiyse onun Id'si formdan arka tarafa gelcek.
                ViewBag.cities = new SelectList(cityService.getAll(), "Id", "CityName");
                return View(announcement);
            }    
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            User user = await userManager.FindByNameAsync(User?.Identity?.Name);
            InternshipAnnouncement announcement = announcementService.getById(id);
            if (announcement.UserId != user.Id && !User.IsInRole("Admin"))
            {
                return NotFound();
            }
            return View(announcementService.getById(id));
        }

        [HttpPost, ActionName("Delete")]
        [Authorize]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            User user = await userManager.FindByNameAsync(User?.Identity?.Name);
            InternshipAnnouncement announcement = announcementService.getById(id);
            if (announcement.UserId != user.Id && !User.IsInRole("Admin"))
            {
                return NotFound();
            }
            announcementService.deleteInternshipAnnouncementById(id);
            TempData["message"] = id+ "id'li Kayıt Silindi.";
            return RedirectToAction("Index","Home");
        }

    }
}
