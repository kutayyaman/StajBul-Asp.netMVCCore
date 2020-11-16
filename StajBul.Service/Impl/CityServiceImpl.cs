using StajBul.Data.Abstract;
using StajBul.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StajBul.Service.Impl
{
    public class CityServiceImpl : ICityService
    {
        private ICityRepo cityRepo;
        public CityServiceImpl(ICityRepo cityRepo)
        {
            this.cityRepo = cityRepo;
        }

        public void addCity(City city)
        {
            cityRepo.addCity(city);
        }

        public void deleteCityById(int cityId)
        {
            cityRepo.deleteCityById(cityId);
        }

        public IQueryable<City> getAll()
        {
            return cityRepo.getAll();
        }

        public City getById(int cityId)
        {
            return cityRepo.getById(cityId);
        }

        public void updateCity(City city)
        {
            cityRepo.updateCity(city);
        }
    }
}
