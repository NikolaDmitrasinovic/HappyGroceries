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
}
