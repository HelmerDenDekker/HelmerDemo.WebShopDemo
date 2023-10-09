using WSD.Common;

namespace WSD.Catalog.Domain.Logic;

public interface ICatalogItemLogic
{


    /// <summary>
    /// Increments the quantity of a particular item in inventory.
    /// This is a command and should not return a value
    /// <param name="quantity"></param>
    /// </summary>
    public Result AddStock(int quantity);

    /// <summary>
    /// Decrements the quantity of a particular item in inventory and ensures the restockThreshold hasn't
    /// been breached. If so, a RestockRequest is generated in CheckThreshold. 
    /// 
    /// If there is sufficient stock of an item, then the integer returned at the end of this call should be the same as quantityDesired. 
    /// In the event that there is not sufficient stock available, the method will remove whatever stock is available and return that quantity to the client.
    /// In this case, it is the responsibility of the client to determine if the amount that is returned is the same as quantityDesired.
    /// It is invalid to pass in a negative number.
    /// This is a command and should not return a value
    /// </summary>
    /// <param name="quantityDesired"></param>
    /// 
    public Result RemoveStock(int quantityDesired);
}