using WSD.ApiGateway.App.Services;

namespace WSD.ApiGateway.App.Extensions
{
    public static class ReverseProxyApplicationBuilderExtension
    {
        /// <summary>
        /// Adds the GatewayService to the pipeline //ToDo rename!
        /// </summary> 
        /// <param name="pipeline"></param>
        public static void UseGatewayPipeline(this IReverseProxyApplicationBuilder pipeline)
        {
            var gatewayService = pipeline.ApplicationServices.GetRequiredService<GatewayService>();

            pipeline.Use(async (httpContext, next) =>
            {
                await gatewayService.AddToken(httpContext);
                await next().ConfigureAwait(false);
            });
        }
    }
}