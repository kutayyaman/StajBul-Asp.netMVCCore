using StajBul.Data.Abstract;
using StajBul.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StajBul.Service.Impl
{
    public class CategoryServiceImpl : ICategoryService
    {
        private ICategoryRepo categoryRepo;
        public CategoryServiceImpl(ICategoryRepo categoryRepo)
        {
            this.categoryRepo = categoryRepo;
        }

        public void addCategory(Category category)
        {
            categoryRepo.addCategory(category);
        }

        public void deleteCategoryById(int categoryId)
        {
            categoryRepo.deleteCategoryById(categoryId);
        }

        public IQueryable<Category> getAll()
        {
            return categoryRepo.getAll();
        }

        public Category getById(int categoryId)
        {
            return categoryRepo.getById(categoryId);
        }

        public void updateCategory(Category category)
        {
            categoryRepo.updateCategory(category);
        }
    }
}
