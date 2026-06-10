using System.Threading.Tasks;

namespace HamkareBlazor.Interfaces
{
    public interface INavigationEventReceiver
    {
        Task OnNavigation();
    }
}
