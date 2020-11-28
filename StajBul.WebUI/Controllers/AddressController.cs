using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using StajBul.Entity;
using StajBul.Service;

namespace StajBul.WebUI.Controllers
{
    public class AddressController : Controller
    {
        private IAddressService addressService;
        private ICityService cityService;
        private UserManager<User> userManager;
        public AddressController(IAddressService addressService, UserManager<User> userManager, ICityService cityService)
        {
            this.addressService = addressService;
            this.userManager = userManager;
            this.cityService = cityService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Create()
        {
            User user = await userManager.FindByNameAsync(User.Identity.Name);
            if (user.AddressId != null)
            {
                TempData["message"] = $"{user.UserName} Sizin Zaten Adres Kaydiniz Var Güncelleyebilirsiniz.";
                return RedirectToAction("Edit", "Address", new { id = user.AddressId });
            }
            ViewBag.cities = new SelectList(cityService.getAll(), "Id", "CityName");
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(Address address, string userName)
        {
            User user = await userManager.FindByNameAsync(userName);
            if(user.AddressId != null)
            {
                TempData["message"] = $"{user.UserName} Sizin Zaten Adres Kaydiniz Var Guncelleyebilirsiniz.";
                return RedirectToAction("Edit", "Address", new { id = user.AddressId });
            }
            if(!User.IsInRole("Admin") && User.Identity.Name != user.UserName)
            {
                return NotFound();
            }
            DateTime dateTime = DateTime.Now;
            address.CreatedDate = dateTime.ToString();
            if (ModelState.IsValid)
            {
                addressService.addAddress(address);
                user.AddressId = addressService.getNextId()-1; //bura onemli kontrol et
                await userManager.UpdateAsync(user);
                return RedirectToAction("Profile","User");
            }

            ViewBag.cities = new SelectList(addressService.getAll(), "Id", "CityName");
            return View(address);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            User user = await userManager.FindByNameAsync(User.Identity.Name);
            if (!User.IsInRole("Admin") && user.AddressId != id)
            {
                return NotFound();
            }
            ViewBag.cities = new SelectList(cityService.getAll(), "Id", "CityName");
            return View(addressService.getById(id));
        }

        [HttpPost]
        [Authorize]
        public IActionResult EditPost(Address address)
        {
            if (ModelState.IsValid)
            {
                address.ModifiedDate = DateTime.Now.ToString();
                addressService.updateAddress(address);
                TempData["message"] = $"{address.AddressName} Güncellendi.";
                return RedirectToAction("Profile","User");
            }

            ViewBag.cities = new SelectList(cityService.getAll(), "Id", "CityName");
            return View(address);
        }

    }
}
