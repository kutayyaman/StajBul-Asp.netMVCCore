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
        public string UserRealName { get; set; }
        public string UserSurname { get; set; }
        public int Age { get; set; }
        public RowStatus RowStatus { get; set; } = RowStatus.ACTIVE;
        public string? AboutMe { get; set; }
        public int? AddressId { get; set; }
        public Address? Address { get; set; }
        public virtual ICollection<InternshipAnnouncement> InternshipAnnouncements { get; set; }
    }

}
