using System.Text.Json.Serialization;

namespace Application.ProcessSignalBoosterFile.Models
{
    public class PatientOrderJson
    {
        [JsonPropertyName("device")]
        public string Device { get; set; } = string.Empty;

        [JsonPropertyName("mask_type")]
        public string? MaskType { get; set; } = string.Empty;

        [JsonPropertyName("add_ons")]
        public IEnumerable<PatientOrderAddOnsJson>? AddOns { get; set; }

        [JsonPropertyName("qualifier")]
        public string Qualifier { get; set; } = string.Empty;

        [JsonPropertyName("ordering_provider")]
        public string OrderingProvider { get; set; } = string.Empty;

        [JsonPropertyName("liters")]
        public string? Liters { get; set; }

        [JsonPropertyName("usage")]
        public string? Usage { get; set; }
    }

    public class PatientOrderAddOnsJson
    {
        public string AddOn { get; set; } = string.Empty;
    }

}
