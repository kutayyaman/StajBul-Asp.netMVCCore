using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace StajBul.Entity
{
    
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? CreatedBy { get; set; }
        public string? ModifiedBy { get; set; }
        public RowStatus RowStatus { get; set; } = RowStatus.ACTIVE;

    }

    public enum RowStatus
    {
        ACTIVE,
        DELETED
    }
}

