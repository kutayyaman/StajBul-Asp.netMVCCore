using StajBul.Data.Abstract;
using StajBul.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StajBul.Service.Impl
{
    public class UserServiceImpl : IUserService
    {
        private IUserRepo userRepo;
        private IAnnouncementService announcementService;
        public UserServiceImpl(IUserRepo userRepo, IAnnouncementService announcementService)
        {
            this.userRepo = userRepo;
            this.announcementService = announcementService;
        }

        public void addUser(User user)
        {
            userRepo.addUser(user);
        }

        public void deleteUserById(int userId)
        {
            userRepo.deleteUserById(userId);
            List<InternshipAnnouncement> announcements = announcementService.getByUserId(userId).ToList();
            foreach (var announcement in announcements)
            {
                announcementService.deleteInternshipAnnouncementById(announcement.Id);
            }
        }

        public IQueryable<User> getAll()
        {
            return userRepo.getAll();
        }

        public User getById(int userId)
        {
            return userRepo.getById(userId);
        }

        public void updateUser(User user)
        {
            userRepo.updateUser(user);
        }
    }
}
