using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using WeatherForecastApp.Models;

namespace WeatherForecastApp.ViewModels
{
    public class ClimateViewModel
    {
        private readonly HttpClient _httpClient = new();
        public ClimateDataPoints? ClimateData { get; private set; }

        public async Task LoadWeatherDataAsync()
        {
            try
            {
                string apiUrl = $"https://api.open-meteo.com/v1/forecast?latitude=52.52&longitude=13.41&hourly=temperature_2m,relativehumidity_2m,windspeed_10m&past_days=30";
                var response = await _httpClient.GetStringAsync(apiUrl);
                var climateData = JsonSerializer.Deserialize<ClimateDataPoints>(response, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                ClimateData = climateData;
            }
            catch (HttpRequestException)
            {
                // Handle HTTP request exceptions
                Console.WriteLine($"HTTP Request Error.");
            }
            catch (JsonException)
            {
                // Handle JSON deserialization exceptions
                Console.WriteLine($"JSON Deserialization Error.");
            }
        }
    }
}
