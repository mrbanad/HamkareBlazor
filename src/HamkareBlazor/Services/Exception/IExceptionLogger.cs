// Copyright (c) HamkareBlazor 2021
// HamkareBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace HamkareBlazor;

public interface IExceptionLogger
{
    void Log(Exception exception);
}
