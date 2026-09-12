namespace HamkareBlazor;

public class PanelContext
{
    public bool IsDarkMode { get; set; }
    
    public event Action? OnChange;

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
