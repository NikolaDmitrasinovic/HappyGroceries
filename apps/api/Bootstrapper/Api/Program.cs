using Inventory;
using Receipt;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddInventoryModule(builder.Configuration)
    .AddReceiptModule(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.

app.Run();
