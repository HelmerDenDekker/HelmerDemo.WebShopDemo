using System.Net;
using WSD.Catalog.Domain.Logic;
using WSD.Common;

namespace WSD.Catalog.Domain.UnitTests.nUnit;

public class CatalogItemLogicTests
{
    private CatalogItemLogic _catalogItemLogic = new();

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

        // I am doing multiple sequential Asserts on the Result object.
        Assert.That(result.IsSuccess, Is.True); // I expect succes
        Assert.That(result.StatusCode,
            Is.EqualTo(HttpStatusCode.Created)); // I expect the statuscode to be Created type
        Assert.That(_catalogItemLogic.AvailableStock,
            Is.EqualTo(availableStock - quantityDesired)); // I expect the available stock to decrease
    }

    // Use of test cases with expectedresult
    [Test]
    [TestCase(200, 7, ExpectedResult = 193)]
    [TestCase(100, 1, ExpectedResult = 99)]
    public int RemoveStock_RemoveItemsInStockFromStock_ShouldRemoveItemsExpectedResult(int availableStock,
        int quantityDesired)
    {
        // arrange

        _catalogItemLogic.AvailableStock = availableStock;

        // act

        var result = _catalogItemLogic.RemoveStock(quantityDesired);

        // assert

        // I am doing multiple sequential Asserts on the Result object.
        Assert.That(result.IsSuccess, Is.True); // I expect succes
        Assert.That(result.StatusCode,
            Is.EqualTo(HttpStatusCode.Created)); // I expect the statuscode to be Created type
        return _catalogItemLogic.AvailableStock; // I expect the available stock to decrease
    }

    // Use of test cases with test source data
    [Test]
    [TestCaseSource(typeof(CatalogItemTestData), "TestCases")]
    public void RemoveStock_RemoveItemsInStockFromStock_ShouldRemoveItems_TestSourceData(int availableStock,
        int quantityDesired)
    {
        // arrange

        _catalogItemLogic.AvailableStock = availableStock;

        // act

        var result = _catalogItemLogic.RemoveStock(quantityDesired);

        // assert

        // I am doing multiple parallel asserts on the result object
        Assert.Multiple(() =>
        {
            Assert.That(result.IsSuccess, Is.True); // I expect succes
            Assert.That(result.StatusCode,
                Is.EqualTo(HttpStatusCode.Created)); // I expect the statuscode to be Created type
            Assert.That(_catalogItemLogic.AvailableStock,
                Is.EqualTo(availableStock - quantityDesired)); // I expect the available stock to decrease
        });
    }

    /// <summary>
    /// Use of automated scenarios with Values and sequential, which is the same as <see cref="RemoveStock_RemoveItemsInStockFromStock_ShouldRemoveItems"/>
    /// </summary>
    /// <param name="availableStock"></param>
    /// <param name="quantityDesired"></param>
    [Test]
    [Sequential]
    public void RemoveStock_RemoveItemsInStockFromStock_ShouldRemoveItems_ValuesSequentialWithAssert(
        [Values(200, 10, 20, 1)] int availableStock, [Values(7, 1, 1, 1)] int quantityDesired)
    {
        // arrange

        _catalogItemLogic.AvailableStock = availableStock;

        // act

        var result = _catalogItemLogic.RemoveStock(quantityDesired);
        
        // assert
        
        Assert.Multiple(() =>
        {
            Assert.That(result.IsSuccess, Is.True); // I expect succes
            Assert.That(result.StatusCode,
                Is.EqualTo(HttpStatusCode.Created)); // I expect the statuscode to be Created type
            Assert.That(_catalogItemLogic.AvailableStock,
                Is.EqualTo(availableStock - quantityDesired)); // I expect the available stock to decrease
        });
    }

    /// <summary>
    /// Use of automated scenarios with Values, it will create any possible combination of input. Be careful with Asserts here.
    /// </summary>
    /// <param name="availableStock"></param>
    /// <param name="quantityDesired"></param>
    [Test]
    public void RemoveStock_RemoveItemsInStockFromStock_ShouldRemoveItems_ValuesWithAssert(
        [Values(200, 100, 20, 7)] int availableStock, [Values(7, 1)] int quantityDesired)
    {
        // arrange

        _catalogItemLogic.AvailableStock = availableStock;

        // act

        var result = _catalogItemLogic.RemoveStock(quantityDesired);

        // assert
        
        Assert.Multiple(() =>
        {
            Assert.That(result.IsSuccess, Is.True); // I expect succes
            Assert.That(result.StatusCode,
                Is.EqualTo(HttpStatusCode.Created)); // I expect the statuscode to be Created type
            Assert.That(_catalogItemLogic.AvailableStock,
                Is.EqualTo(availableStock - quantityDesired)); // I expect the available stock to decrease
        });
    }

    /// <summary>
    /// Range attribute: Start, end, step
    /// </summary>
    /// <param name="availableStock"></param>
    /// <param name="quantityDesired"></param>
    [Ignore("This example will generate 400 tests, of which 190 fail because the use of Assert")]
    [Test]
    public void RemoveStock_RemoveItemsInStockFromStock_ShouldRemoveItems_RangeWithAssert(
        [Range(5, 100, 5)] int availableStock, [Range(5, 100, 5)] int quantityDesired)
    {
        // arrange

        _catalogItemLogic.AvailableStock = availableStock;

        // act

        var result = _catalogItemLogic.RemoveStock(quantityDesired);

        // assert
        
        Assert.Multiple(() =>
        {
            Assert.That(result.IsSuccess, Is.True); // I expect succes
            Assert.That(result.StatusCode,
                Is.EqualTo(HttpStatusCode.Created)); // I expect the statuscode to be Created type
            Assert.That(_catalogItemLogic.AvailableStock,
                Is.EqualTo(availableStock - quantityDesired)); // I expect the available stock to decrease
        });
    }

    [Test]
    public void RemoveStock_RemoveAnItemOutStockFromStock_ShouldReturnNotFound()
    {
        // arrange

        _catalogItemLogic.AvailableStock = 0;

        // act

        var result = _catalogItemLogic.RemoveStock(1);
        
        // assert
        
        Assert.Multiple(() =>
        {
            Assert.That(result.IsSuccess, Is.False);
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
            Assert.That(_catalogItemLogic.AvailableStock, Is.EqualTo(0));
            Assert.That(result.Messages.Count, Is.EqualTo(2));
        });
    }

    [Test]
    public void RemoveStock_RemoveAnItemOutStockFromStock_ShouldReturnNotFound_UsingContstraint()
    {
        // arrange

        _catalogItemLogic.AvailableStock = 0;
        _catalogItemLogic.Name = "TestProduct";
        var expectedResult = Result.NotFound;
        expectedResult.Messages.Add($"Empty stock, product item TestProduct is sold out");
        
        // act

        var result = _catalogItemLogic.RemoveStock(1);

        // assert
        
        Assert.Multiple(() =>
        {
            Assert.That(result, new ResultConstraint(expectedResult));
            Assert.That(_catalogItemLogic.AvailableStock, Is.EqualTo(0));
        });
    }

    [Test]
    public void RemoveStock_RemoveMoreItemsFromStockThanInStock_ShouldReturnConflict()
    {
        // arrange

        _catalogItemLogic.AvailableStock = 1;

        // act

        var result = _catalogItemLogic.RemoveStock(2);

        // assert
        
        Assert.Multiple(() =>
            {
                Assert.That(result.IsSuccess, Is.False);
                Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.Conflict));
                Assert.That(_catalogItemLogic.AvailableStock, Is.EqualTo(1));
                Assert.That(result.Messages, Has.Count.EqualTo(2));
            }
        );
    }

    [Test]
    public void RemoveStock_RemoveNegativeItemsFromStock_ShouldReturnBadRequest()
    {
        // arrange

        _catalogItemLogic.AvailableStock = 100;

        // act

        var result = _catalogItemLogic.RemoveStock(-2);

        // assert

        Assert.Multiple(() =>
        {
            Assert.That(result.IsSuccess, Is.False);
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            Assert.That(_catalogItemLogic.AvailableStock, Is.EqualTo(100));
            Assert.That(result.Messages.Count, Is.EqualTo(2));
        });
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

        Assert.Multiple(() =>
        {
            Assert.That(result.IsSuccess, Is.False);
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            Assert.That(_catalogItemLogic.AvailableStock, Is.EqualTo(100));
            Assert.That(result.Messages.Count, Is.EqualTo(2));
        });
    }
}