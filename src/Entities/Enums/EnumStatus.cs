using System.Text.Json.Serialization;

namespace FluxusApi.Entities
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum EnumStatus
    {
        RECEIVED = 1,
        PENDING = 2,
        SURVEYED = 3,
        DONE = 4
    }
}