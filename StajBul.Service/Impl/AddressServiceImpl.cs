using StajBul.Data.Abstract;
using StajBul.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StajBul.Service.Impl
{
    public class AddressServiceImpl : IAddressService
    {
        private IAddressRepo addressRepo;
        public AddressServiceImpl(IAddressRepo addressRepo)
        {
            this.addressRepo = addressRepo;
        }

        public void addAddress(Address address)
        {
            addressRepo.addAddress(address);
        }

        public void deleteAddressById(int addressId)
        {
            addressRepo.deleteAddressById(addressId);
        }

        public IQueryable<Address> getAll()
        {
            return addressRepo.getAll();
        }

        public Address getById(int addressId)
        {
            return addressRepo.getById(addressId);
        }

        public int getNextId()
        {
            return addressRepo.getNextId();
        }

        public void updateAddress(Address address)
        {
            addressRepo.updateAddress(address);
        }
    }
}
