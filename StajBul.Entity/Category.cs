using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace StajBul.Entity
{
    [Table("category")]
    public class Category : BaseEntity
    {
        public string CategoryName { get; set; }
        public virtual ICollection<InternshipAnnouncement> InternshipAnnouncements { get; set; }

    }
}
