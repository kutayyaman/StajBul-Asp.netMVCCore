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

        [HttpGet]
        public IActionResult Edit(int id)
        {
            return View(userService.getById(id));
        }

        [HttpPost]
        public IActionResult EditPost(User user)
        {
            if (ModelState.IsValid)
            {
                user.ModifiedDate = DateTime.Now.ToString();
                userService.updateUser(user);
                TempData["message"] = $"{user.Mail} Mail Adresli Kullanıcı Güncellendi.";
                return RedirectToAction("List");
            }

            return View(user);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            return View(userService.getById(id));
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirm(int id)
        {
            userService.deleteUserById(id);
            TempData["message"] = id + "id'li Kayıt Silindi.";
            return RedirectToAction("List");
        }
    }
}
