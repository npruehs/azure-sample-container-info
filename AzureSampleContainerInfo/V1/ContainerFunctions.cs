using Azure;
using Azure.Data.Tables;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace AzureSampleContainerInfo.V1
{
    /// <summary>
    /// Fetches container data from Azure Queue Storage for persisting it in Azure Table Storage, and
    /// provides a REST API for accessing container data in Azure Table Storage.
    /// </summary>
    public class ContainerFunctions
    {
        private static readonly TableServiceClient serviceClient = new(Environment.GetEnvironmentVariable("AzureWebJobsStorage", EnvironmentVariableTarget.Process));
        private static readonly TableClient client = serviceClient.GetTableClient("Containers");

        private readonly ILogger<ContainerFunctions> _logger;

        public ContainerFunctions(ILogger<ContainerFunctions> logger)
        {
            _logger = logger;
        }

        [Function("SaveContainer")]
        public async Task SaveContainer([QueueTrigger("container-changed-messages")] ContainerDto dto)
        {
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
