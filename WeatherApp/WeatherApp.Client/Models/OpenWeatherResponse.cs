using System.Text.Json.Serialization;

namespace WeatherApp.Client.Models
{
    public class OpenWeatherResponse
    {
        public MainInfo Main { get; set; }

        public WeatherInfo[] Weather { get; set; }

        public string Name { get; set; }

        public WindInfo Wind { get; set; }

        public SysInfo Sys { get; set; }
    }

    public class MainInfo
    {
        public double Temp { get; set; }

        [JsonPropertyName("temp_min")]
        public double TempMin { get; set; }

        [JsonPropertyName("temp_max")]
        public double TempMax { get; set; }

        public int Humidity { get; set; }
    }

    public class WeatherInfo
    {
        public string Main { get; set; }
    }

    public class WindInfo
    {
        public double Speed { get; set; }
    }

    public class SysInfo
    {
        public string Country { get; set; }
    }
}