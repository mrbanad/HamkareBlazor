using Microsoft.AspNetCore.Components;

namespace HamkareBlazor;

public partial class HamkareSkeletonInput
{
    [Parameter]
    [Category(CategoryTypes.FormComponent.Appearance)] 
    public bool HaveHelper { get; set; } = true;
}
