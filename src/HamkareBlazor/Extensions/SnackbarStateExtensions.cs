// Copyright (c) HamkareBlazor 2021
// HamkareBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using HamkareBlazor.Resources;

namespace HamkareBlazor;

#nullable enable
public static class SnackbarStateExtensions
{
    internal static bool IsShowing(this SnackbarState state) => state == SnackbarState.Showing;

    internal static bool IsVisible(this SnackbarState state) => state == SnackbarState.Visible;

    internal static bool IsHiding(this SnackbarState state) => state == SnackbarState.Hiding;

    public static void IsActive(this ISnackbar snackbar, string entityName)
    {
        snackbar.Add(string.Format(LanguageResource.SuccessActiveMessage, entityName), Severity.Success);
    }

    public static void IsDeactivate(this ISnackbar snackbar, string entityName)
    {
        snackbar.Add(string.Format(LanguageResource.SuccessDeactivateMessage, entityName), Severity.Success);
    }

    public static void SuccessAddOrUpdate(this ISnackbar snackbar, string entityName)
    {
        snackbar.Add(string.Format(LanguageResource.SubmitSuccessMessage, entityName), Severity.Success);
    }

    public static void ErrorAddOrUpdate(this ISnackbar snackbar, string entityName)
    {
        snackbar.Add(string.Format(LanguageResource.ErrorSubmit, entityName), Severity.Error);
    }

    public static void Delete(this ISnackbar snackbar, string entityName)
    {
        snackbar.Add(string.Format(LanguageResource.RequiredNotFound, entityName), Severity.Error);
    }

    public static void ErrorRequired(this ISnackbar snackbar, string entityName)
    {
        snackbar.Add(string.Format(LanguageResource.RequiredNotFound, entityName), Severity.Error);
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
