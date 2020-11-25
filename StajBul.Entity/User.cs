using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StajBul.Entity
{
    [Table("user_table")]
    public class User : IdentityUser<int>
    {

        [Required(ErrorMessage = "Soyadınızı Girmelisiniz")]
        public string UserSurname { get; set; }

        [Range(1,150,ErrorMessage ="Yaşınız 1'den küçük veya gerçek dışı büyüklükte olamaz.")]
        [Required(ErrorMessage = "Yaşınızı Girmelisiniz")]
        public int Age { get; set; }
        public RowStatus RowStatus { get; set; } = RowStatus.ACTIVE;
        public virtual ICollection<Address> Addresses { get; set; }
        public virtual ICollection<InternshipAnnouncement> InternshipAnnouncements { get; set; }
    }

}
