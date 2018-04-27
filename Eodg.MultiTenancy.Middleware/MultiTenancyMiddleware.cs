using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace Eodg.MultiTenancy.Middleware
{
    /// <summary>
    /// `MultiTenancyMiddleware` - This is the entry and exit
    /// https://docs.microsoft.com/en-us/aspnet/core/fundamentals/middleware/?tabs=aspnetcore2x#writing-middleware
    /// </summary>
    public class MultiTenancyMiddleware
    {
        private MultiTenancyMiddlewareOptions _options;
        private readonly RequestDelegate _next;

        /// <summary>
        /// CTOR
        /// </summary>
        /// <param name="next">The request delegate</param>
        /// <param name="options">`MultiTenancyMiddlewareOptions` instance</param>
        public MultiTenancyMiddleware(RequestDelegate next, MultiTenancyMiddlewareOptions options)
        {
            Validate(options);
           
            _options = options;
            _next = next;
        }

        /// <summary>
        /// This gets called for every applicable request
        /// </summary>
        /// <param name="context">The HttpContext for the given request</param>
        /// <param name="multiTenancyResolverService">DI takes care of this</param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext context, IMultiTenancyResolverService multiTenancyResolverService)
        {
            try
            {
                // The "keyed" `Headers` property returns an array of comma-seperated values...
                //  But since we should have only a single value, we're going to use this magic `0`
                var accountId = context.Request.Headers[_options.TenantIdHeaderKey][0];

                // TODO: Validate account id....

                multiTenancyResolverService.ResolveConnectionStrings(accountId);
            }
            catch (Exception ex)
            {
                throw new MultiTenancyMiddlewareException("Unable to resolve connection string(s). See Inner Exception for details...", ex);
            }

            // Go on to the next piece of midleware in the pipeline
            await _next(context);
        }

        /// <summary>
        /// Validates the `options` parameter of the Constructor
        /// </summary>
        /// <param name="options">options to validate</param>
        private static void Validate(MultiTenancyMiddlewareOptions options)
        {
            if (options == null)
            {
                var innerException = new ArgumentNullException("`options` parameter cannot be null.");
                throw new MultiTenancyMiddlewareException("Error in MultiTenancyMiddleware Library. See Inner Exception for details.", innerException);
            }
            else if (string.IsNullOrEmpty(options.TenantIdHeaderKey))
            {
                var innerException = new ArgumentException("`options` parameter must contain a value for `HeaderKey` property when `UseHeaderKey` value is `true`.");
                throw new MultiTenancyMiddlewareException("Error in MultiTenancyMiddleware Library. See Inner Exception for details.", innerException);
            }
        }
    }
}
