using Microsoft.AspNetCore.Components;
using WeatherForecastApp.ViewModels;


namespace WeatherForecastApp.Pages
{
    public class IndexBase : ComponentBase, IDisposable
    {

        protected ClimateViewModel viewModel = new ClimateViewModel();
        protected string updateTime;
        private CancellationTokenSource cancellationTokenSource;
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
                    StateHasChanged();
                }
            }, cancellationTokenSource.Token);
        }

        public void Dispose()
        {
            cancellationTokenSource?.Cancel();
            cancellationTokenSource?.Dispose();
        }
    }
}
