namespace WSD.Catalog.Infrastructure.Models
{
    public class CatalogType : Domain.Models.CatalogType, IBaseEntity
    {
        /// <inheritdoc />
        public int Id { get; set; }
    }
}