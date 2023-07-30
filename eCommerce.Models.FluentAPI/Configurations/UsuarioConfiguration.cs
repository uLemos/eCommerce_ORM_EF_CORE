using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Models.FluentAPI.Configurations
{
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("TB_USUARIOS");
            builder.Property(a => a.RG).HasColumnName("REGISTRO_GERAL").HasMaxLength(10).HasDefaultValue("RG-AUSENTE").IsRequired();
            builder.Ignore(a => a.Sexo);
            builder.Property(a => a.Id).ValueGeneratedOnAdd();

            builder.HasIndex("CPF").IsUnique().HasFilter("[CPF] is not null").HasDatabaseName("IX_CPF_UNIQUE");
            builder.HasIndex("CPF"); //Index padrão
            builder.HasIndex(a => a.CPF); // Com lambda
            builder.HasIndex("CPF", "Email"); // Composto string
            builder.HasIndex(a => new { a.CPF, a.Email }); // Composto Lambda
            builder.HasKey("Id");
            builder.HasKey(a => a.Id);
            builder.HasKey(a => new { a.Id, a.CPF });
            
            //Alterantivas
            builder.HasNoKey(); //Ignora que há uma chave.
            builder.HasAlternateKey("CPF"); //Ter mais um elemento como forte, ou seja, seria uma outra chave(Secundária). 
            builder.HasAlternateKey("CPF", "Email"); //Ter mais um elemento como forte, ou seja, seria uma outra chave(Secundária), podendo ser composta. 
            builder.HasKey("Id", "CPF"); //Exemplo, mas não faz sentido para a regra de negócios o Id estar em conjunto


            //One > 1 Propriedade de Navegação do Objeto único(Composição)
            //Many > 1 Propriedade de Navegação do tipo Coleção/Lista/Enumeradores
            builder.HasOne(usu => usu.Contato).WithOne(cont => cont.Usuario).HasForeignKey<Contato>(a => a.UsuarioId).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(usu => usu.EnderecosEntrega).WithOne(end => end.Usuario).HasForeignKey(end => end.UsuarioId);
            builder.HasMany(usu => usu.Departamentos).WithMany(dep => dep.Usuarios);


        }
    }
}
