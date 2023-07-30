using eCommerce.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Channels;

namespace eCommerce.API.DataBase
{
    public class eCommerceContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=eCommerce;Integrated Security=True")
                .LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information);
                //.EnableSensitiveDataLogging() //Se eu n tiver isso, eu n vou ver os valores e os Ids, sem isso, ele esconde os dados.
                //.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }

        public DbSet<Usuario>? Usuarios { get; set; }
        public DbSet<Contato>? Contatos { get; set; }
        public DbSet<EnderecoEntrega>? EnderecosEntrega { get; set; }
        public DbSet<Departamento>? Departamentos { get; set; }

    }
}
  