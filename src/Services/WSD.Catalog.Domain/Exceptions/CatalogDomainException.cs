namespace WSD.Catalog.Domain.Exceptions
{
    /// <summary>
    /// Exception type for app exceptions from Microsoft.eShopOnContainers.Services.Catalog.API.Infrastructure.Exceptions
    /// </summary>
    public class CatalogDomainException : Exception
    {
        public CatalogDomainException()
        { }

        public CatalogDomainException(string message)
            : base(message)
        { }

        public CatalogDomainException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}