using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace Frontend
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            // Configure HttpClient to point to the WebAPI server
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7297/") });

            await builder.Build().RunAsync();
        }
    }
}                                              