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
            City city = new City() { CityId = cityId };
            context.Cities.Attach(city);
            context.Cities.Remove(city);
            context.SaveChanges();
        }

        public IQueryable<City> getAll()
        {
            return context.Cities;
        }

        public City getById(int cityId)
        {
            return context.Cities.FirstOrDefault(c => c.CityId == cityId);
        }

        public void updateCity(City city)
        {
            context.Entry(city).State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}
