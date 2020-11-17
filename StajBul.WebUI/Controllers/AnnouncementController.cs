using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StajBul.Data.Abstract;

namespace StajBul.WebUI.Controllers
{
    public class AnnouncementController : Controller
    {
        private IAnnouncementRepo announcementRepo;
        public AnnouncementController(IAnnouncementRepo announcementRepo)
        {
            this.announcementRepo = announcementRepo;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
