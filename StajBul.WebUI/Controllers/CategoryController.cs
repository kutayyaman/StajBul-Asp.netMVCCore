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

        [HttpGet]
        public IActionResult Edit(int id)
        {
            return View(categoryService.getById(id));
        }

        [HttpPost]
        public IActionResult EditPost(Category category)
        {
            if (ModelState.IsValid)
            {
                category.ModifiedDate = DateTime.Now.ToString();
                categoryService.updateCategory(category);
                TempData["message"] = $"{category.CategoryName} Güncellendi.";
                return RedirectToAction("List");
            }

            return View(category);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            return View(categoryService.getById(id));
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirm(int id)
        {
            categoryService.deleteCategoryById(id);
            TempData["message"] = id + "id'li Kayıt Silindi.";
            return RedirectToAction("List");
        }
    }
}
