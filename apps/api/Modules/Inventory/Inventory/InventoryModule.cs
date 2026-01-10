using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Data;

namespace Inventory;

public static class InventoryModule
{
    public static IServiceCollection AddInventoryModule(this IServiceCollection services, IConfiguration configuration)
    {
        // Add services to the container.

        // Api Endpoint services

        // Application Use Case services

        // Data - Infrastructure services
        var connectionString = configuration.GetConnectionString("Default");

        services.AddDbContext<InventoryDbContext>(options =>
        options.UseNpgsql(connectionString));

        services.AddScoped<IDataSeeder, InventoryDataSeeder>();

        return services;
    }

    public static IApplicationBuilder UseInventoryModule(this IApplicationBuilder app)
    {
        // Configure the HTTP request pipeline

        // Use Api Endpoint services

        // Use Application Use Case services

        // Use Data - Infrastructure services
        app.UseMigration<InventoryDbContext>();

        SeedDatabaseAsync(app.ApplicationServices).GetAwaiter().GetResult();

        return app;
    }

    private static async Task SeedDatabaseAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();

        var seeders = scope.ServiceProvider.GetServices<IDataSeeder>();
        foreach (var seeder in seeders)
        {
            await seeder.SeedAllAsync();
        }
    }
}
