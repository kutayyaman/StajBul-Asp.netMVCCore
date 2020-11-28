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
            context.SaveChanges();
        }

        public void deleteUserById(int userId)
        {
            //context.Database.ExecuteSqlRaw("UPDATE AspNetUsers SET \"RowStatus\" = '1' WHERE \"Id\" = {0}", userId);
            User user = getById(userId);
            user.RowStatus = RowStatus.DELETED;
            context.SaveChanges();
        }

        public IQueryable<User> getAll()
        {
            return context.Users.Where(u => u.RowStatus == RowStatus.ACTIVE).OrderByDescending(u => u.Id);
        }

        public User getById(int userId)
        {
            return context.Users.Include(u => u.Address).ThenInclude(a => a.City).FirstOrDefault(u => u.Id == userId && u.RowStatus == RowStatus.ACTIVE);
        }

        public User getByUserName(string userName)
        {
            return context.Users.Include(u => u.Address).ThenInclude(a => a.City).FirstOrDefault(u => u.UserName == userName && u.RowStatus == RowStatus.ACTIVE);
        }

        public void updateUser(User user)
        {
            //context.Database.ExecuteSqlRaw("UPDATE AspNetUsers SET \"UName\" = {1}, \"UserSurname\" = {2}, \"Mail\" = {3}, \"Age\" = {4} WHERE \"Id\" = {5}", user.UserName, user.UserSurname, user.Email, user.Age, user.Id);
            User userFromDatabase = getById(user.Id);
            userFromDatabase.UserName = user.UserName;
            userFromDatabase.UserSurname = user.UserSurname;
            userFromDatabase.Email = user.Email;
            userFromDatabase.Age = user.Age;
            context.SaveChanges();
        }
    }
}
