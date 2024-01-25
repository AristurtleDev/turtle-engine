// Copyright (c) Christopher Whitley. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.

using Microsoft.Xna.Framework;

namespace Aristurtle.TurtleEngine.Input;

/// <summary>
///     Represents the state of input for connected keyboard, mouse, and gamepads.
/// </summary>
public static class TurtleInput
{
    /// <summary>
    /// Indicates whether input is enabled.
    /// </summary>
    public static bool Enabled { get; set; }

    /// <summary>
    ///     The information representing the state of keyboard input.
    /// </summary>
    public static KeyboardInfo Keyboard { get; private set; }

    /// <summary>
    ///     The information representing the state of mouse input.
    /// </summary>
    public static MouseInfo Mouse { get; private set; }

    /// <summary>
    ///     The information representing the state of gamepad input.
    /// </summary>
    public static GamePadInfo[] GamePads { get; private set; }

    /// <summary>
    ///     Initializes the input system.
    /// </summary>
    /// <param name="gamePadCount">
    ///     The total number of gamepads to be supported by the game.
    /// </param>
    public static void Initialize(int gamePadCount)
    {
        Keyboard = new KeyboardInfo();
        Mouse = new MouseInfo();
        GamePads = new GamePadInfo[gamePadCount];
        for (int i = 0; i < gamePadCount; i++)
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
    public static void Update(GameTime gameTime)
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
