using System.ComponentModel;

namespace Domain.Enums
{
    public enum OxygenUsage
    {
        [Description("sleep and exertion")]
        SleepAndExertion = 1,

        [Description("sleep")]
        Sleep = 2,

        [Description("exertion")]
        Exertion = 3,
    }
}
