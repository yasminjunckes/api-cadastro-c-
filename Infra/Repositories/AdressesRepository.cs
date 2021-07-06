using Domain.Entities;
using Domain.Interfaces;
using Infra.Context;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infra.Repositories
{
    public class AdressesRepository : GenericRepository<Adress>, IAdressesRepository
    {
        private readonly ApiCadastroContext _db;
        public AdressesRepository(ApiCadastroContext dbContext) : base(dbContext)
        {
            _db = dbContext;
        }

        public IEnumerable<Adress> GetByUserId(Guid id)
        {
            {
                return _db.Set<Adress>().Where(x => x.UserId == id).ToArray();
            }
        }

    }
}