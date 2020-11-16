using Microsoft.EntityFrameworkCore;
using StajBul.Data.Abstract;
using StajBul.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StajBul.Data.Concrete.EfCore
{
    public class EfUserRepoImpl : IUserRepo
    {
        private StajBulContext context;
        public EfUserRepoImpl(StajBulContext context)
        {
            this.context = context;
        }
        public void addUser(User user)
        {
            context.Users.Add(user);
        }

        public void deleteUserById(int userId)
        {
            User user = new User() { UserId = userId };
            context.Users.Attach(user);
            context.Users.Remove(user);
            context.SaveChanges();
        }

        public IQueryable<User> getAll()
        {
            return context.Users;
        }

        public User getById(int userId)
        {
            return context.Users.FirstOrDefault(u => u.UserId == userId);
        }

        public void updateUser(User user)
        {
            context.Entry(user).State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}
