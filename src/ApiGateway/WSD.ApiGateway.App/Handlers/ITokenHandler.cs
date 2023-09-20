using Microsoft.AspNetCore.Authentication.OpenIdConnect;

namespace WSD.ApiGateway.App.Handlers
{
    public interface ITokenHandler
    {
        /// <summary>
        /// HandleToken
        /// </summary>
        /// <param name="context">Token validated context</param>
        void HandleToken(TokenValidatedContext context);
    }
}