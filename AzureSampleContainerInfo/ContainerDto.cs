using System.Text.Json.Serialization;

namespace AzureSampleContainerInfo
{
    public class ContainerDto
    {
        public required string ContainerId { get; set; }

        public required string IsoCode { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ContainerStatus Status { get; set; }
    }
}

