namespace WSD.ApiGateway.App.Models
{
    /// <summary>
    /// Gets or sets the gateway configuration data
    /// </summary>
    public class GatewayConfig
    {
        public string Url { get; set; } = string.Empty;
        public int SessionTimeoutInMin { get; set; }
        public string TokenExchangeStrategy { get; set; } = string.Empty;
        public string Authority { get; set; } = string.Empty;
        public string ClientId { get; set; } = string.Empty;
        public string ClientSecret { get; set; } = string.Empty;
        public string Scopes { get; set; } = string.Empty;
        public string LogoutUrl { get; set; } = string.Empty;
        public string PrlgAccessApi { get; set; } = string.Empty;
        public string PrlgAccessTokenEndpoint { get; set; } = string.Empty;
        public bool QueryUserInfoEndpoint { get; set; } = true;
        public ApiConfig[] ApiConfigs { get; set; } = { };
    }
}