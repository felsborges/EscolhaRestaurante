using EscolhaRestaurante.ApplicationCore.Entities;
using EscolhaRestaurante.Infratructure.Data.Config;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscolhaRestaurante.Infratructure.Data
{
    public class RestauranteContext : DbContext
    {
        public RestauranteContext(DbContextOptions<RestauranteContext> options) : base(options)
        {

        }

        public DbSet<Voto> Votos { get; set; }
        public DbSet<Restaurante> Restaurantes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Voto>()
                .ToTable("Voto");
            modelBuilder.ApplyConfiguration(new VotoMap());

            modelBuilder.Entity<Restaurante>()
                .ToTable("Restaurante");
            modelBuilder.ApplyConfiguration(new RestauranteMap());
        }
    }
}
