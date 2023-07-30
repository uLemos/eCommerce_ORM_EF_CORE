using eCommerce.Models.Exercicio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Models.Exercicio.Models
{
    public class Pedido
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public string? NomePedido { get; set; }
        public Usuario? Usuario { get; set; }
    }
}
