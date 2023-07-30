using System.ComponentModel.DataAnnotations.Schema;

namespace eCommerce.Models
{
    public class Pedido
    {
        //Navegação
        //Pedido.Cliente.Nome
        public int Id { get; set; }

        //FK
        [ForeignKey("Cliente")]
        public int ClienteId { get; set; }

        [ForeignKey("Colaborador")]
        public int ColaboradorId { get; set; }

        [ForeignKey("Supervisor")]
        public int SupervisorId { get; set; }

        public Usuario? Cliente { get; set; }
        public Usuario? Colaborador { get; set; }
        public Usuario? Supervisor { get; set; }


    }
}
