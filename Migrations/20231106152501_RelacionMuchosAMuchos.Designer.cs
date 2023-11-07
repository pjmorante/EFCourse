﻿// <auto-generated />
using System;
using CursoEFCore.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CursoEFCore.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20231106152501_RelacionMuchosAMuchos")]
    partial class RelacionMuchosAMuchos
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("CursoEFCore.Models.Articulo", b =>
                {
                    b.Property<int>("Articulo_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Articulo_Id"), 1L, 1);

                    b.Property<double>("Calificacion")
                        .HasColumnType("float");

                    b.Property<int>("Categoria_Id")
                        .HasColumnType("int");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("datetime2");

                    b.Property<string>("TituloArticulo")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("Articulo");

                    b.HasKey("Articulo_Id");

                    b.HasIndex("Categoria_Id");

                    b.ToTable("Tbl_Articulo");
                });

            modelBuilder.Entity("CursoEFCore.Models.ArticuloEtiqueta", b =>
                {
                    b.Property<int>("Etiqueta_Id")
                        .HasColumnType("int");

                    b.Property<int>("Articulo_Id")
                        .HasColumnType("int");

                    b.HasKey("Etiqueta_Id", "Articulo_Id");

                    b.HasIndex("Articulo_Id");

                    b.ToTable("ArticuloEtiqueta");
                });

            modelBuilder.Entity("CursoEFCore.Models.Categoria", b =>
                {
                    b.Property<int>("Categoria_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Categoria_Id"), 1L, 1);

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Categoria_Id");

                    b.ToTable("Categoria");
                });

            modelBuilder.Entity("CursoEFCore.Models.DetalleUsuario", b =>
                {
                    b.Property<int>("DetalleUsuario_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DetalleUsuario_Id"), 1L, 1);

                    b.Property<string>("Cedula")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Deporte")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Mascota")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DetalleUsuario_Id");

                    b.ToTable("DetalleUsuario");
                });

            modelBuilder.Entity("CursoEFCore.Models.Etiqueta", b =>
                {
                    b.Property<int>("Etiqueta_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Etiqueta_Id"), 1L, 1);

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("datetime2");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Etiqueta_Id");

                    b.ToTable("Etiqueta");
                });

            modelBuilder.Entity("CursoEFCore.Models.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("DetalleUsuario_Id")
                        .HasColumnType("int");

                    b.Property<string>("Direccion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DetalleUsuario_Id")
                        .IsUnique();

                    b.ToTable("Usuario");
                });

            modelBuilder.Entity("CursoEFCore.Models.Articulo", b =>
                {
                    b.HasOne("CursoEFCore.Models.Categoria", "Categoria")
                        .WithMany("Articulo")
                        .HasForeignKey("Categoria_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Categoria");
                });

            modelBuilder.Entity("CursoEFCore.Models.ArticuloEtiqueta", b =>
                {
                    b.HasOne("CursoEFCore.Models.Articulo", "Articulo")
                        .WithMany("ArticuloEtiqueta")
                        .HasForeignKey("Articulo_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CursoEFCore.Models.Etiqueta", "Etiqueta")
                        .WithMany("ArticuloEtiqueta")
                        .HasForeignKey("Etiqueta_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Articulo");

                    b.Navigation("Etiqueta");
                });

            modelBuilder.Entity("CursoEFCore.Models.Usuario", b =>
                {
                    b.HasOne("CursoEFCore.Models.DetalleUsuario", "DetalleUsuario")
                        .WithOne("Usuario")
                        .HasForeignKey("CursoEFCore.Models.Usuario", "DetalleUsuario_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DetalleUsuario");
                });

            modelBuilder.Entity("CursoEFCore.Models.Articulo", b =>
                {
                    b.Navigation("ArticuloEtiqueta");
                });

            modelBuilder.Entity("CursoEFCore.Models.Categoria", b =>
                {
                    b.Navigation("Articulo");
                });

            modelBuilder.Entity("CursoEFCore.Models.DetalleUsuario", b =>
                {
                    b.Navigation("Usuario")
                        .IsRequired();
                });

            modelBuilder.Entity("CursoEFCore.Models.Etiqueta", b =>
                {
                    b.Navigation("ArticuloEtiqueta");
                });
#pragma warning restore 612, 618
        }
    }
}
