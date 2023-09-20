using IdentityModel.Client;
using Serilog;
using System.IdentityModel.Tokens.Jwt;
using WSD.ApiGateway.App.Exceptions;
using WSD.ApiGateway.App.Extensions;
using WSD.ApiGateway.App.Models;
using WSD.Common.Extensions;
using WSD.Common.Tools.Middleware;
using WSD.Common.Tools.Models;



var builder = WebApplication.CreateBuilder(args);

// Disable claim mapping to get claims 1:1 from the tokens
JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
var config = builder.Configuration.GetGatewayConfig();

await AddServices(builder, config);
ConfigureRequestPipeline(builder.Build(), config);

static async Task AddServices(WebApplicationBuilder builder, GatewayConfig config) 
{
    var corsSettings = new CorsSettings();
    builder.Configuration.GetSection("CorsSettings").Bind(corsSettings);
    // Header settings
    builder.Services.Configure<HeaderSettings>(options => builder.Configuration.GetSection("HeaderSettings").Bind(options));

    // IDiscoveryCache is a helper provided in IdentityModel, It allows us to get the info from the discovery endpoint of the OpenID Connect provider (and cache it) so we can use it in other requests to the provider, in this case, for token refresh.
    builder.Services.AddSingleton<IDiscoveryCache>(disc_results =>
    {
        var factory = disc_results.GetRequiredService<IHttpClientFactory>();
        return new DiscoveryCache(config.Authority, () => factory.CreateClient());
    });

    IDiscoveryCache discoverCache = new DiscoveryCache(config.Authority);
    var discoveryResponse = await discoverCache.GetAsync();

    if (discoveryResponse.IsError)
    {
        throw new DiscoveryException(discoveryResponse.Error);
    }

    // Configure Services
    builder.Services.AddDistributedMemoryCache();
        
    builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));
    builder.Services.AddTransient<WebSecurityMiddleware>();

    //ToDo rewrite!
    builder.AddGateway(config, discoveryResponse);

    // CORS
    builder.Services.AddCors(options =>
    options.AddPolicy(
        name: "defaultCorsPolicy",
        policy =>
        {
            policy.WithOrigins(corsSettings.AllowedOrigins);
            policy.WithHeaders(corsSettings.AllowedHeaders);
            policy.WithMethods(corsSettings.AllowedMethods);
        }));
}


static void ConfigureRequestPipeline(WebApplication app, GatewayConfig config)
{
    app.UseMiddleware<WebSecurityMiddleware>();
    app.UseGateway();

    // Start Gateway
    if (config.Url.IsNullOrEmpty())
    {
        app.Run();
    }
    else
    {
        app.Run(config.Url);
    }
}
