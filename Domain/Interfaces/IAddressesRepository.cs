using Domain.Entities;
using System;
using System.Collections.Generic;

namespace Domain.Interfaces
{
    public interface IAddressesRepository : IGenericRepository<Address> 
    {
        IEnumerable<Address> GetByUserId(Guid id);
    }
}
