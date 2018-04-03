namespace Eodg.MultiTenancy.Middleware
{
    /// <summary>
    /// Abstract class to be implemented to allow the middleware to utilize multitenancy
    /// </summary>
    public abstract class MultiTenancyResolverService : IMultiTenancyResolverService
    {
        /// <summary>
        /// Houses the connection string
        /// </summary>
        public abstract string ConnectionString { get; }

        /// <summary>
        /// Implementation needs to set the `ConnectionString` property.
        /// </summary>
        /// <param name="accountId">Account Id of the tenant database</param>
        public abstract void SetConnectionStringByAccountId(string accountId);
    }
}
