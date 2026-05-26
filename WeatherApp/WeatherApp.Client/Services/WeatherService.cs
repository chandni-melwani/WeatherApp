using System.Net.Http.Json;
using WeatherApp.Client.Helpers;
using WeatherApp.Client.Models;

namespace WeatherApp.Client.Services
{
    public class WeatherService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public WeatherService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _apiKey = AppConstants.WeatherApiKey;
        }

        public async Task<WeatherModel> GetWeather(string city)
        {
            string url = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={_apiKey}&units=metric";

            var response = await _httpClient.GetFromJsonAsync<OpenWeatherResponse>(url)
                ?? throw new Exception($"Could not get weather for '{city}'. Check the city name.");  // null check

            return new WeatherModel
            {
                City = response.Name,
                Country = response.Sys.Country,
                Temperature = (int)response.Main.Temp,
                TempMin = (int)response.Main.TempMin, 
                TempMax = (int)response.Main.TempMax,
                Condition = response.Weather.Length > 0 ? response.Weather[0].Main : "Unknown",
                Humidity = response.Main.Humidity,
                WindSpeed = response.Wind.Speed
            };
        }

        public async Task<List<ForecastModel>> GetForecast(string city)
        {
            string url = $"https://api.openweathermap.org/data/2.5/forecast?q={city}&appid={_apiKey}&units=metric";

            var response = await _httpClient.GetFromJsonAsync<ForecastResponse>(url)
                ?? throw new Exception($"Could not get forecast for '{city}'.");  // null check

            var forecastList = new List<ForecastModel>();

            foreach (var item in response.List)
            {
                if (item.Dt_Txt.Contains("12:00:00"))
                {
                    forecastList.Add(new ForecastModel
                    {
                        Date = DateTime.Parse(item.Dt_Txt),
                        Temperature = (int)item.Main.Temp,
                        TempMin = (int)item.Main.TempMin,     
                        TempMax = (int)item.Main.TempMax,
                        Condition = item.Weather[0].Main
                    });
                }
            }

            return forecastList;
        }
    }
}