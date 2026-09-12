using Microsoft.JSInterop;

namespace HamkareBlazor;

public class BlazorModel<TBlazorModel>(DotNetObjectReference<TBlazorModel> dotNetObject)
    where TBlazorModel : class
{
    public DotNetObjectReference<TBlazorModel> DotNetObject { get; set; } = dotNetObject;
}
