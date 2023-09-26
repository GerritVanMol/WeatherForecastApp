using System.Globalization;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration;
using WeatherForecastApp.Models;

namespace WeatherForecastApp.Services
{
    public class ExportService
    {
        public async Task<string> GenerateCsvContentAsync(ClimateDataPoints climateData)
        {
            if (climateData?.Hourly != null)
            {
                var records = new List<ClimateCSVRecords>();

                for (var index = 0; index < climateData.Hourly.Time.Length; index++)
                {
                    var record = new ClimateCSVRecords
                    {
                        Time = DateTime.Parse(climateData.Hourly.Time[index]).ToString("MM/dd/yyyy hh:mm tt"),
                        TemperatureTwoM = climateData.Hourly.TemperatureTwoM[index],
                        RelativeHumidityTwoM = climateData.Hourly.RelativeHumidityTwoM[index],
                        WindSpeedTenM = climateData.Hourly.WindSpeedTenM[index]
                    };
                    records.Add(record);
                }

                using (var memoryStream = new MemoryStream())
                using (var writer = new StreamWriter("wwwroot/files/climate_data_last_30days.csv"))
                using (var csv = new CsvWriter(writer, new CsvConfiguration(CultureInfo.InvariantCulture)))
                {
                    await csv.WriteRecordsAsync(records);
                    return Encoding.UTF8.GetString(memoryStream.ToArray());
                }
            }

            return string.Empty;
        }
    }
}
