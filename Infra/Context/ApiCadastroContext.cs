using System;
using System.Reflection;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infra.Context
{
    public class ApiCadastroContext : DbContext
    {
        public ApiCadastroContext(DbContextOptions options) : base(options)
        {
        }

        public ApiCadastroContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApiCadastroContext).Assembly);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //var connectionString = Environment.GetEnvironmentVariable("ProviderConnectionString");
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseLazyLoadingProxies()
                    .UseNpgsql("User ID=postgres;Password=postgres;Server=localhost;Port=5432;Database=ApiCadastro");
            }
        }
    }

}
