﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Print3dMarketplace.PrintersAPI.EF;

#nullable disable

namespace Print3dMarketplace.PrintersAPI.Migrations
{
    [DbContext(typeof(PrintersDbContext))]
    partial class PrintersDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Print3dMarketplace.PrintersAPI.Entities.Nozzle", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Size")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Nozzles");

                    b.HasData(
                        new
                        {
                            Id = new Guid("74dc42de-4b5f-417d-ac2a-e5ba4faf8ad7"),
                            Size = "0.2mm Nozzle"
                        },
                        new
                        {
                            Id = new Guid("9f82dc95-221e-4e87-9e5c-0627a68acf8d"),
                            Size = "0.3mm Nozzle"
                        },
                        new
                        {
                            Id = new Guid("53090a10-2947-4e67-aa0f-d4f2fd0d3815"),
                            Size = "0.4mm Nozzle"
                        },
                        new
                        {
                            Id = new Guid("f1d958c4-9468-4263-a122-c415d6fda1bb"),
                            Size = "0.5mm Nozzle"
                        },
                        new
                        {
                            Id = new Guid("1ca6b637-9fd8-445f-b7e2-7df328dbeb28"),
                            Size = "0.6mm Nozzle"
                        },
                        new
                        {
                            Id = new Guid("11f2485c-f85b-4d74-af99-c126915282ac"),
                            Size = "0.8mm Nozzle"
                        },
                        new
                        {
                            Id = new Guid("5b70703f-280c-499a-992f-c180aef47fca"),
                            Size = "1.0mm Nozzle"
                        });
                });

            modelBuilder.Entity("Print3dMarketplace.PrintersAPI.Entities.Printer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ApplicationUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("ModelName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("NozzleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("PrintAreaHeight")
                        .HasColumnType("float");

                    b.Property<double>("PrintAreaLength")
                        .HasColumnType("float");

                    b.Property<double>("PrintAreaWidth")
                        .HasColumnType("float");

                    b.Property<Guid>("TemplatePrinterId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("NozzleId");

                    b.HasIndex("TemplatePrinterId");

                    b.ToTable("Printers");
                });

            modelBuilder.Entity("Print3dMarketplace.PrintersAPI.Entities.TemplatePrinter", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ModelName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("PrintAreaHeight")
                        .HasColumnType("float");

                    b.Property<double>("PrintAreaLength")
                        .HasColumnType("float");

                    b.Property<double>("PrintAreaWidth")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("TemplatePrinters");

                    b.HasData(
                        new
                        {
                            Id = new Guid("067f16d2-e3b3-4d37-8d87-bff0e3e338c9"),
                            ModelName = "Ender i3",
                            PrintAreaHeight = 150.0,
                            PrintAreaLength = 150.0,
                            PrintAreaWidth = 150.0
                        },
                        new
                        {
                            Id = new Guid("01249e15-d84d-406b-b9e0-21fc5e97d412"),
                            ModelName = "Prusa 360",
                            PrintAreaHeight = 200.0,
                            PrintAreaLength = 200.0,
                            PrintAreaWidth = 200.0
                        },
                        new
                        {
                            Id = new Guid("9c7469f2-420a-4740-ac67-6151d27c6ce1"),
                            ModelName = "Anet 300",
                            PrintAreaHeight = 300.0,
                            PrintAreaLength = 300.0,
                            PrintAreaWidth = 300.0
                        });
                });

            modelBuilder.Entity("Print3dMarketplace.PrintersAPI.Entities.Printer", b =>
                {
                    b.HasOne("Print3dMarketplace.PrintersAPI.Entities.Nozzle", "Nozzle")
                        .WithMany()
                        .HasForeignKey("NozzleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Print3dMarketplace.PrintersAPI.Entities.TemplatePrinter", "TemplatePrinter")
                        .WithMany()
                        .HasForeignKey("TemplatePrinterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Nozzle");

                    b.Navigation("TemplatePrinter");
                });
#pragma warning restore 612, 618
        }
    }
}
