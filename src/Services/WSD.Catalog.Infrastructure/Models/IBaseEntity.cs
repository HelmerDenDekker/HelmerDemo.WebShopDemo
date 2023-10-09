namespace WSD.Catalog.Infrastructure.Models;

public interface IBaseEntity
{
    /// <summary>
    /// Primary key
    /// </summary>
    public int Id { get; set; }
}