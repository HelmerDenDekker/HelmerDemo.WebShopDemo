using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using WSD.ApiGateway.App.Models;
using WSD.Common.Extensions;

namespace WSD.ApiGateway.App.Handlers
{
    public static class LogoutHandler
    {
        /// <summary>
        /// Handle logout
        /// </summary>
        /// <param name="context"></param>
        /// <param name="config"></param>
        public static void HandleLogout(RedirectContext context, GatewayConfig config)
        {
            if (!config.LogoutUrl.IsNullOrEmpty())
            {
                var req = context.Request;
                var gatewayUrl = Uri.EscapeDataString(req.Scheme + "://" + req.Host + req.PathBase);

                var logoutUri = config.LogoutUrl
                                        .Replace("{authority}", config.Authority)
                                        .Replace("{clientId}", config.ClientId)
                                        .Replace("{gatewayUrl}", gatewayUrl);

                context.Response.Redirect(logoutUri);
                context.HandleResponse();
            }
            //Todo What if null or empty?
        }
    }
}