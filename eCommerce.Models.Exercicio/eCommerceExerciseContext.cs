using eCommerce.Models.Exercicio.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Models.Exercicio
{
    public class eCommerceExerciseContext : DbContext
    {
        public eCommerceExerciseContext(DbContextOptions<eCommerceExerciseContext> options) : base(options)
        {

        }

        public DbSet<Usuario>? Usuarios { get; set; }
        public DbSet<Contato>? Contatos { get; set; }
        public DbSet<EnderecoEntrega>? EndereceEntragas { get; set; }
        public DbSet<Departamento>? Departamentos { get; set; }
        public DbSet<Pedido>? Pedidos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>().ToTable("Tabela_Exercício");
            modelBuilder.Entity<Usuario>().Property(a => a.Nome).HasMaxLength(30).HasColumnName("NOME").IsRequired();
            modelBuilder.Entity<Usuario>().Property(a => a.Mae).HasMaxLength(30);
            modelBuilder.Entity<Usuario>().Ignore(a => new { a.Mae, a.Sexo, a.RG });
            modelBuilder.Entity<Usuario>().HasIndex(a => new { a.CPF, a.Email }).HasFilter("[CPF] && [EMAIL] is not null").HasDatabaseName("Dados");
            modelBuilder.Entity<Usuario>().HasKey(a => a.Id);
            modelBuilder.Entity<Usuario>().HasAlternateKey(a => new { a.CPF, a.Email });
            modelBuilder.Entity<Usuario>().HasOne(a => a.Contato).WithOne(a => a.Usuario);
            modelBuilder.Entity<Usuario>().HasMany(a => a.EnderecosEntrega).WithOne(a => a.Usuario).HasForeignKey(a => a.UsuarioId);
            modelBuilder.Entity<Usuario>().HasMany(a => a.Departamentos).WithMany(a => a.Usuarios);
            modelBuilder.Entity<Usuario>().HasMany(a => a.Pedidos).WithOne(a => a.Usuario);

            modelBuilder.Entity<Contato>().ToTable("Tabela_Exercício_Contato");
            modelBuilder.Entity<Contato>().HasOne(a => a.Usuario).WithOne(a => a.Contato);
            modelBuilder.Entity<Contato>().HasIndex(a => new { a.Celular, a.Telefone });
            modelBuilder.Entity<Contato>().HasKey(a => a.Id);

            modelBuilder.Entity<EnderecoEntrega>().ToTable("TABELA_EXERCÍCIO_END");
            modelBuilder.Entity<EnderecoEntrega>().HasKey(a => a.Id);
            modelBuilder.Entity<EnderecoEntrega>().HasOne(a => a.Usuario).WithMany(a => a.EnderecosEntrega).HasForeignKey(a => a.UsuarioId);
            modelBuilder.Entity<EnderecoEntrega>().HasIndex(a =>
                new
                {
                    a.NomeEndereco,
                    a.CEP,
                    a.Endereco,
                    a.Estado,
                    a.Cidade,
                    a.Bairro,
                    a.Numero,
                    a.Complemento
                });


            modelBuilder.Entity<Departamento>().ToTable("DEPARTAMENTOS");
            modelBuilder.Entity<Departamento>().HasMany(a => a.Usuarios).WithMany(a => a.Departamentos);
            modelBuilder.Entity<Departamento>().HasKey(a => a.Id);
            modelBuilder.Entity<Departamento>().Property(a => a.Nome).IsRequired();

            modelBuilder.Entity<Pedido>().ToTable("PEDIDOS");
            modelBuilder.Entity<Pedido>().HasKey(a => a.Id);
            modelBuilder.Entity<Pedido>().HasIndex(a => a.NomePedido);
            modelBuilder.Entity<Pedido>().Property(a => a.NomePedido).IsRequired();
            modelBuilder.Entity<Pedido>().HasOne(a => a.Usuario).WithMany(a => a.Pedidos).HasForeignKey(a => a.UsuarioId); 
        }
    }
}
