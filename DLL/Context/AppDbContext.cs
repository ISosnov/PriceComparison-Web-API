﻿using Domain.Models.DBModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DLL.Context
{
    public class AppDbContext : IdentityDbContext<ApplicationUserDBModel, IdentityRole<int>, int>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<CategoryDBModel> Categories { get; set; }
        public DbSet<CharacteristicDBModel> Characteristics { get; set; }
        public DbSet<CategoryCharacteristicDBModel> CategoryCharacteristics { get; set; }
        public DbSet<ProductDBModel> Products { get; set; }
        public DbSet<ProductImageDBModel> ProductImages { get; set; }
        public DbSet<FeedbackDBModel> Feedbacks { get; set; }
        public DbSet<FeedbackImageDBModel> FeedbackImages { get; set; }
        public DbSet<ProductVideoDBModel> ProductVideos { get; set; }
        public DbSet<ReviewDBModel> Reviews { get; set; }
        public DbSet<InstructionDBModel> Instructions { get; set; }
        public DbSet<RelatedCategoryDBModel> RelatedCategories { get; set; }
        public DbSet<PriceDBModel> Prices { get; set; }
        public DbSet<PriceHistoryDBModel> PricesHistory { get; set; }
        public DbSet<ProductCharacteristicDBModel> ProductCharacteristics { get; set; }
        public DbSet<SellerDBModel> Sellers { get; set; }
        public DbSet<ApplicationUserDBModel> Users { get; set; }
        public DbSet<RoleDBModel> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Settings for the CategoryDBModel
            modelBuilder.Entity<CategoryDBModel>(entity =>
            {
                entity.Property(c => c.Title)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(c => c.ImageUrl)
                    .HasMaxLength(2083);

                entity.HasOne(c => c.ParentCategory)
                    .WithMany(c => c.Subcategories)
                    .HasForeignKey(c => c.ParentCategoryId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Settings for the CategoryCharacteristicDBModel
            modelBuilder.Entity<CategoryCharacteristicDBModel>(entity =>
            {
                entity.HasKey(cc => new { cc.CategoryId, cc.CharacteristicId });

                entity.HasOne(cc => cc.Category)
                    .WithMany(c => c.CategoryCharacteristics)
                    .HasForeignKey(cc => cc.CategoryId);

                entity.HasOne(cc => cc.Characteristic)
                    .WithMany(ch => ch.CategoryCharacteristics)
                    .HasForeignKey(cc => cc.CharacteristicId);
            });

            // Settings for the CharacteristicDBModel
            modelBuilder.Entity<CharacteristicDBModel>(entity =>
            {
                entity.Property(c => c.Title)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(c => c.DataType)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(c => c.Unit)
                    .HasMaxLength(50);

                entity.HasMany(c => c.CategoryCharacteristics)
                    .WithOne(cc => cc.Characteristic)
                    .HasForeignKey(cc => cc.CharacteristicId);

                entity.HasMany(c => c.ProductCharacteristics)
                    .WithOne(pc => pc.Characteristic)
                    .HasForeignKey(pc => pc.CharacteristicId);
            });

            // Settings for the FeedbackDBModel
            modelBuilder.Entity<FeedbackDBModel>(entity =>
            {
                entity.HasOne(f => f.Product)
                    .WithMany(p => p.Feedbacks)
                    .HasForeignKey(f => f.ProductId);

                entity.HasOne(f => f.User)
                    .WithMany(u => u.Feedbacks)
                    .HasForeignKey(f => f.UserId);

                entity.Property(f => f.FeedbackText)
                    .IsRequired()
                    .HasMaxLength(1000);

                entity.Property(f => f.CreatedAt)
                    .IsRequired();

                entity.Property(f => f.Rating)
                    .IsRequired()
                    .HasColumnType("int");

                entity.ToTable(t => t.HasCheckConstraint("CK_Rating_Range", "[Rating] BETWEEN 1 AND 5"));
            });

            // Settings for the FeedbackImageDBModel
            modelBuilder.Entity<FeedbackImageDBModel>(entity =>
            {
                entity.Property(fi => fi.ImageUrl)
                    .IsRequired()
                    .HasMaxLength(2083);

                entity.HasOne(fi => fi.Feedback)
                    .WithMany(f => f.FeedbackImages)
                    .HasForeignKey(fi => fi.FeedbackId);
            });

            // Settings for the InstructionDBModel
            modelBuilder.Entity<InstructionDBModel>(entity =>
            {
                entity.Property(fi => fi.InstructionUrl)
                    .IsRequired()
                    .HasMaxLength(2083);

                entity.HasOne(fi => fi.Product)
                    .WithMany(f => f.Instructions)
                    .HasForeignKey(fi => fi.ProductId);
            });

            // Settings for the PriceDBModel
            modelBuilder.Entity<PriceDBModel>(entity =>
            {
                entity.HasKey(p => new { p.ProductId, p.SellerId });

                entity.Property(pr => pr.PriceValue)
                    .IsRequired()
                    .HasColumnType("decimal(18,2)");

                entity.Property(pr => pr.LastUpdated)
                    .IsRequired();

                entity.HasOne(pr => pr.Product)
                    .WithMany(p => p.Prices)
                    .HasForeignKey(pr => pr.ProductId);

                entity.HasOne(pr => pr.Seller)
                    .WithMany(s => s.Prices)
                    .HasForeignKey(pr => pr.SellerId);
            });

            // Settings for the PriceHistoryDBModel
            modelBuilder.Entity<PriceHistoryDBModel>(entity =>
            {
                entity.Property(pr => pr.PriceValue)
                    .IsRequired()
                    .HasColumnType("decimal(18,2)");

                entity.Property(pr => pr.CreatedAt)
                    .IsRequired();

                entity.HasOne(pr => pr.Product)
                    .WithMany(p => p.PricesHistory)
                    .HasForeignKey(pr => pr.ProductId);

                entity.HasOne(pr => pr.Seller)
                    .WithMany()
                    .HasForeignKey(pr => pr.SellerId);
            });

            // Settings for the ProductDBModel
            modelBuilder.Entity<ProductDBModel>(entity =>
            {
                entity.Property(p => p.Title)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.HasOne(p => p.Category)
                    .WithMany(c => c.Products)
                    .HasForeignKey(p => p.CategoryId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Settings for the ProductCharacteristicDBModel
            modelBuilder.Entity<ProductCharacteristicDBModel>(entity =>
            {
                entity.HasKey(pc => new { pc.ProductId, pc.CharacteristicId });

                entity.HasOne(pc => pc.Product)
                    .WithMany(p => p.ProductCharacteristics)
                    .HasForeignKey(pc => pc.ProductId);

                entity.HasOne(pc => pc.Characteristic)
                    .WithMany(ch => ch.ProductCharacteristics)
                    .HasForeignKey(pc => pc.CharacteristicId);

                entity.Property(e => e.ValueNumber)
                    .HasColumnType("decimal(18, 2)");
            });

            // Settings for the ProductImageDBModel
            modelBuilder.Entity<ProductImageDBModel>(entity =>
            {
                entity.Property(p => p.ImageUrl)
                    .IsRequired()
                    .HasMaxLength(2083);

                entity.HasOne(p => p.Product)
                    .WithMany(c => c.ProductImages)
                    .HasForeignKey(p => p.ProductId);
            });

            // Settings for the ProductVideoDBModel
            modelBuilder.Entity<ProductVideoDBModel>(entity =>
            {
                entity.Property(p => p.VideoUrl)
                    .IsRequired()
                    .HasMaxLength(2083);

                entity.HasOne(p => p.Product)
                    .WithMany(c => c.ProductVideos)
                    .HasForeignKey(p => p.ProductId);
            });

            // Settings for the RelatedCategoryDBModel
            modelBuilder.Entity<RelatedCategoryDBModel>(entity =>
            {
                entity.HasKey(rc => new { rc.CategoryId, rc.RelatedCategoryId });

                entity.HasOne(rc => rc.Category)
                    .WithMany(c => c.RelatedCategories)
                    .HasForeignKey(rc => rc.CategoryId);

                entity.HasOne(rc => rc.RelatedCategoryItem)
                    .WithMany()
                    .HasForeignKey(rc => rc.RelatedCategoryId)
                    .OnDelete(DeleteBehavior.NoAction);

            });

            // Settings for the ReviewDBModel
            modelBuilder.Entity<ReviewDBModel>(entity =>
            {
                entity.Property(p => p.ReviewUrl)
                    .IsRequired()
                    .HasMaxLength(2083);

                entity.HasOne(p => p.Product)
                    .WithMany(c => c.Reviews)
                    .HasForeignKey(p => p.ProductId);
            });

            // Settings for the RoleDBModel
            modelBuilder.Entity<RoleDBModel>(entity =>
            {
                entity.Property(r => r.Title)
                    .IsRequired()
                    .HasMaxLength(50);

                //entity.HasMany(r => r.Users)
                //    .WithOne(u => u.Role)
                //    .HasForeignKey(u => u.RoleId);
            });

            // Settings for the SellerDBModel
            modelBuilder.Entity<SellerDBModel>(entity =>
            {
                entity.Property(s => s.ApiKey)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(s => s.LogoImageUrl)
                    .HasMaxLength(2083);

                entity.Property(s => s.WebsiteUrl)
                    .HasMaxLength(2083);

                entity.HasOne(s => s.User)
                    .WithMany(u => u.Sellers)
                    .HasForeignKey(s => s.UserId);

                entity.HasMany(s => s.Prices)
                    .WithOne(p => p.Seller)
                    .HasForeignKey(p => p.SellerId);

                entity.HasMany(s => s.PriceHistories)
                    .WithOne(ph => ph.Seller)
                    .HasForeignKey(ph => ph.SellerId);
            });

            // Settings for the ApplicationUserDBModel
            modelBuilder.Entity<ApplicationUserDBModel>(entity =>
            {
                entity.HasMany(u => u.Sellers)
                    .WithOne(s => s.User)
                    .HasForeignKey(s => s.UserId);

                entity.HasMany(u => u.Feedbacks)
                    .WithOne(f => f.User)
                    .HasForeignKey(f => f.UserId);
            });
        }

    }
}
