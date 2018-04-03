namespace Eodg.MultiTenancy.Middleware
{
    public interface IMultiTenancyResolverService
    {
        /// <summary>
        /// Houses the connection string
        /// </summary>
        string ConnectionString { get; }

        /// <summary>
        /// Sets the `ConnectionString` property.
        /// </summary>
        /// <param name="accountId">Account Id of the tenant database</param>
        void SetConnectionStringByAccountId(string accountId);
    }
}
