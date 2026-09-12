// Copyright (c) HamkareBlazor 2021
// HamkareBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
using Microsoft.Extensions.Logging;

namespace HamkareBlazor;

public sealed class ExceptionLogger(
    ILogger<ExceptionLogger> logger) : IExceptionLogger
{
    public void Log(Exception exception)
    {
        if (exception is HamkareException hamkareException)
        {
            logger.LogError(
                exception,
                "Hamkare exception occurred. Type: {ExceptionType}, Code: {ErrorCode}, Behavior: {Behavior}",
                exception.GetType().Name,
                hamkareException.ErrorCode,
                hamkareException.Behavior);

            return;
        }

        logger.LogError(
            exception,
            "Unhandled exception occurred. Type: {ExceptionType}",
            exception.GetType().FullName);
    }
}
