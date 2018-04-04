using System.Collections.Generic;

namespace Eodg.MultiTenancy.Middleware
{
    public interface IMultiTenancyResolverService
    {
        /// <summary>
        /// Houses the connection strings
        /// </summary>
        Dictionary<string, string> ConnectionStringsByName { get; }

        /// <summary>
        /// Implementation to set the `ConnectionStringsByName` values.
        /// </summary>
        /// <param name="accountId">Account Id of the tenant database</param>
        void SetConnectionStrings(string accountId);

        /// <summary>
        /// Implementation to set the `ConnectionStringsByName` values.
        /// </summary>
        void SetConnectionStrings();
    }
}
