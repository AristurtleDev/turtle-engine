using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TurtleEngine;

/// <summary>
///     Handles the management of the graphics device and presentation of graphics for the game.
/// </summary>
public static class TurtleGraphics
{
    private static Size _resolution;
    private static Size _virtualResolution;
    private static int _viewPadding;
    private static Viewport _viewport;
    private static Matrix _scaleMatrix;
    private static GraphicsDeviceManager? _graphicsDeviceManager;
    private static GameWindow? _gameWindow;
    private static bool _isResizing;
    private static SpriteBatch? _spriteBatch;
    private static ContentManager? _globalContentManager;
    private static TurtleTexture2D? _pixel;

    /// <summary>
    ///     Gets the <see cref="Microsoft.Xna.Framework.GraphicsDeviceManager"/> instance responsible for management of
    ///     the graphics device.
    /// </summary>
    /// <exception cref="InvalidOperationException">
    ///     Thrown if the underlying graphics device manager reference is <see langword="null"/>.  The underlying
    ///     graphics device manager reference will be <see langword="null"/> if this is accessed before calling.
    ///     <see cref="TurtleGraphics.Initialize(Game, Size, Size, bool)"/>.
    /// </exception>
    public static GraphicsDeviceManager DeviceManager
    {
        get
        {
            if (_graphicsDeviceManager is null)
            {
                throw new InvalidOperationException($"{nameof(TurtleGraphics)}.{nameof(TurtleGraphics.DeviceManager)} is null.  Did you forget to call {nameof(TurtleGraphics)}.{nameof(TurtleGraphics.Initialize)}");
            }

            return _graphicsDeviceManager;
        }
    }

    /// <summary>
    ///     Gets the <see cref="Microsoft.Xna.Framework.Graphics.GraphicsDevice"/> instance used to create assets
    ///     and present the graphics for the game.
    /// </summary>
    /// <exception cref="InvalidOperationException">
    ///     Thrown if the underlying graphics device manager reference is <see langword="null"/>.  The underlying
    ///     graphics device manager reference will be <see langword="null"/> if this is accessed before calling.
    ///     <see cref="TurtleGraphics.Initialize(Game, Size, Size, bool)"/>.
    /// </exception>
    public static GraphicsDevice Device => DeviceManager.GraphicsDevice;

    /// <summary>
    ///     Gets the <see cref="Microsoft.Xna.Framework.GameWindow"/> instance that the game is presented on.
    /// </summary>
    /// <exception cref="InvalidOperationException">
    ///     Thrown if the underlying game window reference is <see langword="null"/>.  The underlying game window
    ///     reference will be <see langword="null"/> if this is accessed before calling 
    ///     <see cref="TurtleGraphics.Initialize(Game, Size, Size, bool)"/>.
    /// </exception>
    public static GameWindow Window
    {
        get
        {
            if (_gameWindow is null)
            {
                throw new InvalidOperationException($"{nameof(TurtleGraphics)}.{nameof(TurtleGraphics.Window)} is null.  Did you forget to call {nameof(TurtleGraphics)}.{nameof(TurtleGraphics.Initialize)}");
            }

            return _gameWindow;
        }
    }

    /// <summary>
    ///     Gets the rendering resolution of the game.
    /// </summary>
    /// <value>
    ///     A <see cref="Size"/> value who's <see cref="Size.Width"/> and <see cref="Size.Height"/> components
    ///     represent the width and height extents, in pixels, of the rendering resolution of the game.
    /// </value>
    public static Size Resolution => _resolution;

    /// <summary>
    ///     Gets the virtual rendering resolution of the game.
    /// </summary>
    /// <value>
    ///     A <see cref="Size"/> value who's <see cref="Size.Width"/> and <see cref="Size.Height"/> components
    ///     represent the width and height extents, in pixels, of the virtual rendering resolution of the game.
    /// </value>
    public static Size VirtualResolution => _virtualResolution;

