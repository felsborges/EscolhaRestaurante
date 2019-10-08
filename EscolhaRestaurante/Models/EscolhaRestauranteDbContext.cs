using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EscolhaRestaurante.Models
{
    public class EscolhaRestauranteDbContext : DbContext
    {
        public EscolhaRestauranteDbContext(DbContextOptions<EscolhaRestauranteDbContext> options)
            : base(options) { }

        public DbSet<Voto> Votos { get; set; }
        public DbSet<Restaurante> Restaurantes { get; set; }
    }
}
