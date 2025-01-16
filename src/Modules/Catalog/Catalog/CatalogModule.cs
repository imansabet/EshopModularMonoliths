using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog;

public static class CatalogModule
{
    public static IServiceCollection AddCatalogModule(this IServiceCollection services,IConfiguration configuration) 
    {
        // api endpoint services

        // application use case services

        // data-infrastructure services
        var connectionString = configuration.GetConnectionString("Database");
        
        services.AddDbContext<CatalogDbContext>(options =>
            options.UseNpgsql(connectionString));


        return services;
    }
    public static IApplicationBuilder UseCatalogModule(this IApplicationBuilder app) 
    {
        return app;
    }
}
 