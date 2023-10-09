using System.Net;
using WSD.Catalog.Domain.Logic;

namespace WSD.Catalog.Domain.UnitTests.msTest;

[TestClass]
public class CatalogItemLogicTests
{
    private CatalogItemLogic _catalogItemLogic;

    [TestInitialize]
    public void Initialize()
    {
        _catalogItemLogic = new CatalogItemLogic();
    }
    
    [TestMethod]
    public void RemoveStock_RemoveAnItemInStockFromStock_ShouldRemoveAnItem()
    {
        // arrange

        _catalogItemLogic.AvailableStock = 100;
        
        // act
        
        var result = _catalogItemLogic.RemoveStock(1);

        // assert
        
        // I am doing multiple Asserts on the Result object.
        Assert.IsTrue(result.IsSuccess); // I expect succes
        Assert.AreEqual(HttpStatusCode.Created, result.StatusCode ); // I expect the statuscode to be Created type
        Assert.AreEqual(99, _catalogItemLogic.AvailableStock); // I expect the available stock to decrease
    }
    
    [TestMethod]
    public void RemoveStock_RemoveAnItemOutStockFromStock_ShouldReturnNotFound()
    {
        // arrange

        _catalogItemLogic.AvailableStock = 0;
        
        // act
        
        var result = _catalogItemLogic.RemoveStock(1);

        // assert
        Assert.IsFalse(result.IsSuccess);
        Assert.AreEqual(HttpStatusCode.NotFound, result.StatusCode );
        Assert.AreEqual(0, _catalogItemLogic.AvailableStock);
        Assert.AreEqual(2,result.Messages.Count);
    }
    
    [TestMethod]
    public void RemoveStock_RemoveMoreItemsFromStockThanInStock_ShouldReturnConflict()
    {
        // arrange

        _catalogItemLogic.AvailableStock = 1;
        
        // act
        
        var result = _catalogItemLogic.RemoveStock(2);

        // assert
        Assert.IsFalse(result.IsSuccess);
        Assert.AreEqual(HttpStatusCode.Conflict, result.StatusCode );
        Assert.AreEqual(1, _catalogItemLogic.AvailableStock);
        Assert.AreEqual(2,result.Messages.Count);
    }
    
    [TestMethod]
    public void RemoveStock_RemoveNegativeItemsFromStock_ShouldReturnBadRequest()
    {
        // arrange

        _catalogItemLogic.AvailableStock = 100;
        
        // act
        
        var result = _catalogItemLogic.RemoveStock(-2);

        // assert
        Assert.IsFalse(result.IsSuccess);
        Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode );
        Assert.AreEqual(100, _catalogItemLogic.AvailableStock);
        Assert.AreEqual(2,result.Messages.Count);
    }
    
    [Ignore("Requested functionality")]
    [TestMethod]
    public void RemoveStock_RemoveDesiredItemsExceedsMaxItemsFromStock_ShouldReturnBadRequest()
    {
        // arrange

        _catalogItemLogic.AvailableStock = 100;
        _catalogItemLogic.MaxStockThreshold = 100;
        
        // act
        
        var result = _catalogItemLogic.RemoveStock(101);

        // assert
        Assert.IsFalse(result.IsSuccess);
        Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode );
        Assert.AreEqual(100, _catalogItemLogic.AvailableStock);
        Assert.AreEqual(2,result.Messages.Count);
    }
    
}