using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Spending;

public static class SpendingModule
{
    public static IServiceCollection AddSpendingModule(this IServiceCollection services, IConfiguration configuration)
    {
        //services
        //    .AddApplicationServices()
        //    .AddInfrastructureServices(configuration)
        //    .AddApiServices(services);

        return services;
    }
}
