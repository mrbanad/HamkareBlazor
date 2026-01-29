using System.Threading.Tasks;

namespace HamkareBlazor.Interfaces
{
#nullable enable
    public interface INavigationEventReceiver
    {
        Task OnNavigation();
    }
}
