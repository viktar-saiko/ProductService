using Data.Sql.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace Data.Sql.Repositories
{
    public class AppDbContext : DbContext
    {
        public DbSet<ProductDto> Products { get; set; } = null!;
        public DbSet<CategoryDto> Categories { get; set; } = null!;

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            try
            {
                var databaseCreator = Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
                if (databaseCreator != null)
                {
                    if (!databaseCreator.CanConnect())
                    {
                        databaseCreator.Create();
                    }

                    if (!databaseCreator.HasTables())
                    {
                        databaseCreator.CreateTables();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductCategoryDto>()
                .HasKey(t => new { t.ProductId, t.CategoryId });

            modelBuilder.Entity<ProductDto>()
                .HasMany(t => t.Categories)
                .WithMany(t => t.Product)
                .UsingEntity<ProductCategoryDto>(
                    r => r.HasOne(t => t.Category).WithMany().HasForeignKey(t => t.CategoryId),
                    l => l.HasOne(t => t.Product).WithMany().HasForeignKey(t => t.ProductId)
                )
                //.HasForeignKey(t => t.ProductId)
                //.IsRequired()
                //.UsingEntity<ProductCategoryDto>()
                //.UsingEntity(t => t.ToTable("Products_Categories"))
                ;

            SeedCategories(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        private void SeedCategories(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CategoryDto>()
                .HasData(
                    new CategoryDto { Id = 1, Name = "Furniture" },
                    new CategoryDto { Id = 2, Name = "Electronics" },
                    new CategoryDto { Id = 3, Name = "Health & Beauty" },
                    new CategoryDto { Id = 4, Name = "Bathroom" },
                    new CategoryDto { Id = 5, Name = "Housekeeping" },
                    new CategoryDto { Id = 6, Name = "Travel" },
                    new CategoryDto { Id = 7, Name = "Food" },
                    new CategoryDto { Id = 8, Name = "Sport" },
                    new CategoryDto { Id = 9, Name = "Women's" },
                    new CategoryDto { Id = 10, Name = "Men's" }
                );
        }
    }
}
