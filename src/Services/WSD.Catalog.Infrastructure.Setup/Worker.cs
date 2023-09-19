using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


namespace WSD.Catalog.Infrastructure.Setup
{
    public class Worker : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;


        public Worker(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Start migration process.");
            using var scope = _serviceProvider.CreateScope();

            var dbContext = scope.ServiceProvider.GetRequiredService<CatalogDbContext>();

            if (dbContext.Database.GetPendingMigrations().Any())
            {
                Console.WriteLine("Migrations found, executing migrations.");
                await dbContext.Database.MigrateAsync();
                Console.WriteLine("Migrations succeeded");
            }
            else
            {
                Console.WriteLine("No migrations found");
            }
            
            Console.WriteLine("Seeding the database");

            await new CatalogDbContextSeed().SeedAsync(dbContext);

            Console.WriteLine("%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%");
            Console.WriteLine("% Ready! Close the terminal with CTRL+C %");
            Console.WriteLine("%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%");
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
