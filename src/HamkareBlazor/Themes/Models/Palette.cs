// Copyright (c) HamkareBlazor 2021
// HamkareBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Text.Json.Serialization;
using HamkareBlazor.Utilities;

namespace HamkareBlazor
{
#nullable enable
    /// <summary>
    /// Represents a palette of colors used throughout the application.
    /// </summary>
    [JsonDerivedType(typeof(PaletteLight), typeDiscriminator: nameof(PaletteLight))]
    [JsonDerivedType(typeof(PaletteDark), typeDiscriminator: nameof(PaletteDark))]
    public abstract class Palette
    {
        private HamkareColor? _primaryDarken;
        private HamkareColor? _primaryLighten;
        private HamkareColor? _secondaryDarken;
        private HamkareColor? _secondaryLighten;
        private HamkareColor? _tertiaryDarken;
        private HamkareColor? _tertiaryLighten;
        private HamkareColor? _infoDarken;
        private HamkareColor? _infoLighten;
        private HamkareColor? _successDarken;
        private HamkareColor? _successLighten;
        private HamkareColor? _warningDarken;
        private HamkareColor? _warningLighten;
        private HamkareColor? _errorDarken;
        private HamkareColor? _errorLighten;
        private HamkareColor? _darkDarken;
        private HamkareColor? _darkLighten;

        /// <summary>
        /// The black color.
        /// </summary>
        public virtual HamkareColor Black { get; set; } = "#272c34";

        /// <summary>
        /// The white color.
        /// </summary>
        public virtual HamkareColor White { get; set; } = Colors.Shades.White;

        /// <summary>
        /// The primary color.
        /// </summary>
        public virtual HamkareColor Primary { get; set; } = "#594AE2";

        /// <summary>
        /// The contrast text color for the primary color.
        /// </summary>
        public virtual HamkareColor PrimaryContrastText { get; set; } = Colors.Shades.White;

        /// <summary>
        /// The secondary color.
        /// </summary>
        public virtual HamkareColor Secondary { get; set; } = Colors.Pink.Accent2;

        /// <summary>
        /// The contrast text color for the secondary color.
        /// </summary>
        public virtual HamkareColor SecondaryContrastText { get; set; } = Colors.Shades.White;

        /// <summary>
        /// The tertiary color.
        /// </summary>
        public virtual HamkareColor Tertiary { get; set; } = "#1EC8A5";

        /// <summary>
        /// The contrast text color for the tertiary color.
        /// </summary>
        public virtual HamkareColor TertiaryContrastText { get; set; } = Colors.Shades.White;

        /// <summary>
        /// The info color.
        /// </summary>
        public virtual HamkareColor Info { get; set; } = Colors.Blue.Default;

        /// <summary>
        /// The contrast text color for the info color.
        /// </summary>
        public virtual HamkareColor InfoContrastText { get; set; } = Colors.Shades.White;

        /// <summary>
        /// The success color.
        /// </summary>
        public virtual HamkareColor Success { get; set; } = Colors.Green.Accent4;

        /// <summary>
        /// The contrast text color for the success color.
        /// </summary>
        public virtual HamkareColor SuccessContrastText { get; set; } = Colors.Shades.White;

        /// <summary>
        /// The warning color.
        /// </summary>
        public virtual HamkareColor Warning { get; set; } = Colors.Orange.Default;

        /// <summary>
        /// The contrast text color for the warning color.
        /// </summary>
        public virtual HamkareColor WarningContrastText { get; set; } = Colors.Shades.White;

        /// <summary>
        /// The error color.
        /// </summary>
        public virtual HamkareColor Error { get; set; } = Colors.Red.Default;

        /// <summary>
        /// The contrast text color for the error color.
        /// </summary>
        public virtual HamkareColor ErrorContrastText { get; set; } = Colors.Shades.White;

        /// <summary>
        /// The dark color.
        /// </summary>
        public virtual HamkareColor Dark { get; set; } = Colors.Gray.Darken3;

        /// <summary>
        /// The contrast text color for the dark color.
        /// </summary>
        public virtual HamkareColor DarkContrastText { get; set; } = Colors.Shades.White;

        /// <summary>
        /// The primary text color.
        /// </summary>
        public virtual HamkareColor TextPrimary { get; set; } = Colors.Gray.Darken3;

