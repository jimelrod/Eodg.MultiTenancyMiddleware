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
        private string _accountIdHeaderKey;
        private readonly RequestDelegate _next;

        /// <summary>
        /// CTOR
        /// </summary>
        /// <param name="next">The request delegate</param>
        /// <param name="options">`MultiTenancyMiddlewareOptions` instance</param>
        public MultiTenancyMiddleware(RequestDelegate next, MultiTenancyMiddlewareOptions options)
        {
            if (options == null)
            {
                var innerException = new ArgumentNullException("`options` parameter cannot be null.");
                throw new MultiTenancyMiddlewareException("Error in MultiTenancyMiddleware Library. See Inner Exception for details.", innerException);
            }
            else if (options.UseHeaderKey)
            {
                if (string.IsNullOrEmpty(options.TenantIdHeaderKey))
                {
                    var innerException = new ArgumentException("`options` parameter must contain a value for `HeaderKey` property when `UseHeaderKey` value is `true`.");
                    throw new MultiTenancyMiddlewareException("Error in MultiTenancyMiddleware Library. See Inner Exception for details.", innerException);
                }

                _accountIdHeaderKey = options.TenantIdHeaderKey;
            }

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
                // Whenever this method gets invoked, we expect a header with a key of `_accountIdHeaderKey`
                if (!context.Request.Headers.ContainsKey(_accountIdHeaderKey))
                {
                    // Kill it if it ain't there!
                    throw new MultiTenancyMiddlewareException($"{_accountIdHeaderKey} header not supplied.");
                }

                // The "keyed" `Headers` property returns an array of comma-seperated values...
                //  But since we have only a single value, we're going to use this magic `0`
                var accountId = context.Request.Headers[_accountIdHeaderKey][0];

                try
                {
                    multiTenancyResolverService.SetConnectionStringByAccountId(accountId);
                }
                catch(Exception ex)
                {
                    throw new MultiTenancyMiddlewareException("Unable to set connection string. See Inner Exception for details...", ex);
                }
                
                // Go on to the next piece of midleware in the pipeline
                await _next(context);
            }

            // TODO: Actually figure out exception handling

            // This let's folks know it's something fucked up on the middleware's end... I think...
            catch (MultiTenancyMiddlewareException ex)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync($"Error parsing account id... {ex.ToString(true)} ");

                return;
            }
            // A veritable "Catch-All" - see what I did there?
            catch (Exception ex)
            {
                // Just assume it's a bad request...
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync($"Account ID not found (possibly... here's what happened in the exception)...{ex.ToString(true)} ");

                return;
            }
        }
    }
}
