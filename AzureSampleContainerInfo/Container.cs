using Azure;
using Azure.Data.Tables;

namespace AzureSampleContainerInfo
{
    /// <summary>
    /// Container data stored in Azure Table storage.
    /// </summary>
    public class Container : ITableEntity
    {
        public string PartitionKey { get; set; } = "Containers";
        public string RowKey { get; set; } = $"{Guid.NewGuid()}";

        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; } = ETag.All;

        public string ContainerId { get; set; } = string.Empty;

        public string IsoCode { get; set; } = string.Empty;

        public ContainerStatus Status { get; set; }

        public string? ContainerOperatorId { get; set; }
    }
}
