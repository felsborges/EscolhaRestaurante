using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EscolhaRestaurante.UI.Web.Models
{
    public class VotoViewModel
    {
        [Required(ErrorMessage = "O nome é obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O e-mail é obrigatório")]
        [Display(Name = "E-mail")]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Insira um e-mail válido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O nome do restaurante é obrigatório")]
        [Display(Name = "Nome do Restaurante")]
        public string Restaurante { get; set; }

        [Display(Name = "Data e Hora do Voto")]
        public DateTime DataVoto { get; set; }
    }
}
