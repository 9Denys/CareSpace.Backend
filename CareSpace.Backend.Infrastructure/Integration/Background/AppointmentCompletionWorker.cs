using CareSpace.Backend.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CareSpace.Backend.Infrastructure.Integration.Background
{
    public class AppointmentCompletionWorker : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<AppointmentCompletionWorker> _logger;
        private readonly TimeSpan _interval;

        public AppointmentCompletionWorker(
            IServiceScopeFactory scopeFactory,
            ILogger<AppointmentCompletionWorker> logger,
            IConfiguration configuration)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;

            var intervalMinutesStr = configuration
                .GetSection("AppointmentCompletion:IntervalMinutes")
                .Value;

            var intervalMinutes = !string.IsNullOrWhiteSpace(intervalMinutesStr)
                ? int.Parse(intervalMinutesStr)
                : 5;

            _interval = TimeSpan.FromMinutes(intervalMinutes);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation(
                "AppointmentCompletionWorker started. Interval: {Interval}",
                _interval);

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using var scope = _scopeFactory.CreateScope();

                    var completionService = scope.ServiceProvider
                        .GetRequiredService<IAppointmentCompletionService>();

                    await completionService.CompleteExpiredAppointmentsAsync(stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error during appointment completion.");
                }

                try
                {
                    await Task.Delay(_interval, stoppingToken);
                }
                catch (TaskCanceledException)
                {
                }
            }

            _logger.LogInformation("AppointmentCompletionWorker is stopping.");
        }
    }
}