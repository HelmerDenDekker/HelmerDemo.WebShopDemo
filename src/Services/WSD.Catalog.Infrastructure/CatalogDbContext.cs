using Microsoft.EntityFrameworkCore;
using WSD.Catalog.Infrastructure.EntityConfigurations;
using WSD.Catalog.Infrastructure.Models;

namespace WSD.Catalog.Infrastructure
{
    public class CatalogDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CatalogDbContext"/> class.
        /// </summary>
        /// <param name="options">The options for this DbContext</param>
        public CatalogDbContext(DbContextOptions<CatalogDbContext> options) : base(options)
        {
        }

        public DbSet<CatalogItem> CatalogItems { get; set; }
        public DbSet<CatalogBrand> CatalogBrands { get; set; }
        public DbSet<CatalogType> CatalogTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new CatalogBrandEntityTypeConfiguration());
            builder.ApplyConfiguration(new CatalogTypeEntityTypeConfiguration());
            builder.ApplyConfiguration(new CatalogItemEntityTypeConfiguration());
        }
    }
}