        /// <summary>
        /// The secondary text color.
        /// </summary>
        public virtual HamkareColor TextSecondary { get; set; } = new HamkareColor(Colors.Shades.Black).SetAlpha(0.54).ToString(HamkareColorOutputFormats.RGBA);

        /// <summary>
        /// The disabled text color.
        /// </summary>
        public virtual HamkareColor TextDisabled { get; set; } = new HamkareColor(Colors.Shades.Black).SetAlpha(0.38).ToString(HamkareColorOutputFormats.RGBA);

        /// <summary>
        /// The default action color.
        /// </summary>
        public virtual HamkareColor ActionDefault { get; set; } = new HamkareColor(Colors.Shades.Black).SetAlpha(0.54).ToString(HamkareColorOutputFormats.RGBA);

        /// <summary>
        /// The disabled action color.
        /// </summary>
        public virtual HamkareColor ActionDisabled { get; set; } = new HamkareColor(Colors.Shades.Black).SetAlpha(0.26).ToString(HamkareColorOutputFormats.RGBA);

        /// <summary>
        /// The background color for disabled actions.
        /// </summary>
        public virtual HamkareColor ActionDisabledBackground { get; set; } = new HamkareColor(Colors.Shades.Black).SetAlpha(0.12).ToString(HamkareColorOutputFormats.RGBA);

        /// <summary>
        /// The background color.
        /// </summary>
        public virtual HamkareColor Background { get; set; } = Colors.Shades.White;

        /// <summary>
        /// The gray background color.
        /// </summary>
        public virtual HamkareColor BackgroundGray { get; set; } = Colors.Gray.Lighten4;

        /// <summary>
        /// The surface color.
        /// </summary>
        public virtual HamkareColor Surface { get; set; } = Colors.Shades.White;

        /// <summary>
        /// The drawer background color.
        /// </summary>
        public virtual HamkareColor DrawerBackground { get; set; } = Colors.Shades.White;

        /// <summary>
        /// The drawer text color.
        /// </summary>
        public virtual HamkareColor DrawerText { get; set; } = Colors.Gray.Darken3;

        /// <summary>
        /// The drawer icon color.
        /// </summary>
        public virtual HamkareColor DrawerIcon { get; set; } = Colors.Gray.Darken2;

        /// <summary>
        /// The appbar background color.
        /// </summary>
        public virtual HamkareColor AppbarBackground { get; set; } = "#594AE2";

        /// <summary>
        /// The appbar text color.
        /// </summary>
        public virtual HamkareColor AppbarText { get; set; } = Colors.Shades.White;

        /// <summary>
        /// The default color for lines.
        /// </summary>
        public virtual HamkareColor LinesDefault { get; set; } = new HamkareColor(Colors.Shades.Black).SetAlpha(0.12).ToString(HamkareColorOutputFormats.RGBA);

        /// <summary>
        /// The color for input lines.
        /// </summary>
        public virtual HamkareColor LinesInputs { get; set; } = Colors.Gray.Lighten1;

        /// <summary>
        /// The color for table lines.
        /// </summary>
        public virtual HamkareColor TableLines { get; set; } = new HamkareColor(Colors.Gray.Lighten2).SetAlpha(1.0).ToString(HamkareColorOutputFormats.RGBA);

        /// <summary>
        /// The color for striped rows in a table.
        /// </summary>
        public virtual HamkareColor TableStriped { get; set; } = new HamkareColor(Colors.Shades.Black).SetAlpha(0.02).ToString(HamkareColorOutputFormats.RGBA);

        /// <summary>
        /// The color for table rows on hover.
        /// </summary>
        public virtual HamkareColor TableHover { get; set; } = new HamkareColor(Colors.Shades.Black).SetAlpha(0.04).ToString(HamkareColorOutputFormats.RGBA);

        /// <summary>
        /// The color for dividers.
        /// </summary>
        public virtual HamkareColor Divider { get; set; } = Colors.Gray.Lighten2;

        /// <summary>
        /// The light color for dividers.
        /// </summary>
        public virtual HamkareColor DividerLight { get; set; } = new HamkareColor(Colors.Shades.Black).SetAlpha(0.8).ToString(HamkareColorOutputFormats.RGBA);

