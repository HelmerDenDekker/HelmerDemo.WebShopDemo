namespace WSD.Catalog.Infrastructure.Models
{
    public class CatalogItem : Domain.Models.CatalogItem, IBaseEntity
    {
        /// <inheritdoc />
        public int Id { get; set; }

        /// <summary>
        /// Primary key for Catalog Type
        /// </summary>
        public int CatalogTypeId { get; set; }
        
        /// <summary>
        /// Navigational property to <see cref="CatalogType"/>
        /// </summary>
        public CatalogType CatalogType { get; set; }

        /// <summary>
        /// Primary key for Catalog Brand
        /// </summary>
        public int CatalogBrandId { get; set; }

        /// <summary>
        /// Navigational property for <see cref="CatalogBrand"/>
        /// </summary>
        public CatalogBrand CatalogBrand { get; set; }
    }
}