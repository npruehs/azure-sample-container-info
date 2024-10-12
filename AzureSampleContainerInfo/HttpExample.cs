using Azure;
using Azure.Data.Tables;
using Azure.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace AzureSampleContainerInfo
{
    public class HttpExample
    {
        private readonly ILogger<HttpExample> _logger;

        public HttpExample(ILogger<HttpExample> logger)
        {
            _logger = logger;
        }

        [Function("HttpExample")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            TableServiceClient serviceClient = new("UseDevelopmentStorage=true");

            TableClient client = serviceClient.GetTableClient(
                tableName: "Containers"
            );

            Container container = new Container("TEST12345678", "22G1", ContainerStatus.Announced);

            Response response = await client.UpsertEntityAsync(
                entity: container,
                mode: TableUpdateMode.Replace
            );

            return new OkObjectResult("Welcome to Azure Functions!");
        }
    }
}
