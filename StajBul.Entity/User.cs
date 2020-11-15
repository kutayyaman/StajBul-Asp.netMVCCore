﻿using System;
using System.Collections.Generic;
using System.Text;

namespace StajBul.Entity
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserSurname { get; set; }
        public int Age { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string Mail { get; set; }
        public UserType UserType { get; set; }
        public virtual ICollection<Address> Addresses { get; set; }
        public virtual ICollection<InternshipAnnouncement> InternshipAnnouncements { get; set; }
    }

    public enum UserType
    {
        USER,
        ADMIN
    }
}
