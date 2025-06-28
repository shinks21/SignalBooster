using Domain.Enums;

namespace Application.ProcessSignalBoosterFile.Responses
{
    public record SignalBoosterResponse(string FileText)
    {
        public Device Device { get; set; } = Device.Unknown;

        public MaskType? MaskType { get; set; }

        public AddOn? AddOn { get; set; }

        public string Qualifier { get; set; } = String.Empty;

        public string OrderingProvider { get; set; } = "Unknown";

        public string OxygenLiters = string.Empty;

        public OxygenUsage? OxygenUsage { get; set; }

        public string JsonToSend { get; set; } = null!;
    }
}
