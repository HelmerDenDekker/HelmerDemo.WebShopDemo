using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WSD.Catalog.Infrastructure;
using WSD.Catalog.Infrastructure.Setup;
using WSD.Common.Tools.Extensions;

Console.WriteLine("%%%%%%%%%%%%%%%%%%%%%%%%%%%%");
Console.WriteLine("% The Setup helper started %");
Console.WriteLine("%%%%%%%%%%%%%%%%%%%%%%%%%%%%");
Console.WriteLine("Host starten...");

var config = ConfigHelper.GetConfiguration();
var dbConnection = config.GetDatabaseConnectionString("CatalogDatabase");


if (string.IsNullOrEmpty(dbConnection))
{
    Console.WriteLine("! Error reading database connection string");
}
else
{
    
    

    await Host.CreateDefaultBuilder(args)
        .ConfigureServices(services =>
        {
            services.AddDbContextPool<CatalogDbContext>(options =>
                options.UseSqlServer(dbConnection));

            services.AddHostedService<Worker>();
        })
        .Build()
        .RunAsync();
}