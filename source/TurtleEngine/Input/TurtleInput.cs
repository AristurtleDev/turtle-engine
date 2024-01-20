// Copyright (c) Christopher Whitley. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.

using Microsoft.Xna.Framework;

namespace TurtleEngine.Input;

/// <summary>
///     Represents the state of input for connected keyboard, mouse, and gamepads.
/// </summary>
public sealed class TurtleInput
{
    /// <summary>
    /// Indicates whether input is enabled.
    /// </summary>
    public bool Enabled;

    /// <summary>
    ///     The information representing the state of keyboard input.
    /// </summary>
    public KeyboardInfo Keyboard;

    /// <summary>
    ///     The information representing the state of mouse input.
    /// </summary>
    public MouseInfo Mouse;

    /// <summary>
    ///     The information representing the state of gamepad input.
    /// </summary>
    public GamePadInfo[] GamePads;

    /// <summary>
    ///     Creates a new instance of the <see cref="TurtleInput"/> class.
    /// </summary>
    public TurtleInput()
    {
        Keyboard = new KeyboardInfo();
        Mouse = new MouseInfo();
        GamePads = new GamePadInfo[4];
        for (int i = 0; i < 4; i++)
        {
            GamePads[i] = new GamePadInfo((PlayerIndex)i);
        }
    }

    /// <summary>
    ///     Updates the internal state values.
    /// </summary>
    /// <param name="gameTime">
    ///     A snapshot of the timing values for the current frame.
    /// </param>
    public void Update(GameTime gameTime)
    {
        if (!Enabled)
        {
            return;
        }

        Keyboard.Update();
        Mouse.Update();
        for (int i = 0; i < GamePads.Length; i++)
        {
            GamePads[i].Update(gameTime);
        }
    }
}
