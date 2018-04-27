using System.Collections.Generic;
using System.Linq;

namespace Eodg.MultiTenancy.Middleware
{
    /// <summary>
    /// Abstract class to be implemented to allow the middleware to utilize multitenancy
    /// </summary>
    public abstract class MultiTenancyResolverService
    {
        /// <summary>
        /// Houses the connection strings
        /// </summary>
        private Dictionary<string, string> _connectionStringsByName;

        protected MultiTenancyResolverService()
        {
            _connectionStringsByName = new Dictionary<string, string>();
        }

        public abstract void ResolveConnectionStrings(string accountId);

        protected void AddConnectionString(string key, string connectionString)
        {
            _connectionStringsByName.Add(key, connectionString);
        }

        protected string GetConnectionString(string key)
        {
            return _connectionStringsByName[key];
        }

        protected IList<string> GetGetAllConnectionStringKeys()
        {
            return _connectionStringsByName.Keys.ToList();
        }
    }
}
