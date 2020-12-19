using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StajBul.Entity;
using StajBul.WebUI.Models;

namespace StajBul.WebUI.Controllers
{
    public class RoleController : Controller
    {
        private RoleManager<IdentityRole<int>> roleManager;
        private UserManager<User> userManager;
        private IRoleValidator<IdentityRole<int>> roleValidator;


        public RoleController(RoleManager<IdentityRole<int>> roleManager, IRoleValidator<IdentityRole<int>> roleValidator,UserManager<User> userManager)
        {
            this.roleManager = roleManager;
            this.roleValidator = roleValidator;
            this.userManager = userManager;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult List()
        {

            return View(roleManager.Roles);
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create (string Name)
        {
            if (ModelState.IsValid)
            {
                var result = await roleManager.CreateAsync(new IdentityRole<int>(Name));
                if (result.Succeeded)
                {
                    return RedirectToAction("List");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }

                return View(Name);
        }

        //[HttpGet]
        //public async Task<IActionResult> Edit(string id)
        //{
        //    return View(await roleManager.FindByIdAsync(id));
        //}

        //[HttpPost]
        //public async Task<IActionResult> EditPost(string Id, string Name)
        //{
        //    var role = await roleManager.FindByIdAsync(Id);
        //    IdentityResult validRole = null;
        //    if (role != null)
        //    {
        //        role.Name = Name;

        //        validRole = await roleValidator.ValidateAsync(roleManager, role);

        //        if (!validRole.Succeeded)
        //        {
        //            foreach (var error in validRole.Errors)
        //            {
        //                ModelState.AddModelError("", error.Description);
        //            }
        //        }

        //        if (validRole.Succeeded && ModelState.IsValid)
        //        {
        //            var result = await roleManager.UpdateAsync(role);
        //            if (result.Succeeded)
        //            {
        //                TempData["message"] = $"{role.Name} Rolü Güncellendi.";
        //                return RedirectToAction("List");
        //            }
        //        }
        //    }
        //    else //Role isnot available
        //    {
        //        ModelState.AddModelError("", "Kullanıcı Bulunamadı");
        //    }

        //    return View("Edit", role);
        //}

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(string id)
        {
            return View(await roleManager.FindByIdAsync(id));
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirm(string id)
        {
            var role = await roleManager.FindByIdAsync(id);

            if (role != null)
            {
                var result = await roleManager.DeleteAsync(role);

                if (result.Succeeded)
                {
                    TempData["message"] = role.Name + " Rolü Silindi.";
                    return RedirectToAction("List");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            TempData["message"] = role.Name + "Böyle Bir Rol Bulunamadı.";
            return RedirectToAction("List");
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit (string id)
        {
            IdentityRole<int> role = await roleManager.FindByIdAsync(id);

            var members = new List<User>();
            var nonmembers = new List<User>();

            var users = await userManager.Users.ToListAsync(); //asenkron olarak yapmazsak burasi daha bitmeden foreache giriyor ve orada sorgu calistirmaya calisiyor ama henuz bu bitmedigi icin hata veriyor

            foreach (var user in users)
            {
                var list = (await userManager.IsInRoleAsync(user, role.Name)) ? members : nonmembers;
                list.Add(user);
            }

            var model = new RoleDetails()
            {
                Role = role,
                Members = members,
                NonMembers = nonmembers
            };

            return View(model);

        }

        [HttpPost]
        [Authorize(Roles = "Admin")] //burada tek tek rolleri atamaktansa hepsini tek bir kere database ile iletisim kurarak yap bunu duzel cunku suan 100 kisiyi degistirirse 100 kere databaseye baglanip baglantiyi kesecek bunu 1'e dusurebiliriz.
        public async Task<IActionResult> EditPost(RoleEditModel roleEditModel)
        {
            IdentityResult result;
            if (ModelState.IsValid)
            {
                foreach (var userId in roleEditModel.IdsToAdd ?? new string[]{ })
                {
                    var user = await userManager.FindByIdAsync(userId);
                    if(user != null)
                    {
                        result = await userManager.AddToRoleAsync(user, roleEditModel.RoleName);

                        if (!result.Succeeded)
                        {
                            foreach(var error in result.Errors)
                            {
                                ModelState.AddModelError("", error.Description);
                            }
                        }
                    }
                }
                foreach (var userId in roleEditModel.IdsToDelete ?? new string[] { })
                {
                    var user = await userManager.FindByIdAsync(userId);
                    if (user != null)
                    {
                        result = await userManager.RemoveFromRoleAsync(user, roleEditModel.RoleName);

                        if (!result.Succeeded)
                        {
                            foreach (var error in result.Errors)
                            {
                                ModelState.AddModelError("", error.Description);
                            }
                        }
                    }
   
                } 

            }

            if (ModelState.IsValid)
            {
                TempData["message"] = "Yetkilendirme İşlemleri Tamamlandı.";
                return RedirectToAction("List");
            }
            else
            {
                TempData["message"] = "Yetkilendirme İşlemlerinde Sorun Çıktı!!!!";
                return RedirectToAction("Edit", roleEditModel.RoleId);
            }
        }
    }
}
