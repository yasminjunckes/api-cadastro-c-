using System;
using System.Collections.Generic;
using System.Linq;
using Domain;
using Domain.Interfaces;
using Infra.Context;
using Microsoft.EntityFrameworkCore;


namespace Infra.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : Entity
    {
        private readonly ApiCadastroContext _db;

        protected GenericRepository(ApiCadastroContext dbContext)
        {
            _db = dbContext;
        }

        public void Add(TEntity entity)
        {
            {
                _db.Add<TEntity>(entity);
                _db.SaveChanges();
            }
        }

        public TEntity Get(Func<TEntity, bool> predicate)
        {
            {
                return _db.Set<TEntity>().FirstOrDefault(predicate);
            }
        }

        public IEnumerable<TEntity> GetAll(Func<TEntity, bool> predicate)
        {
            {
                return _db.Set<TEntity>().Where(predicate).ToArray();
            }
        }        

        public TEntity Get(Guid id)
        {
            {
                return _db.Set<TEntity>().SingleOrDefault(x => x.Id == id);
            }
        }

        public void Modify(TEntity entity)
        {
            {
                _db.Entry(entity).State = EntityState.Modified;
                _db.SaveChanges();
            }
        }

        public void Remove(TEntity entity)
        {
            {
                _db.Remove(entity);
                _db.SaveChanges();
            }
        }
    }
}