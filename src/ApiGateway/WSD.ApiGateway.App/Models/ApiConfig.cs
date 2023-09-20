namespace WSD.ApiGateway.App.Models
{
    /// <summary>
    /// Api config values 
    /// </summary>
    public class ApiConfig
    {
        public string ApiPath { get; set; } = string.Empty;
        public string ApiScopes { get; set; } = string.Empty;
        public string ApiAudience { get; set; } = string.Empty;
    }
}