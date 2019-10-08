using System;
using System.Collections.Generic;
using System.Text;
using EscolhaRestaurante.Models;
using Xunit;

namespace EscolhaRestaurante.Tests
{
    public class RestauranteTests
    {
        [Fact]
        public void CanChangeRestauranteUltimaEscolha()
        {
            var r = new Restaurante
            {
                Nome = "Platu's",
                UltimaEscolha = DateTime.Now.AddDays(-7)
            };

            var ultimaEscolha = DateTime.Now;
            r.UltimaEscolha = ultimaEscolha;

            Assert.Equal(ultimaEscolha, r.UltimaEscolha);
        }
    }
}
