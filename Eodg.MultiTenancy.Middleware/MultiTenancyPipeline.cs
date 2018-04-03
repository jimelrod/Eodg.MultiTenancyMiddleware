using Microsoft.AspNetCore.Builder;

namespace Eodg.MultiTenancy.Middleware
{
    /// <summary>
    /// Pipeline class to allow for middleware to be applied
    /// controller-wide or just for a particular method.
    /// 
    /// How-to use - 
    ///     Add the following attribute above the controller/method
    ///     you want to apply the middleware to:
    ///     
    ///     `[MiddlewareFilter(typeof(MultiTenancyPipeline))]`
    /// </summary>
    public class MultiTenancyPipeline
    {
        /// <summary>
        /// Implements MiltiTenancy middleware for a given request
        /// </summary>
        /// <param name="app">`IApplicationBuilder` instance</param>
        /// <param name="options">`MultiTenancyMiddlewareOptions` instance</param>
        public void Configure(IApplicationBuilder app, MultiTenancyMiddlewareOptions options)
        {
            app.UseMultiTenancy(options);
        }
    }
}
