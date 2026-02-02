using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using MyShop.Infrastructure.EF.Models;

namespace MyShop.Infrastructure.EF.Data;

public partial class MyShopContext : DbContext
{
    public MyShopContext(DbContextOptions<MyShopContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<AccountRole> AccountRoles { get; set; }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<CartItem> CartItems { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<FavoriteStore> FavoriteStores { get; set; }

    public virtual DbSet<HotStore> HotStores { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderItem> OrderItems { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductImage> ProductImages { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Store> Stores { get; set; }

    public virtual DbSet<StoreAdvertisement> StoreAdvertisements { get; set; }

    public virtual DbSet<SystemAnnouncement> SystemAnnouncements { get; set; }

    public virtual DbSet<VendorProfile> VendorProfiles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.ToTable("Account");

            entity.HasIndex(e => e.Username, "UQ_Account_Username").IsUnique();

            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())", "DF_Account_CreatedAt");
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.Email).HasMaxLength(254);
            entity.Property(e => e.LastLoginAt).HasPrecision(0);
            entity.Property(e => e.PasswordHash).HasMaxLength(200);
            entity.Property(e => e.UpdatedAt).HasPrecision(0);
            entity.Property(e => e.UpdatedBy).HasMaxLength(50);
            entity.Property(e => e.Username).HasMaxLength(50);
        });

        modelBuilder.Entity<AccountRole>(entity =>
        {
            entity.HasKey(e => new { e.AccountId, e.RoleId });

            entity.ToTable("AccountRole");

            entity.HasIndex(e => e.RoleId, "IX_AccountRole_RoleId");

            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())", "DF_AccountRole_CreatedAt");
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.UpdatedAt).HasPrecision(0);
            entity.Property(e => e.UpdatedBy).HasMaxLength(50);

            entity.HasOne(d => d.Account).WithMany(p => p.AccountRoles)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AccountRole_Account");

            entity.HasOne(d => d.Role).WithMany(p => p.AccountRoles)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AccountRole_Role");
        });

        modelBuilder.Entity<Cart>(entity =>
        {
            entity.ToTable("Cart");

            entity.HasIndex(e => e.AccountId, "IX_Cart_AccountId");

            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())", "DF_Cart_CreatedAt");
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.UpdatedAt).HasPrecision(0);
            entity.Property(e => e.UpdatedBy).HasMaxLength(50);

            entity.HasOne(d => d.Account).WithMany(p => p.Carts)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Cart_Account");
        });

        modelBuilder.Entity<CartItem>(entity =>
        {
            entity.ToTable("CartItem");

            entity.HasIndex(e => e.CartId, "IX_CartItem_CartId");

            entity.HasIndex(e => e.ProductId, "IX_CartItem_ProductId");

            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())", "DF_CartItem_CreatedAt");
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.UpdatedAt).HasPrecision(0);
            entity.Property(e => e.UpdatedBy).HasMaxLength(50);

            entity.HasOne(d => d.Cart).WithMany(p => p.CartItems)
                .HasForeignKey(d => d.CartId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CartItem_Cart");

            entity.HasOne(d => d.Product).WithMany(p => p.CartItems)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CartItem_Product");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.ToTable("Category");

            entity.HasIndex(e => e.ParentCategoryId, "IX_Category_ParentCategoryId");

            entity.HasIndex(e => e.StoreId, "IX_Category_StoreId");

            entity.Property(e => e.CategoryName).HasMaxLength(100);
            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())", "DF_Category_CreatedAt");
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.UpdatedAt).HasPrecision(0);
            entity.Property(e => e.UpdatedBy).HasMaxLength(50);

            entity.HasOne(d => d.ParentCategory).WithMany(p => p.InverseParentCategory)
                .HasForeignKey(d => d.ParentCategoryId)
                .HasConstraintName("FK_Category_Parent");

            entity.HasOne(d => d.Store).WithMany(p => p.Categories)
                .HasForeignKey(d => d.StoreId)
                .HasConstraintName("FK_Category_Store");
        });

        modelBuilder.Entity<FavoriteStore>(entity =>
        {
            entity.HasKey(e => new { e.AccountId, e.StoreId });

            entity.ToTable("FavoriteStore");

            entity.HasIndex(e => e.StoreId, "IX_FavoriteStore_StoreId");

            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())", "DF_FavoriteStore_CreatedAt");
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.UpdatedAt).HasPrecision(0);
            entity.Property(e => e.UpdatedBy).HasMaxLength(50);

            entity.HasOne(d => d.Account).WithMany(p => p.FavoriteStores)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FavoriteStore_Account");

            entity.HasOne(d => d.Store).WithMany(p => p.FavoriteStores)
                .HasForeignKey(d => d.StoreId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FavoriteStore_Store");
        });

        modelBuilder.Entity<HotStore>(entity =>
        {
            entity.HasKey(e => e.StoreId);

            entity.ToTable("HotStore");

            entity.Property(e => e.StoreId).ValueGeneratedNever();
            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())", "DF_HotStore_CreatedAt");
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.UpdatedAt).HasPrecision(0);
            entity.Property(e => e.UpdatedBy).HasMaxLength(50);

            entity.HasOne(d => d.Store).WithOne(p => p.HotStore)
                .HasForeignKey<HotStore>(d => d.StoreId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HotStore_Store");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.ToTable("Order");

            entity.HasIndex(e => e.BuyerAccountId, "IX_Order_BuyerAccountId");

            entity.HasIndex(e => e.StoreId, "IX_Order_StoreId");

            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())", "DF_Order_CreatedAt");
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.UpdatedAt).HasPrecision(0);
            entity.Property(e => e.UpdatedBy).HasMaxLength(50);

            entity.HasOne(d => d.BuyerAccount).WithMany(p => p.Orders)
                .HasForeignKey(d => d.BuyerAccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Order_BuyerAccount");

            entity.HasOne(d => d.Store).WithMany(p => p.Orders)
                .HasForeignKey(d => d.StoreId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Order_Store");
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.ToTable("OrderItem");

            entity.HasIndex(e => e.OrderId, "IX_OrderItem_OrderId");

            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())", "DF_OrderItem_CreatedAt");
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.UnitPrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.UpdatedAt).HasPrecision(0);
            entity.Property(e => e.UpdatedBy).HasMaxLength(50);

            entity.HasOne(d => d.Order).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderItem_Order");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderItem_Product");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("Product");

            entity.HasIndex(e => e.CategoryId, "IX_Product_CategoryId");

            entity.HasIndex(e => e.StoreId, "IX_Product_StoreId");

            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())", "DF_Product_CreatedAt");
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ProductName).HasMaxLength(200);
            entity.Property(e => e.UpdatedAt).HasPrecision(0);
            entity.Property(e => e.UpdatedBy).HasMaxLength(50);

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Product_Category");

            entity.HasOne(d => d.Store).WithMany(p => p.Products)
                .HasForeignKey(d => d.StoreId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Product_Store");
        });

        modelBuilder.Entity<ProductImage>(entity =>
        {
            entity.HasKey(e => e.ImageId);

            entity.ToTable("ProductImage");

            entity.HasIndex(e => e.ProductId, "IX_ProductImage_ProductId");

            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())", "DF_ProductImage_CreatedAt");
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.ImageUrl).HasMaxLength(500);
            entity.Property(e => e.UpdatedAt).HasPrecision(0);
            entity.Property(e => e.UpdatedBy).HasMaxLength(50);

            entity.HasOne(d => d.Product).WithMany(p => p.ProductImages)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductImage_Product");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable("Role");

            entity.HasIndex(e => e.RoleCode, "UQ_Role_RoleCode").IsUnique();

            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())", "DF_Role_CreatedAt");
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.RoleCode).HasMaxLength(30);
            entity.Property(e => e.RoleName).HasMaxLength(50);
            entity.Property(e => e.UpdatedAt).HasPrecision(0);
            entity.Property(e => e.UpdatedBy).HasMaxLength(50);
        });

        modelBuilder.Entity<Store>(entity =>
        {
            entity.ToTable("Store");

            entity.HasIndex(e => e.VendorId, "IX_Store_VendorId");

            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())", "DF_Store_CreatedAt");
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.StoreName).HasMaxLength(100);
            entity.Property(e => e.UpdatedAt).HasPrecision(0);
            entity.Property(e => e.UpdatedBy).HasMaxLength(50);

            entity.HasOne(d => d.Vendor).WithMany(p => p.Stores)
                .HasForeignKey(d => d.VendorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Store_VendorProfile");
        });

        modelBuilder.Entity<StoreAdvertisement>(entity =>
        {
            entity.HasKey(e => e.AdId);

            entity.ToTable("StoreAdvertisement");

            entity.HasIndex(e => e.StoreId, "IX_StoreAdvertisement_StoreId");

            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())", "DF_StoreAdvertisement_CreatedAt");
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.EndAt).HasPrecision(0);
            entity.Property(e => e.ImageUrl).HasMaxLength(500);
            entity.Property(e => e.StartAt).HasPrecision(0);
            entity.Property(e => e.UpdatedAt).HasPrecision(0);
            entity.Property(e => e.UpdatedBy).HasMaxLength(50);

            entity.HasOne(d => d.Store).WithMany(p => p.StoreAdvertisements)
                .HasForeignKey(d => d.StoreId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StoreAdvertisement_Store");
        });

        modelBuilder.Entity<SystemAnnouncement>(entity =>
        {
            entity.HasKey(e => e.AnnouncementId);

            entity.ToTable("SystemAnnouncement");

            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())", "DF_SystemAnnouncement_CreatedAt");
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.PublishAt).HasPrecision(0);
            entity.Property(e => e.Title).HasMaxLength(200);
            entity.Property(e => e.UpdatedAt).HasPrecision(0);
            entity.Property(e => e.UpdatedBy).HasMaxLength(50);
        });

        modelBuilder.Entity<VendorProfile>(entity =>
        {
            entity.HasKey(e => e.VendorId);

            entity.ToTable("VendorProfile");

            entity.HasIndex(e => e.AccountId, "UQ_VendorProfile_Account").IsUnique();

            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())", "DF_VendorProfile_CreatedAt");
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.ReviewComment).HasMaxLength(500);
            entity.Property(e => e.ReviewedAt).HasPrecision(0);
            entity.Property(e => e.UpdatedAt).HasPrecision(0);
            entity.Property(e => e.UpdatedBy).HasMaxLength(50);

            entity.HasOne(d => d.Account).WithOne(p => p.VendorProfile)
                .HasForeignKey<VendorProfile>(d => d.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_VendorProfile_Account");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
