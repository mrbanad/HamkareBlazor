namespace HamkareBlazor.Services
{
#nullable enable
    // Add additional configuration objects here when adding new services

    /// <summary>
    /// Common services configuration required by HamkareBlazor components
    /// </summary>
    public class HamkareServicesConfiguration
    {
        public SnackbarConfiguration SnackbarConfiguration { get; set; } = new SnackbarConfiguration();

        public ResizeOptions ResizeOptions { get; set; } = new ResizeOptions();

        public ResizeObserverOptions ResizeObserverOptions { get; set; } = new ResizeObserverOptions();

        public PopoverOptions PopoverOptions { get; set; } = new PopoverOptions();
    }
}
