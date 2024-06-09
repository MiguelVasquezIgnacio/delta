﻿// <auto-generated />
using System;
using MiRadio_Startup.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MiRadio_Startup.Migrations
{
    [DbContext(typeof(MyContext))]
    partial class MyContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.5");

            modelBuilder.Entity("MiRadio_Startup.Models.Comentario", b =>
                {
                    b.Property<int>("IdComentario")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("FechaPublicacion")
                        .HasColumnType("TEXT");

                    b.Property<int>("Id")
                        .HasColumnType("INTEGER");

                    b.Property<int>("MusicaId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Texto")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("INTEGER");

                    b.HasKey("IdComentario");

                    b.HasIndex("MusicaId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Comentarios");
                });

            modelBuilder.Entity("MiRadio_Startup.Models.Etiqueta", b =>
                {
                    b.Property<int>("IdEtiqueta")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("MusicaId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Valor")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("IdEtiqueta");

                    b.HasIndex("MusicaId");

                    b.ToTable("Etiquetas");
                });

            modelBuilder.Entity("MiRadio_Startup.Models.Musica", b =>
                {
                    b.Property<int>("IdMusica")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Autor")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("FechaPublicacion")
                        .HasColumnType("TEXT");

                    b.Property<string>("Genero")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("TamanoKB")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Titulo")
                        .HasColumnType("TEXT");

                    b.Property<string>("UrlMusica")
                        .HasColumnType("TEXT");

                    b.HasKey("IdMusica");

                    b.ToTable("Musicas");
                });

            modelBuilder.Entity("MiRadio_Startup.Models.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("NombreUsuario")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Rol")
                        .HasColumnType("INTEGER");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("MiRadio_Startup.Models.Comentario", b =>
                {
                    b.HasOne("MiRadio_Startup.Models.Musica", "MusicaS")
                        .WithMany()
                        .HasForeignKey("MusicaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MiRadio_Startup.Models.Usuario", "UsuarioS")
                        .WithMany("Comentarios")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MusicaS");

                    b.Navigation("UsuarioS");
                });

            modelBuilder.Entity("MiRadio_Startup.Models.Etiqueta", b =>
                {
                    b.HasOne("MiRadio_Startup.Models.Musica", "Musica")
                        .WithMany("Etiquetas")
                        .HasForeignKey("MusicaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Musica");
                });

            modelBuilder.Entity("MiRadio_Startup.Models.Musica", b =>
                {
                    b.Navigation("Etiquetas");
                });

            modelBuilder.Entity("MiRadio_Startup.Models.Usuario", b =>
                {
                    b.Navigation("Comentarios");
                });
#pragma warning restore 612, 618
        }
    }
}
