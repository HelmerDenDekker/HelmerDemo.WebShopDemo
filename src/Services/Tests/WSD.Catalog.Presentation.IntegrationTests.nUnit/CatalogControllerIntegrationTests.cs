using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;

namespace WSD.Catalog.Presentation.IntegrationTests.nUnit;

/// <summary>
/// The TestFixture is optional since NUnit 3
/// </summary>
[TestFixture]
public class CatalogControllerIntegrationTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public async Task Get_Request_ShouldReturnVersionString()
    {
        // --arrange--

        var application = new WebApplicationFactory<Program>();
        var httpClient = application.CreateClient();

        // --act--
        var response = await httpClient.GetAsync("/v1/catalog");

        // --assert-- 
        
        // assert NUnit style
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }
}