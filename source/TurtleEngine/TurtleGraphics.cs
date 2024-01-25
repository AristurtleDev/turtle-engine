// Copyright (c) Christopher Whitley. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Aristurtle.TurtleEngine;

/// <summary>
///     Handles management of graphics for the engine.
/// </summary>
public static class TurtleGraphics
{
    private static bool s_isResizing;
    private static int s_viewPadding;
    private static Size s_virtualResolution;

    /// <summary>
    ///     The manager responsible for the management of graphic devices.
    /// </summary>
    public static GraphicsDeviceManager GraphicsDeviceManager { get; private set; }

    /// <summary>
    ///     The graphics device responsible for creating graphics assets and presenting graphics for the game.
    /// </summary>
    public static GraphicsDevice GraphicsDevice => GraphicsDeviceManager.GraphicsDevice;

    /// <summary>
    ///     A reference too the game window that represents the client window the game is rendered to.
    /// </summary>
    public static GameWindow GameWindow { get; private set; }

    /// <summary>
    ///     The size of the actual rendering resolution of the game.
    /// </summary>
    public static Size Resolution { get; private set; }

    /// <summary>
    ///     Gets the size of the internal virtual rendering resolution of the game.
    /// </summary>
    public static Size VirtualResolution => s_virtualResolution;

    /// <summary>
    ///     A matrix that represents the scale fo apply when rendering to property scale the render to account for
    ///     independent screen resolution rendering.
    /// </summary>
    public static Matrix ScaleMatrix { get; private set; }

    /// <summary>
    ///     The default viewport to use for the graphics device when rendering.
    /// </summary>
    public static Viewport Viewport;

    /// <summary>
    ///     The default color value to use when clearing the back buffer.
    /// </summary>
    public static Color DefaultClearColor;

    /// <summary>
    ///     Invoke when the size of the client window is changed.
    /// </summary>
    public static event EventHandler<EventArgs> ClientSizeChanged;

    /// <summary>
    ///     Invoked when a graphics device reset occurs.
    /// </summary>
    public static event EventHandler<EventArgs> GraphicsDeviceReset;

    /// <summary>
    ///     Invoked when a graphics device created event occurs.
    /// </summary>
    public static event EventHandler<EventArgs> GraphicsDeviceCreated;

    /// <summary>
    ///     Initializes the graphics component management of the engine.
    /// </summary>
    /// <param name="game">
    ///     A reference to the game.
    /// </param>
    /// <param name="resolution">
    ///     The rendering resolution of the game.
    /// </param>
    /// <param name="virtualResolution">
    ///     The initial virtual rendering resolution of the game.
    /// </param>
    /// <param name="fullScreen">
    ///     <see langword="true"/> if the game should start in fullscreen mode; otherwise, <see langword="false"/>.
    /// </param>
    public static void Initialize(Game game, Size resolution, Size virtualResolution, bool fullScreen)
    {
        Ensure.NotNull(game);
        Ensure.GreaterThanZero(resolution.Width);
        Ensure.GreaterThanZero(resolution.Height);
        Ensure.GreaterThanZero(virtualResolution.Width);
        Ensure.GreaterThanZero(virtualResolution.Height);

        GraphicsDeviceManager = new GraphicsDeviceManager(game);

        GraphicsDeviceManager.SynchronizeWithVerticalRetrace = false;
        GraphicsDeviceManager.PreferMultiSampling = true;
        GraphicsDeviceManager.GraphicsProfile = GraphicsProfile.HiDef;
        GraphicsDeviceManager.PreferredBackBufferFormat = SurfaceFormat.Color;
        GraphicsDeviceManager.PreferredDepthStencilFormat = DepthFormat.Depth24Stencil8;
        GraphicsDeviceManager.PreferHalfPixelOffset = true;

        GraphicsDeviceManager.DeviceCreated += OnGraphicsDeviceCreated;
        GraphicsDeviceManager.DeviceReset += OnGraphicsDeviceReset;

        GameWindow = game.Window;
        GameWindow.AllowAltF4 = true;
        GameWindow.AllowUserResizing = true;
        GameWindow.ClientSizeChanged += OnClientSizeChanged;

        game.IsMouseVisible = true;

        Resolution = resolution;
        s_virtualResolution = virtualResolution;

        if (fullScreen)
        {
            SetFullScreen();
        }
        else
        {
            SetWindowSize(virtualResolution);
        }

        UpdateView();
    }

