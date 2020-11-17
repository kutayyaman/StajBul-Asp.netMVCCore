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
            return context.Announcements.Where(a => a.RowStatus == RowStatus.ACTIVE);
        }

        public InternshipAnnouncement getById(int internshipAnnouncementId)
        {
            return context.Announcements.FirstOrDefault(i => i.Id == internshipAnnouncementId && i.RowStatus == RowStatus.ACTIVE);
        }

        public void updateInternshipAnnouncement(InternshipAnnouncement internshipAnnouncement)
        {
            context.Entry(internshipAnnouncement).State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}
