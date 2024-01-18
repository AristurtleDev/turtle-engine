// Copyright (c) Christopher Whitley. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.

using Microsoft.Xna.Framework.Input;

namespace TurtleEngine.Input;

/// <summary>
///     Represents a snapshot of the state of keyboard input.
/// </summary>
public sealed class KeyboardInfo
{
    /// <summary>
    ///     Gets the state of keyboard input during the previous frame.
    /// </summary>
    public KeyboardState PreviousState { get; private set; }

    /// <summary>
    ///     Gets the state of keyboard input during the current frame.
    /// </summary>
    public KeyboardState CurrentState { get; private set; }

    /// <summary>
    ///     Creates a new instance of the <see cref="KeyboardInfo"/> class.
    /// </summary>
    public KeyboardInfo()
    {
        PreviousState = new();
        CurrentState = Keyboard.GetState();
    }

    /// <summary>
    ///     Updates the state of this instance of the <see cref="KeyboardInfo"/>
    ///     class.
    /// </summary>
    public void Update()
    {
        PreviousState = CurrentState;
        CurrentState = Keyboard.GetState();
    }

    /// <summary>
    ///     Returns a value that indicates if the specified keyboard key is
    ///     currently held down.
    /// </summary>
    /// <param name="key">
    ///     The keyboard key to check.
    /// </param>
    /// <returns>
    ///     <see langword="true"/> if the specified keyboard is is currently
    ///     held down; otherwise, <see langword="false"/>.
    /// </returns>
    public bool KeyDown(Keys key) => CurrentState.IsKeyDown(key);

    /// <summary>
    ///     Returns a value that indicates if the specified keyboard key was
    ///     just pressed.
    /// </summary>
    /// <remarks>
    ///     "Just pressed" means the keyboard key was up on the previous frame
    ///     and down on the current frame.
    /// </remarks>
    /// <param name="key">
    ///     The keyboard key to check.
    /// </param>
    /// <returns>
    ///     <see langword="true"/> if the specified keyboard key was just
    ///     pressed; otherwise, <see langword="false"/>.
    /// </returns>
    public bool KeyPressed(Keys key) => KeyDown(key) && PreviousState.IsKeyUp(key);

    /// <summary>
    ///     Returns a value that indicates whether the specified keyboard key
    ///     was just released.
    /// </summary>
    /// <remarks>
    ///     "Just released" means the keyboard key was down on the previous
    ///     frame and up on the current frame.
    /// </remarks>
    /// <param name="key">
    ///     The keyboard key to check.
    /// </param>
    /// <returns>
    ///     <see langword="true"/> if the specified keyboard key was just
    ///     released; otherwise, <see langword="false"/>.
    /// </returns>
    public bool KeyReleased(Keys key) => !KeyDown(key) && PreviousState.IsKeyDown(key);
}
