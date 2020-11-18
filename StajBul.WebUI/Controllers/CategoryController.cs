using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StajBul.Entity;
using StajBul.Service;

namespace StajBul.WebUI.Controllers
{
    public class CategoryController : Controller
    {
        private ICategoryService categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult List()
        {
            return View(categoryService.getAll());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {
            DateTime dateTime = DateTime.Now;
            category.CreatedDate = dateTime.ToString();

            if (ModelState.IsValid)
            {
                categoryService.addCategory(category);
                return RedirectToAction("List");
            }
            
            return View(category);
        }
    }
}
