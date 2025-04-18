﻿using Domain.Models.DBModels;
using Domain.Models.Primitives;
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
        public DbSet<SellerProductDetailsDBModel> SellerProductDetails { get; set; }
        public DbSet<PriceHistoryDBModel> PricesHistory { get; set; }
        public DbSet<ProductCharacteristicDBModel> ProductCharacteristics { get; set; }
        public DbSet<SellerDBModel> Sellers { get; set; }
        public DbSet<SellerRequestDBModel> SellerRequests { get; set; }
        public DbSet<ProductReferenceClickDBModel> ProductReferenceClicks { get; set; }
        public DbSet<ApplicationUserDBModel> Users { get; set; }
        public DbSet<ProductClicksDBModel> ProductClicks { get; set; }
        public DbSet<CharacteristicGroupDBModel> CharacteristicGroups { get; set; }
        public DbSet<CategoryCharacteristicGroupDBModel> CategoryCharacteristicGroups { get; set; }
        public DbSet<AuctionClickRateDBModel> AuctionClickRates { get; set; }
        public DbSet<FilterDBModel> Filters { get; set; }
        public DbSet<ProductGroupDBModel> ProductGroups { get; set; }
        public DbSet<ProductGroupTypeDBModel> ProductGroupTypes { get; set; }
        public DbSet<BaseProductDBModel> BaseProducts { get; set; }
        public DbSet<ColorDBModel> Colors { get; set; }

        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ProductGroupTypeDBModel
            modelBuilder.Entity<ProductGroupTypeDBModel>(entity =>
            {
                entity.HasIndex(pg => new { pg.Name })
                  .IsUnique();
            });

            // ProductGroupDBModel
            modelBuilder.Entity<ProductGroupDBModel>(entity =>
            {
                entity.HasIndex(pg => new { pg.Name})
                  .IsUnique();

                entity.HasOne(p => p.ProductGroupType)
                 .WithMany(c => c.ProductGroups)
                 .HasForeignKey(p => p.ProductGroupTypeId)
                 .OnDelete(DeleteBehavior.Restrict);

                entity.Property(p => p.Name)
                  .HasMaxLength(255);

                entity.Property(p => p.NormalizedName)
                  .HasMaxLength(255);

                entity.HasIndex(p => p.NormalizedName)
                    .HasDatabaseName("IX_ProductGroup_NormalizedName"); // index for search
            });

            // ColorDBModel
            modelBuilder.Entity<ColorDBModel>(entity =>
            {
            });


            // BaseProductDBModel
            modelBuilder.Entity<BaseProductDBModel>(entity =>
            {
                entity.Property(p => p.Title)
                    .HasMaxLength(255);

                entity.Property(p => p.NormalizedTitle)
                    .HasMaxLength(255);

                entity.HasIndex(p => p.NormalizedTitle)
                    .HasDatabaseName("IX_BaseProduct_NormalizedTitle"); // index for search

                entity.Property(p => p.Brand)
                    .HasMaxLength(255);

                entity.Property(c => c.AddedToDatabase)
                    .HasColumnType("DATETIME2(0)");

                entity.HasOne(p => p.Category)
                    .WithMany(c => c.BaseProducts)
                    .HasForeignKey(p => p.CategoryId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // ProductDBModel
            modelBuilder.Entity<ProductDBModel>(entity =>
            {
                entity.HasOne(p => p.BaseProduct)
                    .WithMany(c => c.Products)
                    .HasForeignKey(p => p.BaseProductId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(p => p.ProductGroup)
                    .WithMany(c => c.Products)
                    .HasForeignKey(p => p.ProductGroupId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.Property(p => p.ModelNumber)
                    .HasMaxLength(255);

                entity.Property(p => p.NormalizedModelNumber)
                    .HasMaxLength(255);

                entity.Property(p => p.GTIN)
                    .HasMaxLength(15);

                entity.Property(p => p.UPC)
                    .HasMaxLength(15);

                entity.Property(c => c.AddedToDatabase)
                    .HasColumnType("DATETIME2(0)");

                entity.HasOne(p => p.Color)
                    .WithMany(c => c.Products)
                    .HasForeignKey(p => p.ColorId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasIndex(p => new {p.Id, p.ProductGroupId, p.ColorId })
                    .IsUnique();

                entity.HasIndex(p => p.NormalizedModelNumber)
                    .HasDatabaseName("IX_Product_NormalizedModelNumber"); // index for search
            });

            // FilterDBModel
            modelBuilder.Entity<FilterDBModel>(entity =>
            {
                entity.Property(e => e.Title)
                    .HasMaxLength(100);

                entity.Property(e => e.ShortTitle)
                    .HasMaxLength(50);

                entity.Property(e => e.Description)
                    .HasMaxLength(1000);

                entity.HasOne(e => e.Characteristic)
                    .WithMany(c => c.Filter)
                    .HasForeignKey(e => e.CharacteristicId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.Property(e => e.Operator)
                    .IsRequired()
                    .HasMaxLength(10);
            });

            // AuctionClickRateDBModel
            modelBuilder.Entity<AuctionClickRateDBModel>(entity =>
            {
                entity.HasOne(e => e.Category)
                    .WithMany(p => p.AuctionClickRates)
                    .HasForeignKey(e => e.CategoryId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Seller)
                    .WithMany(p => p.AuctionClickRates)
                    .HasForeignKey(e => e.SellerId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.Property(e => e.ClickRate)
                    .HasColumnType("decimal(18,2)");
            });

            // CategoryCharacteristicGroupDBModel
            modelBuilder.Entity<CategoryCharacteristicGroupDBModel>(entity =>
            {
                entity.Property(cc => cc.GroupDisplayOrder)
                   .HasDefaultValue(1)
                   .HasAnnotation("CheckConstraint", "GroupDisplayOrder >= 1"); // Min value = 1

                entity.HasOne(cc => cc.Category)
                    .WithMany(c => c.CategoryCharacteristicGroups)
                    .HasForeignKey(c => c.CategoryId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(cc => cc.CharacteristicGroup)
                    .WithMany(c => c.CategoryCharacteristicGroups)
                    .HasForeignKey(c => c.CharacteristicGroupId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // CharacteristicGroupDBModel
            modelBuilder.Entity<CharacteristicGroupDBModel>(entity =>
            {
                entity.HasMany(cg => cg.Characteristics)
                    .WithOne(c => c.CharacteristicGroup)
                    .HasForeignKey(c => c.CharacteristicGroupId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(cg => cg.CategoryCharacteristicGroups)
                    .WithOne(cc => cc.CharacteristicGroup)
                    .HasForeignKey(c => c.CharacteristicGroupId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // CharacteristicDBModel
            modelBuilder.Entity<CharacteristicDBModel>(entity =>
            {
                entity.Property(c => c.DisplayOrder)
                   .HasDefaultValue(1)
                   .HasAnnotation("CheckConstraint", "DisplayOrder >= 1"); // Min value = 1

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

                entity.HasOne(c => c.CharacteristicGroup)
                   .WithMany(pc => pc.Characteristics)
                   .HasForeignKey(pc => pc.CharacteristicGroupId);
            });

            // ProductClicksDBModel
            modelBuilder.Entity<ProductClicksDBModel>(entity =>
            {
                entity.Property(c => c.ClickDate)
                    .HasColumnType("DATETIME2(0)");

                entity.HasIndex(p => new { p.ClickDate, p.ProductId })
                    .HasDatabaseName("IX_ProductClicks_ClickDate_ProductId")
                    .IncludeProperties(p => new { p.Id }); // for index count but not table data 

                entity.HasOne(c => c.Product)
                    .WithMany(c => c.ProductClicks)
                    .HasForeignKey(c => c.ProductId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // CategoryDBModel
            modelBuilder.Entity<CategoryDBModel>(entity =>
            {
                entity.Property(c => c.Title)
                    .HasMaxLength(255);

                entity.Property(c => c.ImageUrl)
                    .HasMaxLength(2083);

                entity.HasOne(c => c.ParentCategory)
                    .WithMany(c => c.Subcategories)
                    .HasForeignKey(c => c.ParentCategoryId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // CategoryCharacteristicDBModel
            modelBuilder.Entity<CategoryCharacteristicDBModel>(entity =>
            {
                entity.Ignore(p => p.Id);

                entity.HasKey(cc => new { cc.CategoryId, cc.CharacteristicId });

                entity.HasOne(cc => cc.Category)
                    .WithMany(c => c.CategoryCharacteristics)
                    .HasForeignKey(cc => cc.CategoryId);

                entity.HasOne(cc => cc.Characteristic)
                    .WithMany(ch => ch.CategoryCharacteristics)
                    .HasForeignKey(cc => cc.CharacteristicId);
            });

            // FeedbackDBModel
            modelBuilder.Entity<FeedbackDBModel>(entity =>
            {
                entity.HasOne(f => f.BaseProduct)
                    .WithMany(p => p.Feedbacks)
                    .HasForeignKey(f => f.BaseProductId);

                entity.HasOne(f => f.User)
                    .WithMany(u => u.Feedbacks)
                    .HasForeignKey(f => f.UserId);

                entity.Property(f => f.FeedbackText)
                    .HasMaxLength(1000);

                entity.Property(f => f.Rating)
                    .HasColumnType("int");

                entity.ToTable(t => t.HasCheckConstraint("CK_Rating_Range", "[Rating] BETWEEN 1 AND 5"));
            });

            // FeedbackImageDBModel
            modelBuilder.Entity<FeedbackImageDBModel>(entity =>
            {
                entity.Property(fi => fi.ImageUrl)
                    .IsRequired()
                    .HasMaxLength(2083);

                entity.HasOne(fi => fi.Feedback)
                    .WithMany(f => f.FeedbackImages)
                    .HasForeignKey(fi => fi.FeedbackId);
            });

            // InstructionDBModel
            modelBuilder.Entity<InstructionDBModel>(entity =>
            {
                entity.Property(fi => fi.InstructionUrl)
                    .HasMaxLength(2083);

                entity.HasOne(fi => fi.BaseProduct)
                    .WithMany(f => f.Instructions)
                    .HasForeignKey(fi => fi.BaseProductId);
            });

            // SellerProductDetailsDBModel
            modelBuilder.Entity<SellerProductDetailsDBModel>(entity =>
            {
                entity.Ignore(p => p.Id);

                entity.HasKey(p => new { p.ProductId, p.SellerId });

                entity.ToTable("SellerProductDetails");

                entity.Property(pr => pr.PriceValue)
                    .HasColumnType("decimal(18,2)");

                entity.Property(pr => pr.LastUpdated)
                    .HasColumnType("DATETIME2(0)");

                entity.Property(pr => pr.ProductStoreUrl)
                    .HasMaxLength(2083);

                entity.HasOne(pr => pr.Product)
                    .WithMany(p => p.SellerProductDetails)
                    .HasForeignKey(pr => pr.ProductId);

                entity.HasOne(pr => pr.Seller)
                    .WithMany(s => s.SellerProductDetails)
                    .HasForeignKey(pr => pr.SellerId);
            });

            // PriceHistoryDBModel
            modelBuilder.Entity<PriceHistoryDBModel>(entity =>
            {
                entity.Property(pr => pr.PriceValue)
                    .IsRequired()
                    .HasColumnType("decimal(18,2)");

                entity.Property(pr => pr.CreatedAt)
                    .HasColumnType("DATETIME2(0)");

                entity.Property(pr => pr.PriceDate)
                    .HasColumnType("DATETIME2(0)");

                entity.HasOne(pr => pr.Product)
                    .WithMany(p => p.PricesHistories)
                    .HasForeignKey(pr => pr.ProductId);

                entity.HasOne(pr => pr.Seller)
                    .WithMany(s => s.PriceHistories)
                    .HasForeignKey(pr => pr.SellerId);
            });

            // ProductCharacteristicDBModel
            modelBuilder.Entity<ProductCharacteristicDBModel>(entity =>
            {
                entity.HasOne(pc => pc.Product)
                    .WithMany(p => p.ProductCharacteristics)
                    .HasForeignKey(pc => pc.ProductId)
                    .IsRequired(false)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(pc => pc.Characteristic)
                    .WithMany(ch => ch.ProductCharacteristics)
                    .HasForeignKey(pc => pc.CharacteristicId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(pc => pc.BaseProduct)
                    .WithMany(p => p.ProductCharacteristics)
                    .HasForeignKey(pc => pc.BaseProductId)
                    .IsRequired(false)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.Property(e => e.ValueNumber)
                    .HasColumnType("decimal(18, 2)");

                // Require at least one product field to be filled
                entity.ToTable(tb => tb.HasCheckConstraint(
                    "CK_ProductCharacteristic_AtLeastOneProduct",
                    "(ProductId IS NOT NULL) OR (BaseProductId IS NOT NULL)"));

                // Require at least one value field to be filled
                entity.ToTable(tb => tb.HasCheckConstraint(
                    "CK_ProductCharacteristic_AtLeastOneValue",
                    "(ValueText IS NOT NULL) OR (ValueNumber IS NOT NULL) OR (ValueBoolean IS NOT NULL) OR (ValueDate IS NOT NULL)"));
            });

            // ProductImageDBModel
            modelBuilder.Entity<ProductImageDBModel>(entity =>
            {
                entity.Property(p => p.ImageUrl)
                    .IsRequired()
                    .HasMaxLength(2083);

                entity.HasOne(p => p.Product)
                    .WithMany(c => c.ProductImages)
                    .HasForeignKey(p => p.ProductId);
            });

            // ProductVideoDBModel
            modelBuilder.Entity<ProductVideoDBModel>(entity =>
            {
                entity.Property(p => p.VideoUrl)
                    .HasMaxLength(2083);

                entity.HasOne(p => p.BaseProduct)
                    .WithMany(c => c.ProductVideos)
                    .HasForeignKey(p => p.BaseProductId);
            });

            // RelatedCategoryDBModel
            modelBuilder.Entity<RelatedCategoryDBModel>(entity =>
            {
                entity.Ignore(p => p.Id);

                entity.HasKey(rc => new { rc.CategoryId, rc.RelatedCategoryId });

                entity.HasOne(rc => rc.Category)
                    .WithMany(c => c.RelatedCategories)
                    .HasForeignKey(rc => rc.CategoryId);

                entity.HasOne(rc => rc.RelatedCategoryItem)
                    .WithMany()
                    .HasForeignKey(rc => rc.RelatedCategoryId)
                    .OnDelete(DeleteBehavior.NoAction);

            });

            // ReviewDBModel
            modelBuilder.Entity<ReviewDBModel>(entity =>
            {
                entity.Property(p => p.ReviewUrl)
                    .HasMaxLength(2083);

                entity.HasOne(p => p.BaseProduct)
                    .WithMany(c => c.Reviews)
                    .HasForeignKey(p => p.BaseProductId);
            });

            // SellerDBModel
            modelBuilder.Entity<SellerDBModel>(entity =>
            {
                entity.Property(s => s.ApiKey)
                    .HasMaxLength(255);

                entity.Property(s => s.StoreName)
                    .HasMaxLength(255);

                entity.Property(s => s.LogoImageUrl)
                    .HasMaxLength(2083);

                entity.Property(s => s.WebsiteUrl)
                    .HasMaxLength(2083);

                entity.Property(s => s.AccountBalance)
                    .HasPrecision(18, 2);

                entity.HasOne(s => s.User)
                    .WithMany(u => u.Sellers)
                    .HasForeignKey(s => s.UserId);

                entity.HasMany(s => s.SellerProductDetails)
                    .WithOne(p => p.Seller)
                    .HasForeignKey(p => p.SellerId);

                entity.HasMany(s => s.PriceHistories)
                    .WithOne(ph => ph.Seller)
                    .HasForeignKey(ph => ph.SellerId);
            });

            // SellerRequestDBModel
            modelBuilder.Entity<SellerRequestDBModel>(entity =>
            {
                entity.Property(s => s.StoreName)
                    .HasMaxLength(255);

                entity.Property(s => s.WebsiteUrl)
                    .HasMaxLength(2083);

                entity.Property(s => s.ContactPerson)
                    .HasMaxLength(100);
                
                entity.Property(s => s.ContactPhone)
                    .HasMaxLength(20);

                entity.Property(s => s.CreatedAt)
                    .HasColumnType("DATETIME2(0)");

                entity.Property(s => s.ProcessedAt)
                    .HasColumnType("DATETIME2(0)");

                entity.HasOne(s => s.User)
                    .WithMany(u => u.SellerRequests)
                    .HasForeignKey(s => s.UserId);
            });

            // ApplicationUserDBModel
            modelBuilder.Entity<ApplicationUserDBModel>(entity =>
            {
                entity.HasMany(u => u.Sellers)
                    .WithOne(s => s.User)
                    .HasForeignKey(s => s.UserId);

                entity.HasMany(u => u.Feedbacks)
                    .WithOne(f => f.User)
                    .HasForeignKey(f => f.UserId);
            });

            // ProductReferenceClickDBModel. ClickTracking
            modelBuilder.Entity<ProductReferenceClickDBModel>(entity =>
            {
                entity.ToTable("ProductReferenceClicks");

                entity.HasOne(e => e.Product)
                      .WithMany(p => p.ProductSellerReferenceClicks)
                      .HasForeignKey(e => e.ProductId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Seller)
                      .WithMany(p => p.ProductSellerReferenceClicks)
                      .HasForeignKey(e => e.SellerId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.Property(e => e.UserIp)
                      .HasMaxLength(50);

                entity.Property(e => e.ClickRate)
                      .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.ClickedAt);
            });
        }
    }
}
