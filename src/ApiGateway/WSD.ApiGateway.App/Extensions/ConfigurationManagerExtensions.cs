using WSD.ApiGateway.App.Models;

namespace WSD.ApiGateway.App.Extensions
{
    public static class ConfigurationManagerExtensions
    {
        /// <summary>
        /// Obsolete way to load the configuration, as soon as multitenancy is required this must be changed to dynamically load the data //ToDo: this is weird!!!!!!!!!
        /// </summary>
        /// <param name="config">Configuration manager</param>
        /// <returns>Returns gateway configuration</returns>
        public static GatewayConfig GetGatewayConfig(this ConfigurationManager config)
        {
            var result = new GatewayConfig
            {
                Url = config.GetValue("Gateway:Url", string.Empty),
                SessionTimeoutInMin = config.GetValue("Gateway:SessionTimeoutInMin", 60),
                TokenExchangeStrategy = config.GetValue("Gateway:TokenExchangeStrategy", string.Empty),
                Authority = config.GetValue<string>("OpenIdConnect:Authority"),
                ClientId = config.GetValue<string>("OpenIdConnect:ClientId"),
                ClientSecret = config.GetValue<string>("OpenIdConnect:ClientSecret"),
                Scopes = config.GetValue("OpenIdConnect:Scopes", string.Empty),
                LogoutUrl = config.GetValue("OpenIdConnect:LogoutUrl", string.Empty),
                PrlgAccessApi = config.GetValue("PrlgAccessApi:Url", string.Empty),
                PrlgAccessTokenEndpoint = config.GetValue("PrlgAccessApi:Endpoint", string.Empty),
                QueryUserInfoEndpoint = config.GetValue("OpenIdConnect:QueryUserInfoEndpoint", true),
                ApiConfigs = config.GetSection("Apis").Get<ApiConfig[]>()
            };

            return result;
        }
    }
}