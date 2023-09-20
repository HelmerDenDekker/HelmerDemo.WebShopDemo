using WSD.ApiGateway.App.Models;

namespace WSD.ApiGateway.App.Strategies
{
    /// <summary>
    /// Allows to exchange the access token 
    /// </summary>
    public interface ITokenExchangeStrategy
    {
        Task<TokenExchangeResponse> Exchange(string accessToken, ApiConfig apiConfig);
    }
}