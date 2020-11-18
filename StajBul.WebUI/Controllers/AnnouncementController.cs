using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public AnnouncementController(IAnnouncementService announcementService, ICategoryService categoryService, IAddressService addressService)
        {
            this.announcementService = announcementService;
            this.categoryService = categoryService;
            this.addressService = addressService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult List()
        {
            IQueryable<InternshipAnnouncement> announcements = announcementService.getAll();
            return View(announcements);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.categories = new SelectList(categoryService.getAll(), "Id", "CategoryName"); //kullaniciya CategoryName'ler gozukcek ama hangisini sectiyse onun Id'si formdan arka tarafa gelcek.
            return View();
        }

        [HttpPost]
        public IActionResult Create(InternshipAnnouncement announcement)
        {
            //TO DO
            //BURADAKI USERID ARTIK SISTEMDEKI USER KIMSE ONUN IDSI OLCAK
            //CITY BILGISI ICIN TABLOYA BUTUN SEHIRLER EKLENECEK VE O SEHIRLER BIR LISTBOX OLARAK GELCEK KULLANICI ORADAN SECECEK
            Random random = new Random();
            announcement.UserId = random.Next(1, 4);
            //yukardaki iki satirda kalkacak unutma
            DateTime dateTime = DateTime.Now;
            string createdDate = dateTime.ToString();
            announcement.CreatedDate = createdDate;
            announcement.Address.CityId = 1;
            announcement.Address.UserId = announcement.UserId;
            addressService.addAddress(announcement.Address);
            announcement.AddressId = addressService.getNextId();
            if (ModelState.IsValid)//Validasyonlar tamamsa demek oluyor yani validasyonlari ekledigim zaman anlamli olacak.
            {
                announcementService.addInternshipAnnouncement(announcement);
                return RedirectToAction("List");
            }

            return View(announcement);
        }
    }
}
