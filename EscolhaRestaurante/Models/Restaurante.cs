using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EscolhaRestaurante.Models
{
    public class Restaurante
    {
        public int RestauranteID { get; set; }
        public string Nome { get; set; }
        public DateTime? UltimaEscolha { get; set; }
    }
}
