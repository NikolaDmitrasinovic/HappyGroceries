using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Receipt;

public static class ReceiptModule
{
    public static IServiceCollection AddReceiptModule(this IServiceCollection services, IConfiguration configuration)
    {
        //services
        //    .AddApplicationServices()
        //    .AddInfrastructureServices(configuration)
        //    .AddApiServices(services);

        return services;
    }

    public static IApplicationBuilder UseReceiptModule(this IApplicationBuilder app)
    {
        // HTTP request pipeline
        //app
        //    .UseApplicationServices()
        //    .UseInfrastructureServices()
        //    .UseApiServices();

        return app;
    }
}
