﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace StajBul.Entity
{
    [Table("address")]
    public class Address : BaseEntity
    {
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
