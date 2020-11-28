using StajBul.Data.Abstract;
using StajBul.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StajBul.Service.Impl
{
    public class AnnouncementServiceImpl : IAnnouncementService
    {
        private IAnnouncementRepo announcementRepo;
        public AnnouncementServiceImpl(IAnnouncementRepo announcementRepo)
        {
            this.announcementRepo = announcementRepo;
        }

        public void addInternshipAnnouncement(InternshipAnnouncement internshipAnnouncement)
        {
            announcementRepo.addInternshipAnnouncement(internshipAnnouncement);
        }

        public void deleteInternshipAnnouncementById(int internshipAnnouncementId)
        {
            announcementRepo.deleteInternshipAnnouncementById(internshipAnnouncementId);
        }

        public IQueryable<InternshipAnnouncement> getAll()
        {
            return announcementRepo.getAll();
        }

        public IQueryable<InternshipAnnouncement> getAllCompanyAnnouncement()
        {
            return announcementRepo.getAllCompanyAnnouncement();
        }

        public IQueryable<InternshipAnnouncement> getAllCompanyAnnouncementByCategoryId(int categoryId)
        {
            return announcementRepo.getAllCompanyAnnouncementByCategoryId(categoryId);
        }

        public IQueryable<InternshipAnnouncement> getAllStajyerAnnouncement()
        {
            return announcementRepo.getAllStajyerAnnouncement();
        }

        public IQueryable<InternshipAnnouncement> getAllStajyerAnnouncementByCategoryId(int categoryId)
        {
            return announcementRepo.getAllStajyerAnnouncementByCategoryId(categoryId);
        }

        public InternshipAnnouncement getById(int internshipAnnouncementId)
        {
            return announcementRepo.getById(internshipAnnouncementId);
        }

        public IQueryable<InternshipAnnouncement> getByUserId(int userId)
        {
            return announcementRepo.getByUserId(userId);
        }

        public void updateInternshipAnnouncement(InternshipAnnouncement internshipAnnouncement)
        {
            announcementRepo.updateInternshipAnnouncement(internshipAnnouncement);
        }
    }
}
