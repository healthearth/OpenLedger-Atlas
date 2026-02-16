using OpenLedgerAtlas.Infrastructure.Data;
using OpenLedgerAtlas.Application.Services;
using OpenLedgerAtlas.Simulation_Engine;
using OpenLedgerAtlas.Forecasting;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// --------------------------------------------
// 1. Register DbContext
// --------------------------------------------
builder.Services.AddDbContext<OpenLedgerDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// --------------------------------------------
// 2. Register Application Services
// --------------------------------------------
builder.Services.AddScoped<TransferService>();
builder.Services.AddScoped<CapitalRegistrationService>();
builder.Services.AddScoped<MerchantSettlementService>();
builder.Services.AddScoped<ReconciliationService>();   
builder.Services.AddScoped<IBankingOperations, BankingOperations>(); // ← NEW LINE

// --------------------------------------------
// 3. Register Simulation / Engine Services
// --------------------------------------------
builder.Services.AddScoped<TransactionSimulator>();
builder.Services.AddScoped<FraudScenarioGenerator>();

// --------------------------------------------
// 4. Add Controllers / API
// --------------------------------------------
builder.Services.AddControllers();

var app = builder.Build();

// --------------------------------------------
// 5. Handle CLI flags BEFORE running the API
// --------------------------------------------
if (args.Contains("--simulate"))
{
    using var scope = app.Services.CreateScope();
    var simulator = scope.ServiceProvider.GetRequiredService<TransactionSimulator>();

    await simulator.GenerateAsync(new List <Guid>(), 10000);
    Console.WriteLine("Generated 10,000 transactions.");
    return;
}

if (args.Contains("--fraudburst"))
{
    using var scope = app.Services.CreateScope();
    var fraud = scope.ServiceProvider.GetRequiredService<FraudScenarioGenerator>();

    await fraud.TriggerBurstAsync(500);
    Console.WriteLine("Fraud spike triggered.");
    return;
}

if (args.Contains("--reconcile"))
{
    using var scope = app.Services.CreateScope();
    var reconcile = scope.ServiceProvider.GetRequiredService<ReconciliationService>();

    reconcile.Run();
    Console.WriteLine("Reconciliation complete.");
    return;
}

// --------------------------------------------
// 6. Normal API pipeline
// --------------------------------------------
app.MapControllers();
app.Run();
