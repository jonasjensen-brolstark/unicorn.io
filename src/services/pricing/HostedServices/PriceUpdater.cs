using System;
using System.Threading;
using System.Threading.Tasks;
using Elastic.Apm.Api;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using pricing.Context;

namespace pricing.HostedServices
{
    public class PriceUpdater : IHostedService, IDisposable
    {
        private readonly ILogger<PriceUpdater> _logger;
        private readonly IServiceScopeFactory _scopeFactory;
        private Timer _timer;

        public PriceUpdater(ILogger<PriceUpdater> logger, IServiceScopeFactory scopeFactory)
        {
            _logger = logger;
            _scopeFactory = scopeFactory;
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Timed Hosted Service running.");

            _timer = new Timer(UpdatePrices, null, TimeSpan.Zero,
                TimeSpan.FromSeconds(30));

            return Task.CompletedTask;
        }

        private void UpdatePrices(object state)
        {
            var transaction = Elastic.Apm.Agent
              .Tracer.StartTransaction("UpdatePriceTransaction", ApiConstants.TypeRequest);              
            try
            {
                var random = new Random();
                using (var scope = _scopeFactory.CreateScope())
                {
                    using (var pricingContext = scope.ServiceProvider.GetRequiredService<PricingContext>())
                    {
                        foreach (var pricing in pricingContext.Pricings)
                        {
                            pricing.Price = pricing.Price * (random.Next(-10, 10) * random.NextDouble());
                            if (pricing.Price <= 0)
                            {
                                pricing.Price = random.Next(10, 100);

                                _logger.LogInformation("new price: {0}", pricing.Price);
                            }
                        }
                        pricingContext.SaveChanges();
                    }
                }
                _logger.LogInformation("Price updater updating prices");
            }
            catch (Exception e)
            {
                transaction.CaptureException(e);
                throw;
            }
            finally
            {
                transaction.End();
            }


        }

        public Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Price updater Hosted Service is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}