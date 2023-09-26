using Microsoft.AspNetCore.Components;
using WeatherForecastApp.ViewModels;
using WeatherForecastApp.Services;
using Microsoft.JSInterop;


namespace WeatherForecastApp.Pages
{
    public class IndexBase : ComponentBase, IDisposable
    {
        [Inject]
        ExportService ExportService { get; set; }
        [Inject]
        IJSRuntime JSRuntime { get; set; }
        [Inject]
        NavigationManager NavigationManager { get; set; }

        protected ClimateViewModel viewModel = new ClimateViewModel();
        protected string updateTime;
        private CancellationTokenSource cancellationTokenSource;
        protected ExportService service = new();
        protected override async Task OnInitializedAsync()
        {
            await LoadWeatherDataAndScheduleUpdateAsync();
        }

        private async Task LoadWeatherDataAndScheduleUpdateAsync()
        {
            try
            {
                await viewModel.LoadWeatherDataAsync();
                updateTime =DateTime.UtcNow.AddHours(2).ToString("dddd, dd MMMM yyyy HH:mm:ss");
                ScheduleAutoUpdate();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private void ScheduleAutoUpdate()
        {
            cancellationTokenSource = new CancellationTokenSource();

            Task.Run(async () =>
            {
                while (!cancellationTokenSource.Token.IsCancellationRequested)
                {
                    await Task.Delay(TimeSpan.FromHours(24), cancellationTokenSource.Token);
                    await InvokeAsync(LoadWeatherDataAndScheduleUpdateAsync);
                    await ExportService.GenerateCsvContentAsync(viewModel.ClimateData);
                    StateHasChanged();
                }
            }, cancellationTokenSource.Token);
        }

        public void Dispose()
        {
            cancellationTokenSource?.Cancel();
            cancellationTokenSource?.Dispose();
        }

        protected async Task DownloadFile()
        {
            await ExportService.GenerateCsvContentAsync(viewModel.ClimateData);
            string filePath = "files/climate_data_last_30days.csv";
            string fileUrl = NavigationManager.ToAbsoluteUri(filePath).ToString();
            await JSRuntime.InvokeVoidAsync("downloadFile", fileUrl);
        }
    }
}
