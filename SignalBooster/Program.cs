using Application.ProcessSignalBoosterFile.Interfaces;
using Application.ProcessSignalBoosterFile.Requests;
using Domain.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using SignalBooster;

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .Build();

var services = new ServiceCollection();

SignalBoosterOptions options = new();
configuration.GetSection(nameof(SignalBoosterOptions))
    .Bind(options);

Services.AddServices(services);

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(configuration)
    .CreateLogger();

var serviceProvider = services.BuildServiceProvider();

var signalBooster = serviceProvider.GetRequiredService<ISignalBooster>();

try
{
    Log.Information("Starting Signal Booster processing...");

    var result = await signalBooster.Process(new SignalBoosterRequest(options.PhysicianFileName));

    if (result.IsSuccess)
    {
        Log.Information("File processed successfully.");
    }
    else
    {
        Log.Warning($"Error processing file: {result.Error}");
    }
}
catch (Exception ex)
{
	Log.Error(ex, "Signal Booster Error");
}
finally
{
    Log.CloseAndFlush();
}

