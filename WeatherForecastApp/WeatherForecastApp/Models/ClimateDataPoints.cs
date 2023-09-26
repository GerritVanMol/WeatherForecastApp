using System;
using System.Text.Json.Serialization;

namespace WeatherForecastApp.Models
{
    public class ClimateDataPoints
    {
        [JsonPropertyName("latitude")]
        public double Latitude { get; set; }

        [JsonPropertyName("longitude")]
        public double Longitude { get; set; }

        [JsonPropertyName("generationtime_ms")]
        public double GenerationTimeMs { get; set; }

        [JsonPropertyName("utc_offset_seconds")]
        public int UtcOffsetSeconds { get; set; }

        [JsonPropertyName("timezone")]
        public string? Timezone { get; set; }

        [JsonPropertyName("timezone_abbreviation")]
        public string? TimezoneAbbreviation { get; set; }

        [JsonPropertyName("elevation")]
        public double Elevation { get; set; }

        [JsonPropertyName("current_weather")]
        public CurrentWeather? CurrentWeather { get; set; }

        [JsonPropertyName("hourly_units")]
        public HourlyUnits? HourlyUnits { get; set; }

        [JsonPropertyName("hourly")]
        public HourlyData? Hourly { get; set; }
    }
    public class CurrentWeather
    {
        [JsonPropertyName("temperature")]
        public double Temperature { get; set; }

        [JsonPropertyName("windspeed")]
        public double WindSpeed { get; set; }

        [JsonPropertyName("winddirection")]
        public int WindDirection { get; set; }

        [JsonPropertyName("weathercode")]
        public int WeatherCode { get; set; }

        [JsonPropertyName("is_day")]
        public int IsDay { get; set; }

        [JsonPropertyName("time")]
        public DateTime Time { get; set; }
    }

    public class HourlyData
    {
        [JsonPropertyName("time")]
        public string[]? Time { get; set; }

        [JsonPropertyName("temperature_2m")]
        public double[]? TemperatureTwoM { get; set; }

        [JsonPropertyName("relativehumidity_2m")]
        public int[]? RelativeHumidityTwoM { get; set; }

        [JsonPropertyName("windspeed_10m")]
        public double[]? WindSpeedTenM { get; set; }
    }

    public class HourlyUnits
    {
        [JsonPropertyName("time")]
        public string? Time { get; set; }

        [JsonPropertyName("temperature_2m")]
        public string? TemperatureTwoM { get; set; }

        [JsonPropertyName("relativehumidity_2m")]
        public string? RelativeHumidityTwoM { get; set; }

        [JsonPropertyName("windspeed_10m")]
        public string? WindSpeedTenM { get; set; }
    }
}
