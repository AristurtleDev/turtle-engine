// Copyright (c) Christopher Whitley. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.

#pragma warning disable CS1591
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Aristurtle.TurtleEngine;

public static class Ensure
{
    [Conditional("DEBUG")]
    public static void NotNull<TValue>(TValue value, [CallerArgumentExpression(nameof(value))] string paramName = null)
        where TValue : class =>
        ArgumentNullException.ThrowIfNull(value, paramName);

    [Conditional("DEBUG")]
    public static void GreaterThanOrEqualToZero(int value, [CallerArgumentExpression(nameof(value))] string paramName = null)
    {
        if (value < 0)
        {
            throw new ArgumentOutOfRangeException(paramName, $"{paramName} must be greater than or equal to zero");
        }
    }

    [Conditional("DEBUG")]
    public static void GreaterThanZero(int value, [CallerArgumentExpression(nameof(value))] string paramName = null)
    {
        if (value <= 0)
        {
            throw new ArgumentOutOfRangeException(paramName, $"{paramName} must be greater than zero");
        }
    }
}
#pragma warning restore CS1591
