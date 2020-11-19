using Microsoft.EntityFrameworkCore;
using StajBul.Data.Abstract;
using StajBul.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StajBul.Data.Concrete.EfCore
{
    public class EfAnnouncementRepoImpl : IAnnouncementRepo
    {
        private StajBulContext context;
        public EfAnnouncementRepoImpl(StajBulContext context)
        {
            this.context = context;
        }
        public void addInternshipAnnouncement(InternshipAnnouncement internshipAnnouncement)
        {
            context.Announcements.Add(internshipAnnouncement);
            context.SaveChanges();
        }

        public void deleteInternshipAnnouncementById(int internshipAnnouncementId)
        {
            context.Database.ExecuteSqlRaw("UPDATE announcement SET \"RowStatus\" = '1' WHERE \"Id\" = {0}", internshipAnnouncementId);
        }

        public IQueryable<InternshipAnnouncement> getAll()
        {
            return context.Announcements.Include(a => a.Category).Include(a => a.User).Where(a => a.RowStatus == RowStatus.ACTIVE).OrderByDescending(a => a.Id);
        }

        public IQueryable<InternshipAnnouncement> getAllStajyerAnnouncement()
        {
            return context.Announcements.Include(a => a.Category).Include(a => a.Address.City).Where(a => a.RowStatus == RowStatus.ACTIVE && a.AnnouncementType == AnnouncementType.STAJYER).OrderByDescending(a => a.Id);
        }

        public IQueryable<InternshipAnnouncement> getAllCompanyAnnouncement()
        {
            return context.Announcements.Include(a => a.Category).Include(a => a.Address.City).Where(a => a.RowStatus == RowStatus.ACTIVE && a.AnnouncementType == AnnouncementType.COMPANY).OrderByDescending(a => a.Id);
        }
        public IQueryable<InternshipAnnouncement> getAllStajyerAnnouncementByCategoryId(int categoryId)
        {
            return context.Announcements.Include(a => a.Category).Include(a => a.Address.City).Where(a => a.RowStatus == RowStatus.ACTIVE && a.AnnouncementType == AnnouncementType.STAJYER && a.CategoryId == categoryId).OrderByDescending(a => a.Id);
        }
        public IQueryable<InternshipAnnouncement> getAllCompanyAnnouncementByCategoryId(int categoryId)
        {
            return context.Announcements.Include(a => a.Category).Include(a => a.Address.City).Where(a => a.RowStatus == RowStatus.ACTIVE && a.AnnouncementType == AnnouncementType.COMPANY && a.CategoryId == categoryId).OrderByDescending(a => a.Id);
        }

        public InternshipAnnouncement getById(int internshipAnnouncementId)
        {
            return context.Announcements.Include(i => i.Address).Include(i => i.Address.City).Include(i => i.Category).FirstOrDefault(i => i.Id == internshipAnnouncementId && i.RowStatus == RowStatus.ACTIVE); //Lazy loading oldugu icin Include ile cagirdim.
        }

        public void updateInternshipAnnouncement(InternshipAnnouncement internshipAnnouncement)
        {
            context.Entry(internshipAnnouncement).State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}