    private static void OnClientSizeChanged(object sender, EventArgs e)
    {
        if (s_isResizing)
        {
            return;
        }

        if (GameWindow.ClientBounds.Width > 0 && GameWindow.ClientBounds.Height > 0)
        {
            s_isResizing = true;
            GraphicsDeviceManager.PreferredBackBufferWidth = GameWindow.ClientBounds.Width;
            GraphicsDeviceManager.PreferredBackBufferHeight = GameWindow.ClientBounds.Height;
            UpdateView();
            s_isResizing = false;

            if (ClientSizeChanged != null)
            {
                ClientSizeChanged.Invoke(sender, e);
            }
        }
    }

    private static void OnGraphicsDeviceCreated(object sender, EventArgs e)
    {
        UpdateView();

        if (GraphicsDeviceCreated != null)
        {
            GraphicsDeviceCreated.Invoke(sender, e);
        }
    }

    private static void OnGraphicsDeviceReset(object sender, EventArgs e)
    {
        UpdateView();

        if (GraphicsDeviceReset != null)
        {
            GraphicsDeviceReset.Invoke(sender, e);
        }
    }


    /// <summary>
    ///     Sets the amount of padding, in pixels, to apply to the view when rendering in a resolution where letter
    ///     or pillar boxing is required to maintain aspect ratio.
    /// </summary>
    /// <param name="padding">
    ///     The amount of padding, in pixels.
    /// </param>
    public static void SetViewPadding(int padding)
    {
        Ensure.GreaterThanOrEqualToZero(padding);

        if (s_viewPadding == padding)
        {
            return;
        }

        s_viewPadding = padding;
        UpdateView();
    }

    /// <summary>
    ///     Sets the graphics to render in windowed mode.
    /// </summary>
    /// <param name="size">
    ///     A <see cref="Size"/> value that represents the width and height, in pixels, to set the game window
    ///     size to.
    /// </param>
    public static void SetWindowSize(Size size)
    {
        Ensure.GreaterThanZero(size.Width);
        Ensure.GreaterThanZero(size.Height);

        s_isResizing = true;
        GraphicsDeviceManager.PreferredBackBufferWidth = size.Width;
        GraphicsDeviceManager.PreferredBackBufferHeight = size.Height;
        GraphicsDeviceManager.IsFullScreen = false;
        s_isResizing = false;
    }

    /// <summary>
    ///     Sets the graphics to render in fullscreen mode.
    /// </summary>
    public static void SetFullScreen()
    {
        s_isResizing = true;
        GraphicsDeviceManager.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
        GraphicsDeviceManager.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
        GraphicsDeviceManager.IsFullScreen = true;
        GraphicsDeviceManager.ApplyChanges();
        s_isResizing = false;
    }

    /// <summary>
    ///     Binds the <see cref="Viewport"/> to the graphics device viewport.
    /// </summary>
    public static void BindViewport() => BindViewport(Viewport);

    /// <summary>
    ///     Binds the given viewport to the graphics device viewport.
    /// </summary>
    /// <param name="viewport">
    ///     The viewport to bind.
    /// </param>
    public static void BindViewport(Viewport viewport) => GraphicsDevice.Viewport = viewport;

    /// <summary>
    ///     Clears the back buffer using the <see cref="DefaultClearColor"/> value.
    /// </summary>
    public static void Clear() => Clear(DefaultClearColor);

    /// <summary>
    ///     Clears the back buffer using hte specified color value.
    /// </summary>
    /// <param name="color"></param>
    public static void Clear(Color color) => GraphicsDevice.Clear(color);

    private static void UpdateView()
    {
        float screenWidth = GraphicsDevice.PresentationParameters.BackBufferWidth;
        float screenHeight = GraphicsDevice.PresentationParameters.BackBufferHeight;

        if (screenWidth / Resolution.Width > screenHeight / Resolution.Height)
        {
            s_virtualResolution.Width = (int)(screenHeight / Resolution.Height * Resolution.Width);
            s_virtualResolution.Height = (int)screenHeight;
        }
        else
        {
            s_virtualResolution.Width = (int)screenWidth;
            s_virtualResolution.Height = (int)(screenWidth / Resolution.Width * Resolution.Height);
        }

        float aspect = s_virtualResolution.Height / (float)s_virtualResolution.Width;
        s_virtualResolution.Width -= s_viewPadding * 2;
        s_virtualResolution.Height -= (int)(aspect * s_viewPadding * 2);

        ScaleMatrix = Matrix.CreateScale(s_virtualResolution.Width / (float)Resolution.Width);

        Viewport = new Viewport()
        {
            X = (int)(screenWidth / 2 - s_virtualResolution.Width / 2),
            Y = (int)(screenHeight / 2 - s_virtualResolution.Height / 2),
            Width = s_virtualResolution.Width,
            Height = s_virtualResolution.Height,
            MinDepth = 0,
            MaxDepth = 1
        };
    }
}

