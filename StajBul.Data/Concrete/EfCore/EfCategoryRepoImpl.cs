﻿using Microsoft.EntityFrameworkCore;
using StajBul.Data.Abstract;
using StajBul.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StajBul.Data.Concrete.EfCore
{
    public class EfCategoryRepoImpl : ICategoryRepo
    {
        private StajBulContext context;
        public EfCategoryRepoImpl(StajBulContext context)
        {
            this.context = context;
        }
        public void addCategory(Category category)
        {
            context.Categories.Add(category);
        }

        public void deleteCategoryById(int categoryId)
        {
            context.Categories.FromSqlRaw("DELETE FROM Categories WHERE CategoryId = {0};", categoryId);        
        }

        public IQueryable<Category> getAll()
        {
            return context.Categories;
        }

        public Category getById(int categoryId)
        {
            return context.Categories.FirstOrDefault(c => c.CategoryId == categoryId);
        }

        public void updateCategory(Category category)
        {
            context.Entry(category).State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}
