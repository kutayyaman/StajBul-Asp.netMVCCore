using Microsoft.EntityFrameworkCore;
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
            context.Database.ExecuteSqlRaw("UPDATE category SET \"RowStatus\" = '1' WHERE \"Id\" = {0}", categoryId);
        }

        public IQueryable<Category> getAll()
        {
            return context.Categories.Where(c => c.RowStatus == RowStatus.ACTIVE);
        }

        public Category getById(int categoryId)
        {
            return context.Categories.FirstOrDefault(c => c.Id == categoryId && c.RowStatus == RowStatus.ACTIVE);
        }

        public void updateCategory(Category category)
        {
            context.Entry(category).State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}
