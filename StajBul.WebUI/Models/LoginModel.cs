using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StajBul.WebUI.Models
{
    public class LoginModel
    {
        [Required]
        [UIHint("email")]
        [Display(Name = "UserName")]
        public string UserName { get; set; }
        [Required]
        [UIHint("password")]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}
