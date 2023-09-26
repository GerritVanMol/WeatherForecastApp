using System.Text.Json;
using WeatherForecastApp.Models;

namespace WeatherForecastApp.ViewModels
{
    public class ClimateViewModel
    {
        public ClimateDataPoints? ClimateData { get; private set; }
        public async Task LoadWeatherDataAsync()
        {
            string apiUrl = $"https://api.open-meteo.com/v1/forecast?latitude=52.52&longitude=13.41&hourly=temperature_2m,relativehumidity_2m,windspeed_10m&past_days=30";
            var httpClient = new HttpClient();
            var response = await httpClient.GetStringAsync(apiUrl);
            var climateData = JsonSerializer.Deserialize<ClimateDataPoints>(response, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            ClimateData = climateData;
        }
    }
}
