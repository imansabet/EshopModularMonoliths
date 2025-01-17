using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Data.Interceptors;


namespace Catalog;

public static class CatalogModule
{
    public static IServiceCollection AddCatalogModule(this IServiceCollection services,IConfiguration configuration) 
    {
        // api endpoint services

        // application use case services
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
        });
        // data-infrastructure services
        var connectionString = configuration.GetConnectionString("Database");

        services.AddScoped<ISaveChangesInterceptor,AuditableEntityInterceptor>();
        services.AddScoped<ISaveChangesInterceptor,DispatchDomainEventsInterceptor>();

        services.AddDbContext<CatalogDbContext>((sp, options) =>
        {
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
            options.UseNpgsql(connectionString);
        });    

        services.AddScoped<IDataSeeder, CatalogDataSeeder>();

        return services;
    }
    public static IApplicationBuilder UseCatalogModule(this IApplicationBuilder app) 
    {

        //InitialiseDatabaseAsync(app).GetAwaiter().GetResult();
        app.UseMigration<CatalogDbContext>();
        return app;
    }

    //private static async Task InitialiseDatabaseAsync(IApplicationBuilder app)
    //{
    //    using var scope = app.ApplicationServices.CreateScope();
    //    var context = scope.ServiceProvider.GetRequiredService<CatalogDbContext>();
    //    await context.Database.MigrateAsync();
    //}
}
 