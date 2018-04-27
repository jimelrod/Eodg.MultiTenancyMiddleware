namespace Eodg.MultiTenancy.Middleware
{
    public interface IMultiTenancyResolverService
    {
        /// <summary>
        /// Resolves all connection strings for the supplied `accountId`
        /// </summary>
        /// <param name="accountId">Account Id of the tenant database</param>
        void ResolveConnectionStrings(string accountId);
    }
}