    /// <summary>
    ///     Gets the scale matrix used to transform the scale of the game rendering for independent screen resolution
    ///     rendering.
    /// </summary>
    /// <value>
    ///     A <see cref="Microsoft.Xna.Framework.Matrix"/> value that defines the scale at which to render the 
    ///     graphics for the game in order to support independent screen resolution rendering.
    /// </value>
    public static Matrix ScaleMatrix => _scaleMatrix;

    /// <summary>
    ///     Gets the viewport to bind the graphics rendering to.
    /// </summary>
    /// <value>
    ///     A <see cref="Microsoft.Xna.Framework.Graphics.Viewport"/> value that defines the bounds in which graphics
    ///     are rendered for the graphics device.
    /// </value>
    public static Viewport Viewport => _viewport;

    /// <summary>
    ///     Gets or Sets the amount of padding, in pixels, to apply to the edges of the game view when independent
    ///     screen resolution rendering calls for letter or pillar boxing.
    /// </summary>
    /// <value>
    ///     A 32-bit signed integer value that defines the amount of padding, in pixels, to apply to the edges of the
    ///     game view when independent screen resolution rendering calls for letter or pillar boxing.
    /// </value>
    public static int ViewPadding
    {
        get => _viewPadding;
        set
        {
            if (_viewPadding == value) return;
            _viewPadding = value;
            UpdateView();
        }
    }

    /// <summary>
    ///     Gets or Sets the color mask to apply when clearing the back buffer.
    /// </summary>
    /// <value>
    ///     A <see cref="Microsoft.Xna.Framework.Color"/> value that defines the color mask to apply when clearing the
    ///     back buffer.
    /// </value>
    public static Color ClearColor { get; set; }

    /// <summary>
    ///     An event that is invoked when the client game window size changes.
    /// </summary>
    public static event EventHandler<EventArgs>? ClientSizeChanged;

    /// <summary>
    ///     An event that is invoked when a graphics device is created.
    /// </summary>
    public static event EventHandler<EventArgs>? GraphicsDeviceCreated;

    /// <summary>
    ///     An event that is invoked when a graphics device is reset.
    /// </summary>
    public static event EventHandler<EventArgs>? GraphicsDeviceReset;

    /// <summary>
    ///     Gets the sprite batcher used for 2D rendering.
    /// </summary>
    /// <value>
    ///     The <see cref="Microsoft.Xna.Framework.Graphics.SpriteBatch"/> instance used for 2D rendering.
    /// </value>
    public static SpriteBatch SpriteBatch
    {
        get
        {
            if (_spriteBatch is null)
            {
                throw new InvalidOperationException($"{nameof(TurtleGraphics)}.{nameof(TurtleGraphics.SpriteBatch)} is null.  Did you forget to call {nameof(TurtleGraphics)}.{nameof(TurtleGraphics.Initialize)}");
            }

            return _spriteBatch;
        }
    }

    /// <summary>
    ///     Gets the content manager responsible for loading and caching global content assets.
    /// </summary>
    /// <value>
    ///     The <see cref="Microsoft.Xna.Framework.Content.ContentManager"/> instance responsible for loading and
    ///     caching global content assets.
    /// </value>
    public static ContentManager GlobalContentManager
    {
        get
        {
            if (_globalContentManager is null)
            {
                throw new InvalidOperationException($"{nameof(TurtleGraphics)}.{nameof(TurtleGraphics.GlobalContentManager)} is null.  Did you forget to call {nameof(TurtleGraphics)}.{nameof(TurtleGraphics.Initialize)}");
            }

            return _globalContentManager;
        }
    }

    public static TurtleTexture2D Pixel

