namespace WeatherApp.Client.Models
{
    public class ForecastResponse
    {
        public ForecastItem[] List { get; set; }
    }

    public class ForecastItem
    {
        public string Dt_Txt { get; set; }

        public MainInfo Main { get; set; }

        public WeatherInfo[] Weather { get; set; }
    }
}