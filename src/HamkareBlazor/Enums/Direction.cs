using System.ComponentModel;
using NetEscapades.EnumGenerators;

namespace HamkareBlazor
{
    [EnumExtensions]
    public enum Direction
    {
        [Description("bottom")]
        Bottom,
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
