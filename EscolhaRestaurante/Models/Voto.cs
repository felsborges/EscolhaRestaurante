using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EscolhaRestaurante.Models
{
    public class Voto
    {
        public int VotoID { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public DateTime DataVoto { get; set; }
        public Restaurante Restaurante { get; set; }
    }
}
