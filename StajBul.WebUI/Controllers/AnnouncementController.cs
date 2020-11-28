using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public AnnouncementController(IAnnouncementService announcementService, ICategoryService categoryService, IAddressService addressService,ICityService cityService, UserManager<User> userManager)
        {
            this.announcementService = announcementService;
            this.categoryService = categoryService;
            this.addressService = addressService;
            this.cityService = cityService;
            this.userManager = userManager;
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
            announcement.Address.UserId = announcement.UserId;
            announcement.AddressId = addressService.getNextId();
            addressService.addAddress(announcement.Address);
            if (ModelState.IsValid)//Validasyonlar tamamsa demek oluyor yani validasyonlari ekledigim zaman anlamli olacak.
            {
                announcementService.addInternshipAnnouncement(announcement);
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
            
            if (announcement.UserId != user.Id && !User.IsInRole("Admin"))
            {
                return NotFound();
            }
            else
            {
                ViewBag.categories = new SelectList(categoryService.getAll(), "Id", "CategoryName"); //kullaniciya CategoryName'ler gozukcek ama hangisini sectiyse onun Id'si formdan arka tarafa gelcek.
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
                    return RedirectToAction("List");
                }

                ViewBag.categories = new SelectList(categoryService.getAll(), "Id", "CategoryName"); //kullaniciya CategoryName'ler gozukcek ama hangisini sectiyse onun Id'si formdan arka tarafa gelcek.
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
