using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace StajBul.Entity
{
    [Table("city")]
    public class City : BaseEntity
    {
        public string CityName { get; set; }

    }
}
