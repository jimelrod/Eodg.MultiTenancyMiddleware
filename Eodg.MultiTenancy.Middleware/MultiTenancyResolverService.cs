using System.Collections.Generic;

namespace Eodg.MultiTenancy.Middleware
{
    /// <summary>
    /// Abstract class to be implemented to allow the middleware to utilize multitenancy
    /// </summary>
    public abstract class MultiTenancyResolverService : IMultiTenancyResolverService
    {
        /// <summary>
        /// Houses the connection strings
        /// </summary>
        public abstract Dictionary<string, string> ConnectionStringsByName { get; protected set; }

        /// <summary>
        /// Implementation to set the `ConnectionStringsByName` values.
        /// </summary>
        /// <param name="accountId">Account Id of the tenant database</param>
        public abstract void SetConnectionStrings(string accountId);

        /// <summary>
        /// Implementation to set the `ConnectionStringsByName` values.
        /// </summary>
        public abstract void SetConnectionStrings();
    }
}
