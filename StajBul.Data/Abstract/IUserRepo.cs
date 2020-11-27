using Microsoft.AspNetCore.Identity;
using StajBul.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StajBul.Data.Abstract
{
    public interface IUserRepo
    {
        IQueryable<User> getAll();
        User getById(int userId);
        void addUser(User user);
        void updateUser(User user);
        void deleteUserById(int userId);
    }
}
