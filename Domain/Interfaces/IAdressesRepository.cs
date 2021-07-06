using Domain.Entities;
using System;
using System.Collections.Generic;

namespace Domain.Interfaces
{
    public interface IAdressesRepository : IGenericRepository<Adress> 
    {
        IEnumerable<Adress> GetByUserId(Guid id);
    }
}
