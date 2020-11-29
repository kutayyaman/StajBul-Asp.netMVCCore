using StajBul.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StajBul.WebUI.Models
{
    public class PaginationModel
    {
        public int PageSize { get; set; } = 9;
        public int CurrentPage { get; set; } = 1;
        public int TotalItem { get; set; }

        public int TotalPages()
        {
            return (int)Math.Ceiling((decimal)TotalItem / PageSize);
        }

    }

    public class AnnouncementListAndPaginationModel
    {
        public IEnumerable<InternshipAnnouncement> Announcements { get; set; }
        public PaginationModel PaginationModel { get; set; }
        public CategoryFilterModel CategoryFilterModel { get; set; }
        public UserProfileViewModel UserProfileViewModel { get; set; } = null;
    }
}
