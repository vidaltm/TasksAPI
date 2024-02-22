﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TasksAPI.Data;

namespace TasksAPI.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240222191913_InclusaoDadosComentario")]
    partial class InclusaoDadosComentario
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.11");

            modelBuilder.Entity("TasksAPI.Models.Comentarios", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Comentario")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("ProjetoId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TarefaId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Usuario")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("TarefaId");

                    b.ToTable("Comentarios");
                });

            modelBuilder.Entity("TasksAPI.Models.HistoricoTarefa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DataModificacao")
                        .HasColumnType("TEXT");

                    b.Property<string>("Modificacao")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("ProjetoId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TarefaId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Usuario")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("HistoricoTarefas");
                });

            modelBuilder.Entity("TasksAPI.Models.Projeto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("NomeProjeto")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Usuario")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Projetos");
                });

            modelBuilder.Entity("TasksAPI.Models.Tarefa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("DescricaoTarefa")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("NomeTarefa")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Prioridade")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ProjetoId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Status")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Usuario")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ProjetoId");

                    b.ToTable("Tarefas");
                });

            modelBuilder.Entity("TasksAPI.Models.Comentarios", b =>
                {
                    b.HasOne("TasksAPI.Models.Tarefa", null)
                        .WithMany("Comentario")
                        .HasForeignKey("TarefaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TasksAPI.Models.Tarefa", b =>
                {
                    b.HasOne("TasksAPI.Models.Projeto", null)
                        .WithMany("Tarefas")
                        .HasForeignKey("ProjetoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TasksAPI.Models.Projeto", b =>
                {
                    b.Navigation("Tarefas");
                });

            modelBuilder.Entity("TasksAPI.Models.Tarefa", b =>
                {
                    b.Navigation("Comentario");
                });
#pragma warning restore 612, 618
        }
    }
}
