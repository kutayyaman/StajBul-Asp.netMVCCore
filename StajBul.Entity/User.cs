using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace StajBul.Entity
{
    [Table("user_table")]
    public class User : BaseEntity
    {
        [Required(ErrorMessage = "Adınızı Girmelisiniz")]
        public string UName { get; set; }

        [Required(ErrorMessage = "Şifrenizi Girmelisiniz")]
        [StringLength(maximumLength:55, MinimumLength = 6, ErrorMessage ="6 ile 100 karakter arasında olmalı")]
        public string Pswd { get; set; }
       
        [Required(ErrorMessage = "Soyadınızı Girmelisiniz")]
        public string UserSurname { get; set; }

        [Range(1,150,ErrorMessage ="Yaşınız 1'den küçük veya gerçek dışı büyüklükte olamaz.")]
        [Required(ErrorMessage = "Yaşınızı Girmelisiniz")]
        public int Age { get; set; }

        [EmailAddress(ErrorMessage = "Geçersiz Mail Adresi")]
        [Required(ErrorMessage = "Mail Adresinizi Girmelisiniz")]
        public string Mail { get; set; }
        public UserType UserType { get; set; } = UserType.USER;
        public virtual ICollection<Address> Addresses { get; set; }
        public virtual ICollection<InternshipAnnouncement> InternshipAnnouncements { get; set; }
    }

    public enum UserType
    {
        USER,
        ADMIN
    }
}
