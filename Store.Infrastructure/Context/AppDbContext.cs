using Microsoft.EntityFrameworkCore;
using Store.Domain.Entities;

namespace Store.Infrastructure.Context;

public class AppDbContext : DbContext
{
	public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
	{

    }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>().HasKey(c => c.Id);

        modelBuilder.Entity<Category>()
            .Property(c => c.Name)
            .HasMaxLength(50)
            .IsRequired();

        modelBuilder.Entity<Product>().HasKey(p => p.Id);

        modelBuilder.Entity<Product>()
            .Property(p => p.Name)
            .HasMaxLength(100)
            .IsRequired();

        modelBuilder.Entity<Product>()
            .Property(p => p.Description)
            .HasMaxLength(255)
            .IsRequired();

        modelBuilder.Entity<Product>()
            .Property(p => p.ImageURL)
            .HasMaxLength(255)
            .IsRequired();

        modelBuilder.Entity<Product>()
            .Property(p => p.Price)
            .HasPrecision(12, 2)
            .IsRequired();

        modelBuilder.Entity<Category>()
            .HasMany(c => c.Products)
            .WithOne(p => p.Category)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Category>()
            .HasData(
            new Category
            {
                Id = 1,
                Name = "Material Escolar"
            },
            new Category
            {
                Id = 2,
                Name = "Acessórios"
            });
    }
}