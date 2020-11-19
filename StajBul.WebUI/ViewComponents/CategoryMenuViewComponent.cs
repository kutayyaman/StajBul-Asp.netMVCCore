using Microsoft.AspNetCore.Mvc;
using StajBul.Service;
using StajBul.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StajBul.WebUI.ViewComponents
{
    public class CategoryMenuViewComponent : ViewComponent
    {
        private ICategoryService categoryService;

        public CategoryMenuViewComponent(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        public IViewComponentResult Invoke(CategoryFilterModel categoryFilterModel)
        {
            //ViewBag.isIntern = RouteData?.Values["isIntern"]; //bu calismaz cunku bizim default route icinde isIntern diye bir alan yok ama controller adi action adi veya id olsaydi ise yarardi.
            ViewBag.categoryId = categoryFilterModel.CategoryId;
            ViewBag.isIntern = categoryFilterModel.IsIntern;
            return View(categoryService.getAll());
        }
    }
}
