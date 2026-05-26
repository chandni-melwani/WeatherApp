using MongoDB.Driver;
using WeatherApp.Models;

namespace WeatherApp.Services;

public class MongoFavoritesService
{
    private readonly IMongoCollection<FavoriteDocument> _favorites;

    public MongoFavoritesService(IConfiguration configuration)
    {
        var connectionString = configuration["MongoDB:ConnectionString"];
        var databaseName = configuration["MongoDB:DatabaseName"];

        var client = new MongoClient(connectionString);
        var database = client.GetDatabase(databaseName);
        _favorites = database.GetCollection<FavoriteDocument>("favorites");
        _favoriteWeather = database.GetCollection<FavoriteWeatherDocument>("favoriteWeather");
    }

    public async Task<List<FavoriteDocument>> GetFavorites(string userId)
    {
        return await _favorites
            .Find(f => f.UserId == userId)
            .ToListAsync();
    }

    public async Task AddFavorite(string userId, string city)
    {
        var favorite = new FavoriteDocument
        {
            UserId = userId,
            City = city,
            SavedAt = DateTime.UtcNow
        };
        await _favorites.InsertOneAsync(favorite);
    }

    public async Task RemoveFavorite(string userId, string city)
    {
        await _favorites.DeleteOneAsync(f => f.UserId == userId && f.City == city);
    }

    private readonly IMongoCollection<FavoriteWeatherDocument> _favoriteWeather;

    public async Task<string?> GetFavoriteWeather(string userId)
    {
        var result = await _favoriteWeather
            .Find(f => f.UserId == userId)
            .FirstOrDefaultAsync();

        return result?.Condition;
    }

    public async Task SaveFavoriteWeather(string userId, string condition)
    {
        var existing = await _favoriteWeather
            .Find(f => f.UserId == userId)
            .FirstOrDefaultAsync();

        if (existing == null)
        {
            await _favoriteWeather.InsertOneAsync(new FavoriteWeatherDocument
            {
                UserId = userId,
                Condition = condition
            });
        }
        else
        {
            await _favoriteWeather.ReplaceOneAsync(
                f => f.UserId == userId,
                new FavoriteWeatherDocument
                {
                    Id = existing.Id,
                    UserId = userId,
                    Condition = condition
                });
        }
    }
}