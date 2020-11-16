using StajBul.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StajBul.Data.Abstract
{
    public interface ICategoryRepo
    {
        IQueryable<Category> getAll();
        Category getById(int categoryId);
        void addCategory(Category category);
        void updateCategory(Category category);
        void deleteCategoryById(int categoryId);
    }
}
