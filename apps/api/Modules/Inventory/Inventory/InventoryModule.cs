using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Inventory;

public static class InventoryModule
{
    public static IServiceCollection AddInventoryModule(this IServiceCollection services, IConfiguration configuration)
    {
        //services
        //    .AddApplicationServices()
        //    .AddInfrastructureServices(configuration)
        //    .AddApiServices(services);

        return services;
    }

    public static IApplicationBuilder UseInventoryModule(this IApplicationBuilder app)
    {
        // HTTP request pipeline
        //app
        //    .UseApplicationServices()
        //    .UseInfrastructureServices()
        //    .UseApiServices();

        return app;
    }
}
