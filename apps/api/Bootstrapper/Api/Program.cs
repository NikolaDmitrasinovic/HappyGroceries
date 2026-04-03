using Api.Middleware;
using Asp.Versioning;
using System.Net.Sockets;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .AddApplicationPart(typeof(InventoryModule).Assembly)
    .AddApplicationPart(typeof(ReceiptModule).Assembly);

builder.Services
    .AddInventoryModule(builder.Configuration)
    .AddReceiptModule(builder.Configuration)
    .AddSpendingModule(builder.Configuration);

builder.Services.AddMediator();
builder.Services.AddScoped<ICurrentUser, SystemCurrentUser>();
builder.Services.AddSingleton<IClock, SystemClock>();

builder.Services
    .AddApiVersioning(options =>
    {
        options.DefaultApiVersion = new ApiVersion(1, 0);
        options.AssumeDefaultVersionWhenUnspecified = true;
        options.ReportApiVersions = true;
    })
    .AddMvc();

var app = builder.Build();

// Proof of concept - conainerized postgres reachable
var host = builder.Configuration["Db:Host"]!;
var port = int.Parse(builder.Configuration["Db:Port"]!);

using var tcpClient = new TcpClient(host, port);

app.MapGet("/health", () => Results.Ok("API is up and DB port is reachable"));
//

// Configure the HTTP request pipeline.
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.MapControllers();

app
    .UseInventoryModule()
    .UseReceiptModule()
    .UseSpendingModule();

app.Run();
