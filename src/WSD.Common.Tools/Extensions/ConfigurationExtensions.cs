using Microsoft.Extensions.Configuration;
using WSD.Common.Tools.Exceptions;

namespace WSD.Common.Tools.Extensions
{
    public static class ConfigurationExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static string GetRequiredValue(this IConfiguration configuration, string name) =>
            configuration[name] ?? throw new ConfigurationValueMissingException(configuration.ConstructErrorMessage(name));

        /// <summary>
        /// Gets the connection string to a database
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static string GetDatabaseConnectionString(this IConfiguration configuration, string name) =>
            configuration.GetConnectionString(name) ?? throw new ConfigurationValueMissingException(configuration.ConstructErrorMessage($"ConnectionStrings: {name}"));


        private static string ConstructErrorMessage(this IConfiguration configuration, string name)
        {
            return $"Configuration missing value for: {(configuration is IConfigurationSection s ? s.Path + ":" + name : name)}";
        }
    }
}