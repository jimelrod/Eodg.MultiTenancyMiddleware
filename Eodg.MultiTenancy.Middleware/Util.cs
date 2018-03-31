using System;

namespace Jelly.MultiTenantExample.MultiTenancyMiddleware
{
    /// <summary>
    /// Static utility/extension class
    /// </summary>
    public static class Util
    {
        /// <summary>
        /// Extension method for `Exception.ToString()` that allows for
        /// `InnerException` messages to be included
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="isVerbose">
        ///     `bool` flag specifying whether or not to include 
        ///     `InnerException` messages
        /// </param>
        /// <returns></returns>
        public static string ToString(this Exception ex, bool isVerbose = false)
        {
            if (!isVerbose)
            {
                return ex.ToString();
            }

            var exceptionMessage = ex.Message;

            while(ex.InnerException != null)
            {
                ex = ex.InnerException;

                exceptionMessage += $"\r\n{ex.Message}";
            }

            return exceptionMessage;
        }
    }
}
