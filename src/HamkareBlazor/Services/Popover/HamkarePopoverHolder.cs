// Copyright (c) HamkareBlazor 2021
// HamkareBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.AspNetCore.Components;

namespace HamkareBlazor;

/// <summary>
/// Holds the state for a single popover instance managed by <see cref="PopoverService"/>.
/// </summary>
/// <remarks>
/// This is a mutable container used internally to track the render fragment, styling, and lifecycle metadata while a popover is active or queued for updates.
/// </remarks>
internal class HamkarePopoverHolder : IHamkarePopoverHolder
{
    private readonly TimeProvider _timeProvider;

    /// <inheritdoc />
    public Guid Id { get; }

    /// <inheritdoc />
    public RenderFragment? Fragment { get; internal set; }

    /// <inheritdoc />
    public bool IsConnected { get; internal set; }

    /// <inheritdoc />
    public bool IsDetached { get; internal set; }

    /// <inheritdoc />
    public string? Class { get; private set; }

    /// <inheritdoc />
    public string? Style { get; private set; }

    /// <inheritdoc />
    public object? Tag { get; private set; }

    /// <inheritdoc />
    public bool ShowContent { get; private set; }

    /// <inheritdoc />
    public DateTime? ActivationDate { get; private set; }

    /// <inheritdoc />
    public Dictionary<string, object?> UserAttributes { get; set; } = new();

    /// <inheritdoc />
    public HamkareRender? ElementReference { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="HamkarePopoverHolder"/> class.
    /// </summary>
    /// <param name="id">The unique identifier of the popover.</param>
    /// <param name="timeProvider">The time provider for obtaining the current time.</param>
    public HamkarePopoverHolder(Guid id, TimeProvider timeProvider)
    {
        Id = id;
        _timeProvider = timeProvider;
    }

    /// <summary>
    /// Sets the CSS class of the popover.
    /// </summary>
    /// <param name="class">The CSS class of the popover.</param>
    /// <returns>The updated <see cref="HamkarePopoverHolder"/> instance.</returns>
    public HamkarePopoverHolder SetClass(string @class)
    {
        Class = @class;

        return this;
    }

    /// <summary>
    /// Sets the inline styles of the popover.
    /// </summary>
    /// <param name="style">The inline styles of the popover.</param>
    /// <returns>The updated <see cref="HamkarePopoverHolder"/> instance.</returns>
    public HamkarePopoverHolder SetStyle(string style)
    {
        Style = style;

        return this;
    }

    /// <summary>
    /// Sets the visibility of the popover content.
    /// </summary>
    /// <param name="showContent">A value indicating whether the popover is visible.</param>
    /// <returns>The updated <see cref="HamkarePopoverHolder"/> instance.</returns>
    public HamkarePopoverHolder SetShowContent(bool showContent)
    {
        ShowContent = showContent;
        if (showContent)
        {
            ActivationDate = _timeProvider.GetLocalNow().DateTime;
        }
        else
        {
            ActivationDate = null;
        }

        return this;
    }

    /// <summary>
    /// Sets the user-defined data object attached to the component.
    /// </summary>
    /// <param name="tag">The user-defined data object.</param>
    /// <returns>The updated <see cref="HamkarePopoverHolder"/> instance.</returns>
    public HamkarePopoverHolder SetTag(object? tag)
    {
        Tag = tag;

        return this;
    }

    /// <summary>
    /// Sets the user-defined attributes added to the component.
    /// </summary>
    /// <param name="userAttributes">The user-defined attributes.</param>
    /// <returns>The updated <see cref="HamkarePopoverHolder"/> instance.</returns>
    public HamkarePopoverHolder SetUserAttributes(Dictionary<string, object?> userAttributes)
    {
        UserAttributes = userAttributes;

        return this;
    }

    /// <summary>
    /// Sets the content of the popover.
    /// </summary>
    /// <param name="renderFragment">The new content of the popover.</param>
    /// <returns>The updated <see cref="HamkarePopoverHolder"/> instance.</returns>
    public HamkarePopoverHolder SetFragment(RenderFragment? renderFragment)
    {
        Fragment = renderFragment;

        return this;
    }
}
