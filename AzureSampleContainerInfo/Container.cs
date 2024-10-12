using Azure;
using Azure.Data.Tables;

namespace AzureSampleContainerInfo
{
    public class Container : ITableEntity
    {
        public string PartitionKey { get; set; } = "Containers";
        public string RowKey { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; } = ETag.All;

        public string ContainerId { get; set; }

        public string IsoCode { get; set; }

        public ContainerStatus Status { get; set; }

        public Container(string containerId, string isoCode, ContainerStatus status)
        {
            RowKey = containerId;

            ContainerId = containerId;
            IsoCode = isoCode;
            Status = status;
        }
    }
}
