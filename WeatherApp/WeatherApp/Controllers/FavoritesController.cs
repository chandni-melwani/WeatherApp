using Microsoft.AspNetCore.Mvc;
using WeatherApp.Services;

namespace WeatherApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FavoritesController : ControllerBase
{
    private readonly MongoFavoritesService _mongoService;

    public FavoritesController(MongoFavoritesService mongoService)
    {
        _mongoService = mongoService;
    }

    [HttpGet("{userId}")]
    public async Task<IActionResult> GetFavorites(string userId)
    {
        var favorites = await _mongoService.GetFavorites(userId);
        return Ok(favorites.Select(f => f.City).ToList());
    }

    [HttpPost]
    public async Task<IActionResult> AddFavorite([FromBody] FavoriteRequest request)
    {
        await _mongoService.AddFavorite(request.UserId, request.City);
        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> RemoveFavorite([FromBody] FavoriteRequest request)
    {
        await _mongoService.RemoveFavorite(request.UserId, request.City);
        return Ok();
    }

    [HttpGet("weather/{userId}")]
    public async Task<IActionResult> GetFavoriteWeather(string userId)
    {
        var condition = await _mongoService.GetFavoriteWeather(userId);
        return Ok(new { condition = condition ?? "" });
    }

    [HttpPost("weather")]
    public async Task<IActionResult> SaveFavoriteWeather([FromBody] FavoriteWeatherRequest request)
    {
        await _mongoService.SaveFavoriteWeather(request.UserId, request.Condition);
        return Ok();
    }

}

public class FavoriteRequest
{
    public string UserId { get; set; } = "";
    public string City { get; set; } = "";
}

public class FavoriteWeatherRequest
{
    public string UserId { get; set; } = "";
    public string Condition { get; set; } = "";
}