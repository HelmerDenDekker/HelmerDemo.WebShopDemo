namespace WSD.Catalog.Infrastructure.Models
{
    public class CatalogBrand : Domain.Models.CatalogBrand, IBaseEntity
    {
        /// <inheritdoc />
        public int Id { get; set; }
    }
}