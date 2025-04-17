using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Radzen;

namespace VintageCashCowTechTestUI.Client
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            builder.Services.AddRadzenComponents();

            builder.Services.AddClientServices(builder.HostEnvironment);

            await builder.Build().RunAsync();
        }
    }
}
