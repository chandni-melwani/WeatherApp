namespace WeatherApp.Client.Helpers;

public static class WeatherHelper
{
    public static string GetWeatherIcon(string condition)
    {
        return condition switch
        {
            "Clear" => "☀️",
            "Clouds" => "☁️",
            "Rain" => "🌧",
            "Thunderstorm" => "⛈",
            "Snow" => "❄️",
            "Mist" => "🌫",
            _ => "🌍"
        };
    }

    public static string GetWeatherColor(string condition)
    {
        return condition switch
        {
            "Clear" => "#FFD54F",
            "Clouds" => "#B0BEC5",
            "Rain" => "#64B5F6",
            "Thunderstorm" => "#9575CD",
            "Snow" => "#E1F5FE",
            "Mist" => "#CFD8DC",
            _ => "#A5D6A7"
        };
    }

    public static string GetGradient(string condition)
    {
        return condition switch
        {
            "Clear" => "linear-gradient(135deg, #F9A825, #FF6F00)",
            "Clouds" => "linear-gradient(135deg, #546E7A, #78909C)",
            "Rain" => "linear-gradient(135deg, #1565C0, #0D47A1)",
            "Thunderstorm" => "linear-gradient(135deg, #4A148C, #6A1B9A)",
            "Snow" => "linear-gradient(135deg, #80DEEA, #4DD0E1)",
            "Mist" => "linear-gradient(135deg, #90A4AE, #B0BEC5)",
            _ => "linear-gradient(135deg, #1565C0, #1976D2)"
        };
    }

}