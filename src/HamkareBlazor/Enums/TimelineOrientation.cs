using System.ComponentModel;
using NetEscapades.EnumGenerators;

namespace HamkareBlazor;

/// <summary>
/// Specifies the orientation of items in a <see cref="HamkareTimeline"/>
/// </summary>
[EnumExtensions]
public enum TimelineOrientation
{
    /// <summary>
    /// Items are displayed vertically.
    /// </summary>
    [Description("vertical")]
    Vertical,

    /// <summary>
    /// Items are displayed horizontally.
    /// </summary>
    [Description("horizontal")]
    Horizontal
}
