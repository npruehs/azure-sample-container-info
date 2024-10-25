using System.Text.Json.Serialization;

namespace AzureSampleContainerInfo
{
    /// <summary>
    /// Container data transmitted over the wire, e.g. from Azure Queue Storage or as an HTTP response.
    /// </summary>
    public class ContainerDto
    {
        public required string ContainerId { get; set; }

        public required string IsoCode { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ContainerStatus Status { get; set; }
    }
}

