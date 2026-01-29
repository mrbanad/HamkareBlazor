using System.ComponentModel;
using NetEscapades.EnumGenerators;

namespace HamkareBlazor
{
    [EnumExtensions]
    public enum Severity
    {
        [Description("normal")]
        Normal,
        [Description("info")]
        Info,
        [Description("success")]
        Success,
        [Description("warning")]
        Warning,
        [Description("error")]
        Error
    }
}
