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
        [Required(ErrorMessage = "Adınızı Girmelisiniz")]
        public string UserRealName { get; set; }

        [Required(ErrorMessage ="Kullanıcı Adınızı Girmelisiniz")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Şifrenizi Girmelisiniz")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Mail Adresinizi Girmelisiniz")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Soyadınızı Girmelisiniz")]
        public string UserSurname { get; set; }

        [Range(1, 150, ErrorMessage = "Yaşınız 1'den küçük veya gerçek dışı büyüklükte olamaz.")]
        [Required(ErrorMessage = "Yaşınızı Girmelisiniz")]
        public int Age { get; set; }
        public RowStatus RowStatus { get; set; } = RowStatus.ACTIVE;
    }
}
