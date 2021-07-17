using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Shared;

namespace WorkerConsumer
{
    public class MessageConsumer : IConsumer<Message>
    {
        private readonly ILogger<MessageConsumer> _logger;

        public MessageConsumer(ILogger<MessageConsumer> logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<Message> context)
        {
            _logger.LogInformation(context?.Message?.Text ?? "null");
            await Task.Delay(2500);
        }
    }
}