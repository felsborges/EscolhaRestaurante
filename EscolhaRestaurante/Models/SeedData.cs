using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EscolhaRestaurante.Models
{
    public static class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            EscolhaRestauranteDbContext context = app.ApplicationServices
                .GetRequiredService<EscolhaRestauranteDbContext>();
            context.Database.Migrate();
            if (!context.Votos.Any())
            {
                context.Votos.AddRange(
                    new Voto
                    {
                        Nome = "Felipe",
                        Email = "felipe@email.com",
                        DataVoto = DateTime.Now,
                        Restaurante = new Restaurante { Nome = "Palatu's" }
                    },
                    new Voto
                    {
                        Nome = "José",
                        Email = "jose@email.com",
                        DataVoto = DateTime.Now,
                        Restaurante = new Restaurante { Nome = "Panorama" }
                    });
                context.SaveChanges();
            }
        }
    }
}
