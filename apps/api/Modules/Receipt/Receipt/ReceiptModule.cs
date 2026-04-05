using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Receipt.Application.Features.AddLineToReceipt;
using Receipt.Application.Features.OpenPurchaseReceipt;
using Shared.Data;
using Shared.Data.Interceptors;

namespace Receipt;

public static class ReceiptModule
{
    public static IServiceCollection AddReceiptModule(this IServiceCollection services, IConfiguration configuration)
    {
        // Add services to the container.

        // Api Endpoint services

        // Application Use Case services
        services.AddScoped<IRequestValidator<OpenPurchaseReceiptCommand>, OpenPurchaseReceiptValidator>();
        services.AddScoped<IRequestValidator<AddLineToReceiptCommand>, AddLineToReceiptValidator>();

        services.AddScoped<IRequestHandler<OpenPurchaseReceiptCommand, OpenPurchaseReceiptResult>, OpenPurchaseReceiptHandler>();
        services.AddScoped<IRequestHandler<AddLineToReceiptCommand, AddLineToReceiptResult>, AddLineToReceiptHandler>();

        // Data - Infrastructure services
        var connectionString = configuration.GetConnectionString("Default");

        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

        services.AddDbContext<ReceiptDbContext>((sp, options) =>
        {
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
            options.UseNpgsql(connectionString);
        });

        return services;
    }

    public static IApplicationBuilder UseReceiptModule(this IApplicationBuilder app)
    {
        // Configure the HTTP request pipeline

        // Use Api Endpoint services

        // Use Application Use Case services

        // Use Data - Infrastructure services
        app.UseMigration<ReceiptDbContext>();

        return app;
    }
}
