namespace WSD.ApiGateway.App.Models
{
    /// <summary>
    /// Gets or sets the token exchange response
    /// </summary>
    public class TokenExchangeResponse
    {
        public string AccessToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public long ExpiresIn { get; set; }
    }
}