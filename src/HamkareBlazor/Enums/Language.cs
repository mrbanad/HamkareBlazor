using System.ComponentModel.DataAnnotations;
using HamkareBlazor.Resources;

namespace HamkareBlazor;

public enum Language : byte
{
    [Display(Name = nameof(LanguageResource.Fa), ResourceType = typeof(LanguageResource))]
    Fa = 0,

    [Display(Name = nameof(LanguageResource.Fa), ResourceType = typeof(LanguageResource))]
    faIR = 0,

    [Display(Name = nameof(LanguageResource.En), ResourceType = typeof(LanguageResource))]
    En = 1,

    [Display(Name = nameof(LanguageResource.Ar), ResourceType = typeof(LanguageResource))]
    Ar = 2,

    [Display(Name = nameof(LanguageResource.Tr), ResourceType = typeof(LanguageResource))]
    Tr = 3
}
