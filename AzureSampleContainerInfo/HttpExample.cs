using Azure;
using Azure.Data.Tables;
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

        [Function("SaveContainer")]
        public async Task SaveContainer([QueueTrigger("container-changed-messages")] ContainerDto dto)
        {
            TableServiceClient serviceClient = new("UseDevelopmentStorage=true");

            TableClient client = serviceClient.GetTableClient(
                tableName: "Containers"
            );

            Response response = await client.UpsertEntityAsync(
                entity: new Container
                {
                    RowKey = dto.ContainerId,
                    ContainerId = dto.ContainerId,
                    IsoCode = dto.IsoCode,
                    Status = dto.Status
                },
                mode: TableUpdateMode.Replace
            );
        }

        [Function("GetContainer")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get", Route = "containers/{id}")] HttpRequest req, string id)
        {
            TableServiceClient serviceClient = new("UseDevelopmentStorage=true");

            TableClient client = serviceClient.GetTableClient(
                tableName: "Containers"
            );

            NullableResponse<Container> entity = await client.GetEntityAsync<Container>(
                partitionKey: "Containers",
                rowKey: id
            );

            if (!entity.HasValue)
            {
                return new NotFoundObjectResult(id);
            }
            else
            {
                var container = entity.Value;

                var dto = new ContainerDto
                { 
                    ContainerId = container.ContainerId,
                    IsoCode = container.IsoCode,
                    Status = container.Status
                };

                return new OkObjectResult(dto);
            }
        }
    }
}
