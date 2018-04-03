# MultiTenancyMiddleware
###### It's a little black box... kind of

This library allows you to incorporate multitenancy into a .net core 2.0 web api project. After setting up a handful of things (outlined below), you will be able to make a request, and by adding a header to said request containing the account id of the tenant for whose data you want to access you can get said data.

## How do you implement it?
* Write a concrete implementation of the provided `MultiTenancyResolverService` abstract class / `IMultiTenancyResolverService` interface. 
   * There only has to be a `ConnectionString` property and a `SetConnectionStringByAccountId(string accountId)` method that sets the `ConnectionString` property.
* In the `Startup.cs` file, we'll have to do a couple of things:
   * Add your implementation to the built-in DI container a la: 
     
     `services.AddScoped<IMultiTenancyResolverService, ExampleMultiTenancyResolverService>();`

     where `ExampleMultiTenancyResolverService` is the name of your concrete implementation of the provided `MultiTenancyResolverService` abstract class / `IMultiTenancyResolverService` interface
   * Add the options for the middleware. This only contains a `HeaderKey` and this will be the header key used in the request that contains the account id of the tenant whose database you want to reference for the particular request.
     
     ```
     var multiTenancyMiddlewareOptions = new MultiTenancyMiddlewareOptions
     {
         TenantIdHeaderKey = "Account-Id"
     };

     services.AddSingleton(multiTenancyMiddlewareOptions);
     ```
* Database context class for the tenant template database:
   * Inject the `IMultiTenancyResolver` into the constructor, and store in private variable:
    
    `private IMultiTenancyResolverService _multiTenancyResolverService;`
   * Add the following method:
     ```C#
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_multiTenancyResolverService.ConnectionString);
            base.OnConfiguring(optionsBuilder);
        }
     ```

## How do you use it?
* For any controller/method utilizing multitenancy add the following attribute
  
  `[MiddlewareFilter(typeof(MultiTenancyPipeline))]`

* Any time a request is made to said controllers/methods, a header will have to exist on the request that has the key specified in the `Startup.cs` class, and will have to have a value of the account id.
