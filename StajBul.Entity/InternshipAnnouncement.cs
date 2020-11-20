using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace StajBul.Entity
{
    [Table("announcement")]
    public class InternshipAnnouncement : BaseEntity
    {
        [Required(ErrorMessage = "İlan Adı Girmelisiniz")]
        public string Name { get; set; }

        [Required(ErrorMessage = "İlan Başlığı Girmelisiniz")]
        public string Title { get; set; }

        [Required(ErrorMessage = "İlan Açıklaması Girmelisiniz")]
        public string Explanation { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }

        [Required(ErrorMessage = "Kategori Seçmelisiniz Girmelisiniz")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        [Required(ErrorMessage = "İlan Tipini Seçmelisiniz Girmelisiniz")]
        public AnnouncementType AnnouncementType { get; set; }
        public int AddressId { get; set; }
        public Address Address { get; set; }

        [Required(ErrorMessage = "İlan İle İlgilenecek Mail Adresi Girmelisiniz")]
        [EmailAddress(ErrorMessage ="Email Formatınız Doğru Değil")]
        public string Mail { get; set; }

    }

    public enum AnnouncementType
    {
        STAJYER,
        COMPANY
    }
}
