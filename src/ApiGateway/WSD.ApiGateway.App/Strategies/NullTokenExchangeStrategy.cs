using WSD.ApiGateway.App.Models;

namespace WSD.ApiGateway.App.Strategies
{
    public class NullTokenExchangeStrategy : ITokenExchangeStrategy
    {
        /// <inheritdoc/>
        public Task<TokenExchangeResponse> Exchange(string accessToken, ApiConfig apiConfig)
        {
            var result = new TokenExchangeResponse
            {
                AccessToken = string.Empty,
                ExpiresIn = 0,
                RefreshToken = string.Empty,
            };

            return Task.FromResult(result);
        }
    }
}