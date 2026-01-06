using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WMTDotNetCoreTraining.Database.Models;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TblBlog> TblBlogs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string _connectionString = "Data Source=.;Initial Catalog=DotNetTrainingBatch5;User ID=sa;Password=Temporary123; TrustServerCertificate=True;";
        optionsBuilder.UseSqlServer(_connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblBlog>(entity =>
        {
            entity.HasKey(e => e.BlogId).HasName("PK_dbo.Tbl_Blog");

            entity.ToTable("Tbl_Blogs");

            entity.Property(e => e.BlogAuthor).HasMaxLength(50);
            entity.Property(e => e.BlogTitle).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
