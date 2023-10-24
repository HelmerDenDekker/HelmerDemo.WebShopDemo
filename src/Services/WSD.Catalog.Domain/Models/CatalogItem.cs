using WSD.Catalog.Domain.Exceptions;

namespace WSD.Catalog.Domain.Models
{
    public class CatalogItem
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string PictureFileName { get; set; }

        public string PictureUri { get; set; }

        /// <summary>
        /// Quantity in stock
        /// </summary>
        public int AvailableStock { get; set; }

        /// <summary>
        /// Available stock at which reordering should take place
        /// </summary>
        public int RestockThreshold { get; set; }


        /// <summary>
        /// Maximum number of units that can be in-stock at any time (due to physicial/logistical constraints in warehouses)
        /// </summary>
        public int MaxStockThreshold { get; set; }

        /// <summary>
        /// True if item is on reorder
        /// </summary>
        public bool OnReorder { get; set; }
    }
}