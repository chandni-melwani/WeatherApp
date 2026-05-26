namespace WeatherApp.Client.Models
{
    public class ForecastModel
    {
        public DateTime Date { get; set; }

        public int Temperature { get; set; }

        public int TempMin { get; set; }       
        public int TempMax { get; set; }
        public string? Condition { get; set; }  
    }
}