// Copyright (c) HamkareBlazor 2021
// HamkareBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using HamkareBlazor.Resources;

namespace HamkareBlazor;

public sealed class ErrorTranslator : IErrorTranslator
{
    public string Translate(Exception exception)
    {
        if (exception is HamkareException hamkareException)
            return hamkareException.Message;

        return exception switch
        {
            TaskCanceledException =>
                LanguageResource.Error_ConnectWithServer,

            OperationCanceledException =>
                LanguageResource.Error_ConnectWithServer,

            _ when exception.Message == "TypeError: Failed to fetch" =>
                LanguageResource.Error_ConnectWithServer,

            _ => exception.Message
        };
    }
}
