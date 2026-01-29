// Copyright (c) HamkareBlazor 2021
// HamkareBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;

namespace HamkareBlazor
{
#nullable enable
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class LabelAttribute : Attribute
    {
        public LabelAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}
