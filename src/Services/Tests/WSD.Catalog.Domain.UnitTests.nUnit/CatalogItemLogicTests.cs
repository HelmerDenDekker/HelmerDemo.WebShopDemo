using System.Net;
using WSD.Catalog.Domain.Logic;

namespace WSD.Catalog.Domain.UnitTests.nUnit;

public class CatalogItemLogicTests
{
    [SetUp]
    public void Setup()
    {
    }

    private CatalogItemLogic _catalogItemLogic;

    [OneTimeSetUp]
    public void Initialize()
    {
        _catalogItemLogic = new CatalogItemLogic();
    }
    
    // Use of test cases
    [Test]
    [TestCase(200, 7)]
    [TestCase(100, 1)]
    [TestCase(2, 1)]
    [TestCase(1, 1)]
    public void RemoveStock_RemoveItemsInStockFromStock_ShouldRemoveItems(int availableStock, int quantityDesired)
    {
        // arrange

        _catalogItemLogic.AvailableStock = availableStock;
        
        // act
        
        var result = _catalogItemLogic.RemoveStock(quantityDesired);

        // assert
        
        // I am doing multiple Asserts on the Result object.
        Assert.IsTrue(result.IsSuccess); // I expect succes
        Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.Created) ); // I expect the statuscode to be Created type
        Assert.That(_catalogItemLogic.AvailableStock, Is.EqualTo(availableStock - quantityDesired)); // I expect the available stock to decrease
    }
    
    // Use of test cases with expectedresult
    [Test]
    [TestCase(200, 7, ExpectedResult = 193)]
    [TestCase(100, 1, ExpectedResult = 99)]
    public int RemoveStock_RemoveItemsInStockFromStock_ShouldRemoveItemsExpectedResult(int availableStock, int quantityDesired)
    {
        // arrange

        _catalogItemLogic.AvailableStock = availableStock;
        
        // act
        
        var result = _catalogItemLogic.RemoveStock(quantityDesired);

        // assert
        
        // I am doing multiple Asserts on the Result object.
        Assert.IsTrue(result.IsSuccess); // I expect succes
        Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.Created) ); // I expect the statuscode to be Created type
        return _catalogItemLogic.AvailableStock; // I expect the available stock to decrease
    }
    
    // Use of test cases
    [Test]
    [TestCaseSource(typeof(CatalogItemTestData), "TestCases")]
    public void RemoveStock_RemoveItemsInStockFromStock_ShouldRemoveItems_TestSourceData(int availableStock, int quantityDesired)
    {
        // arrange

        _catalogItemLogic.AvailableStock = availableStock;
        
        // act
        
        var result = _catalogItemLogic.RemoveStock(quantityDesired);

        // assert
        
        // I am doing multiple Asserts on the Result object.
        Assert.IsTrue(result.IsSuccess); // I expect succes
        Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.Created) ); // I expect the statuscode to be Created type
        Assert.That(_catalogItemLogic.AvailableStock, Is.EqualTo(availableStock - quantityDesired)); // I expect the available stock to decrease
    }
    
    [Test]
    public void RemoveStock_RemoveAnItemOutStockFromStock_ShouldReturnNotFound()
    {
        // arrange

        _catalogItemLogic.AvailableStock = 0;
        
        // act
        
        var result = _catalogItemLogic.RemoveStock(1);

        // assert
        Assert.IsFalse(result.IsSuccess);
        Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        Assert.That(_catalogItemLogic.AvailableStock, Is.EqualTo(0));
        Assert.That(result.Messages.Count, Is.EqualTo(2));
    }
    
    [Test]
    public void RemoveStock_RemoveMoreItemsFromStockThanInStock_ShouldReturnConflict()
    {
        // arrange

        _catalogItemLogic.AvailableStock = 1;
        
        // act
        
        var result = _catalogItemLogic.RemoveStock(2);

        // assert
        Assert.IsFalse(result.IsSuccess);
        Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.Conflict));
        Assert.That(_catalogItemLogic.AvailableStock, Is.EqualTo(1));
        Assert.That(result.Messages.Count, Is.EqualTo(2));
    }
    
    [Test]
    public void RemoveStock_RemoveNegativeItemsFromStock_ShouldReturnBadRequest()
    {
        // arrange

        _catalogItemLogic.AvailableStock = 100;
        
        // act
        
        var result = _catalogItemLogic.RemoveStock(-2);

        // assert
        Assert.IsFalse(result.IsSuccess);
        Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        Assert.That(_catalogItemLogic.AvailableStock, Is.EqualTo(100));
        Assert.That(result.Messages.Count, Is.EqualTo(2));
    }
    
    [Ignore("Requested functionality")]
    [Test]
    public void RemoveStock_RemoveDesiredItemsExceedsMaxItemsFromStock_ShouldReturnBadRequest()
    {
        // arrange

        _catalogItemLogic.AvailableStock = 100;
        _catalogItemLogic.MaxStockThreshold = 100;
        
        // act
        
        var result = _catalogItemLogic.RemoveStock(101);

        // assert
        Assert.IsFalse(result.IsSuccess);
        Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        Assert.That(_catalogItemLogic.AvailableStock, Is.EqualTo(100));
        Assert.That(result.Messages.Count, Is.EqualTo(2));
    }
}