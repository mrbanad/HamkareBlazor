using HamkareBlazor.Resources;

namespace HamkareBlazor;

public static class DialogExtension
{
    public static async Task<bool?> RemoveDialog(this IDialogService dialogService)
    {
        return await dialogService.ShowMessageBoxAsync(
            LanguageResource.Remove,
            LanguageResource.RemoveMessage,
            yesText: LanguageResource.HamkareDataGrid_Apply, cancelText: LanguageResource.HamkareDataGrid_Cancel);
    }

    public static async Task<bool?> OperationDialog(this IDialogService dialogService, string title, string message)
    {
        return await dialogService.ShowMessageBoxAsync(
            title,
            message,
            yesText: LanguageResource.HamkareDataGrid_Apply, cancelText: LanguageResource.HamkareDataGrid_Cancel);
    }
}
