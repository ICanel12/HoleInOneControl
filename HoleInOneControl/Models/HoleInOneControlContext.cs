using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace HoleInOneControl.Models;

public partial class HoleInOneControlContext : DbContext
{
    public HoleInOneControlContext()
    {
    }

    public HoleInOneControlContext(DbContextOptions<HoleInOneControlContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Article> Articles { get; set; }

    public virtual DbSet<Handicap> Handicaps { get; set; }

    public virtual DbSet<Transaction> Transactions { get; set; }

    public virtual DbSet<TransactionArticle> TransactionArticles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json")
                        .Build();

            var connectionString = configuration.GetConnectionString("DBHoleInOneControl");

            optionsBuilder.UseMySQL(connectionString);
        }

    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Article>(entity =>
        {
            entity.HasKey(e => e.IdArticle).HasName("PRIMARY");

            entity.ToTable("article");

            entity.HasIndex(e => e.IdUser, "id_user");

            entity.Property(e => e.IdArticle).HasColumnName("id_article");
            entity.Property(e => e.Brand)
                .HasMaxLength(50)
                .HasColumnName("brand");
            entity.Property(e => e.Capacity).HasColumnName("capacity");
            entity.Property(e => e.Color)
                .HasMaxLength(50)
                .HasColumnName("color");
            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .HasColumnName("description");
            entity.Property(e => e.IdUser).HasColumnName("id_user");
            entity.Property(e => e.Material)
                .HasMaxLength(50)
                .HasColumnName("material");
            entity.Property(e => e.Model)
                .HasMaxLength(50)
                .HasColumnName("model");
            entity.Property(e => e.NameArticle)
                .HasMaxLength(100)
                .HasColumnName("name_article");
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .HasColumnName("type");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Articles)
                .HasForeignKey(d => d.IdUser)
                .HasConstraintName("article_ibfk_1");
        });

        modelBuilder.Entity<Handicap>(entity =>
        {
            entity.HasKey(e => e.IdHandicap).HasName("PRIMARY");

            entity.ToTable("handicap");

            entity.HasIndex(e => e.IdUser, "id_user");

            entity.Property(e => e.IdHandicap).HasColumnName("id_handicap");
            entity.Property(e => e.DateHour)
                .HasColumnType("datetime")
                .HasColumnName("date_hour");
            entity.Property(e => e.HoleEight).HasColumnName("hole_eight");
            entity.Property(e => e.HoleEighteen).HasColumnName("hole_eighteen");
            entity.Property(e => e.HoleEleven).HasColumnName("hole_eleven");
            entity.Property(e => e.HoleFifteen).HasColumnName("hole_fifteen");
            entity.Property(e => e.HoleFive).HasColumnName("hole_five");
            entity.Property(e => e.HoleFour).HasColumnName("hole_four");
            entity.Property(e => e.HoleFourteen).HasColumnName("hole_fourteen");
            entity.Property(e => e.HoleNine).HasColumnName("hole_nine");
            entity.Property(e => e.HoleOne).HasColumnName("hole_one");
            entity.Property(e => e.HoleSeven).HasColumnName("hole_seven");
            entity.Property(e => e.HoleSeventeen).HasColumnName("hole_seventeen");
            entity.Property(e => e.HoleSix).HasColumnName("hole_six");
            entity.Property(e => e.HoleSixteen).HasColumnName("hole_sixteen");
            entity.Property(e => e.HoleTen).HasColumnName("hole_ten");
            entity.Property(e => e.HoleThirteen).HasColumnName("hole_thirteen");
            entity.Property(e => e.HoleThree).HasColumnName("hole_three");
            entity.Property(e => e.HoleTwelve).HasColumnName("hole_twelve");
            entity.Property(e => e.HoleTwo).HasColumnName("hole_two");
            entity.Property(e => e.IdUser).HasColumnName("id_user");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Handicaps)
                .HasForeignKey(d => d.IdUser)
                .HasConstraintName("handicap_ibfk_1");
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasKey(e => e.IdTransaction).HasName("PRIMARY");

            entity.ToTable("transaction");

            entity.HasIndex(e => e.IdUser, "id_user");

            entity.Property(e => e.IdTransaction).HasColumnName("id_transaction");
            entity.Property(e => e.DateHour)
                .HasColumnType("datetime")
                .HasColumnName("date_hour");
            entity.Property(e => e.IdUser).HasColumnName("id_user");
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .HasColumnName("type");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.IdUser)
                .HasConstraintName("transaction_ibfk_1");
        });

        modelBuilder.Entity<TransactionArticle>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("transaction_articles");

            entity.HasIndex(e => e.IdArticle, "id_article");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdArticle).HasColumnName("id_article");
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .HasColumnName("type");

            entity.HasOne(d => d.IdArticleNavigation).WithMany(p => p.TransactionArticles)
                .HasForeignKey(d => d.IdArticle)
                .HasConstraintName("transaction_articles_ibfk_1");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.IdUser).HasName("PRIMARY");

            entity.ToTable("user");

            entity.Property(e => e.IdUser).HasColumnName("id_user");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .HasColumnName("last_name");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .HasColumnName("password");
            entity.Property(e => e.UserName)
                .HasMaxLength(50)
                .HasColumnName("user_name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
