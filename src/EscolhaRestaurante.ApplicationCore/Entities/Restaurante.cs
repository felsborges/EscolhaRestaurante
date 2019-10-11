using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EscolhaRestaurante.ApplicationCore.Entities
{
    public class Restaurante
    {
        public int RestauranteID { get; set; }
        public string Nome { get; set; }
        public DateTime? UltimaEscolha { get; set; }
        public ICollection<Voto> Votos { get; set; }
    }
}
