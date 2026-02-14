using HamkareBlazor.Resources;

namespace HamkareBlazor;

public static class DialogExtension
{
    public static async Task<bool?> RemoveDialog(this IDialogService dialogService)
    {
        return await dialogService.ShowMessageBoxAsync(
            LanguageResource.GetResourceString(LanguageResource.Remove),
            LanguageResource.GetResourceString(LanguageResource.RemoveMessage) ?? string.Empty,
            yesText: LanguageResource.GetResourceString(LanguageResource.HamkareDataGrid_Apply) ?? string.Empty ,LanguageResource.GetResourceString(LanguageResource.HamkareDataGrid_Cancel));
    }

    public static async Task<bool?> OperationDialog(this IDialogService dialogService, string title, string message)
    {
        return await dialogService.ShowMessageBoxAsync(
            title,
            message,
            yesText: LanguageResource.GetResourceString(LanguageResource.HamkareDataGrid_Apply) ?? string.Empty, cancelText: LanguageResource.GetResourceString(LanguageResource.HamkareDataGrid_Cancel));
    }
}
