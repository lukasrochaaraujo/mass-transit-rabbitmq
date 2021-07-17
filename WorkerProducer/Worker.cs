using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Hosting;
using Shared;

namespace WorkerProducer
{
    public class Worker : BackgroundService
    {
        private readonly IBus _bus;

        public Worker(IBus bus)
        {
            _bus = bus;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var sender = await _bus.GetSendEndpoint(new Uri("queue:worker"));

            while (!stoppingToken.IsCancellationRequested)
            {
                await sender.Send(new Message() { Text = $"The time is {DateTimeOffset.Now}" }, stoppingToken);
                await Task.Delay(500, stoppingToken);
            }
        }
    }
}
