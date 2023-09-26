using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;
using WeatherForecastApp.Models;

namespace WeatherForecastApp.Services
{
    public class ExportService
    {
        public async Task<string> GenerateCsvContentAsync(ClimateDataPoints climateData)
        {
            try
            {            
                // Check if climateData and hourly data are not null
                if (climateData?.Hourly == null || climateData.Hourly.Time.Length == 0)
                {
                    return string.Empty; // No valid data to export
                }

                var records = new List<ClimateCSVRecords>();

                for (var index = 0; index < climateData.Hourly.Time.Length; index++)
                {
                    var record = new ClimateCSVRecords
                    {
                        Time = DateTime.Parse(climateData.Hourly.Time[index]).ToString("dd/MM/yyyy hh:mm tt"),
                        TemperatureTwoM = climateData.Hourly.TemperatureTwoM[index],
                        RelativeHumidityTwoM = climateData.Hourly.RelativeHumidityTwoM[index],
                        WindSpeedTenM = climateData.Hourly.WindSpeedTenM[index]
                    };
                    records.Add(record);
                }

                var filePath = "wwwroot/files/climate_data_last_30days.csv";
                // Using statements to ensure resources are disposed properly
                using (var memoryStream = new MemoryStream())
                using (var writer = new StreamWriter(filePath))
                using (var csv = new CsvWriter(writer, new CsvConfiguration(CultureInfo.InvariantCulture)))
                {
                    await csv.WriteRecordsAsync(records);
                    return Encoding.UTF8.GetString(memoryStream.ToArray());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during CSV export.");
                // Return an empty string if there's no valid climate data
                return string.Empty;
            }
        }
    }
}
