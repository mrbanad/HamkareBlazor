// Copyright (c) HamkareBlazor 2021
// HamkareBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using HamkareBlazor.Utilities;

namespace HamkareBlazor
{
    /// <summary>
    /// Represents a dark color palette.
    /// </summary>
    public class PaletteDark : Palette
    {
        /// <inheritdoc />
        public override HamkareColor Black { get; set; } = "#27272f";

        /// <inheritdoc />
        public override HamkareColor Primary { get; set; } = "#776be7";

        /// <inheritdoc />
        public override HamkareColor Info { get; set; } = "#3299ff";

        /// <inheritdoc />
        public override HamkareColor Success { get; set; } = "#0bba83";

        /// <inheritdoc />
        public override HamkareColor Warning { get; set; } = "#ffa800";

        /// <inheritdoc />
        public override HamkareColor Error { get; set; } = "#f64e62";

        /// <inheritdoc />
        public override HamkareColor Dark { get; set; } = "#27272f";

        /// <inheritdoc />
        public override HamkareColor TextPrimary { get; set; } = "rgba(255,255,255, 0.70)";

        /// <inheritdoc />
        public override HamkareColor TextSecondary { get; set; } = "rgba(255,255,255, 0.50)";

        /// <inheritdoc />
        public override HamkareColor TextDisabled { get; set; } = "rgba(255,255,255, 0.2)";

        /// <inheritdoc />
        public override HamkareColor ActionDefault { get; set; } = "#adadb1";

        /// <inheritdoc />
        public override HamkareColor ActionDisabled { get; set; } = "rgba(255,255,255, 0.26)";

        /// <inheritdoc />
        public override HamkareColor ActionDisabledBackground { get; set; } = "rgba(255,255,255, 0.12)";

        /// <inheritdoc />
        public override HamkareColor Background { get; set; } = "#32333d";

        /// <inheritdoc />
        public override HamkareColor BackgroundGray { get; set; } = "#27272f";

        /// <inheritdoc />
        public override HamkareColor Surface { get; set; } = "#373740";

        /// <inheritdoc />
        public override HamkareColor DrawerBackground { get; set; } = "#27272f";

        /// <inheritdoc />
        public override HamkareColor DrawerText { get; set; } = "rgba(255,255,255, 0.50)";

        /// <inheritdoc />
        public override HamkareColor DrawerIcon { get; set; } = "rgba(255,255,255, 0.50)";

        /// <inheritdoc />
        public override HamkareColor AppbarBackground { get; set; } = "#27272f";

        /// <inheritdoc />
        public override HamkareColor AppbarText { get; set; } = "rgba(255,255,255, 0.70)";

        /// <inheritdoc />
        public override HamkareColor LinesDefault { get; set; } = "rgba(255,255,255, 0.12)";

        /// <inheritdoc />
        public override HamkareColor LinesInputs { get; set; } = "rgba(255,255,255, 0.3)";

        /// <inheritdoc />
        public override HamkareColor TableLines { get; set; } = "rgba(255,255,255, 0.12)";

        /// <inheritdoc />
        public override HamkareColor TableStriped { get; set; } = "rgba(255,255,255, 0.2)";

        /// <inheritdoc />
        public override HamkareColor Divider { get; set; } = "rgba(255,255,255, 0.12)";

        /// <inheritdoc />
        public override HamkareColor DividerLight { get; set; } = "rgba(255,255,255, 0.06)";

        /// <inheritdoc />
        public override HamkareColor Skeleton { get; set; } = "rgba(255,255,255, 0.11)";
    }
}
