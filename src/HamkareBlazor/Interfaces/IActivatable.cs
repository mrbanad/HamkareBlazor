using Microsoft.AspNetCore.Components.Web;

namespace HamkareBlazor.Interfaces
{
    public interface IActivatable
    {
        void Activate(object activator, MouseEventArgs args);
    }
}
