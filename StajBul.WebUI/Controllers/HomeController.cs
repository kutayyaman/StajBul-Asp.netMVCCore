using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StajBul.Entity;
using StajBul.Service;

namespace StajBul.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private IAnnouncementService announcementService;
        public HomeController(IAnnouncementService announcementService)
        {
            this.announcementService = announcementService;
        }
        public IActionResult Index(bool? isIntern)
        {
            IQueryable<InternshipAnnouncement> announcements = (isIntern == true) ? announcementService.getAllStajyerAnnouncement() : announcementService.getAllCompanyAnnouncement();
            return View(announcements);
        }

        public IActionResult Details(int id)
        {
            InternshipAnnouncement announcement = announcementService.getById(id);
            return View(announcement);
        }
    }
}
