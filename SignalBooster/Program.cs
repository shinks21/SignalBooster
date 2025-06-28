using Application.ProcessSignalBoosterFile.Interfaces;
using Application.ProcessSignalBoosterFile.Requests;
using Domain.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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

var serviceProvider = services.BuildServiceProvider();

var signalBooster = serviceProvider.GetRequiredService<ISignalBooster>();

var result = await signalBooster.Process(new SignalBoosterRequest(options.PhysicianFileName));

if (result.IsSuccess)
{
    Console.WriteLine("File processed successfully.");
}
else
{
    Console.WriteLine($"Error processing file: {result.Error}");
}
