using WSD.Catalog.Domain.Exceptions;
using WSD.Catalog.Domain.Models;
using WSD.Common;
using WSD.Common.Extensions;

namespace WSD.Catalog.Domain.Logic;

public class CatalogItemLogic : CatalogItem, ICatalogItemLogic
{
    /// <inheritdoc />
    public Result RemoveStock(int quantityDesired)
    {
        if (quantityDesired <= 0)
        {
            var result = Result.BadRequest;
            result.Messages.Add("Item units desired should be greater than zero");
            return result;
        }

        if (AvailableStock == 0)
        {
            var result = Result.NotFound;
            result.Messages.Add($"Empty stock, product item {Name} is sold out");
            return result;
        }
        
        if (AvailableStock < quantityDesired)
        {
            string message =
                $"The quantity ordered is larger than available. There are {AvailableStock} items left. Please reconsider your order";
            var result = Result.Conflict;
            result.Messages.Add(message);
            return result;
        }
        
        int removed = Math.Min(quantityDesired, AvailableStock);

        AvailableStock -= removed;

        return Result.Created;
    }

    /// <inheritdoc />
    public Result AddStock(int quantity)
    {
        if (AvailableStock + quantity > MaxStockThreshold)
        {
            int availableSpace = MaxStockThreshold - AvailableStock;
            string message =
                $"The quantity ordered is larger than the space available in the warehouse. There is space for {availableSpace} items. Please reconsider your order";
            var result = Result.Conflict;
            result.Messages.Add(message);
            return result;
        }

        AvailableStock += quantity;

        OnReorder = false;

        return Result.Created;
    }
}