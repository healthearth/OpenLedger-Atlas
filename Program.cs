if (args.Contains("--simulate"))
{
    using var scope = app.Services.CreateScope();
    var simulator = scope.ServiceProvider.GetRequiredService<TransactionSimulator>();

    await simulator.GenerateAsync(10000);

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
    var reconcile = scope.ServiceProvider.GetRequiredService<ReconciliationService>();
    reconcile.Run();

    Console.WriteLine("Reconciliation complete.");
    return;
}
