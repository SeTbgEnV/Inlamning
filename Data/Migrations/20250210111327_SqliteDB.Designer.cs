﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ViktorEngmanInlämning.Data;

#nullable disable

namespace MormorDagnysInlämning.Data.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20250210111327_SqliteDB")]
    partial class SqliteDB
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.0");

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClaimType")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("TEXT");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClaimType")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("RoleId")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Value")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("ViktorEngmanInlämning.Entities.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("AddressLine")
                        .HasColumnType("TEXT");

                    b.Property<int>("AddressTypeId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PostalAddressId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("AddressTypeId");

                    b.HasIndex("PostalAddressId");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("ViktorEngmanInlämning.Entities.AddressType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Value")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("AddressTypes");
                });

            modelBuilder.Entity("ViktorEngmanInlämning.Entities.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .HasColumnType("TEXT");

                    b.Property<string>("Phone")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("ViktorEngmanInlämning.Entities.CustomerAddress", b =>
                {
                    b.Property<int>("AddressId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("CustomerId")
                        .HasColumnType("INTEGER");

                    b.HasKey("AddressId", "CustomerId");

                    b.HasIndex("CustomerId");

                    b.ToTable("CustomerAddresses");
                });

            modelBuilder.Entity("ViktorEngmanInlämning.Entities.OrderItem", b =>
                {
                    b.Property<int>("ProductId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SalesOrderId")
                        .HasColumnType("INTEGER");

                    b.Property<double>("Price")
                        .HasColumnType("REAL");

                    b.Property<int>("Quantity")
                        .HasColumnType("INTEGER");

                    b.HasKey("ProductId", "SalesOrderId");

                    b.HasIndex("SalesOrderId");

                    b.ToTable("OrderItems");
                });

            modelBuilder.Entity("ViktorEngmanInlämning.Entities.PostalAddress", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("City")
                        .HasColumnType("TEXT");

                    b.Property<string>("PostalCode")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("PostalAddresses");
                });

            modelBuilder.Entity("ViktorEngmanInlämning.Entities.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("ImageURL")
                        .HasColumnType("TEXT");

                    b.Property<string>("ItemNumber")
                        .HasColumnType("TEXT");

                    b.Property<double>("KgPrice")
                        .HasColumnType("REAL");

                    b.Property<string>("ProductName")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("ViktorEngmanInlämning.Entities.SalesOrder", b =>
                {
                    b.Property<int>("SalesOrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("TEXT");

                    b.HasKey("SalesOrderId");

                    b.ToTable("SalesOrders");
                });

            modelBuilder.Entity("ViktorEngmanInlämning.Entities.Supplier", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("CompanyName")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Salespeople");
                });

            modelBuilder.Entity("ViktorEngmanInlämning.Entities.SupplierAddress", b =>
                {
                    b.Property<int>("AddressId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SupplierId")
                        .HasColumnType("INTEGER");

                    b.HasKey("AddressId", "SupplierId");

                    b.HasIndex("SupplierId");

                    b.ToTable("SupplierAddresses");
                });

            modelBuilder.Entity("ViktorEngmanInlämning.Entities.SupplierProduct", b =>
                {
                    b.Property<int>("ProductId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SupplierId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ItemNumber")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Price")
                        .HasColumnType("TEXT");

                    b.HasKey("ProductId", "SupplierId");

                    b.HasIndex("ProductId")
                        .IsUnique();

                    b.HasIndex("SupplierId")
                        .IsUnique();

                    b.ToTable("SupplierProducts");
                });

            modelBuilder.Entity("ViktorEngmanInlämning.Entities.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<string>("FirstName")
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .HasColumnType("TEXT");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("TEXT");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("TEXT");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("ViktorEngmanInlämning.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("ViktorEngmanInlämning.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ViktorEngmanInlämning.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("ViktorEngmanInlämning.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ViktorEngmanInlämning.Entities.Address", b =>
                {
                    b.HasOne("ViktorEngmanInlämning.Entities.AddressType", "AddressType")
                        .WithMany("Addresses")
                        .HasForeignKey("AddressTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ViktorEngmanInlämning.Entities.PostalAddress", "PostalAddress")
                        .WithMany("Addresses")
                        .HasForeignKey("PostalAddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AddressType");

                    b.Navigation("PostalAddress");
                });

            modelBuilder.Entity("ViktorEngmanInlämning.Entities.CustomerAddress", b =>
                {
                    b.HasOne("ViktorEngmanInlämning.Entities.Address", "Address")
                        .WithMany("CustomerAddresses")
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ViktorEngmanInlämning.Entities.Customer", "Customer")
                        .WithMany("CustomerAddresses")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Address");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("ViktorEngmanInlämning.Entities.OrderItem", b =>
                {
                    b.HasOne("ViktorEngmanInlämning.Entities.Product", "Product")
                        .WithMany("OrderItems")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ViktorEngmanInlämning.Entities.SalesOrder", "SalesOrder")
                        .WithMany("OrderItems")
                        .HasForeignKey("SalesOrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("SalesOrder");
                });

            modelBuilder.Entity("ViktorEngmanInlämning.Entities.SupplierAddress", b =>
                {
                    b.HasOne("ViktorEngmanInlämning.Entities.Address", "Address")
                        .WithMany("SupplierAddresses")
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ViktorEngmanInlämning.Entities.Supplier", "Supplier")
                        .WithMany("SupplierAddresses")
                        .HasForeignKey("SupplierId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Address");

                    b.Navigation("Supplier");
                });

            modelBuilder.Entity("ViktorEngmanInlämning.Entities.SupplierProduct", b =>
                {
                    b.HasOne("ViktorEngmanInlämning.Entities.Product", "Product")
                        .WithOne("SupplierProduct")
                        .HasForeignKey("ViktorEngmanInlämning.Entities.SupplierProduct", "ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ViktorEngmanInlämning.Entities.Supplier", "Supplier")
                        .WithOne("SupplierProducts")
                        .HasForeignKey("ViktorEngmanInlämning.Entities.SupplierProduct", "SupplierId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("Supplier");
                });

            modelBuilder.Entity("ViktorEngmanInlämning.Entities.Address", b =>
                {
                    b.Navigation("CustomerAddresses");

                    b.Navigation("SupplierAddresses");
                });

            modelBuilder.Entity("ViktorEngmanInlämning.Entities.AddressType", b =>
                {
                    b.Navigation("Addresses");
                });

            modelBuilder.Entity("ViktorEngmanInlämning.Entities.Customer", b =>
                {
                    b.Navigation("CustomerAddresses");
                });

            modelBuilder.Entity("ViktorEngmanInlämning.Entities.PostalAddress", b =>
                {
                    b.Navigation("Addresses");
                });

            modelBuilder.Entity("ViktorEngmanInlämning.Entities.Product", b =>
                {
                    b.Navigation("OrderItems");

                    b.Navigation("SupplierProduct");
                });

            modelBuilder.Entity("ViktorEngmanInlämning.Entities.SalesOrder", b =>
                {
                    b.Navigation("OrderItems");
                });

            modelBuilder.Entity("ViktorEngmanInlämning.Entities.Supplier", b =>
                {
                    b.Navigation("SupplierAddresses");

                    b.Navigation("SupplierProducts");
                });
#pragma warning restore 612, 618
        }
    }
}
