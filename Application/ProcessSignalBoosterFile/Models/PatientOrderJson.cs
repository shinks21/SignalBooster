using System.Text.Json.Serialization;

namespace Application.ProcessSignalBoosterFile.Models
{
    public class PatientOrderJson
    {
        [JsonPropertyName("device")]
        public string Device { get; set; } = string.Empty;

        [JsonPropertyName("mask_type")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? MaskType { get; set; } = string.Empty;

        [JsonPropertyName("add_ons")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public IEnumerable<string>? AddOns { get; set; }

        [JsonPropertyName("qualifier")]
        public string Qualifier { get; set; } = string.Empty;

        [JsonPropertyName("ordering_provider")]
        public string OrderingProvider { get; set; } = string.Empty;

        [JsonPropertyName("liters")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Liters { get; set; }

        [JsonPropertyName("usage")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Usage { get; set; }
    }
}
