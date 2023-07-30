using Microsoft.EntityFrameworkCore.Infrastructure;
using eCommerce.Console.Query.Utils;
namespace eCommerce.Models
{
    public class Usuario
    {
        private readonly Action<object, string> LazyLoader;

        public Usuario()
        {
            
        }

        public Usuario(Action<object, string> lazyLoader)
        {
            LazyLoader = lazyLoader;
        }


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

        private ICollection<EnderecoEntrega>? _enderecosEntrega;

        public ICollection<EnderecoEntrega>? EnderecosEntrega 
        { 
            get => LazyLoader.Load(this, ref _enderecosEntrega); 
            set => _enderecosEntrega = value;
        } //Lista de Endereços
        
        public ICollection<Departamento>? Departamentos { get; set; } //Usuario pode ter vários Departamentos de interesse a ele

        //TODO - Vincular com as classes:
        //TODO - Contato
        //TODO - EnderecoEntrega
        //- Departamento 
    }
}
