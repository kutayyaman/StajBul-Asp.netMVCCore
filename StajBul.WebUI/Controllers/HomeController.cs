using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StajBul.Entity;
using StajBul.Service;
using StajBul.WebUI.Models;

namespace StajBul.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private IAnnouncementService announcementService;
        public HomeController(IAnnouncementService announcementService)
        {
            this.announcementService = announcementService;
        }
        public IActionResult Index(bool? isIntern, int? categoryId, string? searchedWords)
        {
            CategoryFilterModel categoryFilterModel = new CategoryFilterModel(categoryId,isIntern,searchedWords);
            ViewBag.categoryFilterModel = categoryFilterModel;
            return View();
        }

        public IActionResult Details(int id)
        {
            InternshipAnnouncement announcement = announcementService.getById(id);
            return View(announcement);
        }
    }
}
