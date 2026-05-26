using MudBlazor.Services;
using WeatherApp.Components;
using WeatherApp.Client.Services;
using WeatherApp.Client.Helpers;
using Blazored.LocalStorage;
using WeatherApp.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMudServices();
builder.Services.AddScoped(sp => new HttpClient());
builder.Services.AddScoped<WeatherService>();
builder.Services.AddScoped<MongoFavoritesService>();
builder.Services.AddScoped<FavoritesService>();

// Add Supabase
builder.Services.AddScoped(provider => new Supabase.Client(
    AppConstants.SupabaseUrl,
    AppConstants.SupabaseAnonKey,
    new Supabase.SupabaseOptions
    {
        AutoRefreshToken = true,
        AutoConnectRealtime = false
    }
));

// Add AuthService
builder.Services.AddScoped<AuthService>();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddControllers();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();
app.MapControllers();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(WeatherApp.Client._Imports).Assembly);

app.Run();