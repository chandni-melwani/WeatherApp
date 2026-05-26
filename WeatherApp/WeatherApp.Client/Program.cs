using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using WeatherApp.Client.Helpers;
using WeatherApp.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddMudServices();
builder.Services.AddBlazoredLocalStorage();

builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
});

builder.Services.AddScoped(provider => new Supabase.Client(
    AppConstants.SupabaseUrl,
    AppConstants.SupabaseAnonKey,
    new Supabase.SupabaseOptions
    {
        AutoRefreshToken = true,
        AutoConnectRealtime = false,
    }
));

builder.Services.AddScoped<WeatherService>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<FavoritesService>();

var app = builder.Build();

var supabase = app.Services.GetRequiredService<Supabase.Client>();
await supabase.InitializeAsync();

await app.RunAsync();