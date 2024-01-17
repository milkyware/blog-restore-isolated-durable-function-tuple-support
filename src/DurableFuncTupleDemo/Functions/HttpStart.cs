using System.Net.Mime;
using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.DurableTask.Client;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace DurableFuncTupleDemo.Functions
{
    public class HttpStart(ILogger<HttpStart> logger)
    {
        private readonly ILogger<HttpStart> _logger = logger;

        [Function(nameof(HttpStart))]
        [OpenApiOperation(operationId: nameof(HttpStart))]
        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.Accepted, contentType: MediaTypeNames.Application.Json, typeof(object))]
        public async Task<HttpResponseData> RunAsync(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequestData request,
            [DurableClient] DurableTaskClient client)
        {
            var instanceId = await client.ScheduleNewOrchestrationInstanceAsync(
                nameof(Orchestrator));

            _logger.LogInformation("Started orchestration with ID = '{instanceId}'.", instanceId);

            return client.CreateCheckStatusResponse(request, instanceId);
        }
    }
}
