using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StajBul.WebUI.Models
{
    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name ="Şifreni Onayla")]
        [Compare("Password", ErrorMessage ="Şifre Eşleşmiyor")]
        public string ConfirmPassword { get; set; }

        public string Token { get; set; }

    }
}
