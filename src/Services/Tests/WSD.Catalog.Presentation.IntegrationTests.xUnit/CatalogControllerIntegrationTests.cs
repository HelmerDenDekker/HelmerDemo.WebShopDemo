using System.Net;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;

namespace WSD.Catalog.Presentation.IntegrationTests.xUnit;

public class CatalogControllerIntegrationTests
{
    [Fact]
    public async Task Get_Request_ShouldReturnVersionString()
    {
        // --arrange--
        
        var application = new WebApplicationFactory<Program>();
        var httpClient = application.CreateClient();
        
        // --act--
        var response = await httpClient.GetAsync("/v1/catalog");
        
        // --assert-- 
        
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}