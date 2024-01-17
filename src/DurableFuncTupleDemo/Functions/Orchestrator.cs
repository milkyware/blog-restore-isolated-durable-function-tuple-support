using Castle.Core.Logging;
using Microsoft.Azure.Functions.Worker;
using Microsoft.DurableTask;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DurableFuncTupleDemo.Functions
{
    public class Orchestrator(ILogger<Orchestrator> logger)
    {
        [Function(nameof(Orchestrator))]
        public async Task<List<string>> RunAsync(
            [OrchestrationTrigger] TaskOrchestrationContext context)
        {
            logger.LogInformation("Saying hello.");
            var outputs = new List<string>();

            outputs.Add(await context.CallActivityAsync<string>(nameof(ActivitySayHello), ("Tokyo", "Madrid")));
            outputs.Add(await context.CallActivityAsync<string>(nameof(ActivitySayHello), ("London", "Paris")));

            return outputs;
        }
    }
}
