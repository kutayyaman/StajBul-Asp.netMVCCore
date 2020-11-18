﻿using StajBul.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StajBul.Service
{
    public interface IAnnouncementService
    {
        IQueryable<InternshipAnnouncement> getAll();
        IQueryable<InternshipAnnouncement> getAllStajyerAnnouncement();
        IQueryable<InternshipAnnouncement> getAllCompanyAnnouncement();
        InternshipAnnouncement getById(int internshipAnnouncementId);
        void addInternshipAnnouncement(InternshipAnnouncement internshipAnnouncement);
        void updateInternshipAnnouncement(InternshipAnnouncement internshipAnnouncement);
        void deleteInternshipAnnouncementById(int internshipAnnouncementId);
    }
}
