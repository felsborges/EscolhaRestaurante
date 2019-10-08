using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EscolhaRestaurante.Models
{
    public class ResultadoViewModel
    {
        public int RestauranteID { get; set; }
        public string Restaurante { get; set; }

        [Display(Name = "Quantidade de Votos")]
        public int QuantidadeVotos { get; set; }
    }
}
