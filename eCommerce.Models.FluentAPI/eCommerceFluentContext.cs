using eCommerce.Models.FluentAPI.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Models.FluentAPI
{
    public class eCommerceFluentContext : DbContext
    {
        public eCommerceFluentContext(DbContextOptions<eCommerceFluentContext> options) : base(options)
        {

        }

        public DbSet<Usuario>? Usuarios { get; set; }
        public DbSet<Contato>? Contatos { get; set; }
        public DbSet<EnderecoEntrega>? EnderecosEntrega { get; set; }
        public DbSet<Departamento>? Departamentos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region EXPLICAÇÕES
            /* 
             * Table,
             * Column,
             * NotMapped,
             * DatabaseGenerated(ValueGeneratedNever = None,
             * ValueGeneratedOnAdd = Identity,
             * ValueGeneratedOnAddOrUpdate = Computed)
             * 
             * Key
             * ForeignKey
             * 
             * Relacionamentos entre Tabelas/Entidades
             * Tem/Com + um/muitos
             * Has/With + One/Many = HasOne,HasMany, WithOne, WithMany
             * 
             * 
             */

            modelBuilder.Entity<Usuario>().ToTable("TB_USUARIOS");
            modelBuilder.Entity<Usuario>().Property(a => a.RG).HasColumnName("REGISTRO_GERAL").HasMaxLength(10).HasDefaultValue("RG-AUSENTE").IsRequired();
            modelBuilder.Entity<Usuario>().Ignore(a => a.Sexo);
            modelBuilder.Entity<Usuario>().Property(a => a.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<Usuario>().HasIndex("CPF").IsUnique().HasFilter("[CPF] is not null").HasDatabaseName("IX_CPF_UNIQUE");
            modelBuilder.Entity<Usuario>().HasIndex("CPF"); //Index padrão
            modelBuilder.Entity<Usuario>().HasIndex(a => a.CPF); // Com lambda
            modelBuilder.Entity<Usuario>().HasIndex("CPF", "Email"); // Composto string
            modelBuilder.Entity<Usuario>().HasIndex(a => new { a.CPF, a.Email }); // Composto Lambda

            modelBuilder.Entity<Usuario>().HasKey("Id");
            modelBuilder.Entity<Usuario>().HasKey(a => a.Id);
            modelBuilder.Entity<Usuario>().HasKey(a => new { a.Id, a.CPF });

            //Alterantivas
            modelBuilder.Entity<Usuario>().HasNoKey(); //Ignora que há uma chave.
            modelBuilder.Entity<Usuario>().HasAlternateKey("CPF"); //Ter mais um elemento como forte, ou seja, seria uma outra chave(Secundária). 
            modelBuilder.Entity<Usuario>().HasAlternateKey("CPF", "Email"); //Ter mais um elemento como forte, ou seja, seria uma outra chave(Secundária), podendo ser composta. 

            modelBuilder.Entity<Usuario>().HasKey("Id", "CPF"); //Exemplo, mas não faz sentido para a regra de negócios o Id estar em conjunto


            //One > 1 Propriedade de Navegação do Objeto único(Composição)
            //Many > 1 Propriedade de Navegação do tipo Coleção/Lista/Enumeradores
            modelBuilder.Entity<Usuario>().HasOne(usu => usu.Contato).WithOne(cont => cont.Usuario).HasForeignKey<Contato>(a => a.UsuarioId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Usuario>().HasMany(usu => usu.EnderecosEntrega).WithOne(end => end.Usuario).HasForeignKey(end => end.UsuarioId);
            modelBuilder.Entity<Usuario>().HasMany(usu => usu.Departamentos).WithMany(dep => dep.Usuarios);


            //Cascade
            //SetNull -> Desvinculação, a coluna tem que aceitar receber um valor nulo.

            //ClientCascade
            //ClientNoAction
            //Toda vez que for visto a palavra Client, somente o EF Core estará realizando uma operação.

            modelBuilder.Entity<Usuario>().Property(a => a.RG).IsRequired().HasMaxLength(12);
            //modelBuilder.Entity<Usuario>().Property(a => a.Preco).HasPrecision(2); //Digitos após a vírgula.
            #endregion

            #region Estrutura de organização - Pequenos/Médios
            /*
             Pequenos/Médios > Cerca 0 - 10 usando o OnModelCreating
             FluentAPI deixa mais limpo e dá mais poder para o código
             Usar regions para organizar

            #region Usuario
            modelBuilder.Entity<Usuario>();
            modelBuilder.Entity<Usuario>();
            modelBuilder.Entity<Usuario>();
            modelBuilder.Entity<Usuario>();
            #endregion

            #region Contato
            modelBuilder.Entity<Usuario>();
            modelBuilder.Entity<Usuario>();
            modelBuilder.Entity<Usuario>();
            modelBuilder.Entity<Usuario>();
            #endregion
             */
            #endregion

            #region Médio/Grande > +10 Tabelas


            #endregion
            modelBuilder.ApplyConfiguration(new UsuarioConfiguration()); //Instanciando a classe do mapeamento do Usuario.

        }
    }
}
