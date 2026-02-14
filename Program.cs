if (args.Contains("--simulate"))
{
    using var scope = app.Services.CreateScope();
    var simulator = scope.ServiceProvider.GetRequiredService<TransactionSimulator>();

    await simulator.GenerateAsync(10000);

    Console.WriteLine("Generated 10,000 transactions.");
    return;
}
