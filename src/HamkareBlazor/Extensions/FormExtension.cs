// Copyright (c) HamkareBlazor 2021
// HamkareBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using HamkareBlazor.Resources;

namespace HamkareBlazor;

public static class FormExtension
{
    [UnconditionalSuppressMessage("Trimming", "IL2026:Members annotated with 'RequiresUnreferencedCodeAttribute' require dynamic access otherwise can break functionality when trimming application code", Justification = "<Pending>")]
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
            messages.Add(LanguageResource.ErrorSubmit);
        }

        return messages.Distinct().ToList();
    }
}
