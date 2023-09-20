using IdentityModel.Client;
using WSD.ApiGateway.App.Models;
using WSD.Common.Tools.Constants;

namespace WSD.ApiGateway.App.Services
{
    public class TokenRefreshService
    {
        private readonly DiscoveryDocumentResponse _disco;
        private readonly GatewayConfig _config;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly Serilog.ILogger _logger;

        public TokenRefreshService(GatewayConfig config, DiscoveryDocumentResponse disco, IHttpClientFactory httpClientFactory, Serilog.ILogger logger)
        {
            _disco = disco;
            _config = config;
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public async Task<RefreshResponse?> RefreshAsync(string refreshToken)
        {
            var payload = new Dictionary<string, string>
        {
            { "grant_type", OpenIdConnectConstants.Tokens.RefreshToken },
            { OpenIdConnectConstants.Tokens.RefreshToken, refreshToken },
            { "client_id", _config.ClientId },
            { "client_secret", _config.ClientSecret }
        };

            var httpClient = _httpClientFactory.CreateClient("TokenRefreshService");

            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(_disco.TokenEndpoint),
                Method = HttpMethod.Post,
                Content = new FormUrlEncodedContent(payload),
            };

            var response = await httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                _logger.Debug("Failed to refresh Token");
                return null;
            }

            var result = await response.Content.ReadFromJsonAsync<RefreshResponse>();
            return result;
        }
    }
}