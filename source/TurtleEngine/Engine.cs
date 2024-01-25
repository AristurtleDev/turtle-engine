// Copyright (c) Christopher Whitley. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Aristurtle.TurtleEngine.Input;

namespace Aristurtle.TurtleEngine;

/// <summary>
///     The engine that the game runs on top of.
/// </summary>
public class Engine : Game
{
    /// <summary>
    ///     Gets or Sets if pressing an escape key should exit the game.
    ///     <see langword="true"/> if pressing an escape key should exit the game; otherwise, <see langword="false"/>.
    /// </summary>
    public static bool ExitOnEscapeKeyPress = true;

    /// <summary>
    ///     Gets or Sets if the game should be paused when focus is lost.
    /// </summary>
    public static bool PauseOnFocusLost = true;

    /// <summary>
    ///     Creates a new instance of the <see cref="Engine"/> class.
    /// </summary>
    /// <param name="width">
    ///     The width, in pixels, of the game window.
    /// </param>
    /// <param name="height">
    ///     The height, in pixels, of the game window.
    /// </param>
    /// <param name="fullscreen">
    ///     <see langword="true"/> if the game should start in fullscreen mode; otherwise, <see langword="false"/>.
    /// </param>
    /// <param name="title">
    ///     The title to display in the title bar of the game window.
    /// </param>
    public Engine(int width, int height, bool fullscreen, string title)
        : this(width, height, width, height, fullscreen, title) { }

    /// <summary>
    ///     Creates a new instance of the <see cref="Engine"/> class.
    /// </summary>
    /// <param name="width">
    ///     The width, in pixels, of the game window.
    /// </param>
    /// <param name="height">
    ///     The height, in pixels, of the game window.
    /// </param>
    /// <param name="virtualWidth">
    ///     The virtual rendering resolution width, in pixels, of the game.
    /// </param>
    /// <param name="virtualHeight">
    ///     The virtual rendering resolution height, in pixels, of the game.
    /// </param>
    /// <param name="fullscreen">
    ///     <see langword="true"/> if the game should start in fullscreen mode; otherwise, <see langword="false"/>.
    /// </param>
    /// <param name="title">
    ///     The title to display in the title bar of the game window.
    /// </param>
    public Engine(int width, int height, int virtualWidth, int virtualHeight, bool fullscreen, string title)
    {
        Size resolution = new Size(width, height);
        Size virtualResolution = new Size(virtualWidth, virtualHeight);
        TurtleGraphics.Initialize(this, resolution, virtualResolution, fullscreen);
        TurtleInput.Initialize(4);
    }

    /// <summary>
    ///     Initializes the engine.
    /// </summary>
    protected override void Initialize()
    {
        TurtleDraw.Initialize(GraphicsDevice);
        base.Initialize();
    }

    /// <summary>
    ///     Updates the internal state of the engine.
    /// </summary>
    /// <param name="gameTime">
    ///     A snapshot of the timing values for the current frame.
    /// </param>
    protected override void Update(GameTime gameTime)
    {
        if (PauseOnFocusLost && !IsActive)
        {
            SuppressDraw();
            return;
        }

        //  Always update base framework before continuing doing engine updates
        base.Update(gameTime);

        TurtleInput.Update(gameTime);

        if (ShouldExit())
        {
            Exit();
        }
    }

    /// <summary>
    ///     <para>Returns a value that indicates if the game should exit.</para>
    ///     <para>
    ///         By default this will only check for the Escape key on the keyboard.  You can override this method
    ///         to perform custom checks if needed.
    ///     </para>
    /// </summary>
    /// <returns>
    ///     <see langword="true"/> if the game should exit; otherwise, <see langword="false"/>.
    /// </returns>
    protected virtual bool ShouldExit() => ExitOnEscapeKeyPress && TurtleInput.Keyboard.Pressed(Keys.Escape);

}
