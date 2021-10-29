﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WS.Data.Context;

namespace WS.Data.Migrations
{
    [DbContext(typeof(WsContext))]
    [Migration("20211029135517_v4")]
    partial class v4
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("WS.Core.Domain.AspNetClient", b =>
                {
                    b.Property<int>("ClientId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Banco")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Orgao")
                        .HasMaxLength(5)
                        .HasColumnType("int");

                    b.Property<string>("RazaoSocial")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("Server")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime?>("Vencimento")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("Date")
                        .HasDefaultValue(new DateTime(2025, 10, 29, 10, 55, 17, 266, DateTimeKind.Local).AddTicks(206));

                    b.HasKey("ClientId");

                    b.ToTable("AspNetClient");
                });

            modelBuilder.Entity("WS.Core.Domain.AspNetClientMenu", b =>
                {
                    b.Property<int>("ClientId")
                        .HasColumnType("int");

                    b.Property<int>("MenuId")
                        .HasColumnType("int");

                    b.Property<bool>("Editar")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(true);

                    b.Property<bool>("Excluir")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(true);

                    b.Property<bool>("Exibir")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(true);

                    b.Property<bool>("Inserir")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(true);

                    b.HasKey("ClientId", "MenuId");

                    b.HasIndex("MenuId");

                    b.ToTable("AspNetClientMenu");
                });

            modelBuilder.Entity("WS.Core.Domain.AspNetClientModule", b =>
                {
                    b.Property<int>("ClientId")
                        .HasColumnType("int");

                    b.Property<int>("ModuleId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Vencimento")
                        .HasColumnType("Date");

                    b.HasKey("ClientId", "ModuleId");

                    b.HasIndex("ModuleId");

                    b.ToTable("AspNetClientModule");
                });

            modelBuilder.Entity("WS.Core.Domain.AspNetMenu", b =>
                {
                    b.Property<int>("MenuId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Menu")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("ModuleId")
                        .HasColumnType("int");

                    b.Property<int>("Ordem")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.HasKey("MenuId");

                    b.HasIndex("ModuleId");

                    b.ToTable("AspNetMenu");
                });

            modelBuilder.Entity("WS.Core.Domain.AspNetModule", b =>
                {
                    b.Property<int>("ModuleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("ImgMenu")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Module")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Ordem")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.HasKey("ModuleId");

                    b.ToTable("AspNetModule");
                });

            modelBuilder.Entity("WS.Core.Domain.AspNetClientMenu", b =>
                {
                    b.HasOne("WS.Core.Domain.AspNetClient", "Client")
                        .WithMany("aspNetClientMenus")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WS.Core.Domain.AspNetMenu", "Menu")
                        .WithMany("aspNetClientMenus")
                        .HasForeignKey("MenuId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");

                    b.Navigation("Menu");
                });

            modelBuilder.Entity("WS.Core.Domain.AspNetClientModule", b =>
                {
                    b.HasOne("WS.Core.Domain.AspNetClient", "Client")
                        .WithMany("aspNetClientModules")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WS.Core.Domain.AspNetModule", "Module")
                        .WithMany("aspNetClientModules")
                        .HasForeignKey("ModuleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");

                    b.Navigation("Module");
                });

            modelBuilder.Entity("WS.Core.Domain.AspNetMenu", b =>
                {
                    b.HasOne("WS.Core.Domain.AspNetModule", "Module")
                        .WithMany("aspNetMenus")
                        .HasForeignKey("ModuleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Module");
                });

            modelBuilder.Entity("WS.Core.Domain.AspNetClient", b =>
                {
                    b.Navigation("aspNetClientMenus");

                    b.Navigation("aspNetClientModules");
                });

            modelBuilder.Entity("WS.Core.Domain.AspNetMenu", b =>
                {
                    b.Navigation("aspNetClientMenus");
                });

            modelBuilder.Entity("WS.Core.Domain.AspNetModule", b =>
                {
                    b.Navigation("aspNetClientModules");

                    b.Navigation("aspNetMenus");
                });
#pragma warning restore 612, 618
        }
    }
}
