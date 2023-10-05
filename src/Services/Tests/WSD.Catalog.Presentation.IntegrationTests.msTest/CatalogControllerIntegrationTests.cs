using System.Net;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;

namespace WSD.Catalog.Presentation.IntegrationTests.msTest;

// Context: https://andrewlock.net/exploring-dotnet-6-part-6-supporting-integration-tests-with-webapplicationfactory-in-dotnet-6/


[TestClass]
public class CatalogControllerIntegrationTests
{
    [TestMethod]
    public async Task Get_Request_ShouldReturnVersionString()
    {
        // --arrange--
        var application = new WebApplicationFactory<Program>();
        var httpClient = application.CreateClient();
        // --act--
        var response = await httpClient.GetAsync("/v1/catalog");
        
        // --assert-- 
        
        // assert VS TestTools
        Assert.IsTrue(response.IsSuccessStatusCode);
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        
        // assert Fluent Assertions
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}