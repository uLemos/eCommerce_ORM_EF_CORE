using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Models.Exercicio.Models
{
    public class Usuario
    {
        public int Id { get; set; } //Chave primária
        public string Nome { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Sexo { get; set; }
        public string? RG { get; set; }
        public string CPF { get; set; } = null!;
        public string? Mae { get; set; }
        public string? SituacaoCadastro { get; set; }
        public DateTimeOffset DataCadastro { get; set; }

        public Contato? Contato { get; set; }
        public ICollection<EnderecoEntrega>? EnderecosEntrega { get; set; } //Lista de Endereços
        public ICollection<Departamento>? Departamentos { get; set; } //Usuario pode ter vários Departamentos de interesse a ele
        public ICollection<Pedido>? Pedidos { get; set; }
        //Imagine que estamos na Entidade Produto
        //public double Preco { get; set; }


        //TODO - Vincular com as classes:
        //TODO - Contato
        //TODO - EnderecoEntrega
        //- Departamento 
    }
}
