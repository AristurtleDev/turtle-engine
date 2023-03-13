using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TurtleEngine;

/// <summary>
///     Handles the management of the graphics device and presentation of graphics for the game.
/// </summary>
public static class TurtleGraphics
{
    private static Point _resolution;
    private static Point _virtualResolution;
    private static int _viewPadding;
    private static Viewport _viewport;
    private static Matrix _scaleMatrix;
    private static GraphicsDeviceManager _graphicsDeviceManager;
    private static GameWindow _gameWindow;
    private static bool _isResizing;

    public static GraphicsDeviceManager DeviceManager => _graphicsDeviceManager;
    public static GraphicsDevice Device => _graphicsDeviceManager.GraphicsDevice;
    public static GameWindow Window => _gameWindow;
    public static Point Resolution => _resolution;
    public static Point VirtualResolution => _virtualResolution;
    public static Matrix ScaleMatrix => _scaleMatrix;
    public static Viewport Viewport => _viewport;

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

    public static Color ClearColor { get; set; }
    public static event EventHandler<EventArgs> ClientSizeChanged;
    public static event EventHandler<EventArgs> GraphicsDeviceCreated;
    public static event EventHandler<EventArgs> GraphicsDeviceReset;

    public static void Initialize(Game game, Point resolution, Point virtualResolution, bool fullScreen)
    {
        if (resolution.X <= 0) throw new ArgumentException($"{nameof(resolution)}.{nameof(resolution.X)} cannot be less than or equal to zero");
        if (resolution.Y <= 0) throw new ArgumentException($"{nameof(resolution)}.{nameof(resolution.Y)} cannot be less than or equal to zero");
        if (virtualResolution.X <= 0) throw new ArgumentException($"{nameof(virtualResolution)}.{nameof(virtualResolution.X)} cannot be less than or equal to zero");
        if (virtualResolution.Y <= 0) throw new ArgumentException($"{nameof(virtualResolution)}.{nameof(virtualResolution.Y)} cannot be less than or equal to zero");

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
            _graphicsDeviceManager.PreferredBackBufferWidth = virtualResolution.X;
            _graphicsDeviceManager.PreferredBackBufferHeight = virtualResolution.Y;
        }

        _graphicsDeviceManager.IsFullScreen = fullScreen;
        _graphicsDeviceManager.ApplyChanges();
        UpdateView();
    }


    private static void OnClientSizeChanged(object sender, EventArgs e)
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

    private static void OnGraphicsDeviceCreated(object sender, EventArgs e)
    {
        UpdateView();
        GraphicsDeviceCreated?.Invoke(sender, e);
    }

    private static void OnGraphicsDeviceReset(object sender, EventArgs e)
    {
        UpdateView();
        GraphicsDeviceReset?.Invoke(sender, e);
    }

    
    public static void SetWindowed(Point size)
    {
        if (size.X <= 0) throw new ArgumentOutOfRangeException(nameof(size), $"{nameof(size)}.{nameof(size.X)} must be greater than zero");
        if (size.Y <= 0) throw new ArgumentOutOfRangeException(nameof(size), $"{nameof(size)}.{nameof(size.Y)} must be greater than zero");

        _isResizing = true;
        _graphicsDeviceManager.PreferredBackBufferWidth = size.X;
        _graphicsDeviceManager.PreferredBackBufferHeight = size.Y;
        _graphicsDeviceManager.IsFullScreen = false;
        _graphicsDeviceManager.ApplyChanges();
        _isResizing = false;
    }

    public static void SetFullScreen()
    {
        _isResizing = true;
        _graphicsDeviceManager.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
        _graphicsDeviceManager.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
        _graphicsDeviceManager.IsFullScreen = true;
        _graphicsDeviceManager.ApplyChanges();
        _isResizing = false;
    }

    public static void BindViewport() => BindViewport(_viewport);
    public static void BindViewport(Viewport viewPort) => _graphicsDeviceManager.GraphicsDevice.Viewport = Viewport;

    public static void Clear() => Clear(ClearColor);
    public static void Clear(Color clearColor) => _graphicsDeviceManager.GraphicsDevice.Clear(clearColor);

    private static void UpdateView()
    {
        float screenWidth = Device.PresentationParameters.BackBufferWidth;
        float screenHeight = Device.PresentationParameters.BackBufferHeight;

        if (screenWidth / _resolution.X > screenHeight / _resolution.Y)
        {
            _virtualResolution.X = (int)(screenHeight / _resolution.Y * _resolution.X);
            _virtualResolution.Y = (int)screenHeight;
        }
        else
        {
            _virtualResolution.X = (int)screenWidth;
            _virtualResolution.Y = (int)(screenWidth / _resolution.X * _resolution.Y);
        }

        float aspect = _virtualResolution.Y / (float)_virtualResolution.X;
        _virtualResolution.X -= _viewPadding * 2;
        _virtualResolution.Y -= (int)(aspect * _viewPadding * 2);

        _scaleMatrix = Matrix.CreateScale(_virtualResolution.X / (float)_resolution.X);

        _viewport = new()
        {
            X = (int)(screenWidth / 2 - _virtualResolution.X / 2),
            Y = (int)(screenHeight / 2 - _virtualResolution.Y / 2),
            Width = _virtualResolution.X,
            Height = _virtualResolution.Y,
            MinDepth = 0,
            MaxDepth = 1
        };
    }
}