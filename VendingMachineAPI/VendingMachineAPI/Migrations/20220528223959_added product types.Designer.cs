﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using VendingMachineAPI.Models.DAL;

#nullable disable

namespace VendingMachineAPI.Migrations
{
    [DbContext(typeof(VendingMachineContext))]
    [Migration("20220528223959_added product types")]
    partial class addedproducttypes
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("VendingMachineAPI.Models.CreditCardVerification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("Approved")
                        .HasColumnType("bit");

                    b.Property<DateTime>("InsertDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("OriginalTransactionAmount")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("CreditCardsVerification");
                });

            modelBuilder.Entity("VendingMachineAPI.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("InsertDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ProductTypeId")
                        .HasColumnType("int");

                    b.Property<DateTime>("RefundDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("SaleDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("TransactionId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProductTypeId");

                    b.HasIndex("TransactionId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("VendingMachineAPI.Models.ProductType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<decimal>("Cost")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ProductTypes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Cost = 0.95m,
                            Type = "Soda"
                        },
                        new
                        {
                            Id = 2,
                            Cost = 0.60m,
                            Type = "Candy Bar"
                        },
                        new
                        {
                            Id = 3,
                            Cost = 0.99m,
                            Type = "Chips"
                        });
                });

            modelBuilder.Entity("VendingMachineAPI.Models.Transaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CCVerificationId")
                        .HasColumnType("int");

                    b.Property<DateTime>("InsertDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("RefundRequested")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("CCVerificationId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("VendingMachineAPI.Models.Product", b =>
                {
                    b.HasOne("VendingMachineAPI.Models.ProductType", "ProductType")
                        .WithMany()
                        .HasForeignKey("ProductTypeId");

                    b.HasOne("VendingMachineAPI.Models.Transaction", null)
                        .WithMany("Products")
                        .HasForeignKey("TransactionId");

                    b.Navigation("ProductType");
                });

            modelBuilder.Entity("VendingMachineAPI.Models.Transaction", b =>
                {
                    b.HasOne("VendingMachineAPI.Models.CreditCardVerification", "CCVerification")
                        .WithMany()
                        .HasForeignKey("CCVerificationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CCVerification");
                });

            modelBuilder.Entity("VendingMachineAPI.Models.Transaction", b =>
                {
                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}
