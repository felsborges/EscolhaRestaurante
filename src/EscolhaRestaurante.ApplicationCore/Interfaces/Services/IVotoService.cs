using EscolhaRestaurante.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolhaRestaurante.ApplicationCore.Interfaces.Services
{
    public interface IVotoService
    {
        Voto GetById(int id);
        IEnumerable<Voto> ListAll();
        Voto Add(Voto voto);
    }
}
