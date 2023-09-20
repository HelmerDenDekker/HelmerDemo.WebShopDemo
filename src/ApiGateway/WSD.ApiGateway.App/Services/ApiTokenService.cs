using WSD.ApiGateway.App.Extensions;
using WSD.ApiGateway.App.Models;
using WSD.ApiGateway.App.Strategies;
using WSD.Common.Tools.Constants;

namespace WSD.ApiGateway.App.Services
{
    public class ApiTokenService
    {
        private readonly ITokenExchangeStrategy _tokenExchangeService;
        private readonly Serilog.ILogger _logger;

        public ApiTokenService(
            ITokenExchangeStrategy tokenExchangeService,
            Serilog.ILogger logger)
        {
            _tokenExchangeService = tokenExchangeService;
            _logger = logger;
        }

        public void InvalidateApiTokens(HttpContext httpContext)
        {
            _logger.Information("ApiAccessToken will be removed");
            httpContext.Session.Remove(OpenIdConnectConstants.Tokens.ApiAccessToken);
        }

        private TokenExchangeResponse? GetCachedApiToken(HttpContext httpContext, ApiConfig apiConfig)
        {
            var cache = httpContext.Session.GetObject<Dictionary<string, TokenExchangeResponse>>(OpenIdConnectConstants.Tokens.ApiAccessToken);
            if (cache == null)
            {
                _logger.Error("CachedAPIToken returns null");
                return null;
            }

            if (!cache.ContainsKey(apiConfig.ApiPath))
            {
                _logger.Error("Cache does not contain key for {apiConfig.ApiPath}", apiConfig.ApiPath);
                return null;
            }

            _logger.Information("Cached API Token retrieved");
            return cache[apiConfig.ApiPath];
        }

        private void SetCachedApiToken(HttpContext httpContext, ApiConfig apiConfig, TokenExchangeResponse response)
        {
            var cache = httpContext.Session.GetObject<Dictionary<string, TokenExchangeResponse>>(OpenIdConnectConstants.Tokens.ApiAccessToken);
            if (cache == null)
            {
                cache = new Dictionary<string, TokenExchangeResponse>();
            }

            cache[apiConfig.ApiPath] = response;

            httpContext.Session.SetObject(OpenIdConnectConstants.Tokens.ApiAccessToken, cache);
        }

        public async Task<string> LookupApiToken(HttpContext httpContext, ApiConfig apiConfig, string token)
        {
            var apiToken = GetCachedApiToken(httpContext, apiConfig);

            if (apiToken != null)
            {
                // TODO: Perform individual token refresh
                return apiToken.AccessToken;
            }

            _logger.Debug("---- Perform Token Exchange for {apiConfig.ApiScopes} ----", apiConfig.ApiScopes);

            var response = await _tokenExchangeService.Exchange(token, apiConfig);
            SetCachedApiToken(httpContext, apiConfig, response);

            return response.AccessToken;
        }
    }
}