        /// <summary>
        /// The color for skeletons.
        /// </summary>
        public virtual HamkareColor Skeleton { get; set; } = new HamkareColor("rgba(0, 0, 0, 0.11)").ToString(HamkareColorOutputFormats.RGBA);

        /// <summary>
        /// The darkened value of the primary color.<br/>
        /// This is calculated using <see cref="HamkareColor.ColorRgbDarken"/> if not set.
        /// </summary>
        public virtual string PrimaryDarken
        {
            get => (_primaryDarken ??= Primary.ColorRgbDarken()).ToString(HamkareColorOutputFormats.RGB);
            set => _primaryDarken = value;
        }

        /// <summary>
        /// The lightened value of the primary color.<br/>
        /// This is calculated using <see cref="HamkareColor.ColorRgbLighten"/> if not set.
        /// </summary>
        public virtual string PrimaryLighten
        {
            get => (_primaryLighten ??= Primary.ColorRgbLighten()).ToString(HamkareColorOutputFormats.RGB);
            set => _primaryLighten = value;
        }

        /// <summary>
        /// The darkened value of the secondary color.<br/>
        /// This is calculated using <see cref="HamkareColor.ColorRgbDarken"/> if not set.
        /// </summary>
        public virtual string SecondaryDarken
        {
            get => (_secondaryDarken ??= Secondary.ColorRgbDarken()).ToString(HamkareColorOutputFormats.RGB);
            set => _secondaryDarken = value;
        }

        /// <summary>
        /// The lightened value of the secondary color.<br/>
        /// This is calculated using <see cref="HamkareColor.ColorRgbLighten"/> if not set.
        /// </summary>
        public virtual string SecondaryLighten
        {
            get => (_secondaryLighten ??= Secondary.ColorRgbLighten()).ToString(HamkareColorOutputFormats.RGB);
            set => _secondaryLighten = value;
        }

        /// <summary>
        /// The darkened value of the tertiary color.<br/>
        /// This is calculated using <see cref="HamkareColor.ColorRgbDarken"/> if not set.
        /// </summary>
        public virtual string TertiaryDarken
        {
            get => (_tertiaryDarken ??= Tertiary.ColorRgbDarken()).ToString(HamkareColorOutputFormats.RGB);
            set => _tertiaryDarken = value;
        }

        /// <summary>
        /// The lightened value of the tertiary color.<br/>
        /// This is calculated using <see cref="HamkareColor.ColorRgbLighten"/> if not set.
        /// </summary>
        public virtual string TertiaryLighten
        {
            get => (_tertiaryLighten ??= Tertiary.ColorRgbLighten()).ToString(HamkareColorOutputFormats.RGB);
            set => _tertiaryLighten = value;
        }

        /// <summary>
        /// The darkened value of the info color.<br/>
        /// This is calculated using <see cref="HamkareColor.ColorRgbDarken"/> if not set.
        /// </summary>
        public virtual string InfoDarken
        {
            get => (_infoDarken ??= Info.ColorRgbDarken()).ToString(HamkareColorOutputFormats.RGB);
            set => _infoDarken = value;
        }

        /// <summary>
        /// The lightened value of the info color.<br/>
        /// This is calculated using <see cref="HamkareColor.ColorRgbLighten"/> if not set.
        /// </summary>
        public virtual string InfoLighten
        {
            get => (_infoLighten ??= Info.ColorRgbLighten()).ToString(HamkareColorOutputFormats.RGB);
            set => _infoLighten = value;
        }

        /// <summary>
        /// The darkened value of the success color.<br/>
        /// This is calculated using <see cref="HamkareColor.ColorRgbDarken"/> if not set.
        /// </summary>
        public virtual string SuccessDarken
        {
            get => (_successDarken ??= Success.ColorRgbDarken()).ToString(HamkareColorOutputFormats.RGB);
            set => _successDarken = value;
        }

        /// <summary>
        /// The lightened value of the success color.<br/>
        /// This is calculated using <see cref="HamkareColor.ColorRgbLighten"/> if not set.
        /// </summary>
        public virtual string SuccessLighten
        {
            get => (_successLighten ??= Success.ColorRgbLighten()).ToString(HamkareColorOutputFormats.RGB);
            set => _successLighten = value;
        }

