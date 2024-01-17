using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace DurableFuncTupleDemo.Functions
{
    public class ActivitySayHello(ILogger<ActivitySayHello> logger)
    {
        private readonly ILogger<ActivitySayHello> _logger = logger;

        [Function(nameof(ActivitySayHello))]
        public string RunAsync([ActivityTrigger] (string, string) input)
        {
            _logger.LogInformation("Saying hello to {to} from {from}.", input.Item1, input.Item2);
            return $"Hello {input.Item1}!";
        }
    }
}
