using System;

namespace Jelly.MultiTenantExample.MultiTenancyMiddleware
{
    /// <summary>
    /// Standard run-of-the-mill `Exception`
    /// All exceptions from this library should throw this
    /// </summary>
    public class MultiTenancyMiddlewareException : Exception
    {
        public MultiTenancyMiddlewareException()
        {
        }

        public MultiTenancyMiddlewareException(string message)
            : base(message)
        {
        }

        public MultiTenancyMiddlewareException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
