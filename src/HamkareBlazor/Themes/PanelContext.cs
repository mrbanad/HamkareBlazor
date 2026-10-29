namespace HamkareBlazor;

public class PanelContext
{
    public bool IsDarkMode { get; set; }
    
    public string? BasePath { get; set; }

    public event Action? OnChange;

    public string Url(string relative) => string.IsNullOrWhiteSpace(BasePath) ? relative : $"/{BasePath}/{relative.TrimStart('/')}";
    
    public void SetDarkMode(bool value)
    {
        if (IsDarkMode != value)
        {
            IsDarkMode = value;
            NotifyStateChanged();
        }
    }

    private void NotifyStateChanged() => OnChange?.Invoke();
}
