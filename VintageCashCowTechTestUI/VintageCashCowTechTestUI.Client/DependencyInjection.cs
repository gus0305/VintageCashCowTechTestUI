using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using VintageCashCowTechTestUI.Client.Mappers;
using VintageCashCowTechTestUI.Client.Services;
using VintageCashCowTechTestUI.Client.Services.Product;

namespace VintageCashCowTechTestUI.Client
{
    public static class DependencyInjection
    {
        public static void AddClientServices(this IServiceCollection serviceCollection, IWebAssemblyHostEnvironment hostEnvironment)
        {
            serviceCollection.AddScoped(sp =>
                new HttpClient()
                {                    
                    BaseAddress = new Uri(hostEnvironment.BaseAddress!)
                });

            serviceCollection.AddScoped<IHttpResponseMessageHandler, HttpResponseMessageHandler>();
            serviceCollection.AddScoped<IProductService, ProductService>();
            serviceCollection.AddScoped<IProductPriceHistoryViewModelMapper, ProductPriceHistoryViewModelMapper>();
            serviceCollection.AddScoped<IProductViewModelMapper, ProductViewModelMapper>();
        }
    }
}
