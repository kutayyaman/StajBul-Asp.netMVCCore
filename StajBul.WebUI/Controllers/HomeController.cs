using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StajBul.Service;

namespace StajBul.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private IAnnouncementService announcementService;
        private ICategoryService categoryService;

        public HomeController(IAnnouncementService announcementService, ICategoryService categoryService)
        {
            this.announcementService = announcementService;
            this.categoryService = categoryService;
        }
        public IActionResult Index()
        {
            return View(announcementService.getAll());
        }

        public IActionResult Details()
        {
            return View();
        }

    }
}
