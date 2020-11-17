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
            context.Database.ExecuteSqlRaw("UPDATE user SET \"RowStatus\" = '1' WHERE \"Id\" = {0}", userId);
        }

        public IQueryable<User> getAll()
        {
            return context.Users.Where(u => u.RowStatus == RowStatus.ACTIVE);
        }

        public User getById(int userId)
        {
            return context.Users.FirstOrDefault(u => u.Id == userId && u.RowStatus == RowStatus.ACTIVE);
        }

        public void updateUser(User user)
        {
            context.Entry(user).State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}
