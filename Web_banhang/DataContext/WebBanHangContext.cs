using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Web_banhang.Data;
using Web_banhang.Models;

namespace Web_banhang.DataContext;

public partial class WebBanHangContext : DbContext
{
    public WebBanHangContext()
    {
    }

    public WebBanHangContext(DbContextOptions<WebBanHangContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AspNetRole> AspNetRoles { get; set; }

    public virtual DbSet<AspNetUser> AspNetUsers { get; set; }

    public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }

    public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }

    public virtual DbSet<TbAdv> TbAdvs { get; set; }

    public virtual DbSet<TbCategory> TbCategories { get; set; }

    public virtual DbSet<TbContact> TbContacts { get; set; }

    public virtual DbSet<TbNews> TbNews { get; set; }

    public virtual DbSet<TbOrder> TbOrders { get; set; }

    public virtual DbSet<TbOrderDetail> TbOrderDetails { get; set; }

    public virtual DbSet<TbPost> TbPosts { get; set; }

    public virtual DbSet<TbProduct> TbProducts { get; set; }

    public virtual DbSet<TbProductCategory> TbProductCategories { get; set; }

    public virtual DbSet<TbProductImage> TbProductImages { get; set; }

    public virtual DbSet<TbSubscribe> TbSubscribes { get; set; }

    public virtual DbSet<TbSystemSetting> TbSystemSettings { get; set; }

    public virtual DbSet<ThongKe> ThongKes { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-7MRL7G7\\SQLEXPRESS;Initial Catalog=Web_BanHang;Integrated Security=True; TrustServerCertificate=True; User ID=sa;Password=1234");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AspNetRole>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.AspNetRoles");
        });

        modelBuilder.Entity<AspNetUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.AspNetUsers");

            entity.HasMany(d => d.Roles).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "AspNetUserRole",
                    r => r.HasOne<AspNetRole>().WithMany()
                        .HasForeignKey("RoleId")
                        .HasConstraintName("FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId"),
                    l => l.HasOne<AspNetUser>().WithMany()
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId").HasName("PK_dbo.AspNetUserRoles");
                        j.ToTable("AspNetUserRoles");
                        j.IndexerProperty<string>("UserId").HasMaxLength(128);
                        j.IndexerProperty<string>("RoleId").HasMaxLength(128);
                    });
        });

        modelBuilder.Entity<AspNetUserClaim>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.AspNetUserClaims");

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserClaims).HasConstraintName("FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId");
        });

        modelBuilder.Entity<AspNetUserLogin>(entity =>
        {
            entity.HasKey(e => new { e.LoginProvider, e.ProviderKey, e.UserId }).HasName("PK_dbo.AspNetUserLogins");

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserLogins).HasConstraintName("FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId");
        });

        modelBuilder.Entity<TbAdv>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.tb_Adv");
        });

        modelBuilder.Entity<TbCategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.tb_Category");
        });

        modelBuilder.Entity<TbContact>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.tb_Contact");
        });

        modelBuilder.Entity<TbNews>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.tb_News");

            entity.HasOne(d => d.Category).WithMany(p => p.TbNews).HasConstraintName("FK_dbo.tb_News_dbo.tb_Category_CategoryId");
        });

        modelBuilder.Entity<TbOrder>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.tb_Order");
        });

        modelBuilder.Entity<TbOrderDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.tb_OrderDetail");

            entity.HasOne(d => d.Order).WithMany(p => p.TbOrderDetails).HasConstraintName("FK_dbo.tb_OrderDetail_dbo.tb_Order_OrderId");

            entity.HasOne(d => d.Product).WithMany(p => p.TbOrderDetails).HasConstraintName("FK_dbo.tb_OrderDetail_dbo.tb_Product_ProductId");
        });

        modelBuilder.Entity<TbPost>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.tb_Posts");

            entity.HasOne(d => d.Category).WithMany(p => p.TbPosts).HasConstraintName("FK_dbo.tb_Posts_dbo.tb_Category_CategoryId");
        });

        modelBuilder.Entity<TbProduct>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.tb_Product");

            entity.HasOne(d => d.ProductCategory).WithMany(p => p.TbProducts).HasConstraintName("FK_dbo.tb_Product_dbo.tb_ProductCategory_ProductCategoryId");
        });

        modelBuilder.Entity<TbProductCategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.tb_ProductCategory");

            entity.Property(e => e.Alias).HasDefaultValueSql("('')");
        });

        modelBuilder.Entity<TbProductImage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.tb_ProductImage");

            entity.HasOne(d => d.Product).WithMany(p => p.TbProductImages).HasConstraintName("FK_dbo.tb_ProductImage_dbo.tb_Product_ProductId");
        });

        modelBuilder.Entity<TbSubscribe>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.tb_Subscribe");
        });

        modelBuilder.Entity<TbSystemSetting>(entity =>
        {
            entity.HasKey(e => e.SettingKey).HasName("PK_dbo.tb_SystemSetting");
        });

        modelBuilder.Entity<ThongKe>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.ThongKes");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    public DbSet<Web_banhang.Models.ProdCategoryVM>? ProdCategoryVM { get; set; }

    public DbSet<Web_banhang.Models.ProductVM>? ProductVM { get; set; }
}
