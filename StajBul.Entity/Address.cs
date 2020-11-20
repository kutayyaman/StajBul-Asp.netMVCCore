using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace StajBul.Entity
{
    [Table("address")]
    public class Address : BaseEntity
    {
        public int UserId { get; set; }
        public User User { get; set; }

        [Required(ErrorMessage = "Adres Adı Girmelisiniz")]
        public string AddressName { get; set; }

        [Required(ErrorMessage ="Şehir Seçmelisiniz")]
        public int CityId { get; set; }
        public City City { get; set; }

        [Required(ErrorMessage = "İlçe Adı Girmelisiniz")]
        public string District { get; set; }

        [Required(ErrorMessage = "Adres Satırı Girmelisiniz")]
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }

        [Required(ErrorMessage = "Posta Kodu Girmelisiniz")]
        public string PostalCode { get; set; }

        public string Phone { get; set; }
    }
}
