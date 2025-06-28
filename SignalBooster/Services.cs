using Application.ProcessSignalBoosterFile;
using Application.ProcessSignalBoosterFile.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace SignalBooster
{
    public static class Services
    {
        public static void AddServices(ServiceCollection services)
        {
            services.AddSingleton<ISignalBooster, ReadFile>()
                .Decorate<ISignalBooster, ReadDevice>()
                .Decorate<ISignalBooster, ReadAddOns>()
                .Decorate<ISignalBooster, ReadQualifier>()
                .Decorate<ISignalBooster, ReadOrderingProvider>()
                .Decorate<ISignalBooster, BuildJson>()
                .Decorate<ISignalBooster, SendPatientOrder>();

            services.AddHttpClient("SignalBooster", (client) =>
            {
                // Configure the HTTP client here
                client.BaseAddress = new Uri("https://alert-api.com");
            });

        }
    }
}
