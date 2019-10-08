using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EscolhaRestaurante.Models;

namespace EscolhaRestaurante.Controllers
{
    public class RestauranteController : Controller
    {
        private IEscolhaRestauranteRepository repository;

        public RestauranteController(IEscolhaRestauranteRepository repo)
        {
            repository = repo;
        }

        public IActionResult Index()
        {
            return View();
        }

        public ViewResult Votar()
        {
            return View();
        }

        [HttpPost]
        public ViewResult Votar(VotoViewModel votoVM)
        {
            var QuantidadeVotos = (from vt in repository.Votos
                                   where vt.Nome.Contains(votoVM.Nome) && vt.DataVoto.Date == DateTime.Today
                                   select vt)
                                   .ToList()
                                   .Count();
            if (QuantidadeVotos > 0)
                ModelState.AddModelError(string.Empty, "É permitido somente um voto por dia");

            if (ModelState.IsValid)
            {
                var restaurante = repository.Restaurantes.FirstOrDefault(r => r.Nome == votoVM.Restaurante);
                if (restaurante == null)
                    restaurante = new Restaurante { Nome = votoVM.Restaurante };

                var voto = new Voto
                {
                    Nome = votoVM.Nome,
                    Email = votoVM.Email,
                    DataVoto = DateTime.Now,
                    Restaurante = restaurante
                };

                repository.SaveVoto(voto);
                return View("Obrigado", votoVM);
            }
            else
            {
                return View();
            }
        }

        public ViewResult Votos()
        {
            var votosVM = (from vt in repository.Votos
                           orderby vt.DataVoto descending
                           select new VotoViewModel
                           {
                               Nome = vt.Nome,
                               Email = vt.Email,
                               Restaurante = vt.Restaurante.Nome,
                               DataVoto = vt.DataVoto
                           })
                           .ToList();

            return View(votosVM);
        }

        public ViewResult Resultado()
        {
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
                             .ToList();

            return View(resultado);
        }

        public ViewResult Eleger()
        {
            var resultadoVM = (from vt in repository.Votos
                               where (vt.Restaurante.UltimaEscolha == null || vt.Restaurante.UltimaEscolha <= DateTime.Today.AddDays(-7))
                                  && vt.DataVoto >= DateTime.Today.AddDays(-7)
                               group vt.VotoID by new { vt.Restaurante.RestauranteID, vt.Restaurante.Nome } into rst
                               orderby rst.Count() descending
                               select new ResultadoViewModel
                               {
                                   RestauranteID = rst.Key.RestauranteID,
                                   Restaurante = rst.Key.Nome,
                                   QuantidadeVotos = rst.Count()
                               })
                              .ToList()
                              .FirstOrDefault();

            repository.SaveRestaurante(new Restaurante
            {
                RestauranteID = resultadoVM.RestauranteID,
                Nome = resultadoVM.Restaurante,
                UltimaEscolha = DateTime.Now
            });

            return View(resultadoVM);
        }
    }
}