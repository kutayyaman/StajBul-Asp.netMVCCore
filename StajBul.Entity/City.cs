using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace StajBul.Entity
{
    [Table("city")]
    public class City
    {
        public int CityId { get; set; }
        public string CityName { get; set; }

    }
}
