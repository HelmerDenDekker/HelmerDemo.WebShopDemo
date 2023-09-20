using Microsoft.Extensions.Configuration;

namespace WSD.Catalog.Infrastructure.Setup
{
    public static class ConfigHelper
    {
        public static IConfiguration GetConfiguration ()
        {
            // Search through all directories for a file named appsettings.Development.json
            // - this works because the api project is linked as a reference
            var file = Directory.GetFiles(Environment.CurrentDirectory, "appsettings.json", SearchOption.AllDirectories)[0];

            var configurationJsons = new ConfigurationBuilder().AddJsonFile(file).Build();
            return configurationJsons;
        }
    }
}