﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RecursosHumanos.Datos;

#nullable disable

namespace RecursosHumanos.Migrations
{
    [DbContext(typeof(AplicationDbContext))]
    partial class AplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.20")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ProyectoYo.Models.Capacitacion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ColaboradorId")
                        .HasColumnType("int");

                    b.Property<string>("Duracion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImagenUrlCap")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MaxPersonas")
                        .HasColumnType("int");

                    b.Property<int>("MinPersonas")
                        .HasColumnType("int");

                    b.Property<string>("Modalidad")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NombreCapacitacion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ColaboradorId");

                    b.ToTable("capacitacion");
                });

            modelBuilder.Entity("RecursosHumanos.Models.Evaluacion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TipoEvaluacion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("evaluacion");
                });

            modelBuilder.Entity("RecursosHumanos.Models.EvaluacionColaborador", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ColaboradorEvaluado")
                        .HasColumnType("int");

                    b.Property<int>("ColaboradorEvaluador")
                        .HasColumnType("int");

                    b.Property<int>("EvaluacionId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EvaluacionId");

                    b.ToTable("evaluacionColaborador");
                });

            modelBuilder.Entity("RecursosHumanos.Models.Pregunta", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("FinalEscala")
                        .HasColumnType("int");

                    b.Property<int>("IdCapacitacion")
                        .HasColumnType("int");

                    b.Property<int>("IdEvaluacion")
                        .HasColumnType("int");

                    b.Property<int>("InicioEscala")
                        .HasColumnType("int");

                    b.Property<string>("Texto")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("IdCapacitacion");

                    b.HasIndex("IdEvaluacion");

                    b.ToTable("pregunta");
                });

            modelBuilder.Entity("RecursosHumanos.Models.Respuesta", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ColaboradorEvaluado")
                        .HasColumnType("int");

                    b.Property<int>("ColaboradorEvaluador")
                        .HasColumnType("int");

                    b.Property<int>("IdPregunta")
                        .HasColumnType("int");

                    b.Property<int>("ValorRespuesta")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("respuesta");
                });

            modelBuilder.Entity("WebApplication1.Models.Colaborador", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Apellido1")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Apellido2")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CedulaColaborador")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CorreoColaborador")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("DepartamentoId")
                        .HasColumnType("int");

                    b.Property<int>("IdDepartamento")
                        .HasColumnType("int");

                    b.Property<int>("IdInstitucion")
                        .HasColumnType("int");

                    b.Property<int>("IdPuesto")
                        .HasColumnType("int");

                    b.Property<string>("ImagenUrlCol")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("InstitucionId")
                        .HasColumnType("int");

                    b.Property<string>("NombreColaborador")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PuestoId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DepartamentoId");

                    b.HasIndex("InstitucionId");

                    b.HasIndex("PuestoId");

                    b.ToTable("colaborador");
                });

            modelBuilder.Entity("WebApplication1.Models.Departamento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("DescripcionPuesto")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NombreDepartamento")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("departamento");
                });

            modelBuilder.Entity("WebApplication1.Models.Institucion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("DescripcionInstitucion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImagenUrlIns")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NombreInstitucion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("institucion");
                });

            modelBuilder.Entity("WebApplication1.Models.Puesto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("DescripcionPuesto")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NombrePuesto")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("puesto");
                });

            modelBuilder.Entity("ProyectoYo.Models.Capacitacion", b =>
                {
                    b.HasOne("WebApplication1.Models.Colaborador", "Colaborador")
                        .WithMany()
                        .HasForeignKey("ColaboradorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Colaborador");
                });

            modelBuilder.Entity("RecursosHumanos.Models.EvaluacionColaborador", b =>
                {
                    b.HasOne("RecursosHumanos.Models.Evaluacion", "Evaluacion")
                        .WithMany()
                        .HasForeignKey("EvaluacionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Evaluacion");
                });

            modelBuilder.Entity("RecursosHumanos.Models.Pregunta", b =>
                {
                    b.HasOne("ProyectoYo.Models.Capacitacion", "Capacitacion")
                        .WithMany()
                        .HasForeignKey("IdCapacitacion")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RecursosHumanos.Models.Evaluacion", "Evaluacion")
                        .WithMany("Preguntas")
                        .HasForeignKey("IdEvaluacion")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Capacitacion");

                    b.Navigation("Evaluacion");
                });

            modelBuilder.Entity("WebApplication1.Models.Colaborador", b =>
                {
                    b.HasOne("WebApplication1.Models.Departamento", "Departamento")
                        .WithMany()
                        .HasForeignKey("DepartamentoId");

                    b.HasOne("WebApplication1.Models.Institucion", "Institucion")
                        .WithMany()
                        .HasForeignKey("InstitucionId");

                    b.HasOne("WebApplication1.Models.Puesto", "Puesto")
                        .WithMany()
                        .HasForeignKey("PuestoId");

                    b.Navigation("Departamento");

                    b.Navigation("Institucion");

                    b.Navigation("Puesto");
                });

            modelBuilder.Entity("RecursosHumanos.Models.Evaluacion", b =>
                {
                    b.Navigation("Preguntas");
                });
#pragma warning restore 612, 618
        }
    }
}
