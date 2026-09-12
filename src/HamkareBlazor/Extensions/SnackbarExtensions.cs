using HamkareBlazor.Resources;

namespace HamkareBlazor;

public static class SnackbarExtensions
{
    public static void IsActive(this ISnackbar snackbar, string entityName)
    {
        snackbar.Add(string.Format(LanguageResource.HamkareSnackbar_SuccessActiveMessage, entityName), Severity.Success);
    }

    public static void IsDeactivate(this ISnackbar snackbar, string entityName)
    {
        snackbar.Add(string.Format(LanguageResource.HamkareSnackbar_SuccessDeactivateMessage, entityName), Severity.Success);
    }

    public static void SuccessAddOrUpdate(this ISnackbar snackbar, string entityName)
    {
        snackbar.Add(string.Format(LanguageResource.HamkareForm_SuccessSubmit, entityName), Severity.Success);
    }

    public static void ErrorAddOrUpdate(this ISnackbar snackbar, string entityName)
    {
        snackbar.Add(string.Format(LanguageResource.HamkareForm_ErrorSubmit, entityName), Severity.Error);
    }

    public static void Delete(this ISnackbar snackbar, string entityName)
    {
        snackbar.Add(string.Format(LanguageResource.HamkareSnackbar_RequiredNotFound, entityName), Severity.Error);
    }

    public static void ErrorRequired(this ISnackbar snackbar, string entityName)
    {
        snackbar.Add(string.Format(LanguageResource.HamkareSnackbar_RequiredNotFound, entityName), Severity.Error);
    }

    public static void Success(this ISnackbar snackbar, string message)
    {
        snackbar.Add(message, Severity.Success);
    }

    public static void Error(this ISnackbar snackbar, string message)
    {
        snackbar.Add(message, Severity.Error);
    }

    public static void Error(this ISnackbar snackbar, List<string> messages)
    {
        foreach (var item in messages)
            snackbar.Add(item, Severity.Error);
    }

    public static void Info(this ISnackbar snackbar, List<string> messages)
    {
        foreach (var item in messages)
            snackbar.Add(item, Severity.Info);
    }

    public static void Info(this ISnackbar snackbar, string message)
    {
        snackbar.Add(message, Severity.Info);
    }

    public static void Warning(this ISnackbar snackbar, string message)
    {
        snackbar.Add(message, Severity.Warning);
    }

    public static void ShowErrorSnackbar(this Exception exception, ISnackbar snackbar, string entityName)
    {
        if (exception.Source == "Validate" || exception.Source == "Custom")
            snackbar.Error(exception.Message);
        else
            snackbar.ErrorAddOrUpdate(entityName);
    }
}
