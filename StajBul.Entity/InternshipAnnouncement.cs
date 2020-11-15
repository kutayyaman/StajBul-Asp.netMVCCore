using System;
using System.Collections.Generic;
using System.Text;

namespace StajBul.Entity
{
    public class InternshipAnnouncement
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Explanation { get; set; }
        public DateTime CreationDate { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public AnnouncementType AnnouncementType { get; set; }
        public int AddressId { get; set; }
        public Address Address { get; set; }
        public string Mail { get; set; }
        public AnnouncementStatus AnnouncementStatus { get; set; }

    }

    public enum AnnouncementType
    {
        STAJYER,
        COMPANY
    }
    public enum AnnouncementStatus
    {
        ACTIVE,
        DELETED
    }
}
