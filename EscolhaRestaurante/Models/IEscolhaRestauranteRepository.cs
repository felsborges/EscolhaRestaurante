using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EscolhaRestaurante.Models
{
    public interface IEscolhaRestauranteRepository
    {
        IQueryable<Voto> Votos { get; }
        void SaveVoto(Voto voto);
        IQueryable<Restaurante> Restaurantes { get; }
        void SaveRestaurante(Restaurante restaurante);
    }
}
