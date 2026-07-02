using Expenses.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.JSInterop;

namespace expenses_app;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);
        builder.RootComponents.Add<App>("#app");
        builder.RootComponents.Add<HeadOutlet>("head::after");

        builder.Services.AddScoped<ExpensesService>(sp => new ExpensesService(sp.GetRequiredService<IJSRuntime>()));
        
        string apiEndpoint;
        if (builder.HostEnvironment.Environment == "Development")
        {
            apiEndpoint = "http://localhost:5058/api/";
        }
        else
        {
            apiEndpoint = Environment.GetEnvironmentVariable("API_ENDPOINT")
                          ?? "http://localhost:5058/api/"; // Production endpoint
        }

        Console.WriteLine($"API Endpoint: {apiEndpoint}");
        builder.Services.AddHttpClient<ApiService>(client =>
        {
            client.BaseAddress = new Uri(apiEndpoint);
            Console.WriteLine($"APIService BaseAddress: {client.BaseAddress}");
        });

        builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

        await builder.Build().RunAsync();
    }
}