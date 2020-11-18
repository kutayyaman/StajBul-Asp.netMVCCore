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

        public IQueryable<InternshipAnnouncement> getAllStajyerAnnouncement()
        {
            return announcementRepo.getAllStajyerAnnouncement();
        }

        public InternshipAnnouncement getById(int internshipAnnouncementId)
        {
            return announcementRepo.getById(internshipAnnouncementId);
        }

        public void updateInternshipAnnouncement(InternshipAnnouncement internshipAnnouncement)
        {
            announcementRepo.updateInternshipAnnouncement(internshipAnnouncement);
        }
    }
}
