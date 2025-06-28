using System.ComponentModel;

namespace Domain.Enums
{
    public enum Device
    {
        [Description("Unknown")]
        Unknown = 0,

        [Description("CPAP")]
        CPAP = 1,

        [Description("Oxygen Tank")]
        Oxygen = 2,

        [Description("Wheelchair")]
        Wheelchair = 3,
    }
}
