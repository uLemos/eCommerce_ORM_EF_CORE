using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Office.Models
{
    public class ColaboradorVeiculo
    {
        /*
         Id = MER
         */
        public int ColaboradorId { get; set; }
        public int VeiculoId { get; set; }
        public DateTimeOffset? DataInicioDeVinculo { get; set; }

        /*POO*/
        public Colaborador? Colaborador { get; set; } 
        public Veiculo? Veiculo { get; set; }
    }
}
