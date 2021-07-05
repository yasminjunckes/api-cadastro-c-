using Domain.Entities;
using Domain.Interfaces;
using Infra.Context;

namespace Infra.Repositories
{
    public class AdressesRepository : GenericRepository<Adress>, IAdressesRepository
    {
      public AdressesRepository(ApiCadastroContext dbContext) : base(dbContext)
        {
        }
    }
}