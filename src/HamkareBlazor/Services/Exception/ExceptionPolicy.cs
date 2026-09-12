// Copyright (c) HamkareBlazor 2021
// HamkareBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace HamkareBlazor;

public sealed class ExceptionPolicy : IExceptionPolicy
{
    public ExceptionBehavior GetBehavior(Exception exception)
    {
        if (exception is HamkareException hamkareException)
            return hamkareException.Behavior;

        if (exception is TaskCanceledException)
            return ExceptionBehavior.Silence;

        if (exception is OperationCanceledException)
            return ExceptionBehavior.Silence;

        return ExceptionBehavior.Redirect;
    }
}
