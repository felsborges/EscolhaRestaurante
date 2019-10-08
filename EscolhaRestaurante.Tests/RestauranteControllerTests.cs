using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using EscolhaRestaurante.Controllers;
using EscolhaRestaurante.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace EscolhaRestaurante.Tests
{
    public class RestauranteControllerTests
    {
        [Fact]
        public void VotosActionModelIsComplete()
        {
            var repository = TestsEscolhaRestauranteRepository.SharedRepository;

            var controller = new RestauranteController(repository);

            var model = (controller.Votos() as ViewResult)?.ViewData.Model as IEnumerable<VotoViewModel>;

            var votosVM = (from vt in repository.Votos
                           orderby vt.DataVoto descending
                           select new VotoViewModel
                           {
                               Nome = vt.Nome,
                               Email = vt.Email,
                               Restaurante = vt.Restaurante.Nome,
                               DataVoto = vt.DataVoto
                           })
                           .ToList()
                           .AsEnumerable<VotoViewModel>();

            Assert.Equal(votosVM, model, Comparer.Get<VotoViewModel>((p1, p2) => p1.Nome == p2.Nome));
        }

        [Fact]
        public void ResultadoActionModelIsComplete()
        {
            var repository = TestsEscolhaRestauranteRepository.SharedRepository;

            var controller = new RestauranteController(repository);
            var model = (controller.Resultado() as ViewResult)?.ViewData.Model as IEnumerable<ResultadoViewModel>;

            var resultado = (from vt in repository.Votos
                             where (vt.Restaurante.UltimaEscolha == null || vt.Restaurante.UltimaEscolha <= DateTime.Today.AddDays(-7))
                                && vt.DataVoto >= DateTime.Today.AddDays(-7)
                             group vt by vt.Restaurante.Nome into rst
                             orderby rst.Count() descending
                             select new ResultadoViewModel
                             {
                                 Restaurante = rst.Key,
                                 QuantidadeVotos = rst.Count()
                             })
                            .ToList()
                            .AsEnumerable<ResultadoViewModel>();

            Assert.Equal(resultado, model, Comparer.Get<ResultadoViewModel>((p1, p2) => p1.Restaurante == p2.Restaurante));
        }

        [Fact]
        public void MoreThanOneVote()
        {
            var repository = TestsEscolhaRestauranteRepository.SharedRepository;

            var votoVM = new VotoViewModel
            {
                Nome = "Felipe",
                Email = "felipe@email.com",
                Restaurante = "Sabor Família",
                DataVoto = DateTime.Now
            };

            var quantidadeVoto1 = (from vt in repository.Votos
                                   where vt.Nome == votoVM.Nome && vt.DataVoto.Date == DateTime.Today
                                   select vt)
                                  .Count();

            var controller = new RestauranteController(repository);
            var model = controller.Votar(votoVM) as ViewResult;

            var quantidadeVoto2 = (from vt in repository.Votos
                                   where vt.Nome == votoVM.Nome && vt.DataVoto.Date == DateTime.Today
                                   select vt)
                                  .Count();

            Assert.Equal(quantidadeVoto1, quantidadeVoto2);
        }

        [Fact]
        public void RestaurantAWeek()
        {
            var repository = TestsEscolhaRestauranteRepository.SharedRepository;
            var controller = new RestauranteController(repository);

            var quantidadeResultado1 = ((controller.Resultado() as ViewResult)?.ViewData.Model as IEnumerable<ResultadoViewModel>).Count();

            var model = controller.Eleger() as ViewResult;

            var quantidadeResultado2 = ((controller.Resultado() as ViewResult)?.ViewData.Model as IEnumerable<ResultadoViewModel>).Count();

            Assert.NotEqual(quantidadeResultado1, quantidadeResultado2);
        }

        [Fact]
        public void VotarActionIsComplete()
        {
            var repository = TestsEscolhaRestauranteRepository.SharedRepository;

            var votoVM = new VotoViewModel
            {
                Nome = "Anderson",
                Email = "anderson@email.com",
                DataVoto = DateTime.Now,
                Restaurante = "Subway"
            };

            var controller = new RestauranteController(repository);
            var viewResult = controller.Votar(votoVM) as ViewResult;

            Assert.NotNull(viewResult.ViewName);
        }
    }

    public class TestsEscolhaRestauranteRepository : IEscolhaRestauranteRepository
    {
        private static TestsEscolhaRestauranteRepository sharedRepository = new TestsEscolhaRestauranteRepository();
        private List<Voto> votos = new List<Voto>();

        public static TestsEscolhaRestauranteRepository SharedRepository => sharedRepository;

        public TestsEscolhaRestauranteRepository()
        {
            var restaurantes = new[]
            {
                new Restaurante
                {
                    RestauranteID = 0,
                    Nome = "Palatu's"
                },
                new Restaurante
                {
                    RestauranteID = 1,
                    Nome = "Panorama"
                }
            };

            votos.AddRange(new List<Voto> {
                new Voto
                {
                    Nome = "Felipe",
                    Email = "felipe@email.com",
                    DataVoto = DateTime.Now,
                    Restaurante = restaurantes[0]
                },
                new Voto
                {
                    Nome = "José",
                    Email = "jose@email.com",
                    DataVoto = DateTime.Now,
                    Restaurante = restaurantes[1]
                },
                new Voto
                {
                    Nome = "Vitor",
                    Email = "vitor@email.com",
                    DataVoto = DateTime.Now,
                    Restaurante = restaurantes[0]
                }
            });
        }

        public IQueryable<Voto> Votos => votos.AsQueryable<Voto>();

        public void SaveVoto(Voto voto) => votos.Add(voto);

        public IQueryable<Restaurante> Restaurantes
            => votos
                   .Select(v => new Restaurante
                   { 
                       RestauranteID = v.Restaurante.RestauranteID,
                       Nome = v.Restaurante.Nome
                   })
                   .Distinct()
                   .AsQueryable();

        public void SaveRestaurante(Restaurante restaurante)
        {
            (from vt in votos
             where vt.Restaurante.Nome == restaurante.Nome
             select vt.Restaurante)
            .ToList()
            .ForEach(r => r.UltimaEscolha = DateTime.Now);
        }
    }
}
