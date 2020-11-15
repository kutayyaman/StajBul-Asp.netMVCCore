using System;
using System.Collections.Generic;
using System.Text;

namespace StajBul.Entity
{
    public class Address
    {
        public int AddressId { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public string AddressName { get; set; }
        public int CityId { get; set; }
        public City City { get; set; }
        public string District { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string PostalCode { get; set; }
        public string Phone { get; set; }
    }
}
