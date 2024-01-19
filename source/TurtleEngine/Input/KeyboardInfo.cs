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
    /// Returns a value that indicates whether the specified keyboard key is
    /// currently held down.
    /// </summary>
    /// <param name="key">The keyboard key to check.</param>
    /// <returns>
    /// <see langword="true"/> if the specified keyboard is is currently held
    /// down; otherwise, <see langword="false"/>.
    /// </returns>
    public readonly bool KeyDown(Keys key)
    {
        return CurrentState.IsKeyDown(key);
    }

    /// <summary>
    /// Returns a value that indicates whether the specified keyboard key was
    /// just pressed.
    /// </summary>
    /// <remarks>
    /// "Just pressed" means the keyboard key was up on the previous frame and
    /// down on the current frame.
    /// </remarks>
    /// <param name="key">The keyboard key to check.</param>
    /// <returns>
    /// <see langword="true"/> if the specified keyboard key was just pressed;
    /// otherwise, <see langword="false"/>.
    /// </returns>
    public readonly bool KeyPressed(Keys key)
    {
        return KeyDown(key) && PreviousState.IsKeyUp(key);
    }

    /// <summary>
    /// Returns a value that indicates whether the specified keyboard key was
    /// just released.
    /// </summary>
    /// <remarks>
    /// "Just released" means the keyboard key was down on the previous frame
    /// and up on the current frame.
    /// </remarks>
    /// <param name="key">The keyboard key to check.</param>
    /// <returns>
    /// <see langword="true"/> if the specified keyboard key was just released;
    /// otherwise, <see langword="false"/>.
    /// </returns>
    public readonly bool KeyReleased(Keys key)
    {
        return !KeyDown(key) && PreviousState.IsKeyDown(key);
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
