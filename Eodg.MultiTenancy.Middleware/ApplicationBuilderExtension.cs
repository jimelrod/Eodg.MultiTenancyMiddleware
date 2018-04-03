using Microsoft.AspNetCore.Builder;

namespace Eodg.MultiTenancy.Middleware
{
    /// <summary>
    /// Class to house extension method for use in the web application's `Startup.cs`
    /// </summary>
    public static class ApplicationBuilderExtension
    {
        /// <summary>
        /// Extension method allowing for use of `MultiTenancyMiddleware`
        /// </summary>
        /// <param name="builder">`IApplicationBuilder` instance</param>
        /// <param name="options">`MultiTenancyMiddlewareOptions` instance</param>
        /// <returns>`IApplicationBuilder` instance with `MultiTenancyMiddleware` enabled</returns>
        public static IApplicationBuilder UseMultiTenancy(this IApplicationBuilder builder, MultiTenancyMiddlewareOptions options)
        {
            builder.UseMiddleware<MultiTenancyMiddleware>(options);

            return builder;
        }
    }
}
