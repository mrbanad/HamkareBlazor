using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using HamkareBlazor.Resources;

namespace HamkareBlazor;

public static class FormExtensions
{
    public static List<string> ValidateModel(object model)
    {
        var messages = new List<string>();

        try
        {
            var results = new List<ValidationResult>();
            var context = new ValidationContext(model, serviceProvider: null, items: null);

            Validator.TryValidateObject(model, context, results, validateAllProperties: true);

            messages.AddRange(results
                .Where(r => !string.IsNullOrWhiteSpace(r.ErrorMessage))
                .Select(r => r.ErrorMessage!.Trim()));
        }
        catch (Exception ex)
        {
            Console.Write(ex);
            messages.Add(LanguageResource.HamkareForm_ErrorSubmit);
        }

        return messages.Distinct().ToList();
    }
}