    /// <summary>
    ///     Initializes the graphics.
    /// </summary>
    /// <param name="game">
    ///     A reference to the instance of the <see cref="Microsoft.Xna.Framework.Game"/> class.
    /// </param>
    /// <param name="resolution">
    ///     A <see cref="Size"/> value that defines the rendering resolution of the game.
    /// </param>
    /// <param name="virtualResolution">
    ///     A <see cref="Size"/> value that defines the virtual rendering resolution of the game.
    /// </param>
    /// <param name="fullScreen">
    ///     Indicates whether the game should render in full screen mode.
    /// </param>
    /// <exception cref="ArgumentException">
    ///     Thrown if either the <see cref="Size.Width"/> or <see cref="Size.Height"/> component values are less than
    ///     or equal to zero for either the <paramref name="resolution"/> or <paramref name="virtualResolution"/>
    ///     parameters.
    /// </exception>
    public static void Initialize(Game game, Size resolution, Size virtualResolution, bool fullScreen)
    {
        if (resolution.Width <= 0) throw new ArgumentException($"{nameof(resolution)}.{nameof(resolution.Width)} cannot be less than or equal to zero");
        if (resolution.Height <= 0) throw new ArgumentException($"{nameof(resolution)}.{nameof(resolution.Height)} cannot be less than or equal to zero");
        if (virtualResolution.Width <= 0) throw new ArgumentException($"{nameof(virtualResolution)}.{nameof(virtualResolution.Width)} cannot be less than or equal to zero");
        if (virtualResolution.Height <= 0) throw new ArgumentException($"{nameof(virtualResolution)}.{nameof(virtualResolution.Height)} cannot be less than or equal to zero");

        _graphicsDeviceManager = new(game);

        _graphicsDeviceManager.SynchronizeWithVerticalRetrace = true;
        _graphicsDeviceManager.PreferMultiSampling = true;
        _graphicsDeviceManager.GraphicsProfile = GraphicsProfile.HiDef;
        _graphicsDeviceManager.PreferredBackBufferFormat = SurfaceFormat.Color;
        _graphicsDeviceManager.PreferredDepthStencilFormat = DepthFormat.Depth24Stencil8;

        _graphicsDeviceManager.DeviceCreated += OnGraphicsDeviceCreated;
        _graphicsDeviceManager.DeviceReset += OnGraphicsDeviceReset;

        _gameWindow = game.Window;
        _gameWindow.AllowAltF4 = true;
        _gameWindow.AllowUserResizing = true;
        _gameWindow.ClientSizeChanged += OnClientSizeChanged;

        game.IsMouseVisible = true;

        _resolution = resolution;
        _virtualResolution = virtualResolution;

        if (fullScreen)
        {
            _graphicsDeviceManager.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            _graphicsDeviceManager.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
        }
        else
        {
            _graphicsDeviceManager.PreferredBackBufferWidth = virtualResolution.Width;
            _graphicsDeviceManager.PreferredBackBufferHeight = virtualResolution.Height;
        }

        _graphicsDeviceManager.IsFullScreen = fullScreen;
        _graphicsDeviceManager.ApplyChanges();
        UpdateView();

        _spriteBatch = new(_graphicsDeviceManager.GraphicsDevice);
        _globalContentManager = new(game.Services);
    }

    private static void LoadContent()
    {

    }


    private static void OnClientSizeChanged(object? sender, EventArgs e)
    {
        if (_isResizing) return;

        if (Window.ClientBounds.Width > 0 && Window.ClientBounds.Height > 0)
        {
            _isResizing = true;

            DeviceManager.PreferredBackBufferWidth = Window.ClientBounds.Width;
            DeviceManager.PreferredBackBufferHeight = Window.ClientBounds.Height;

            UpdateView();

            _isResizing = false;

            ClientSizeChanged?.Invoke(sender, e);
        }
    }

    private static void OnGraphicsDeviceCreated(object? sender, EventArgs e)
    {
        UpdateView();
        GraphicsDeviceCreated?.Invoke(sender, e);
    }

    private static void OnGraphicsDeviceReset(object? sender, EventArgs e)
    {
        UpdateView();
        GraphicsDeviceReset?.Invoke(sender, e);
    }

