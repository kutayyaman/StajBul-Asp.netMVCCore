using StajBul.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StajBul.WebUI.Models
{
    public class UserProfileViewModel
    {
        public List<InternshipAnnouncement> Announcements { get; set; }
        public User User { get; set; }
    }
}
