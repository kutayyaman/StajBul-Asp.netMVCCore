using Microsoft.EntityFrameworkCore;
using StajBul.Data.Abstract;
using StajBul.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StajBul.Data.Concrete.EfCore
{
    public class EfAddressRepoImpl : IAddressRepo
    {
        private StajBulContext context;

        public EfAddressRepoImpl(StajBulContext context)
        {
            this.context = context;
        }

        public void addAddress(Address address)
        {
            context.Addresses.Add(address);
            context.SaveChanges();
        }

        public void deleteAddressById(int addressId) //buraya databasede olmayan bir id gelirse nolcak onu dene eger patlarsa sql sorgusu sıkıntılı olan yolla yaparsin
        {
            context.Database.ExecuteSqlRaw("UPDATE address SET \"RowStatus\" = '1' WHERE \"Id\" = {0}", addressId);
        }

        public IQueryable<Address> getAll()
        {
            return context.Addresses.Where(a => a.RowStatus == RowStatus.ACTIVE);
        }

        public Address getById(int addressId)
        {
            return context.Addresses.FirstOrDefault(a => a.Id == addressId && a.RowStatus == RowStatus.ACTIVE);
        }

        public void updateAddress(Address address)
        {
            context.Entry(address).State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}
