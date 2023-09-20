using IdentityModel.Client;
using WSD.ApiGateway.App.Models;

namespace WSD.ApiGateway.App.Strategies
{
    public class TokenExchangeStrategy : ITokenExchangeStrategy
    {
        private readonly DiscoveryDocumentResponse _disco;
        private readonly GatewayConfig _config;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly Serilog.ILogger _logger;

        public TokenExchangeStrategy(GatewayConfig config, DiscoveryDocumentResponse disco, IHttpClientFactory httpClientFactory, Serilog.ILogger logger)
        {
            _disco = disco;
            _config = config;
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        /// <inheritdoc/>
        public async Task<TokenExchangeResponse> Exchange(string accessToken, ApiConfig apiConfig)
        {
            var httpClient = _httpClientFactory.CreateClient("TokenExchangeClient");
            var scope = apiConfig.ApiScopes;

            // TODO: Allow to config different settings per API
            //  e. g. client_id, client_secrets, token_endpoint

            var url = _disco.TokenEndpoint;
            var dict = new Dictionary<string, string>
            {
                ["grant_type"] = "urn:ietf:params:oauth:grant-type:token-exchange",
                ["client_id"] = _config.ClientId,
                ["client_secret"] = _config.ClientSecret,
                ["subject_token"] = accessToken,
                ["scope"] = scope,
                ["audience"] = apiConfig.ApiAudience,
                ["requested_token_type"] = "urn:ietf:params:oauth:token-type:refresh_token"
            };

            var content = new FormUrlEncodedContent(dict);
            var httpResponse = await httpClient.PostAsync(url, content);
            var response = await httpResponse.Content.ReadFromJsonAsync<TokenExchangeResponse>();

            if (response == null)
            {
                _logger.Error("Error exchanging token at {@url}", url);
            }

            return response;
        }
    }
}