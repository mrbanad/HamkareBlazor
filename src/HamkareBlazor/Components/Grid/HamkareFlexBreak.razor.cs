// Copyright (c) HamkareBlazor 2021
// HamkareBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using HamkareBlazor.Utilities;

namespace HamkareBlazor;


/// <summary>
/// A component for breaking a flex display using CSS styles.
/// </summary>
public partial class HamkareFlexBreak : HamkareComponentBase
{
    /// <summary>
    /// Class names separated by spaces.
    /// </summary>
    protected string Classname =>
        new CssBuilder("hamkare-flex-break")
            .AddClass(Class)
            .Build();
}