    /// <summary>
    ///     Sets the game window to render in windowed mode with the specified virtual rendering resolution.
    /// </summary>
    /// <param name="size">
    ///     A <see cref="Size"/> value who's <see cref="Size.Width"/> and <see cref="Size.Height"/> components define
    ///     the width and height extents to set the game window size to.
    /// </param>
    /// <exception cref="ArgumentOutOfRangeException">  
    ///     Throw if either the <see cref="Size.Width"/> or <see cref="Size.Height"/> component values of the
    ///     <paramref name="size"/> parameter are less than or equal to zero.
    /// </exception>
    public static void SetWindowed(Size size)
    {
        if (size.Width <= 0) throw new ArgumentOutOfRangeException(nameof(size), $"{nameof(size)}.{nameof(size.Width)} must be greater than zero");
        if (size.Height <= 0) throw new ArgumentOutOfRangeException(nameof(size), $"{nameof(size)}.{nameof(size.Height)} must be greater than zero");

        _isResizing = true;
        DeviceManager.PreferredBackBufferWidth = size.Width;
        DeviceManager.PreferredBackBufferHeight = size.Height;
        DeviceManager.IsFullScreen = false;
        DeviceManager.ApplyChanges();
        _isResizing = false;
    }

    /// <summary>
    ///     Sets the game window to render in fullscreen mode.
    /// </summary>
    public static void SetFullScreen()
    {
        _isResizing = true;
        DeviceManager.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
        DeviceManager.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
        DeviceManager.IsFullScreen = true;
        DeviceManager.ApplyChanges();
        _isResizing = false;
    }

    /// <summary>
    ///     Binds the viewport of the graphics device to use the <see cref="TurtleGraphics.Viewport"/> value.
    /// </summary>
    public static void BindViewport() => BindViewport(_viewport);

    /// <summary>
    ///     Binds the viewport of the graphics device to use the specified 
    ///     <see cref="Microsoft.Xna.Framework.Graphics.Viewport"/> value.
    /// </summary>
    /// <param name="viewPort">
    ///     The <see cref="Microsoft.Xna.Framework.Graphics.Viewport"/> value to bind to the graphics device.
    /// </param>
    public static void BindViewport(Viewport viewPort) => Device.Viewport = Viewport;

    /// <summary>
    ///     Clears the back buffer of the graphics device using the default <see cref="TurtleGraphics.ClearColor"/>
    ///     value.
    /// </summary>
    public static void Clear() => Clear(ClearColor);

    /// <summary>
    ///     Clears the back buffer of the graphics device using the specified 
    ///     <see cref="Microsoft.Xna.Framework.Color"/> value.
    /// </summary>
    /// <param name="clearColor">
    ///     The <see cref="Microsoft.Xna.Framework.Color"/> value to apply when clearing the back buffer of the graphics
    ///     device.
    /// </param>
    public static void Clear(Color clearColor) => Device.Clear(clearColor);

    private static void UpdateView()
    {
        float screenWidth = Device.PresentationParameters.BackBufferWidth;
        float screenHeight = Device.PresentationParameters.BackBufferHeight;

        if (screenWidth / _resolution.Width > screenHeight / _resolution.Height)
        {
            _virtualResolution.Width = (int)(screenHeight / _resolution.Height * _resolution.Width);
            _virtualResolution.Height = (int)screenHeight;
        }
        else
        {
            _virtualResolution.Width = (int)screenWidth;
            _virtualResolution.Height = (int)(screenWidth / _resolution.Width * _resolution.Height);
        }

        float aspect = _virtualResolution.Height / (float)_virtualResolution.Width;
        _virtualResolution.Width -= _viewPadding * 2;
        _virtualResolution.Height -= (int)(aspect * _viewPadding * 2);

        _scaleMatrix = Matrix.CreateScale(_virtualResolution.Width / (float)_resolution.Width);

        _viewport = new()
        {
            X = (int)(screenWidth / 2 - _virtualResolution.Width / 2),
            Y = (int)(screenHeight / 2 - _virtualResolution.Height / 2),
            Width = _virtualResolution.Width,
            Height = _virtualResolution.Height,
            MinDepth = 0,
            MaxDepth = 1
        };
    }
}