using Microsoft.AspNetCore.Components;
using HamkareBlazor.Utilities;

namespace HamkareBlazor;


/// <summary>
/// A header displayed as part of a <see cref="HamkareList{T}"/>.
/// </summary>
/// <remarks>
/// Typically used to describe a list.
/// </remarks>
/// <seealso cref="HamkareList{T}"/>
/// <seealso cref="HamkareListItem{T}"/>
public partial class HamkareListSubheader : HamkareComponentBase
{
    protected string Classname =>
        new CssBuilder("hamkare-list-subheader")
            .AddClass("hamkare-list-subheader-gutters", Gutters)
            .AddClass("hamkare-list-subheader-inset", Inset)
            .AddClass(Class)
            .Build();

    /// <summary>
    /// The content within this header.
    /// </summary>
    [Parameter]
    [Category(CategoryTypes.List.Behavior)]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// Applies left and right padding to all list items.
    /// </summary>
    /// <remarks>
    /// Defaults to <c>true</c>.
    /// </remarks>
    [Parameter]
    [Category(CategoryTypes.List.Appearance)]
    public bool Gutters { get; set; } = true;

    /// <summary>
    /// Applies an indent to this header.
    /// </summary>
    [Parameter]
    [Category(CategoryTypes.List.Appearance)]
    public bool Inset { get; set; }
}
