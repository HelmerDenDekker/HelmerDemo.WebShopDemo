namespace WSD.Common.Tools.Constants
{
    public static class OpenIdConnectConstants
    {
        public static class Claims
        {
            public const string UserGuid = "user_guid";

            public const string SubjectId = "sid";
        }

        public static class Schemes
        {
            public const string OpenIdConnect = "oidc";

            public const string Cookies = "Cookies";
        }

        public static class Session
        {
            public const string ExpiresAt = "expires_at";
        }

        public static class Tokens
        {
            public const string IdToken = "id_token";

            public const string AccessToken = "access_token";

            public const string RefreshToken = "refresh_token";

            /// <summary>
            /// I have no idea what this is doing here, please replace!!
            /// </summary>
            public const string ApiAccessToken = "api_access_token";
        }

        public static class HeaderKeys
        {
            public const string Authorization = "Authorization";
        }

        public static class TokenTypes
        {
            public const string JwtAccessToken = "at+jwt";
        }

        public static class HttpMethods
        {
            public const string Get = "GET";

            public const string Post = "POST";

            public const string Put = "PUT";

            public const string Delete = "DELETE";
        }
    }
}
