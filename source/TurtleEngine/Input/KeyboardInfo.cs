// Copyright (c) Christopher Whitley. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.

using System.Diagnostics.CodeAnalysis;
using Microsoft.Xna.Framework.Input;

namespace TurtleEngine.Input;

/// <summary>
/// Represents a snapshot of the state of keyboard input.
/// </summary>
public struct KeyboardInfo : IEquatable<KeyboardInfo>
{
    /// <summary>
    /// A <see cref="Microsoft.Xna.Framework.Input.KeyboardState"/> value that
    /// represents the state of keyboard input during the previous frame.
    /// </summary>
    public KeyboardState PreviousState;

    /// <summary>
    /// A <see cref="Microsoft.Xna.Framework.Input.KeyboardState"/> value that
    /// represents the state of keyboard input during the current frame.
    /// </summary>
    public KeyboardState CurrentState;

    /// <summary>
    /// Initializes a new <see cref="KeyboardInfo"/> value.
    /// </summary>
    public KeyboardInfo()
    {
        PreviousState = default(KeyboardState);
        CurrentState = default(KeyboardState);
    }

    /// <summary>
    /// Initializes a new <see cref="KeyboardInfo"/> value with the specified
    /// previous and current states.
    /// </summary>
    /// <param name="previousState">
    /// A <see cref="Microsoft.Xna.Framework.Input.KeyboardState"/> value that
    /// represents the state of keyboard input during the previous frame.
    /// </param>
    /// <param name="currentState">
    /// A <see cref="Microsoft.Xna.Framework.Input.KeyboardState"/> value that
    /// represents the state of keyboard input during the previous frame.
    /// </param>
    public KeyboardInfo(KeyboardState previousState, KeyboardState currentState)
    {
        PreviousState = previousState;
        CurrentState = currentState;
    }

    /// <summary>
    /// Returns a value that indicates whether the specified key is currently
    /// down.
    /// </summary>
    /// <param name="key">The key to check.</param>
    /// <returns>
    /// <see langword="true"/> if the specified key is currently down;
    /// otherwise, <see langword="false"/>. This returns <see langword="true"/>
    /// for on every frame that the key is down.
    /// </returns>
    public readonly bool Check(Keys key)
    {
        return CurrentState.IsKeyDown(key);
    }

    /// <summary>
    /// Returns a value that indicates whether the specified key was just
    /// pressed.
    /// </summary>
    /// <param name="key">The key to check.</param>
    /// <returns>
    /// <see langword="true"/> if key was just pressed; otherwise,
    /// <see langword="false"/>. This only returns <see langword="true"/> on
    /// the first frame the key was pressed.
    /// </returns>
    public readonly bool Pressed(Keys key)
    {
        return Check(key) && PreviousState.IsKeyUp(key);
    }

    /// <summary>
    /// Returns a value that indicates whether the specified key was just
    /// released.
    /// </summary>
    /// <param name="key">The key to check.</param>
    /// <returns>
    /// <see langword="true"/> if key was just released; otherwise,
    /// <see langword="false"/>. This only returns <see langword="true"/> on the
    /// first frame the key was released.
    /// </returns>
    public readonly bool Released(Keys key)
    {
        return !Check(key) && PreviousState.IsKeyDown(key);
    }

    /// <inheritdoc/>
    public readonly bool Equals(KeyboardInfo other)
    {
        return GetHashCode() == other.GetHashCode();
    }

    /// <inheritdoc />
    public override readonly bool Equals([NotNullWhen(true)] object? obj)
    {
        return obj is KeyboardInfo other && Equals(other);
    }

    /// <inheritdoc />
    public override readonly int GetHashCode()
    {
        return HashCode.Combine(PreviousState.GetHashCode(), CurrentState.GetHashCode());
    }

    /// <inheritdoc />
    public static bool operator ==(KeyboardInfo left, KeyboardInfo right)
    {
        return left.Equals(right);
    }

    /// <inheritdoc />
    public static bool operator !=(KeyboardInfo left, KeyboardInfo right)
    {
        return !(left == right);
    }
}
