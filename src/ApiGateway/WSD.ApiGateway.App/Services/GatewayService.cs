using WSD.ApiGateway.App.Models;
using WSD.Common.Extensions;
using WSD.Common.Tools.Constants;

namespace WSD.ApiGateway.App.Services
{
    public class GatewayService
    {
        private readonly TokenRefreshService _tokenRefreshService;
        private readonly GatewayConfig _config;
        private readonly ApiTokenService _apiTokenService;
        private readonly Serilog.ILogger _logger;

        public GatewayService(
            TokenRefreshService tokenRefreshService,
            GatewayConfig config,
            ApiTokenService apiTokenService,
            Serilog.ILogger logger)
        {
            _tokenRefreshService = tokenRefreshService;
            _config = config;
            _apiTokenService = apiTokenService;
            _logger = logger;
        }

        /// <summary>
        /// Adds the token
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public async Task AddToken(HttpContext httpContext)
        {
            if (IsExpired(httpContext) && HasRefreshToken(httpContext))
            {
                _apiTokenService.InvalidateApiTokens(httpContext);
                await Refresh(httpContext, _tokenRefreshService);
            }

            var token = httpContext.Session.GetString(OpenIdConnectConstants.Tokens.AccessToken);
            var currentUrl = httpContext.Request.Path.ToString().ToLower();

            var apiConfig = _config.ApiConfigs.FirstOrDefault(c => currentUrl.StartsWith(c.ApiPath));

            if (!string.IsNullOrEmpty(token) && apiConfig != null)
            {
                var apiToken = await GetApiToken(httpContext, _apiTokenService, token, apiConfig);

                _logger.Debug("---- Adding Token for reqeuest ----\n{@currentUrl}\n\n{@apiToken}\n--------", currentUrl, apiToken);

                httpContext.Request.Headers.Add("Authorization", "Bearer " + apiToken);
            }
        }

        private bool IsExpired(HttpContext httpContext)
        {
            var expiresAt = Convert.ToInt64(httpContext.Session.GetString(OpenIdConnectConstants.Session.ExpiresAt)) - 30;
            var now = new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds();

            var expired = now >= expiresAt;
            return expired;
        }

        private bool HasRefreshToken(HttpContext httpContext)
        {
            var refreshToken = httpContext.Session.GetString(OpenIdConnectConstants.Tokens.RefreshToken);
            return !string.IsNullOrEmpty(refreshToken);
        }

        private string GetRefreshToken(HttpContext httpContext)
        {
            var refreshToken = httpContext.Session.GetString(OpenIdConnectConstants.Tokens.RefreshToken);
            return refreshToken ?? string.Empty;
        }

        private async Task Refresh(HttpContext httpContext, TokenRefreshService tokenRefreshService)
        {
            var refreshToken = GetRefreshToken(httpContext);

            var resp = await tokenRefreshService.RefreshAsync(refreshToken);

            if (resp == null)
            {
                // Next call to API will fail with 401 and client can take action
                _logger.Information("Token refresh response is null. Next call to API will fail with 401. Client can take action");
                return;
            }

            var expiresAt = new DateTimeOffset(DateTime.Now).AddSeconds(Convert.ToInt32(resp.ExpiresIn));

            httpContext.Session.SetString(OpenIdConnectConstants.Tokens.AccessToken, resp.AccessToken);
            httpContext.Session.SetString(OpenIdConnectConstants.Tokens.IdToken, resp.IdToken);
            httpContext.Session.SetString(OpenIdConnectConstants.Tokens.RefreshToken, resp.RefreshToken);
            httpContext.Session.SetString(OpenIdConnectConstants.Session.ExpiresAt, string.Empty + expiresAt.ToUnixTimeSeconds());
        }

        private async Task<string> GetApiToken(HttpContext httpContext, ApiTokenService apiTokenService, string token, ApiConfig? apiConfig)
        {
            var apiToken = string.Empty;
            if (!string.IsNullOrEmpty(apiConfig?.ApiScopes) || !string.IsNullOrEmpty(apiConfig?.ApiAudience))
            {
                apiToken = await apiTokenService.LookupApiToken(httpContext, apiConfig, token);
            }

            if (!apiToken.IsNullOrEmpty())
            {
                return apiToken;
            }
            else
            {
                return token;
            }
        }
    }
}