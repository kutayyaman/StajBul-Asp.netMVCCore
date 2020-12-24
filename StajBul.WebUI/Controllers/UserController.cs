using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmailService;
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
        private readonly IEmailSender emailSender;

        public UserController(IUserService userService, UserManager<User> userManager, IPasswordValidator<User> passwordValidator, IPasswordHasher<User> passwordHasher, IUserValidator<User> userValidator, SignInManager<User> signInManager, IAnnouncementService announcementService, IEmailSender emailSender)
        {
            this.userService = userService;
            this.userManager = userManager;
            this.passwordHasher = passwordHasher;
            this.passwordValidator = passwordValidator;
            this.userValidator = userValidator;
            this.signInManager = signInManager;
            this.announcementService = announcementService;
            this.emailSender = emailSender;
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
            bool mailExists = userManager.Users.Any(u => u.Email == registerModel.Email);
            if (mailExists)
            {
                ModelState.AddModelError("", "Bu Mail Adresi İle Bir Hesap Bulunuyor.");
            }
            bool usernameExists = userManager.Users.Any(u => u.UserName == registerModel.UserName);
            if (usernameExists)
            {
                ModelState.AddModelError("", "Bu Kullanıcı Adı Kullanılıyor.");
            }
            if (ModelState.IsValid)
            {
                User user = new User();
                user.UserName = registerModel.UserName;
                user.UserSurname = registerModel.UserSurname;
                user.Age = registerModel.Age;
                user.Email = registerModel.Email;
                user.UserRealName = registerModel.UserRealName;
                user.CreatedDate = DateTime.Now.ToString();
                

                var result = await userManager.CreateAsync(user,registerModel.Password);

                if (result.Succeeded && ModelState.IsValid)
                {
                    TempData["message"] = $"{user.UserName} Kullanıcı Adıyla Üyelik Oluşturuldu.";
                    return View("Login");
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
                    else
                    {
                        ModelState.AddModelError("", "Geçersiz Kullanıcı Adı Veya Şifre");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Geçersiz Kullanıcı Adı Veya Şifre");
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
        public async Task<IActionResult> Profile(string username ,string? id, int page = 1)
        {
            if (page <= 0)
            {
                page = 1;
            }
            AnnouncementListAndPaginationModel returnModel = new AnnouncementListAndPaginationModel();
            returnModel.CategoryFilterModel = new CategoryFilterModel();
            returnModel.CategoryFilterModel.id = id;
            returnModel.CategoryFilterModel.username = username;
            UserProfileViewModel model = new UserProfileViewModel();
            PaginationModel paginationModel = new PaginationModel();
            paginationModel.CurrentPage = page;
            paginationModel.PageSize = 8;
            returnModel.PaginationModel = paginationModel;
            if (string.IsNullOrEmpty(username))
            {
                if (!string.IsNullOrEmpty(id)) //username ile degilde id ile profile ulasmak istemis
                {
                    model.User = userService.getById(int.Parse(id));
                    if (model.User == null)
                        return NotFound();
                    var AnnouncementQueryable = announcementService.getByUserId(model.User.Id);
                    returnModel.PaginationModel.TotalItem = AnnouncementQueryable.Count();
                    model.Announcements = AnnouncementQueryable.Skip(((returnModel.PaginationModel.CurrentPage - 1) * returnModel.PaginationModel.PageSize)).Take(returnModel.PaginationModel.PageSize).ToList();
                    //model.Announcements = announcementService.getByUserId(model.User.Id).ToList();
                }
                else //adam username veya id yollamamis
                {     
                    if (!User.Identity.IsAuthenticated)
                    {
                        return RedirectToAction("Login");
                    }
                    else //profile sayfasina gitmek istiyor ancak kiminkine gitmek istedigni yazmamis ve siteye giris yapmis birisi varsa
                    {
                        model.User = userService.getByUserName(User.Identity.Name);
                        var AnnouncementQueryable = announcementService.getByUserId(model.User.Id);
                        returnModel.PaginationModel.TotalItem = AnnouncementQueryable.Count();
                        model.Announcements = AnnouncementQueryable.Skip(((returnModel.PaginationModel.CurrentPage - 1) * returnModel.PaginationModel.PageSize)).Take(returnModel.PaginationModel.PageSize).ToList();
                    }
                }
            }
            else
            {
                model.User = userService.getByUserName(username);
                if(model.User == null)
                {
                    TempData["message"] = "Böyle Bir Profil Bulunamadı.";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    var AnnouncementQueryable = announcementService.getByUserId(model.User.Id);
                    returnModel.PaginationModel.TotalItem = AnnouncementQueryable.Count();
                    model.Announcements = AnnouncementQueryable.Skip(((returnModel.PaginationModel.CurrentPage - 1) * returnModel.PaginationModel.PageSize)).Take(returnModel.PaginationModel.PageSize).ToList();
                }
            }
            returnModel.UserProfileViewModel = model;
            return View(returnModel);
        }


        [Authorize]
        [HttpGet]
        public async Task<IActionResult> AddAboutMe(int id)
        {
            User user = await userManager.FindByNameAsync(User.Identity.Name);
            if(user.Id != id && !User.IsInRole("Admin"))
            {
                return NotFound();
            }
            return View(user);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddAboutMePost(User user)
        {
            if(user.Id != userManager.FindByNameAsync(User.Identity.Name).Id && !User.IsInRole("Admin"))
            {
                return NotFound();
            }
            string aboutMe = user.AboutMe;
            user = await userManager.FindByIdAsync(user.Id.ToString());
            user.AboutMe = aboutMe;
            await userManager.UpdateAsync(user);
            return RedirectToAction("Profile","User");
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> EditAboutMe(int id)
        {
            User user = await userManager.FindByNameAsync(User.Identity.Name);
            if (user.Id != id && !User.IsInRole("Admin"))
            {
                return NotFound();
            }
            return View(user);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> EditAboutMePost(User user)
        {

            User userInSystem = await userManager.FindByNameAsync(User.Identity.Name);
            if (user.Id != userInSystem.Id && !User.IsInRole("Admin"))
            {
                return NotFound();
            }
            userInSystem.AboutMe = user.AboutMe;
            await userManager.UpdateAsync(userInSystem);
            return RedirectToAction("Profile","User");
        }

        [HttpGet]
        public IActionResult IForgetMyPassword()
        {
            
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> IForgetMyPassword(string email)
        {
            User user = await userManager.FindByEmailAsync(email);
            if(user == null)
            {
                ModelState.AddModelError("", "Kayıtsız Mail Adresi");
            }
            else
            {
                string token = await userManager.GeneratePasswordResetTokenAsync(user);
                string link = "https://localhost:44319/User/ResetPassword?Email=" + user.Email + "&Token=" + token;
                var message = new Message(new string[] { user.Email.ToString() }, "StajimiBul.com Şifreni Sıfırla","Kullanıcı Adınız : "+ user.UserName +"\n"+ "StajimiBul Şifreni Sıfırlaman İçin Link: " + link);
                emailSender.SendEmail(message);
                TempData["message"] = "Şifrenizi Sıfırlamanız İçin Mail Yolladık 1-2 DK Gecikmeler Olabilir. Eğer Maili Göremiyorsanız Spam Kısmına Bakın Oraya Atmış Olabilir.";
                return RedirectToAction("Login");

            }

            return View();
        }

        [HttpGet]
        public IActionResult ResetPassword(string token, string email)
        {
            if(token == null || email == null)
            {
                ModelState.AddModelError("", "Geçersiz Resetleme Token'ı geldi");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);
                if(user != null)
                {
                    model.Token =model.Token.Replace(" ", "+");
                    var result = await userManager.ResetPasswordAsync(user, model.Token, model.Password);
                    if (result.Succeeded)
                    {
                        TempData["message"] = user.UserName +" Şifrenizi Yenilediniz Şimdi Giriş Yapabilirsiniz";
                        return RedirectToAction("Login");
                    }
                    foreach(var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View(model);
                }
                TempData["message"] = model.Email + " Bu Maile Ait Bir Kullanıcı Bulunamadı";
                RedirectToAction("Login");

            }
            return View(model);
        }
    }
}
