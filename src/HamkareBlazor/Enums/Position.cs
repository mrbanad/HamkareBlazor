using System.ComponentModel;
using NetEscapades.EnumGenerators;

namespace HamkareBlazor
{
    [EnumExtensions]
    public enum Position
    {
        [Description("bottom")]
        Bottom,
        [Description("center")]
        Center,
        [Description("top")]
        Top,
        [Description("left")]
        Left,
        [Description("right")]
        Right,
        [Description("start")]
        Start,
        [Description("end")]
        End
    }
}
