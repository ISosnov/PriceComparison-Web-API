﻿// <auto-generated />
using System;
using DLL.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DLL.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Domain.Models.DBModels.CategoryCharacteristicDBModel", b =>
                {
                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<int>("CharacteristicId")
                        .HasColumnType("int");

                    b.HasKey("CategoryId", "CharacteristicId");

                    b.HasIndex("CharacteristicId");

                    b.ToTable("CategoryCharacteristics");
                });

            modelBuilder.Entity("Domain.Models.DBModels.CategoryDBModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ImageUrl")
                        .HasMaxLength(2083)
                        .HasColumnType("nvarchar(2083)");

                    b.Property<int?>("ParentCategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("ParentCategoryId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Domain.Models.DBModels.CharacteristicDBModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("DataType")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Unit")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Characteristics");
                });

            modelBuilder.Entity("Domain.Models.DBModels.FeedbackDBModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("FeedbackText")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex("UserId");

                    b.ToTable("Feedbacks");
                });

            modelBuilder.Entity("Domain.Models.DBModels.FeedbackImageDBModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("FeedbackId")
                        .HasColumnType("int");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasMaxLength(2083)
                        .HasColumnType("nvarchar(2083)");

                    b.HasKey("Id");

                    b.HasIndex("FeedbackId");

                    b.ToTable("FeedbackImages");
                });

            modelBuilder.Entity("Domain.Models.DBModels.InstructionDBModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("InstructionUrl")
                        .IsRequired()
                        .HasMaxLength(2083)
                        .HasColumnType("nvarchar(2083)");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("Instructions");
                });

            modelBuilder.Entity("Domain.Models.DBModels.PriceDBModel", b =>
                {
                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("SellerId")
                        .HasColumnType("int");

                    b.Property<DateTime>("LastUpdated")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("PriceValue")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("ProductId", "SellerId");

                    b.HasIndex("SellerId");

                    b.ToTable("Prices");
                });

            modelBuilder.Entity("Domain.Models.DBModels.PriceHistoryDBModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("PriceValue")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("ProductDBModelId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("SellerId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProductDBModelId");

                    b.HasIndex("ProductId");

                    b.HasIndex("SellerId");

                    b.ToTable("PricesHistory");
                });

            modelBuilder.Entity("Domain.Models.DBModels.ProductCharacteristicDBModel", b =>
                {
                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("CharacteristicId")
                        .HasColumnType("int");

                    b.Property<bool?>("ValueBoolean")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("ValueDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal?>("ValueNumber")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<string>("ValueText")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ProductId", "CharacteristicId");

                    b.HasIndex("CharacteristicId");

                    b.ToTable("ProductCharacteristics");
                });

            modelBuilder.Entity("Domain.Models.DBModels.ProductDBModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Domain.Models.DBModels.ProductImageDBModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasMaxLength(2083)
                        .HasColumnType("nvarchar(2083)");

                    b.Property<bool>("IsPrimary")
                        .HasColumnType("bit");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductImages");
                });

            modelBuilder.Entity("Domain.Models.DBModels.ProductVideoDBModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<string>("VideoUrl")
                        .IsRequired()
                        .HasMaxLength(2083)
                        .HasColumnType("nvarchar(2083)");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductVideos");
                });

            modelBuilder.Entity("Domain.Models.DBModels.RelatedCategoryDBModel", b =>
                {
                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<int>("RelatedCategoryId")
                        .HasColumnType("int");

                    b.HasKey("CategoryId", "RelatedCategoryId");

                    b.HasIndex("RelatedCategoryId");

                    b.ToTable("RelatedCategories");
                });

            modelBuilder.Entity("Domain.Models.DBModels.ReviewDBModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<string>("ReviewUrl")
                        .IsRequired()
                        .HasMaxLength(2083)
                        .HasColumnType("nvarchar(2083)");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("Domain.Models.DBModels.RoleDBModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Domain.Models.DBModels.SellerDBModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ApiKey")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("LogoImageUrl")
                        .HasMaxLength(2083)
                        .HasColumnType("nvarchar(2083)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("WebsiteUrl")
                        .HasMaxLength(2083)
                        .HasColumnType("nvarchar(2083)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Sellers");
                });

            modelBuilder.Entity("Domain.Models.DBModels.UserDBModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Domain.Models.DBModels.CategoryCharacteristicDBModel", b =>
                {
                    b.HasOne("Domain.Models.DBModels.CategoryDBModel", "Category")
                        .WithMany("CategoryCharacteristics")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Models.DBModels.CharacteristicDBModel", "Characteristic")
                        .WithMany("CategoryCharacteristics")
                        .HasForeignKey("CharacteristicId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Characteristic");
                });

            modelBuilder.Entity("Domain.Models.DBModels.CategoryDBModel", b =>
                {
                    b.HasOne("Domain.Models.DBModels.CategoryDBModel", "ParentCategory")
                        .WithMany("Subcategories")
                        .HasForeignKey("ParentCategoryId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("ParentCategory");
                });

            modelBuilder.Entity("Domain.Models.DBModels.FeedbackDBModel", b =>
                {
                    b.HasOne("Domain.Models.DBModels.ProductDBModel", "Product")
                        .WithMany("Feedbacks")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Models.DBModels.UserDBModel", "User")
                        .WithMany("Feedbacks")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Models.DBModels.FeedbackImageDBModel", b =>
                {
                    b.HasOne("Domain.Models.DBModels.FeedbackDBModel", "Feedback")
                        .WithMany("FeedbackImages")
                        .HasForeignKey("FeedbackId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Feedback");
                });

            modelBuilder.Entity("Domain.Models.DBModels.InstructionDBModel", b =>
                {
                    b.HasOne("Domain.Models.DBModels.ProductDBModel", "Product")
                        .WithMany("Instructions")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Domain.Models.DBModels.PriceDBModel", b =>
                {
                    b.HasOne("Domain.Models.DBModels.ProductDBModel", "Product")
                        .WithMany("Prices")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Models.DBModels.SellerDBModel", "Seller")
                        .WithMany("Prices")
                        .HasForeignKey("SellerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("Seller");
                });

            modelBuilder.Entity("Domain.Models.DBModels.PriceHistoryDBModel", b =>
                {
                    b.HasOne("Domain.Models.DBModels.ProductDBModel", null)
                        .WithMany("PricesHistory")
                        .HasForeignKey("ProductDBModelId");

                    b.HasOne("Domain.Models.DBModels.ProductDBModel", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Models.DBModels.SellerDBModel", "Seller")
                        .WithMany("PriceHistories")
                        .HasForeignKey("SellerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("Seller");
                });

            modelBuilder.Entity("Domain.Models.DBModels.ProductCharacteristicDBModel", b =>
                {
                    b.HasOne("Domain.Models.DBModels.CharacteristicDBModel", "Characteristic")
                        .WithMany("ProductCharacteristics")
                        .HasForeignKey("CharacteristicId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Models.DBModels.ProductDBModel", "Product")
                        .WithMany("ProductCharacteristics")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Characteristic");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Domain.Models.DBModels.ProductDBModel", b =>
                {
                    b.HasOne("Domain.Models.DBModels.CategoryDBModel", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Domain.Models.DBModels.ProductImageDBModel", b =>
                {
                    b.HasOne("Domain.Models.DBModels.ProductDBModel", "Product")
                        .WithMany("ProductImages")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Domain.Models.DBModels.ProductVideoDBModel", b =>
                {
                    b.HasOne("Domain.Models.DBModels.ProductDBModel", "Product")
                        .WithMany("ProductVideos")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Domain.Models.DBModels.RelatedCategoryDBModel", b =>
                {
                    b.HasOne("Domain.Models.DBModels.CategoryDBModel", "Category")
                        .WithMany("RelatedCategories")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Models.DBModels.CategoryDBModel", "RelatedCategoryItem")
                        .WithMany()
                        .HasForeignKey("RelatedCategoryId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("RelatedCategoryItem");
                });

            modelBuilder.Entity("Domain.Models.DBModels.ReviewDBModel", b =>
                {
                    b.HasOne("Domain.Models.DBModels.ProductDBModel", "Product")
                        .WithMany("Reviews")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Domain.Models.DBModels.SellerDBModel", b =>
                {
                    b.HasOne("Domain.Models.DBModels.UserDBModel", "User")
                        .WithMany("Sellers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Models.DBModels.UserDBModel", b =>
                {
                    b.HasOne("Domain.Models.DBModels.RoleDBModel", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Domain.Models.DBModels.CategoryDBModel", b =>
                {
                    b.Navigation("CategoryCharacteristics");

                    b.Navigation("Products");

                    b.Navigation("RelatedCategories");

                    b.Navigation("Subcategories");
                });

            modelBuilder.Entity("Domain.Models.DBModels.CharacteristicDBModel", b =>
                {
                    b.Navigation("CategoryCharacteristics");

                    b.Navigation("ProductCharacteristics");
                });

            modelBuilder.Entity("Domain.Models.DBModels.FeedbackDBModel", b =>
                {
                    b.Navigation("FeedbackImages");
                });

            modelBuilder.Entity("Domain.Models.DBModels.ProductDBModel", b =>
                {
                    b.Navigation("Feedbacks");

                    b.Navigation("Instructions");

                    b.Navigation("Prices");

                    b.Navigation("PricesHistory");

                    b.Navigation("ProductCharacteristics");

                    b.Navigation("ProductImages");

                    b.Navigation("ProductVideos");

                    b.Navigation("Reviews");
                });

            modelBuilder.Entity("Domain.Models.DBModels.RoleDBModel", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("Domain.Models.DBModels.SellerDBModel", b =>
                {
                    b.Navigation("PriceHistories");

                    b.Navigation("Prices");
                });

            modelBuilder.Entity("Domain.Models.DBModels.UserDBModel", b =>
                {
                    b.Navigation("Feedbacks");

                    b.Navigation("Sellers");
                });
#pragma warning restore 612, 618
        }
    }
}
