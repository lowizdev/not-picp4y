using Microsoft.Extensions.DependencyInjection;
using PaymentProcessingWorkerService.Facades;

namespace PaymentProcessingWorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        private readonly IServiceScopeFactory _scopeFactory;
        public Worker(IServiceScopeFactory serviceScopeFactory, ILogger<Worker> logger)
        {
            _scopeFactory = serviceScopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                if (_logger.IsEnabled(LogLevel.Information))
                {
                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                }

                using IServiceScope scope = _scopeFactory.CreateScope();

                IProcessPaymentFacade paymentFacade =
                    scope.ServiceProvider.GetRequiredService<IProcessPaymentFacade>();

                await paymentFacade.ConsumeItem(); //TODO: CONSUME IN LOOP

                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
