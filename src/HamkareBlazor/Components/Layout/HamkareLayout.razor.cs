using HamkareBlazor.Utilities;

namespace HamkareBlazor;


/// <summary>
/// A component which defines a common structure for multiple pages.
/// </summary>
/// <remarks>
/// Layouts often contain <see cref="HamkareAppBar"/> and <see cref="HamkareDrawer"/> components.  The <see cref="HamkareMainContent"/> component is used to contain page content.  
/// In your layout component, but above this component, add <see cref="HamkareThemeProvider"/>, <see cref="HamkarePopoverProvider"/>, <see cref="HamkareDialogProvider"/>, and <see cref="HamkareSnackbarProvider"/> components to enable all HamkareBlazor features.
/// </remarks>
/// <seealso cref="HamkareMainContent"/>
public partial class HamkareLayout : HamkareDrawerContainer
{
    protected override string Classname =>
        new CssBuilder("hamkare-layout")
            .AddClass(base.Classname)
            .Build();

    public HamkareLayout()
    {
        Fixed = true;
    }
}