        /// <summary>
        /// The darkened value of the warning color.<br/>
        /// This is calculated using <see cref="HamkareColor.ColorRgbDarken"/> if not set.
        /// </summary>
        public virtual string WarningDarken
        {
            get => (_warningDarken ??= Warning.ColorRgbDarken()).ToString(HamkareColorOutputFormats.RGB);
            set => _warningDarken = value;
        }

        /// <summary>
        /// The lightened value of the warning color.<br/>
        /// This is calculated using <see cref="HamkareColor.ColorRgbLighten"/> if not set.
        /// </summary>
        public virtual string WarningLighten
        {
            get => (_warningLighten ??= Warning.ColorRgbLighten()).ToString(HamkareColorOutputFormats.RGB);
            set => _warningLighten = value;
        }

        /// <summary>
        /// The darkened value of the error color.<br/>
        /// This is calculated using <see cref="HamkareColor.ColorRgbDarken"/> if not set.
        /// </summary>
        public virtual string ErrorDarken
        {
            get => (_errorDarken ??= Error.ColorRgbDarken()).ToString(HamkareColorOutputFormats.RGB);
            set => _errorDarken = value;
        }

        /// <summary>
        /// The lightened value of the error color.<br/>
        /// This is calculated using <see cref="HamkareColor.ColorRgbLighten"/> if not set.
        /// </summary>
        public virtual string ErrorLighten
        {
            get => (_errorLighten ??= Error.ColorRgbLighten()).ToString(HamkareColorOutputFormats.RGB);
            set => _errorLighten = value;
        }

        /// <summary>
        /// The darkened value of the dark color.<br/>
        /// This is calculated using <see cref="HamkareColor.ColorRgbDarken"/> if not set.
        /// </summary>
        public virtual string DarkDarken
        {
            get => (_darkDarken ??= Dark.ColorRgbDarken()).ToString(HamkareColorOutputFormats.RGB);
            set => _darkDarken = value;
        }

        /// <summary>
        /// The lightened value of the dark color.<br/>
        /// This is calculated using <see cref="HamkareColor.ColorRgbLighten"/> if not set.
        /// </summary>
        public virtual string DarkLighten
        {
            get => (_darkLighten ??= Dark.ColorRgbLighten()).ToString(HamkareColorOutputFormats.RGB);
            set => _darkLighten = value;
        }

        /// <summary>
        /// The opacity value for most borders.
        /// </summary>
        public virtual double BorderOpacity { get; set; } = 1.0;

        /// <summary>
        /// The opacity value for hover effect.
        /// </summary>
        public virtual double HoverOpacity { get; set; } = 0.06;

        /// <summary>
        /// The opacity for the ripple effect.
        /// </summary>
        public virtual double RippleOpacity { get; set; } = 0.1;

        /// <summary>
        /// The opacity for the ripple effect on specific elements like filled buttons.
        /// </summary>
        public virtual double RippleOpacitySecondary { get; set; } = 0.2;

        /// <summary>
        /// The default gray color.
        /// </summary>
        public virtual string GrayDefault { get; set; } = Colors.Gray.Default;

        /// <summary>
        /// The lightened gray color.
        /// </summary>
        public virtual string GrayLight { get; set; } = Colors.Gray.Lighten1;

        /// <summary>
        /// The further lightened gray color.
        /// </summary>
        public virtual string GrayLighter { get; set; } = Colors.Gray.Lighten2;

        /// <summary>
        /// The darkened gray color.
        /// </summary>
        public virtual string GrayDark { get; set; } = Colors.Gray.Darken1;

        /// <summary>
        /// The further darkened gray color.
        /// </summary>
        public virtual string GrayDarker { get; set; } = Colors.Gray.Darken2;

        /// <summary>
        /// The dark overlay color.
        /// </summary>
        public virtual string OverlayDark { get; set; } = new HamkareColor("#212121").SetAlpha(0.5).ToString(HamkareColorOutputFormats.RGBA);

        /// <summary>
        /// The light overlay color.
        /// </summary>
        public virtual string OverlayLight { get; set; } = new HamkareColor(Colors.Shades.White).SetAlpha(0.5).ToString(HamkareColorOutputFormats.RGBA);
    }
}
