using Microsoft.AspNetCore.Components;
using WeatherForecastApp.ViewModels;
using WeatherForecastApp.Services;
using Microsoft.JSInterop;

namespace WeatherForecastApp.Pages
{
    public class IndexBase : ComponentBase, IDisposable
    {
        [Inject]
        ExportService ExportService { get; set; } // Service for exporting data to CSV
        [Inject]
        IJSRuntime JSRuntime { get; set; } // JavaScript Interop for client-side operations
        [Inject]
        NavigationManager NavigationManager { get; set; } // Navigation management

        // ViewModel for climate data
        protected ClimateViewModel viewModel = new ClimateViewModel();

        // String to display the last data update time
        protected string updateTime;

        private CancellationTokenSource cancellationTokenSource;

        // Service for exporting data to CSV
        protected ExportService service = new();

        // This method is called when the component is initialized/page is loaded
        protected override async Task OnInitializedAsync()
        {
            await LoadWeatherDataAndScheduleUpdateAsync();
        }

        private async Task LoadWeatherDataAndScheduleUpdateAsync()
        {
            try
            {
                await viewModel.LoadWeatherDataAsync();
                // Set the last data update time with a UTC offset of 2 hours
                updateTime = DateTime.UtcNow.AddHours(2).ToString("dddd, dd MMMM yyyy HH:mm:ss");
                ScheduleAutoUpdate();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        // Schedule automatic updates at fixed intervals
        private void ScheduleAutoUpdate()
        {
            cancellationTokenSource = new CancellationTokenSource();
            Task.Run(async () =>
            {
                while (!cancellationTokenSource.Token.IsCancellationRequested)
                {
                    // Delay for 24 hours
                    await Task.Delay(TimeSpan.FromHours(24), cancellationTokenSource.Token);
                    // Reload weather data and update CSV export
                    await InvokeAsync(LoadWeatherDataAndScheduleUpdateAsync);
                    await ExportService.GenerateCsvContentAsync(viewModel.ClimateData);
                    // Notify Blazor to refresh the UI
                    StateHasChanged();
                }
            }, cancellationTokenSource.Token);
        }

        // Dispose of resources when the component is disposed
        public void Dispose()
        {
            cancellationTokenSource?.Cancel();
            cancellationTokenSource?.Dispose();
        }

        // Trigger file download for the CSV export
        protected async Task DownloadFile()
        {
            await ExportService.GenerateCsvContentAsync(viewModel.ClimateData);
            string filePath = "files/climate_data_last_30days.csv";
            string fileUrl = NavigationManager.ToAbsoluteUri(filePath).ToString();
            // Trigger a client-side JavaScript function to initiate the download
            await JSRuntime.InvokeVoidAsync("downloadFile", fileUrl);
        }
    }
}
