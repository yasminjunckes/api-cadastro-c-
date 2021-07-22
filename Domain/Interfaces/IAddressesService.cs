using Domain.DTO;
using Domain.DTOs.Address;
using Domain.Entities;
using System;
using System.Collections.Generic;

namespace Domain.Interfaces
{
    public interface IAddressesService
    {
        AddressDTO Create(AddressRequestDTO request, Guid userId);

        Address GetById(Guid id);

        Address GetAddress(string postalCode);

        IEnumerable<Address> GetAddresses(Guid userId);

        void Delete(Guid id);
    }
}