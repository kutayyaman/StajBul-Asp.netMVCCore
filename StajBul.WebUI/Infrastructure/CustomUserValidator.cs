using Microsoft.AspNetCore.Identity;
using StajBul.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StajBul.WebUI.Infrastructure
{
    public class CustomUserValidator : IUserValidator<User>
    {
        public Task<IdentityResult> ValidateAsync(UserManager<User> manager, User user)
        {
            List<IdentityError> errors = new List<IdentityError>();
            //if(user.UserName.Length < 5) //Bu kontrolu ben Startup.cs icinde ConfigureServices methodunda ekledim ama orada ekleyemeyecegimiz seyleri burada yapabiliiriz.
            //{
            //    return Task.FromResult(IdentityResult.Failed(new IdentityError() {
            //        Code = "EmailLengthError",
            //        Description = "Kullanıcı Adınız En Az 5 Karakterden Oluşmalı"
            //    }));
            //}
            if(!user.Email.Contains("@") || !user.Email.Contains("."))
            {
                errors.Add(new IdentityError()
                {
                    Code = "EmailError",
                    Description = "Mail Adresinizin Formatını Kontrol Edin."
                });

            }

            if (user.Age < 1)
            {
                errors.Add(new IdentityError()
                {
                    Code = "AgeError",
                    Description = "Yaşınızı Kontrol Edin"
                });
            }

            //bu alttaki iki validasyon islemi sifre sifirlarkende calisiyor o yuzden kapattim buna farkli bir cozum lazim
            //bool mailExists = manager.Users.Any(u => u.Email == user.Email);
            //if (mailExists)
            //{
            //    errors.Add(new IdentityError()
            //    {
            //        Code = "EmailExistError",
            //        Description = "Bu Mail Adresi Daha Önceden Üye Olmuş"
            //    });
            //}
            //bool usernameExists = manager.Users.Any(u => u.UserName == user.UserName);
            //if (usernameExists)
            //{
            //    errors.Add(new IdentityError()
            //    {
            //        Code = "UsernameExists",
            //        Description = "Bu Kullanıcı Adı Daha Önceden Üye Olmuş"
            //    });
            //} 

            return Task.FromResult(errors.Count == 0 ? IdentityResult.Success : IdentityResult.Failed(errors.ToArray()));

        }
    }
}
