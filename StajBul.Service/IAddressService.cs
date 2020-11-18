using StajBul.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StajBul.Service
{
    public interface IAddressService
    {
        IQueryable<Address> getAll();
        Address getById(int addressId);
        void addAddress(Address address);
        void updateAddress(Address address);
        void deleteAddressById(int addressId);
        int getNextId();
    }
}
