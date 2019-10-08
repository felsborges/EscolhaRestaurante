using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EscolhaRestaurante.Models
{
    public class EFEscolhaRestauranteRepository : IEscolhaRestauranteRepository
    {
        private EscolhaRestauranteDbContext context;

        public EFEscolhaRestauranteRepository(EscolhaRestauranteDbContext ctx)
        {
            context = ctx;
        }

        public IQueryable<Voto> Votos => context.Votos
            .Include(v => v.Restaurante);
        public void SaveVoto(Voto voto)
        {
            context.Votos.Add(voto);
            context.SaveChanges();
        }
        public IQueryable<Restaurante> Restaurantes => context.Restaurantes;
        public void SaveRestaurante(Restaurante restaurante)
        {
            var rst = context.Restaurantes.Find(restaurante.RestauranteID);
            if (rst == null)
                context.Restaurantes.Add(restaurante);
            else
                rst.UltimaEscolha = restaurante.UltimaEscolha;

            context.SaveChanges();
        }
    }
}
