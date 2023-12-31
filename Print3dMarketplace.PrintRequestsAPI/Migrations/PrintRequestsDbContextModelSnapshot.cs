﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Print3dMarketplace.PrintRequestsAPI.EF;

#nullable disable

namespace Print3dMarketplace.PrintRequestsAPI.Migrations
{
    [DbContext(typeof(PrintRequestsDbContext))]
    partial class PrintRequestsDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Print3dMarketplace.PrintRequestsAPI.Entities.PrintRequest", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ApplicationUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ColorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Comment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("CustomerSubmittedCreatorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FileName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Infill")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<double>("LayerHeight")
                        .HasColumnType("float");

                    b.Property<double>("PrintAreaHeight")
                        .HasColumnType("float");

                    b.Property<double>("PrintAreaLength")
                        .HasColumnType("float");

                    b.Property<double>("PrintAreaWidth")
                        .HasColumnType("float");

                    b.Property<Guid>("PrintRequestStatusId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("TemplateMaterialId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool?>("UseSupports")
                        .HasColumnType("bit");

                    b.Property<double?>("WallThickness")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("PrintRequestStatusId");

                    b.ToTable("PrintRequests");
                });

            modelBuilder.Entity("Print3dMarketplace.PrintRequestsAPI.Entities.PrintRequestStatus", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("PrintRequestStatuses");

                    b.HasData(
                        new
                        {
                            Id = new Guid("b74f7ee1-2a40-4150-9d5c-a247eb0dc47f"),
                            Name = "New"
                        },
                        new
                        {
                            Id = new Guid("a4c32814-cb53-4222-81eb-2da6de21e33a"),
                            Name = "Canceled"
                        },
                        new
                        {
                            Id = new Guid("9e78957b-352c-40f8-bc3a-327312e0f026"),
                            Name = "CreatorSubmission"
                        },
                        new
                        {
                            Id = new Guid("edce0867-a9ec-497f-a49a-1151ea0cfda7"),
                            Name = "CustomerSubmission"
                        });
                });

            modelBuilder.Entity("Print3dMarketplace.PrintRequestsAPI.Entities.SubmittedCreator", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CreatorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CreatorName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("PrintRequestId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("PrintRequestId");

                    b.ToTable("SubmittedCreators");
                });

            modelBuilder.Entity("Print3dMarketplace.PrintRequestsAPI.Entities.PrintRequest", b =>
                {
                    b.HasOne("Print3dMarketplace.PrintRequestsAPI.Entities.PrintRequestStatus", "PrintRequestStatus")
                        .WithMany()
                        .HasForeignKey("PrintRequestStatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PrintRequestStatus");
                });

            modelBuilder.Entity("Print3dMarketplace.PrintRequestsAPI.Entities.SubmittedCreator", b =>
                {
                    b.HasOne("Print3dMarketplace.PrintRequestsAPI.Entities.PrintRequest", "PrintRequest")
                        .WithMany("SubmittedCreators")
                        .HasForeignKey("PrintRequestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PrintRequest");
                });

            modelBuilder.Entity("Print3dMarketplace.PrintRequestsAPI.Entities.PrintRequest", b =>
                {
                    b.Navigation("SubmittedCreators");
                });
#pragma warning restore 612, 618
        }
    }
}
