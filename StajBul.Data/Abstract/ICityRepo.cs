using StajBul.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StajBul.Data.Abstract
{
    public interface ICityRepo
    {
        IQueryable<City> getAll();
        City getById(int cityId);
        void addCity(City city);
        void updateCity(City city);
        void deleteCityById(int cityId);
    }
}
