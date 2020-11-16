using StajBul.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StajBul.Service
{
    public interface IUserService
    {
        IQueryable<User> getAll();
        User getById(int userId);
        void addUser(User user);
        void updateUser(User user);
        void deleteUserById(int userId);
    }
}
