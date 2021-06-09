using System;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infra.Context
{
    public class ApiCadastroContext : DbContext
    {
        public DbSet<User> Users { get; set; }
    }
}
