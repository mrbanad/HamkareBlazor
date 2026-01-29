using Microsoft.AspNetCore.Components.Web;

namespace HamkareBlazor.Interfaces
{
#nullable enable
    public interface IActivatable
    {
        void Activate(object activator, MouseEventArgs args);
    }
}
