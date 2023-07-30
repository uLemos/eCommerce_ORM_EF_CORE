using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Models
{

    //[Index("Email")]
    [Index(nameof(Email),IsUnique = true, Name = "IX_EMAIL_UNICO")]
    [Index(nameof(Nome), nameof(CPF))]
    [Table("TB_USUARIOS")]
    public class Usuario
    {
        //Conversão Id - Usuario = PK - Identity

        /*
         Caso eu n queira usar o Identity, posso simplesmente fazer isso aqui:

        new Usuario(){ Id = 100 } = db.SaveChanges();
        Assim, eu poderei setar como 100, sendo gerenciado pelo banco.

        Isso se aplica para o método Code First, pois estará no código, caso contrário, estará no banco e deve ser observado por lá .
        vvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvv
         */

        [DatabaseGenerated(DatabaseGeneratedOption.None)] //Não será gerenciado
        public int Id { get; set; } //Chave primária

        /*
        [Key]
        [Column("COD")]
        public int Codigo { get; set; }
         */

        public string Nome { get; set; } = null!;
        public string Email { get; set; } = null!;

        [Required]
        [MaxLength(10)]
        public string? Sexo { get; set; }
        [Column("REGISTRO_GERAL")]
        public string? RG { get; set; }
        public string CPF { get; set; } = null!;
        public string? Mae { get; set; }
        public string? SituacaoCadastro { get; set; 
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Matricula { get; set; }

        //Software/Aplicativo -~Não persistido.
        //RegistroAtivo = (Situacao == "ATIVO") ? true : false;
        //RegistroAtivo = SituacaoCadastro = "ATIVO";

        [NotMapped]
        public bool RegistroAtivo { get; set; }

        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTimeOffset DataCadastro { get; set; }

        [ForeignKey("UsuarioId")]
        public Contato? Contato { get; set; }
        public ICollection<EnderecoEntrega>? EnderecosEntrega { get; set; } //Lista de Endereços
        public ICollection<Departamento>? Departamentos { get; set; } //Usuario pode ter vários Departamentos de interesse a ele

        /*
         PedidosCompradosPeloCliente
            - ClienteId *
            - ColaboradorId
            - SupervisorId

        PedidosGerenciadosPeloColaborador
            - ClienteId 
            - ColaboradorId *
            - SupervisorId

        PedidosSupervisionadosPeloColaboradorSupervisor

            - ClienteId 
            - ColaboradorId 
            - SupervisorId *
        
            Usuario.PedidosCompradosPeloCliente[i].Id -> Exemplo.
         */


        [InverseProperty("Cliente")]
        public ICollection<Pedido>? PedidosCompradosPeloCliente { get; set; }

        [InverseProperty("Colaborador")]
        public ICollection<Pedido>? PedidosGerenciadosPeloColaborador { get; set; }

        [InverseProperty("Supervisor")]
        public ICollection<Pedido>? PedidosSupervisionadosPeloColaboradorSupervisor { get; set; }


        //TODO - Vincular com as classes:
        //TODO - Contato
        //TODO - EnderecoEntrega
        //TODO - Departamento 
    }
}
