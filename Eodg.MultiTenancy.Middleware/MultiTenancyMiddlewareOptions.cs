namespace Eodg.MultiTenancy.Middleware
{
    public class MultiTenancyMiddlewareOptions
    {
        public string TenantIdHeaderKey { get; set; }
        public bool UseHeaderKey { get; set; }
    }
}
