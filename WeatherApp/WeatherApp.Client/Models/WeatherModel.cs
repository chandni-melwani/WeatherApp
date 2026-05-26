namespace WeatherApp.Client.Models
{
    public class WeatherModel
    {
        public string? City { get; set; }        
        public string? Country { get; set; }    
        public int Temperature { get; set; }
        public int TempMin { get; set; }       
        public int TempMax { get; set; }       
        public string? Condition { get; set; }   
        public int Humidity { get; set; }
        public double WindSpeed { get; set; }
    }
}