using System.Net.Http.Json;

namespace WeatherApp.Client.Services;

public class FavoritesService
{
    private readonly HttpClient _httpClient;
    private readonly AuthService _authService;

    public FavoritesService(HttpClient httpClient, AuthService authService)
    {
        _httpClient = httpClient;
        _authService = authService;
    }

    public async Task<List<string>> GetFavorites()
    {
        var userId = _authService.GetCurrentUserId();
        if (userId == null) return new List<string>();

        var result = await _httpClient
            .GetFromJsonAsync<List<string>>($"api/favorites/{userId}");

        return result ?? new List<string>();
    }

    public async Task AddFavorite(string city)
    {
        var userId = _authService.GetCurrentUserId();
        if (userId == null) return;

        await _httpClient.PostAsJsonAsync("api/favorites", new
        {
            UserId = userId,
            City = city
        });
    }

    public async Task RemoveFavorite(string city)
    {
        var userId = _authService.GetCurrentUserId();
        if (userId == null) return;

        await _httpClient.SendAsync(new HttpRequestMessage
        {
            Method = HttpMethod.Delete,
            RequestUri = new Uri("api/favorites", UriKind.Relative),
            Content = JsonContent.Create(new { UserId = userId, City = city })
        });
    }

    public async Task<string?> GetFavoriteWeather()
    {
        var userId = _authService.GetCurrentUserId();
        if (userId == null) return null;

        var result = await _httpClient
            .GetFromJsonAsync<FavoriteWeatherResponse>($"api/favorites/weather/{userId}");

        return result?.Condition;
    }

    public async Task SaveFavoriteWeather(string condition)
    {
        var userId = _authService.GetCurrentUserId();
        if (userId == null) return;

        await _httpClient.PostAsJsonAsync("api/favorites/weather", new
        {
            UserId = userId,
            Condition = condition
        });
    }

    public class FavoriteWeatherResponse
    {
        public string Condition { get; set; } = "";
    }
}