using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using EscolhaRestaurante.Models;

namespace EscolhaRestaurante.Tests
{
    public class VotoTests
    {
        [Fact]
        public void NewVoto()
        {
            var voto = new Voto
            {
                Nome = "Felipe",
                Email = "felipe@email.com",
                DataVoto = DateTime.Now,
                Restaurante = new Restaurante { Nome = "Palatu's" }
            };

            Assert.NotNull(voto);
        }
    }
}
