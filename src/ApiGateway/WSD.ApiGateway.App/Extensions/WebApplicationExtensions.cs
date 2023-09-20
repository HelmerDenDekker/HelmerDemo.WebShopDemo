using System.Security.Claims;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using WSD.ApiGateway.App.Exceptions;
using WSD.ApiGateway.App.Models;
using WSD.Common.Extensions;

namespace WSD.ApiGateway.App.Extensions
{
    public static class WebApplicationExtensions
    {
        /// <summary>
        /// Add several gateway endpoints to the application
        /// </summary>
        /// <param name="app"></param>
        public static void UseGatewayEndpoints(this WebApplication app)
        {
            app.UseUserInfoEndpoint();
            app.UseLoginEndpoint();
            app.UseLogoutEndpoint();
            app.UseGatewayStatusEndpoint();
        }

        /// <summary>
        /// Use gateway in the webapplication
        /// </summary>
        /// <param name="app"></param>
        public static void UseGateway(this WebApplication app)
        {
            app.UseRouting();
            app.UseSession();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCookiePolicy();
            app.UseXsrfCookie();
            app.UseGatewayEndpoints();
            app.UseYarp();
        }

        /// <summary>
        /// Use XSRF cookie to prevent cross-site request forgery
        /// </summary>
        /// <param name="app"></param>
        public static void UseXsrfCookie(this WebApplication app)
        {
            app.UseXsrfCookieCreator();
            app.UseXsrfCookieChecks();
        }

        /// <summary>
        /// Create the XSRF cookie
        /// </summary>
        /// <param name="app"></param>
        /// <exception cref="Exception"></exception>
        public static void UseXsrfCookieCreator(this WebApplication app)
        {
            app.Use(async (httpContext, next) =>
            {
                var antiforgery = app.Services.GetService<IAntiforgery>();

                if (antiforgery == null)
                {
                    throw new ServiceMissingException("IAntiforgery service expected!");
                }

                var tokens = antiforgery!.GetAndStoreTokens(httpContext);

                if (tokens.RequestToken == null)
                {
                    throw new TokenMissingException("token expected!");
                }

                httpContext.Response.Cookies.Append("XSRF-TOKEN", tokens.RequestToken,
                            new CookieOptions() { HttpOnly = false });

                await next(httpContext);
            });
        }

        public static void UseXsrfCookieChecks(this WebApplication app)
        {
            var config = app.Services.GetRequiredService<GatewayConfig>();
            var apiConfigs = config.ApiConfigs;

            app.Use(async (httpContext, next) =>
            {
                var antiforgery = app.Services.GetService<IAntiforgery>();

                if (antiforgery == null)
                {
                    throw new ServiceMissingException("IAntiforgery service expected!");
                }

                var currentUrl = httpContext.Request.Path.ToString().ToLower();
                if (apiConfigs.Any(c => currentUrl.StartsWith(c.ApiPath))
                    && !await antiforgery.IsRequestValidAsync(httpContext))
                {
                    httpContext.Response.StatusCode = 400;
                    await httpContext.Response.WriteAsJsonAsync(new
                    {
                        Error = "XSRF token validation failed"
                    });
                    return;
                }

                await next(httpContext);
            });
        }

        private static void UseYarp(this WebApplication app)
        {
            app.MapReverseProxy(pipeline =>
            {
                pipeline.UseGatewayPipeline();
            });
        }

        private static void UseLogoutEndpoint(this WebApplication app)
        {
            app.MapGet("/logout", (string redirectUrl, HttpContext httpContext) =>
            {
                if (redirectUrl.IsNullOrEmpty())
                {
                    redirectUrl = "/";
                }

                httpContext.Session.Clear();

                var authProps = new AuthenticationProperties
                {
                    RedirectUri = redirectUrl
                };

                var authSchemes = new string[]
                {
            CookieAuthenticationDefaults.AuthenticationScheme,
            OpenIdConnectDefaults.AuthenticationScheme
                };

                return Results.SignOut(authProps, authSchemes);
            });
        }

        private static void UseLoginEndpoint(this WebApplication app)
        {
            app.MapGet("/login", (string? redirectUrl, HttpContext httpContext) =>
            {
                if (string.IsNullOrEmpty(redirectUrl))
                {
                    redirectUrl = "/";
                }

                httpContext.ChallengeAsync(OpenIdConnectDefaults.AuthenticationScheme, new AuthenticationProperties
                {
                    RedirectUri = redirectUrl
                });
            });
        }

        private static void UseUserInfoEndpoint(this WebApplication app)
        {
            app.MapGet("/userinfo", (ClaimsPrincipal user) =>
            {
                var claims = user.Claims;
                var dict = new Dictionary<string, string>();

                foreach (var entry in claims)
                {
                    dict[entry.Type] = entry.Value;
                }

                return dict;
            });
        }

        private static void UseGatewayStatusEndpoint(this WebApplication app)
        {
            app.MapGet("/gatewaystatus", (ClaimsPrincipal user) =>
            {
                var dict = new Dictionary<string, string>
                {
                { "version", "1.0.0" }
                };

                return dict;
            });
        }
    }
}