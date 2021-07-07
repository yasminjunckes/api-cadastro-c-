using Domain.Entities;
using Domain.Interfaces;
using Infra.Context;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infra.Repositories
{
    public class AddressesRepository : GenericRepository<Address>, IAddressesRepository
    {
        private readonly ApiCadastroContext _db;
        public AddressesRepository(ApiCadastroContext dbContext) : base(dbContext)
        {
            _db = dbContext;
        }

        public IEnumerable<Address> GetByUserId(Guid id)
        {
            {
                return _db.Set<Address>().Where(x => x.UserId == id).ToArray();
            }
        }

    }
}