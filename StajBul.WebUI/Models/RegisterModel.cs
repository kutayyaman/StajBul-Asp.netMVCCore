using StajBul.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StajBul.WebUI.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "You should enter your name")]
        [Display(Name = "UserRealName")]
        public string UserRealName { get; set; }

        [Required(ErrorMessage = "You should enter your username")]
        [Display(Name = "UserName")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "You should enter your password")]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "You should enter your mail")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "You should enter your surname")]
        [Display(Name = "UserSurname")]
        public string UserSurname { get; set; }

        [Range(1, 150, ErrorMessage = "Your age cannot be less than 1 or unreal")]
        [Required(ErrorMessage = "You should enter your age")]
        [Display(Name = "Age")]
        public int Age { get; set; }
        public RowStatus RowStatus { get; set; } = RowStatus.ACTIVE;
    }
}
