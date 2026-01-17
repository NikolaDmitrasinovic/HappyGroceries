using System.Net.Sockets;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddInventoryModule(builder.Configuration)
    .AddReceiptModule(builder.Configuration)
    .AddSpendingModule(builder.Configuration);

builder.Services.AddMediator();
builder.Services.AddScoped<ICurrentUser, SystemCurrentUser>();
builder.Services.AddSingleton<IClock, SystemClock>();

var app = builder.Build();

// Proof of concept - conainerized postgres reachable
var host = builder.Configuration["Db:Host"]!;
var port = int.Parse(builder.Configuration["Db:Port"]!);

using var tcpClient = new TcpClient(host, port);

app.MapGet("/health", () => Results.Ok("API is up and DB port is reachable"));
//

// Configure the HTTP request pipeline.
app
    .UseInventoryModule()
    .UseReceiptModule()
    .UseSpendingModule();

app.Run();
