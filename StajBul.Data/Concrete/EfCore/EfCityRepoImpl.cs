using Microsoft.EntityFrameworkCore;
using StajBul.Data.Abstract;
using StajBul.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StajBul.Data.Concrete.EfCore
{
    public class EfCityRepoImpl : ICityRepo
    {
        private StajBulContext context;
        public EfCityRepoImpl(StajBulContext context)
        {
            this.context = context;
        }
        public void addCity(City city)
        {
            context.Cities.Add(city);
        }

        public void deleteCityById(int cityId)
        {
            context.Database.ExecuteSqlRaw("UPDATE city SET \"RowStatus\" = '1' WHERE \"Id\" = {0}", cityId);
        }

        public IQueryable<City> getAll()
        {
            return context.Cities.Where(c => c.RowStatus == RowStatus.ACTIVE);
        }

        public City getById(int cityId)
        {
            return context.Cities.FirstOrDefault(c => c.Id == cityId && c.RowStatus == RowStatus.ACTIVE);
        }

        public void updateCity(City city)
        {
            context.Entry(city).State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}
