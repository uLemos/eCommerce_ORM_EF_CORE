﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using eCommerce.Office;

#nullable disable

namespace eCommerce.Office.Migrations
{
    [DbContext(typeof(eCommerceOfficeContext))]
    [Migration("20230708214021_TurmaSeeds")]
    partial class TurmaSeeds
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("ColaboradorTurma", b =>
                {
                    b.Property<int>("ColaboradoresId")
                        .HasColumnType("int");

                    b.Property<int>("TurmasId")
                        .HasColumnType("int");

                    b.HasKey("ColaboradoresId", "TurmasId");

                    b.HasIndex("TurmasId");

                    b.ToTable("ColaboradorTurma");
                });

            modelBuilder.Entity("ColaboradorVeiculo", b =>
                {
                    b.Property<int>("ColaboradoresId")
                        .HasColumnType("int");

                    b.Property<int>("VeiculosId")
                        .HasColumnType("int");

                    b.HasKey("ColaboradoresId", "VeiculosId");

                    b.HasIndex("VeiculosId");

                    b.ToTable("ColaboradorVeiculo");
                });

            modelBuilder.Entity("eCommerce.Office.Models.Colaborador", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Colaboradores");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Nome = "Lemos"
                        },
                        new
                        {
                            Id = 2,
                            Nome = "Santos"
                        },
                        new
                        {
                            Id = 3,
                            Nome = "Trevisano"
                        },
                        new
                        {
                            Id = 4,
                            Nome = "Ana"
                        },
                        new
                        {
                            Id = 5,
                            Nome = "Agnes"
                        },
                        new
                        {
                            Id = 6,
                            Nome = "Luiz"
                        },
                        new
                        {
                            Id = 7,
                            Nome = "Neto"
                        });
                });

            modelBuilder.Entity("eCommerce.Office.Models.ColaboradorSetor", b =>
                {
                    b.Property<int>("ColaboradorId")
                        .HasColumnType("int");

                    b.Property<int>("SetorId")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset>("Criado")
                        .HasColumnType("datetimeoffset");

                    b.HasKey("ColaboradorId", "SetorId");

                    b.HasIndex("SetorId");

                    b.ToTable("ColaboradoresSetores");

                    b.HasData(
                        new
                        {
                            ColaboradorId = 1,
                            SetorId = 1,
                            Criado = new DateTimeOffset(new DateTime(2023, 7, 8, 18, 40, 20, 974, DateTimeKind.Unspecified).AddTicks(9075), new TimeSpan(0, -3, 0, 0, 0))
                        },
                        new
                        {
                            ColaboradorId = 6,
                            SetorId = 1,
                            Criado = new DateTimeOffset(new DateTime(2023, 7, 8, 18, 40, 20, 974, DateTimeKind.Unspecified).AddTicks(9101), new TimeSpan(0, -3, 0, 0, 0))
                        },
                        new
                        {
                            ColaboradorId = 5,
                            SetorId = 2,
                            Criado = new DateTimeOffset(new DateTime(2023, 7, 8, 18, 40, 20, 974, DateTimeKind.Unspecified).AddTicks(9102), new TimeSpan(0, -3, 0, 0, 0))
                        },
                        new
                        {
                            ColaboradorId = 4,
                            SetorId = 2,
                            Criado = new DateTimeOffset(new DateTime(2023, 7, 8, 18, 40, 20, 974, DateTimeKind.Unspecified).AddTicks(9104), new TimeSpan(0, -3, 0, 0, 0))
                        },
                        new
                        {
                            ColaboradorId = 7,
                            SetorId = 3,
                            Criado = new DateTimeOffset(new DateTime(2023, 7, 8, 18, 40, 20, 974, DateTimeKind.Unspecified).AddTicks(9105), new TimeSpan(0, -3, 0, 0, 0))
                        },
                        new
                        {
                            ColaboradorId = 2,
                            SetorId = 4,
                            Criado = new DateTimeOffset(new DateTime(2023, 7, 8, 18, 40, 20, 974, DateTimeKind.Unspecified).AddTicks(9106), new TimeSpan(0, -3, 0, 0, 0))
                        },
                        new
                        {
                            ColaboradorId = 3,
                            SetorId = 4,
                            Criado = new DateTimeOffset(new DateTime(2023, 7, 8, 18, 40, 20, 974, DateTimeKind.Unspecified).AddTicks(9108), new TimeSpan(0, -3, 0, 0, 0))
                        });
                });

            modelBuilder.Entity("eCommerce.Office.Models.Setor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Setores");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Nome = "Logística"
                        },
                        new
                        {
                            Id = 2,
                            Nome = "Separação"
                        },
                        new
                        {
                            Id = 3,
                            Nome = "Financeira"
                        },
                        new
                        {
                            Id = 4,
                            Nome = "Administrativo"
                        });
                });

            modelBuilder.Entity("eCommerce.Office.Models.Turma", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Turmas");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Nome = "Turma A1"
                        },
                        new
                        {
                            Id = 2,
                            Nome = "Turma A2"
                        },
                        new
                        {
                            Id = 3,
                            Nome = "Turma A3"
                        },
                        new
                        {
                            Id = 4,
                            Nome = "Turma A4"
                        },
                        new
                        {
                            Id = 5,
                            Nome = "Turma A5"
                        });
                });

            modelBuilder.Entity("eCommerce.Office.Models.Veiculo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Placa")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Veiculos");
                });

            modelBuilder.Entity("ColaboradorTurma", b =>
                {
                    b.HasOne("eCommerce.Office.Models.Colaborador", null)
                        .WithMany()
                        .HasForeignKey("ColaboradoresId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("eCommerce.Office.Models.Turma", null)
                        .WithMany()
                        .HasForeignKey("TurmasId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ColaboradorVeiculo", b =>
                {
                    b.HasOne("eCommerce.Office.Models.Colaborador", null)
                        .WithMany()
                        .HasForeignKey("ColaboradoresId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("eCommerce.Office.Models.Veiculo", null)
                        .WithMany()
                        .HasForeignKey("VeiculosId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("eCommerce.Office.Models.ColaboradorSetor", b =>
                {
                    b.HasOne("eCommerce.Office.Models.Colaborador", "Colaborador")
                        .WithMany("ColaboradoresSetores")
                        .HasForeignKey("ColaboradorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("eCommerce.Office.Models.Setor", "Setor")
                        .WithMany("ColaboradoresSetores")
                        .HasForeignKey("SetorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Colaborador");

                    b.Navigation("Setor");
                });

            modelBuilder.Entity("eCommerce.Office.Models.Colaborador", b =>
                {
                    b.Navigation("ColaboradoresSetores");
                });

            modelBuilder.Entity("eCommerce.Office.Models.Setor", b =>
                {
                    b.Navigation("ColaboradoresSetores");
                });
#pragma warning restore 612, 618
        }
    }
}
