using CsvHelper.Configuration.Attributes;

namespace WeatherForecastApp.Models
{
    public class ClimateCSVRecords
    {
        [Name("Time")]
        public string Time { get; set; }

        [Name("Temperature (2m height)")]
        public double TemperatureTwoM { get; set; }

        [Name("Relative Humidity (2m height)")]
        public int RelativeHumidityTwoM { get; set; }

        [Name("Wind Speed (10m height)")]
        public double WindSpeedTenM { get; set; }
    }
}
