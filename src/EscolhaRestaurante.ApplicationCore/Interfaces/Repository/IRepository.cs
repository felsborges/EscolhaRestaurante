using System;
using System.Collections.Generic;
using System.Text;

namespace EscolhaRestaurante.ApplicationCore.Interfaces.Repository
{
    public interface IRepository<T> where T : class
    {
        T GetById(int id);
        IEnumerable<T> ListAll();
        T Add(T entity);
    }
}
