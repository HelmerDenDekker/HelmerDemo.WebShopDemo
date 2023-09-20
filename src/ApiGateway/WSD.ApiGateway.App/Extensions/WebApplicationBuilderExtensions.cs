using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using WSD.ApiGateway.App.Handlers;
using WSD.ApiGateway.App.Models;
using WSD.ApiGateway.App.Services;
using WSD.ApiGateway.App.Strategies;
using WSD.Common.Extensions;
using WSD.Common.Tools.Exceptions;

namespace WSD.ApiGateway.App.Extensions
{
    public static class WebApplicationBuilderExtensions
    {
        private static readonly string _gatewayConfig = "GATEWAY_CONFIG";

        /// <summary>
        /// Add Json config files
        /// </summary>
        /// <param name="builder"></param>
        public static void AddConfigFiles(this WebApplicationBuilder builder)
        {
            var envConfig = Environment.GetEnvironmentVariable(_gatewayConfig);
            var cmdLineArgs = Environment.GetCommandLineArgs();

            if (cmdLineArgs != null && cmdLineArgs.Count() > 1)
            {
                builder.Configuration.AddJsonFile(cmdLineArgs[1], false, true);
            }
            else if (envConfig != null)
            {
                builder.Configuration.AddJsonFile(envConfig, false, true);
            }
        }

        /// <summary>
        /// Add gateway to application builder
        /// </summary>
        /// <param name="builder">Web application builder</param>
        /// <param name="config">Gateway configuration</param>
        /// <param name="discoveryResponse">Discovery document response</param>
        public static void AddGateway(this WebApplicationBuilder builder, GatewayConfig config, DiscoveryDocumentResponse discoveryResponse)
        {
            builder.Services.AddReverseProxy()
                .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

            builder.Services.AddSingleton(config);
            builder.Services.AddSingleton<DiscoveryDocumentResponse>(discoveryResponse);

            builder.Services.AddHttpClient("TokenRefreshService");
            builder.Services.AddSingleton<TokenRefreshService>();
            builder.AddTokenExchangeService(config);

            builder.Services.AddSingleton<ApiTokenService>();
            builder.Services.AddSingleton<GatewayService>();
            builder.Services.AddSingleton<TokenHandler>();

            var sessionTimeoutInMin = config.SessionTimeoutInMin;
            builder.Services.AddSession(options => options.IdleTimeout = TimeSpan.FromMinutes(sessionTimeoutInMin));

            builder.Services.AddAntiforgery(setup => setup.HeaderName = "X-XSRF-TOKEN");

            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("authPolicy", policy =>
                {
                    policy.RequireAuthenticatedUser();
                });
            });

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            })
            .AddCookie(setup =>
            {
                setup.ExpireTimeSpan = TimeSpan.FromMinutes(sessionTimeoutInMin);
                setup.SlidingExpiration = true;
            })
            .AddOpenIdConnect(options =>
            {
                options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.Authority = config.Authority;
                options.ClientId = config.ClientId;
                options.UsePkce = true;
                options.ClientSecret = config.ClientSecret;
                options.ResponseType = OpenIdConnectResponseType.Code;
                options.SaveTokens = false;
                options.GetClaimsFromUserInfoEndpoint = config.QueryUserInfoEndpoint;
                options.CorrelationCookie.SecurePolicy = CookieSecurePolicy.Always;
                options.NonceCookie.SecurePolicy = CookieSecurePolicy.Always;
                options.RequireHttpsMetadata = false; //TODO set to true for production

                var scopes = config.Scopes;
                var scopeArray = scopes.Split(" ");
                foreach (var scope in scopeArray)
                {
                    options.Scope.Add(scope);
                }

                options.Events.OnTokenValidated = async (context) =>
                {
                    var tokenHandler = context.HttpContext.RequestServices.GetRequiredService<TokenHandler>();
                    tokenHandler.HandleToken(context);
                };

                options.Events.OnRedirectToIdentityProviderForSignOut = (context) =>
                {
                    LogoutHandler.HandleLogout(context, config);
                    return Task.CompletedTask;
                };
            });
        }

        /// <summary>
        /// Add token exchange service
        /// </summary>
        /// <param name="builder">WebApplication builder</param>
        /// <param name="config"><Gateway configuration/param>
        /// <exception cref="ArgumentException">If strategy isn't supported throw argument exception</exception>
        private static void AddTokenExchangeService(this WebApplicationBuilder builder, GatewayConfig config)
        {
            var strategy = config.TokenExchangeStrategy;
            if (strategy.IsNullOrEmpty())
            {
                strategy = "none";
            }

            switch (strategy.ToLower())
            {
                case "none":
                    builder.Services.AddSingleton<ITokenExchangeStrategy, NullTokenExchangeStrategy>();
                    break;

                case "azuread":
                    builder.Services.AddSingleton<ITokenExchangeStrategy, AzureAdTokenExchangeStrategy>();
                    break;

                case "default":
                    builder.Services.AddHttpClient("TokenExchangeClient");
                    builder.Services.AddSingleton<ITokenExchangeStrategy, TokenExchangeStrategy>();
                    break;

                default:
                    throw new ConfigurationValueMissingException($"Unsupported TokenExchangeStrategy in config found: {config.TokenExchangeStrategy}. Possible values: none, AzureAd, default");
            }
        }
    }
}