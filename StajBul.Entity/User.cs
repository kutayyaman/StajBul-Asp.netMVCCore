using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace StajBul.Entity
{
    [Table("user")]
    public class User : BaseEntity
    {
        public string UName { get; set; }
        public string Pswd { get; set; }
        public string UserSurname { get; set; }
        public int Age { get; set; }

        [EmailAddress(ErrorMessage = "Geçersiz Mail Adresi")]
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
