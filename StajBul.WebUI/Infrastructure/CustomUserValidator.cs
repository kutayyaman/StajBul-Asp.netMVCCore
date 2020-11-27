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

            return Task.FromResult(errors.Count == 0 ? IdentityResult.Success : IdentityResult.Failed(errors.ToArray()));

        }
    }
}
