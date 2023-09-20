namespace WSD.ApiGateway.App.Models
{
    /// <summary>
    /// Gets or sets the token exchange refresh response
    /// </summary>
    public class RefreshResponse : TokenExchangeResponse
    {
        public string IdToken { get; set; } = string.Empty;
    }
}