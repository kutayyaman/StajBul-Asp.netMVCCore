using Microsoft.AspNetCore.Mvc;
using StajBul.Service;
using StajBul.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StajBul.WebUI.ViewComponents
{
    public class AnnouncementListViewComponent : ViewComponent
    {
        private IAnnouncementService announcementService;
        public AnnouncementListViewComponent(IAnnouncementService announcementService)
        {
            this.announcementService = announcementService;
        }
        public IViewComponentResult Invoke(CategoryFilterModel categoryFilterModel)
        {
            AnnouncementListAndPaginationModel announcementListAndPaginationModel = new AnnouncementListAndPaginationModel();
            string searchedWords = categoryFilterModel.SearchedWords?.ToLower();
            if (categoryFilterModel.CategoryId == null)
            {
                var query = (categoryFilterModel.IsIntern == true) ? announcementService.getAllStajyerAnnouncement() : announcementService.getAllCompanyAnnouncement();
                if(!string.IsNullOrEmpty(searchedWords))
                {
                    query = query.Where(a => a.Explanation.ToLower().Contains(searchedWords) || a.Title.ToLower().Contains(searchedWords) || a.Name.ToLower().Contains(searchedWords));
                }
                announcementListAndPaginationModel.PaginationModel = categoryFilterModel.PaginationModel;
                announcementListAndPaginationModel.PaginationModel.TotalItem = query.Count();
                query = query.Skip(((categoryFilterModel.PaginationModel.CurrentPage - 1) * categoryFilterModel.PaginationModel.PageSize)).Take(categoryFilterModel.PaginationModel.PageSize);
                announcementListAndPaginationModel.Announcements = query;
                announcementListAndPaginationModel.CategoryFilterModel = categoryFilterModel;
                return View(announcementListAndPaginationModel);
            }
            var queryWithCategoryId = (categoryFilterModel.IsIntern == true) ? announcementService.getAllStajyerAnnouncementByCategoryId((int)categoryFilterModel.CategoryId) : announcementService.getAllCompanyAnnouncementByCategoryId((int)categoryFilterModel.CategoryId);
            if (!string.IsNullOrEmpty(searchedWords))
            {
                queryWithCategoryId = queryWithCategoryId.Where(a => a.Explanation.ToLower().Contains(searchedWords) || a.Title.ToLower().Contains(searchedWords) || a.Name.ToLower().Contains(searchedWords));
            }
            announcementListAndPaginationModel.PaginationModel = categoryFilterModel.PaginationModel;
            announcementListAndPaginationModel.PaginationModel.TotalItem = queryWithCategoryId.Count();
            queryWithCategoryId = queryWithCategoryId.Skip(((categoryFilterModel.PaginationModel.CurrentPage - 1) * categoryFilterModel.PaginationModel.PageSize)).Take(categoryFilterModel.PaginationModel.PageSize);
            announcementListAndPaginationModel.Announcements = queryWithCategoryId;
            announcementListAndPaginationModel.CategoryFilterModel = categoryFilterModel;
            return View(announcementListAndPaginationModel);

        }
    }
}
