using EscolhaRestaurante.ApplicationCore.Interfaces.Repository;
using EscolhaRestaurante.Infratructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EscolhaRestaurante.Infratructure.Repository
{
    public class EfRepository<T> : IRepository<T> where T : class
    {
        protected readonly RestauranteContext _dbContext;

        public EfRepository(RestauranteContext dbContext)
        {
            _dbContext = dbContext;
        }

        public T Add(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            _dbContext.SaveChanges();

            return entity;
        }

        public T GetById(int id)
        {
            return _dbContext.Set<T>().Find(id);
        }

        public IEnumerable<T> ListAll()
        {
            return _dbContext.Set<T>().AsEnumerable();
        }
    }
}
