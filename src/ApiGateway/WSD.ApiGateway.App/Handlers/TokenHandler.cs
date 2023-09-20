using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using System.Security.Claims;
using WSD.ApiGateway.App.Models;
using WSD.Common.Tools.Constants;

namespace WSD.ApiGateway.App.Handlers
{
    public class TokenHandler : ITokenHandler
    {
        private readonly Serilog.ILogger _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public TokenHandler(Serilog.ILogger logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        /// <summary>
        /// Validate token and set session in HttpContext
        /// </summary>
        /// <param name="context"></param>
        /// <exception cref="Exception"></exception>
        public void HandleToken(TokenValidatedContext context)
        {

            if (context.TokenEndpointResponse == null)
            {
                _logger.Warning("TokenEndpointResponse expected!");
            }

            var accessToken = context.TokenEndpointResponse.AccessToken;
            var idToken = context.TokenEndpointResponse.IdToken;
            var refreshToken = context.TokenEndpointResponse.RefreshToken;
            var expiresIn = context.TokenEndpointResponse.ExpiresIn;
            var expiresAt = new DateTimeOffset(DateTime.Now).AddSeconds(Convert.ToInt32(expiresIn));

            context.HttpContext.Session.SetString(OpenIdConnectConstants.Tokens.AccessToken, accessToken);
            context.HttpContext.Session.SetString(OpenIdConnectConstants.Tokens.IdToken, idToken);
            context.HttpContext.Session.SetString(OpenIdConnectConstants.Tokens.RefreshToken, refreshToken);
            context.HttpContext.Session.SetString(OpenIdConnectConstants.Session.ExpiresAt, string.Empty + expiresAt.ToUnixTimeSeconds());
        }
    }
}