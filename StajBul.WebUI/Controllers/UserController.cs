using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StajBul.Entity;
using StajBul.Service;
using StajBul.WebUI.Models;

namespace StajBul.WebUI.Controllers
{
    public class UserController : Controller
    {
        private IUserService userService;

        private UserManager<User> userManager;
        private IPasswordValidator<User> passwordValidator;
        private IPasswordHasher<User> passwordHasher;
        private IUserValidator<User> userValidator;
        private SignInManager<User> signInManager;
        private IAnnouncementService announcementService;

        public UserController(IUserService userService, UserManager<User> userManager, IPasswordValidator<User> passwordValidator, IPasswordHasher<User> passwordHasher, IUserValidator<User> userValidator, SignInManager<User> signInManager, IAnnouncementService announcementService)
        {
            this.userService = userService;
            this.userManager = userManager;
            this.passwordHasher = passwordHasher;
            this.passwordValidator = passwordValidator;
            this.userValidator = userValidator;
            this.signInManager = signInManager;
            this.announcementService = announcementService;
        }

        [Authorize]
        [Authorize(Roles = "Admin")]
        public IActionResult List()
        {
            return View(userManager.Users.Where(u => u.RowStatus == RowStatus.ACTIVE).OrderByDescending(u => u.Id));
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new RegisterModel() { });
        }

        [HttpPost]
        public async Task<IActionResult> Create(RegisterModel registerModel)
        {
            if (ModelState.IsValid)
            {
                User user = new User();
                user.UserName = registerModel.UserName;
                user.UserSurname = registerModel.UserSurname;
                user.Age = registerModel.Age;
                user.Email = registerModel.Email;
                user.UserRealName = registerModel.UserRealName;

                var result = await userManager.CreateAsync(user,registerModel.Password);

                if (result.Succeeded)
                {
                    return View("Completed", user);
                }

                else
                {
                    foreach(var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
                
            }

            return View(registerModel);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            User user = await userManager.FindByNameAsync(User?.Identity?.Name);
            if (id != user?.Id && !User.IsInRole("Admin"))
            {
                return NotFound();
            }
            return View(userService.getById(id));
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> EditPost(string Id, string Password, string Email, int Age)
        {

            var user = await userManager.FindByIdAsync(Id);
            if (Id != user?.Id.ToString() && !User.IsInRole("Admin"))
            {
                return NotFound();
            }
            if (user != null)
            {
                user.Email = Email;
                user.Age = Age;

                IdentityResult validPass = null;
                IdentityResult validUser = null;
                if (!string.IsNullOrEmpty(Password))
                {
                    validPass = await passwordValidator.ValidateAsync(userManager, user, Password);

                    if (validPass.Succeeded)
                    {
                        user.PasswordHash = passwordHasher.HashPassword(user, Password);
                    }
                    else
                    {
                        foreach(var error in validPass.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                    }
                }

                validUser = await userValidator.ValidateAsync(userManager, user);

                if (!validUser.Succeeded)
                {
                    foreach (var error in validUser.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }

                if (validUser.Succeeded && ModelState.IsValid)
                {
                    var result = await userManager.UpdateAsync(user);
                    if(result.Succeeded)
                    {
                        TempData["message"] = $"{user.Email} Mail Adresli Kullanıcı Güncellendi.";
                        return RedirectToAction("List");
                    }
                }
            }
            else //User isnot available
            {
                ModelState.AddModelError("", "Kullanıcı Bulunamadı");
            }

            return View("Edit",user);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            User user = await userManager.FindByNameAsync(User?.Identity?.Name);
            if (id != user?.Id && !User.IsInRole("Admin"))
            {
                return NotFound();
            }
            return View(userService.getById(id));
        }

        [HttpPost, ActionName("Delete")]
        [Authorize]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            User user = await userManager.FindByNameAsync(User?.Identity?.Name);
            if (id != user?.Id && !User.IsInRole("Admin"))
            {
                return NotFound();
            }
            userService.deleteUserById(id);
            TempData["message"] = id + "id'li Kayıt Silindi.";
            return RedirectToAction("List");
        }

        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken] //ne ise yariyor arastir.
        public async Task<IActionResult> Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByNameAsync(model.UserName);
                if(user != null)
                {
                    await signInManager.SignOutAsync();
                    var result = await signInManager.PasswordSignInAsync(user, model.Password, false, false);
                    if (result.Succeeded)
                    {
                        return Redirect(returnUrl ?? "/");
                    }
                }
                else
                {
                    ModelState.AddModelError("UserName", "Geçersiz Kullanıcı Adı Veya Şifre");
                }
                
            }
            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            TempData["message"] = "Başarıyla Çıkış Yaptınız.";
            return RedirectToAction("login");
        }

        [HttpGet]
        public async Task<IActionResult> Profile(string username)
        {
            UserProfileViewModel model = new UserProfileViewModel();

            if (string.IsNullOrEmpty(username))
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return RedirectToAction("Login");
                }
                else //profile sayfasina gitmek istiyor ancak kiminkine gitmek istedigni yazmamis ve siteye giris yapmis birisi varsa
                {
                    model.User = await userManager.FindByNameAsync(User.Identity.Name);
                    model.Announcements = announcementService.getByUserId(model.User.Id).ToList();
                }
            }
            else
            {
                model.User = await userManager.FindByNameAsync(username);
                if(model.User == null)
                {
                    TempData["message"] = "Böyle Bir Profil Bulunamadı.";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    model.Announcements = announcementService.getByUserId(model.User.Id).ToList();
                }
            }
            return View(model);
        }
    }
}
