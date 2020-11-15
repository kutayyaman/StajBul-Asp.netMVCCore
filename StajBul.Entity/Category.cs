using System;
using System.Collections.Generic;
using System.Text;

namespace StajBul.Entity
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public virtual ICollection<InternshipAnnouncement> InternshipAnnouncements { get; set; }

    }
}
