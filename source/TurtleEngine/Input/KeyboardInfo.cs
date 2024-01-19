// Copyright (c) Christopher Whitley. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.

using Microsoft.Xna.Framework.Input;

namespace TurtleEngine.Input;

/// <summary>
/// Represents a snapshot of the state of keyboard input during the previous and
/// current frame.
/// </summary>
public sealed class KeyboardInfo
{
    /// <summary>
    /// Gets the state of keyboard input during the previous frame.
    /// </summary>
    public KeyboardState PreviousState { get; private set; }

    /// <summary>
    /// Gets the state of keyboard input during the current frame.
    /// </summary>
    public KeyboardState CurrentState { get; private set; }

    internal KeyboardInfo()
    {
        PreviousState = default(KeyboardState);
        CurrentState = default(KeyboardState);
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
    public bool Check(Keys key)
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
    public bool Pressed(Keys key)
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
    public bool Released(Keys key)
    {
        return !Check(key) && PreviousState.IsKeyDown(key);
    }
}
