using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StajBul.Entity;
using StajBul.Service;

namespace StajBul.WebUI.Controllers
{
    public class UserController : Controller
    {
        private IUserService userService;
        public UserController(IUserService userService)
        {
            this.userService = userService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult List()
        {
            return View(userService.getAll());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(User user)
        {
            DateTime dateTime = DateTime.Now;
            user.CreatedDate = dateTime.ToString();

            if (ModelState.IsValid)
            {
                userService.addUser(user);
                return RedirectToAction("List");
            }

            return View(user);
        }
    }
}
