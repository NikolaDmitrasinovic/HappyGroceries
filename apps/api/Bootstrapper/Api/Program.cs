using Inventory;
using Receipt;
using Spending;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddInventoryModule(builder.Configuration)
    .AddReceiptModule(builder.Configuration)
    .AddSpendingModule(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
app
    .UseInventoryModule();

app.Run();
