using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TurtleEngine;

/// <summary>
///     Handles the management of the graphics device and presentation of graphics for the game.
/// </summary>
public static class TurtleGraphics
{
    private static Size _virtualResolution;
    private static int _viewPadding;
    private static bool _isResizing;

    /// <summary>
    ///     Gets the <see cref="Microsoft.Xna.Framework.GraphicsDeviceManager"/> instance responsible for management of
    ///     the graphics device.
    /// </summary>
    /// <exception cref="InvalidOperationException">
    ///     Thrown if the underlying graphics device manager reference is <see langword="null"/>.  The underlying
    ///     graphics device manager reference will be <see langword="null"/> if this is accessed before calling.
    ///     <see cref="TurtleGraphics.Initialize(Game, Size, Size, bool)"/>.
    /// </exception>
    public static GraphicsDeviceManager DeviceManager { get; private set; }

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
    public static GameWindow Window { get; private set; }

    /// <summary>
    ///     Gets the rendering resolution of the game.
    /// </summary>
    /// <value>
    ///     A <see cref="Size"/> value who's <see cref="Size.Width"/> and <see cref="Size.Height"/> components
    ///     represent the width and height extents, in pixels, of the rendering resolution of the game.
    /// </value>
    public static Size Resolution { get; private set; }

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
    public static Matrix ScaleMatrix { get; private set; }

    /// <summary>
    ///     Gets the default viewport to bind the graphics rendering to.
    /// </summary>
    /// <value>
    ///     A <see cref="Microsoft.Xna.Framework.Graphics.Viewport"/> value that defines the bounds in which graphics
    ///     are rendered for the graphics device.
    /// </value>
    public static Viewport Viewport { get; private set; }

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
    public static event EventHandler<EventArgs> ClientSizeChanged;

    /// <summary>
    ///     An event that is invoked when a graphics device is created.
    /// </summary>
    public static event EventHandler<EventArgs> GraphicsDeviceCreated;

    /// <summary>
    ///     An event that is invoked when a graphics device is reset.
    /// </summary>
    public static event EventHandler<EventArgs> GraphicsDeviceReset;

    /// <summary>
    ///     Gets the content manager responsible for loading and caching global content assets.
    /// </summary>
    /// <value>
    ///     The <see cref="Microsoft.Xna.Framework.Content.ContentManager"/> instance responsible for loading and
    ///     caching global content assets.
    /// </value>
    public static ContentManager GlobalContentManager { get; private set; }

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

        DeviceManager = new(game);

        DeviceManager.SynchronizeWithVerticalRetrace = true;
        DeviceManager.PreferMultiSampling = true;
        DeviceManager.GraphicsProfile = GraphicsProfile.HiDef;
        DeviceManager.PreferredBackBufferFormat = SurfaceFormat.Color;
        DeviceManager.PreferredDepthStencilFormat = DepthFormat.Depth24Stencil8;

        DeviceManager.DeviceCreated += OnGraphicsDeviceCreated;
        DeviceManager.DeviceReset += OnGraphicsDeviceReset;

        Window = game.Window;
        Window.AllowAltF4 = true;
        Window.AllowUserResizing = true;
        Window.ClientSizeChanged += OnClientSizeChanged;

        game.IsMouseVisible = true;

        Resolution = resolution;
        _virtualResolution = virtualResolution;

        if (fullScreen)
        {
            DeviceManager.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            DeviceManager.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
        }
        else
        {
            DeviceManager.PreferredBackBufferWidth = virtualResolution.Width;
            DeviceManager.PreferredBackBufferHeight = virtualResolution.Height;
        }

        DeviceManager.IsFullScreen = fullScreen;
        DeviceManager.ApplyChanges();
        UpdateView();

        Draw.Initialize();
        GlobalContentManager = new(game.Services);
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
    public static void BindViewport() => Device.Viewport = Viewport;

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

        if (screenWidth / Resolution.Width > screenHeight / Resolution.Height)
        {
            _virtualResolution.Width = (int)(screenHeight / Resolution.Height * Resolution.Width);
            _virtualResolution.Height = (int)screenHeight;
        }
        else
        {
            _virtualResolution.Width = (int)screenWidth;
            _virtualResolution.Height = (int)(screenWidth / Resolution.Width * Resolution.Height);
        }

        float aspect = _virtualResolution.Height / (float)_virtualResolution.Width;
        _virtualResolution.Width -= _viewPadding * 2;
        _virtualResolution.Height -= (int)(aspect * _viewPadding * 2);

        ScaleMatrix = Matrix.CreateScale(_virtualResolution.Width / (float)Resolution.Width);

        Viewport = new()
        {
            X = (int)(screenWidth / 2 - _virtualResolution.Width / 2),
            Y = (int)(screenHeight / 2 - _virtualResolution.Height / 2),
            Width = _virtualResolution.Width,
            Height = _virtualResolution.Height,
            MinDepth = 0,
            MaxDepth = 1
        };
    }

    public static class Draw
    {
        private static Rectangle _rect = Microsoft.Xna.Framework.Rectangle.Empty;

        /// <summary>
        ///     Gets the sprite batcher used for 2D rendering.
        /// </summary>
        /// <value>
        ///     The <see cref="Microsoft.Xna.Framework.Graphics.SpriteBatch"/> instance used for 2D rendering.
        /// </value>
        public static SpriteBatch SpriteBatch { get; private set; }

        /// <summary>
        ///     Gets a 1x1 single pixel texture.
        /// </summary>
        /// <value>
        ///     A <see cref="TTexture"/> instance that represents a 1x1 single pixel texture where the pixel is
        ///     white.
        /// </value>
        public static TTexture Pixel { get; private set; }

        internal static void Initialize()
        {
            SpriteBatch = new(Device);
            Texture2D pixel = new(Device, 1, 1);
            pixel.SetData<Color>(new Color[] { Color.White });
            pixel.Name = "Pixel";
            Pixel = new(pixel);
        }

        #region SpriteBatch Begin

        /// <summary>
        ///     Prepares the graphics device for drawing.
        /// </summary>
        public static void Begin() =>
            Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, null);

        /// <summary>
        ///     Prepares the graphics device for drawing using the specified 
        ///     <see cref="Microsoft.Xna.Framework.Graphics.SpriteSortMode"/>
        /// </summary>
        /// <param name="sortMode">
        ///     The <see cref="Microsoft.Xna.Framework.Graphics.SpriteSortMode"/> that the graphics device should use.
        /// </param>
        public static void Begin(SpriteSortMode sortMode) =>
            Begin(sortMode, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, null);

        /// <summary>
        ///     Prepares the graphics device for drawing using the specified 
        ///     <see cref="Microsoft.Xna.Framework.Graphics.BlendState"/>
        /// </summary>
        /// <param name="blendState">
        ///     The <see cref="Microsoft.Xna.Framework.Graphics.BlendState"/> that the graphics device should use.
        /// </param>
        public static void Begin(BlendState blendState) =>
            Begin(SpriteSortMode.Deferred, blendState, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, null);

        /// <summary>
        ///     Prepares the graphics device for drawing using the specified 
        ///     <see cref="Microsoft.Xna.Framework.Graphics.SamplerState"/>
        /// </summary>
        /// <param name="samplerState">
        ///     The <see cref="Microsoft.Xna.Framework.Graphics.SamplerState"/> that the graphics device should use.
        /// </param>
        public static void Begin(SamplerState samplerState) =>
            Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, samplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, null);

        /// <summary>
        ///     Prepares the graphics device for drawing using the specified 
        ///     <see cref="Microsoft.Xna.Framework.Graphics.DepthStencilState"/>
        /// </summary>
        /// <param name="depthStencilState">
        ///     The <see cref="Microsoft.Xna.Framework.Graphics.DepthStencilState"/> that the graphics device should use.
        /// </param>
        public static void Being(DepthStencilState depthStencilState) =>
            Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, depthStencilState, RasterizerState.CullCounterClockwise, null, null);

        /// <summary>
        ///     Prepares the graphics device for drawing using the specified 
        ///     <see cref="Microsoft.Xna.Framework.Graphics.RasterizerState"/>
        /// </summary>
        /// <param name="rasterizerState">
        ///     The <see cref="Microsoft.Xna.Framework.Graphics.RasterizerState"/> that the graphics device should use.
        /// </param>
        public static void Begin(RasterizerState rasterizerState) =>
            Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, rasterizerState, null, null);


        /// <summary>
        ///     Prepares the graphics device for drawing using the specified 
        ///     <see cref="Microsoft.Xna.Framework.Graphics.Effect"/>
        /// </summary>
        /// <param name="effect">
        ///     The <see cref="Microsoft.Xna.Framework.Graphics.Effect"/> that the graphics device should use.
        /// </param>
        public static void Begin(Effect effect) =>
            Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise, effect, null);

        /// <summary>
        ///     Prepares the graphics device for drawing using the specified transformation
        ///     <see cref="Microsoft.Xna.Framework.Matrix"/>
        /// </summary>
        /// <param name="transformationMatrix">
        ///     The transformation <see cref="Microsoft.Xna.Framework.Matrix"/> that the graphics device should use.
        /// </param>
        public static void Begin(Matrix transformationMatrix) =>
            Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, transformationMatrix);

        /// <summary>
        ///     Prepares the graphics device for drawing using the specified parameters.
        /// </summary>
        /// <param name="sortMode">
        ///     The <see cref="Microsoft.Xna.Framework.Graphics.SpriteSortMode"/> that the graphics device should use.
        /// </param>
        /// <param name="blendState">
        ///     The <see cref="Microsoft.Xna.Framework.Graphics.BlendState"/> that the graphics device should use.
        /// </param>
        /// <param name="samplerState">
        ///     The <see cref="Microsoft.Xna.Framework.Graphics.SamplerState"/> that the graphics device should use.
        /// </param>
        /// <param name="depthStencilState">
        ///     The <see cref="Microsoft.Xna.Framework.Graphics.DepthStencilState"/> that the graphics device should use.
        /// </param>
        /// <param name="rasterizerState">
        ///     The <see cref="Microsoft.Xna.Framework.Graphics.RasterizerState"/> that the graphics device should use.
        /// </param>
        /// <param name="effect">
        ///     The <see cref="Microsoft.Xna.Framework.Graphics.Effect"/> that the graphics device should use.
        /// </param>
        /// <param name="transformationMatrix">
        ///     The transformation <see cref="Microsoft.Xna.Framework.Matrix"/> that the graphics device should use.
        /// </param>
        public static void Begin(SpriteSortMode sortMode = SpriteSortMode.Deferred, BlendState blendState = null, SamplerState samplerState = null, DepthStencilState depthStencilState = null, RasterizerState rasterizerState = null, Effect effect = null, Matrix? transformMatrix = null) =>
            SpriteBatch.Begin(sortMode, blendState, samplerState, depthStencilState, rasterizerState, effect, transformMatrix);

        #endregion SpriteBatch Begin

        /// <summary>
        ///     Flushes the sprite batch and restores the graphics device state to how it was before begin was called.
        /// </summary>
        public static void End() => SpriteBatch.End();

        #region Draw Point

        /// <summary>
        ///     Draws a 1x1 pixel.
        /// </summary>
        /// <param name="position">
        ///     A <see cref="Microsoft.Xna.Framework.Vector2"/> value that represents the x- and y-coordinate location
        ///     to draw the pixel at.
        /// </param>
        public static void Point(Vector2 position) => Point(position, Color.White, 0.0f);

        /// <summary>
        ///     Draws a 1x1 pixel.
        /// </summary>
        /// <param name="position">
        ///     A <see cref="Microsoft.Xna.Framework.Vector2"/> value that represents the x- and y-coordinate location
        ///     to draw the pixel at.
        /// </param>
        /// <param name="color">
        ///     A <see cref="Microsoft.Xna.Framework.Color"/> value that defines the color mask to apply when drawing
        ///     the pixel.
        /// </param>
        public static void Point(Vector2 position, Color color) => Point(position, color, 0.0f);

        /// <summary>
        ///     Draws a 1x1 pixel.
        /// </summary>
        /// <param name="position">
        ///     A <see cref="Microsoft.Xna.Framework.Vector2"/> value that represents the x- and y-coordinate location
        ///     to draw the pixel at.
        /// </param>
        /// <param name="color">
        ///     A <see cref="Microsoft.Xna.Framework.Color"/> value that defines the color mask to apply when drawing
        ///     the pixel.
        /// </param>
        /// <param name="layerDepth">
        ///     The layer depth to draw the pixel at, between 0.0f (back) and 1.0f (front).
        /// </param>
        public static void Point(Vector2 position, Color color, float layerDepth)
        {
            Ensure.NotNull(Pixel, "{0}.{1}.{2} is null", nameof(TurtleGraphics), nameof(TurtleGraphics.Draw), nameof(TurtleGraphics.Draw.Pixel));
            DrawTTexture(Pixel, position, color, 0.0f, Vector2.Zero, Vector2.One, SpriteEffects.None, layerDepth);
        }

        #endregion Draw Point

        #region Draw Line

        /// <summary>
        ///     Draws a line
        /// </summary>
        /// <param name="x1">
        ///     The starting x-coordinate location of the line.
        /// </param>
        /// <param name="y1">
        ///     The starting y-coordinate location of the line.
        /// </param>
        /// <param name="x2">
        ///     The ending x-coordinate location of the line.
        /// </param>
        /// <param name="y2">
        ///     The ending y-coordinate location of the line.
        /// </param>
        public static void Line(float x1, float y1, float x2, float y2) =>
            Line(new Vector2(x1, y1), new Vector2(x2, y2), Color.White, 1.0f, 0.0f);

        /// <summary>
        ///     Draws a line
        /// </summary>
        /// <param name="x1">
        ///     The starting x-coordinate location of the line.
        /// </param>
        /// <param name="y1">
        ///     The starting y-coordinate location of the line.
        /// </param>
        /// <param name="x2">
        ///     The ending x-coordinate location of the line.
        /// </param>
        /// <param name="y2">
        ///     The ending y-coordinate location of the line.
        /// </param>
        /// <param name="color">
        ///     A <see cref="Microsoft.Xna.Framework.Color"/> value that defines the color mask to apply when drawing
        ///     the line.
        /// </param>
        public static void Line(float x1, float y1, float x2, float y2, Color color) =>
            Line(new Vector2(x1, y1), new Vector2(x2, y2), color, 1.0f, 0.0f);

        /// <summary>
        ///     Draws a line
        /// </summary>
        /// <param name="x1">
        ///     The starting x-coordinate location of the line.
        /// </param>
        /// <param name="y1">
        ///     The starting y-coordinate location of the line.
        /// </param>
        /// <param name="x2">
        ///     The ending x-coordinate location of the line.
        /// </param>
        /// <param name="y2">
        ///     The ending y-coordinate location of the line.
        /// </param>
        /// <param name="color">
        ///     A <see cref="Microsoft.Xna.Framework.Color"/> value that defines the color mask to apply when drawing
        ///     the line.
        /// </param>
        /// <param name="thickness">
        ///     The thickness, in pixels, of the line.
        /// </param>
        public static void Line(float x1, float y1, float x2, float y2, Color color, float thickness) =>
            Line(new Vector2(x1, y1), new Vector2(x2, y2), color, thickness, 0.0f);

        /// <summary>
        ///     Draws a line
        /// </summary>
        /// <param name="x1">
        ///     The starting x-coordinate location of the line.
        /// </param>
        /// <param name="y1">
        ///     The starting y-coordinate location of the line.
        /// </param>
        /// <param name="x2">
        ///     The ending x-coordinate location of the line.
        /// </param>
        /// <param name="y2">
        ///     The ending y-coordinate location of the line.
        /// </param>
        /// <param name="color">
        ///     A <see cref="Microsoft.Xna.Framework.Color"/> value that defines the color mask to apply when drawing
        ///     the line.
        /// </param>
        /// <param name="thickness">
        ///     The thickness, in pixels, of the line.
        /// </param>
        /// <param name="layerDepth">
        ///     The layer depth to draw the line at, between 0.0f (back) and 1.0f (front).
        /// </param>
        public static void Line(float x1, float y1, float x2, float y2, Color color, float thickness, float layerDepth) =>
            Line(new Vector2(x1, y1), new Vector2(x2, y2), color, thickness, layerDepth);


        /// <summary>
        ///     Draws a line.
        /// </summary>
        /// <param name="start">
        ///     A <see cref="Microsoft.Xna.Framework.Vector2"/> value that defines the starting x- and y-coordinate
        ///     location of the line to draw.
        /// </param>
        /// <param name="end">
        ///     A <see cref="Microsoft.Xna.Framework.Vector2"/> value that defines the ending x- and y-coordinate
        ///     location of the line to draw.
        /// </param>
        public static void Line(Vector2 start, Vector2 end) =>
            Line(start, end, Color.White, 1.0f, 0.0f);

        /// <summary>
        ///     Draws a line.
        /// </summary>
        /// <param name="start">
        ///     A <see cref="Microsoft.Xna.Framework.Vector2"/> value that defines the starting x- and y-coordinate
        ///     location of the line to draw.
        /// </param>
        /// <param name="end">
        ///     A <see cref="Microsoft.Xna.Framework.Vector2"/> value that defines the ending x- and y-coordinate
        ///     location of the line to draw.
        /// </param>
        /// <param name="color">
        ///     A <see cref="Microsoft.Xna.Framework.Color"/> value that defines the color mask to apply when drawing
        ///     the line.
        /// </param>
        public static void Line(Vector2 start, Vector2 end, Color color) =>
            Line(start, end, color, 1.0f, 0.0f);

        /// <summary>
        ///     Draws a line.
        /// </summary>
        /// <param name="start">
        ///     A <see cref="Microsoft.Xna.Framework.Vector2"/> value that defines the starting x- and y-coordinate
        ///     location of the line to draw.
        /// </param>
        /// <param name="end">
        ///     A <see cref="Microsoft.Xna.Framework.Vector2"/> value that defines the ending x- and y-coordinate
        ///     location of the line to draw.
        /// </param>
        /// <param name="color">
        ///     A <see cref="Microsoft.Xna.Framework.Color"/> value that defines the color mask to apply when drawing
        ///     the line.
        /// </param>
        /// <param name="thickness">
        ///     The thickness, in pixels, of the line.
        /// </param>
        public static void Line(Vector2 start, Vector2 end, Color color, float thickness) =>
            Line(start, end, color, thickness, 0.0f);

        #endregion Draw Line

        #region Draw Line Angle

        /// <summary>
        ///     Draws a line.
        /// </summary>
        /// <param name="start">
        ///     A <see cref="Microsoft.Xna.Framework.Vector2"/> value that defines the starting x- and y-coordinate
        ///     location of the line to draw.
        /// </param>
        /// <param name="end">
        ///     A <see cref="Microsoft.Xna.Framework.Vector2"/> value that defines the ending x- and y-coordinate
        ///     location of the line to draw.
        /// </param>
        /// <param name="color">
        ///     A <see cref="Microsoft.Xna.Framework.Color"/> value that defines the color mask to apply when drawing
        ///     the line.
        /// </param>
        /// <param name="thickness">
        ///     The thickness, in pixels, of the line.
        /// </param>
        /// <param name="layerDepth">
        ///     The layer depth to draw the line at, between 0.0f (back) and 1.0f (front).
        /// </param>
        public static void Line(Vector2 start, Vector2 end, Color color, float thickness, float layerDepth) =>
            LineAngle(start, Maths.Angle(start, end), Vector2.Distance(start, end), color, thickness, layerDepth);

        /// <summary>
        ///     Draws a line at the specified angle.
        /// </summary>
        /// <param name="startX">
        ///     The starting x-coordinate location of the line.
        /// </param>
        /// <param name="startY">
        ///     The starting y-coordinate location of the line.
        /// </param>
        /// <param name="angle">
        ///     The angle, in radians, to draw the line at.
        /// </param>
        /// <param name="length">
        ///     The length, in pixels, of the line to draw.
        /// </param>
        public static void LineAngle(float startX, float startY, float angle, float length) =>
            LineAngle(new Vector2(startX, startY), angle, length, Color.White, 1.0f, 0.0f);

        /// <summary>
        ///     Draws a line at the specified angle.
        /// </summary>
        /// <param name="startX">
        ///     The starting x-coordinate location of the line.
        /// </param>
        /// <param name="startY">
        ///     The starting y-coordinate location of the line.
        /// </param>
        /// <param name="angle">
        ///     The angle, in radians, to draw the line at.
        /// </param>
        /// <param name="length">
        ///     The length, in pixels, of the line to draw.
        /// </param>
        /// <param name="color">
        ///     A <see cref="Microsoft.Xna.Framework.Color"/> value that defines the color mask to apply when drawing
        ///     the line.
        /// </param>
        public static void LineAngle(float startX, float startY, float angle, float length, Color color) =>
            LineAngle(new Vector2(startX, startY), angle, length, color, 1.0f, 0.0f);

        /// <summary>
        ///     Draws a line at the specified angle.
        /// </summary>
        /// <param name="startX">
        ///     The starting x-coordinate location of the line.
        /// </param>
        /// <param name="startY">
        ///     The starting y-coordinate location of the line.
        /// </param>
        /// <param name="angle">
        ///     The angle, in radians, to draw the line at.
        /// </param>
        /// <param name="length">
        ///     The length, in pixels, of the line to draw.
        /// </param>
        /// <param name="color">
        ///     A <see cref="Microsoft.Xna.Framework.Color"/> value that defines the color mask to apply when drawing
        ///     the line.
        /// </param>
        /// <param name="thickness">
        ///     The thickness, in pixels, of the line.
        /// </param>
        public static void LineAngle(float startX, float startY, float angle, float length, Color color, float thickness) =>
            LineAngle(new Vector2(startX, startY), angle, length, color, thickness, 0.0f);

        /// <summary>
        ///     Draws a line at the specified angle.
        /// </summary>
        /// <param name="startX">
        ///     The starting x-coordinate location of the line.
        /// </param>
        /// <param name="startY">
        ///     The starting y-coordinate location of the line.
        /// </param>
        /// <param name="angle">
        ///     The angle, in radians, to draw the line at.
        /// </param>
        /// <param name="length">
        ///     The length, in pixels, of the line to draw.
        /// </param>
        /// <param name="color">
        ///     A <see cref="Microsoft.Xna.Framework.Color"/> value that defines the color mask to apply when drawing
        ///     the line.
        /// </param>
        /// <param name="thickness">
        ///     The thickness, in pixels, of the line.
        /// </param>
        /// <param name="layerDepth">
        ///     The layer depth to draw the line at, between 0.0f (back) and 1.0f (front).
        /// </param>
        public static void LineAngle(float startX, float startY, float angle, float length, Color color, float thickness, float layerDepth) =>
            LineAngle(new Vector2(startX, startY), angle, length, color, thickness, layerDepth);

        /// <summary>
        ///     Draws a line at the specified angle.
        /// </summary>
        /// <param name="start">
        ///     A <see cref="Microsoft.Xna.Framework.Vector2"/> value that defines the starting x- and y-coordinate
        ///     location of the line to draw.
        /// </param>
        /// <param name="angle">
        ///     The angle, in radians, to draw the line at.
        /// </param>
        /// <param name="length">
        ///     The length, in pixels, of the line to draw.
        /// </param>
        public static void LineAngle(Vector2 start, float angle, float length) =>
            LineAngle(start, angle, length, Color.White, 1.0f, 0.0f);

        /// <summary>
        ///     Draws a line at the specified angle.
        /// </summary>
        /// <param name="start">
        ///     A <see cref="Microsoft.Xna.Framework.Vector2"/> value that defines the starting x- and y-coordinate
        ///     location of the line to draw.
        /// </param>
        /// <param name="angle">
        ///     The angle, in radians, to draw the line at.
        /// </param>
        /// <param name="length">
        ///     The length, in pixels, of the line to draw.
        /// </param>
        /// <param name="color">
        ///     A <see cref="Microsoft.Xna.Framework.Color"/> value that defines the color mask to apply when drawing
        ///     the line.
        /// </param>
        public static void LineAngle(Vector2 start, float angle, float length, Color color) =>
            LineAngle(start, angle, length, color, 1.0f, 0.0f);

        /// <summary>
        ///     Draws a line at the specified angle.
        /// </summary>
        /// <param name="start">
        ///     A <see cref="Microsoft.Xna.Framework.Vector2"/> value that defines the starting x- and y-coordinate
        ///     location of the line to draw.
        /// </param>
        /// <param name="angle">
        ///     The angle, in radians, to draw the line at.
        /// </param>
        /// <param name="length">
        ///     The length, in pixels, of the line to draw.
        /// </param>
        /// <param name="color">
        ///     A <see cref="Microsoft.Xna.Framework.Color"/> value that defines the color mask to apply when drawing
        ///     the line.
        /// </param>
        /// <param name="thickness">
        ///     The thickness, in pixels, of the line.
        /// </param>
        public static void LineAngle(Vector2 start, float angle, float length, Color color, float thickness) =>
            LineAngle(start, angle, length, color, thickness, 0.0f);

        /// <summary>
        ///     Draws a line at the specified angle.
        /// </summary>
        /// <param name="start">
        ///     A <see cref="Microsoft.Xna.Framework.Vector2"/> value that defines the starting x- and y-coordinate
        ///     location of the line to draw.
        /// </param>
        /// <param name="angle">
        ///     The angle, in radians, to draw the line at.
        /// </param>
        /// <param name="length">
        ///     The length, in pixels, of the line to draw.
        /// </param>
        /// <param name="color">
        ///     A <see cref="Microsoft.Xna.Framework.Color"/> value that defines the color mask to apply when drawing
        ///     the line.
        /// </param>
        /// <param name="thickness">
        ///     The thickness, in pixels, of the line.
        /// </param>
        /// <param name="layerDepth">
        ///     The layer depth to draw the line at, between 0.0f (back) and 1.0f (front).
        /// </param>
        public static void LineAngle(Vector2 start, float angle, float length, Color color, float thickness, float layerDepth)
        {
            Ensure.NotNull(Pixel, "{0}.{1}.{2} is null", nameof(TurtleGraphics), nameof(TurtleGraphics.Draw), nameof(TurtleGraphics.Draw.Pixel));
            DrawTTexture(Pixel, start, color, angle, new Vector2(0, 0.5f), new Vector2(length, thickness), SpriteEffects.None, layerDepth);
        }

        #endregion Draw Line Angle

        #region Draw Rectangle

        /// <summary>
        ///     Draws a solid rectangle.
        /// </summary>
        /// <param name="rectangle">
        ///     A <see cref="Microsoft.Xna.Framework.Rectangle"/> value that defines the location and extents of the
        ///     rectangle to draw.
        /// </param>
        /// <param name="color">
        ///     A <see cref="Microsoft.Xna.Framework.Color"/> value that defines the color mask to apply when drawing
        ///     the rectangle.
        /// </param>
        /// <param name="layerDepth">
        ///     The layer depth to draw the rectangle at, between 0.0f (back) and 1.0f (front).
        /// </param>
        public static void Rectangle(Rectangle rectangle) =>
            Rectangle(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height, Color.White, 0.0f);

        /// <summary>
        ///     Draws a solid rectangle.
        /// </summary>
        /// <param name="rectangle">
        ///     A <see cref="Microsoft.Xna.Framework.Rectangle"/> value that defines the location and extents of the
        ///     rectangle to draw.
        /// </param>
        /// <param name="color">
        ///     A <see cref="Microsoft.Xna.Framework.Color"/> value that defines the color mask to apply when drawing
        ///     the rectangle.
        /// </param>
        public static void Rectangle(Rectangle rectangle, Color color) =>
            Rectangle(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height, color, 0.0f);

        /// <summary>
        ///     Draws a solid rectangle.
        /// </summary>
        /// <param name="rectangle">
        ///     A <see cref="Microsoft.Xna.Framework.Rectangle"/> value that defines the location and extents of the
        ///     rectangle to draw.
        /// </param>
        /// <param name="color">
        ///     A <see cref="Microsoft.Xna.Framework.Color"/> value that defines the color mask to apply when drawing
        ///     the rectangle.
        /// </param>
        /// <param name="layerDepth">
        ///     The layer depth to draw the rectangle at, between 0.0f (back) and 1.0f (front).
        /// </param>
        public static void Rectangle(Rectangle rectangle, Color color, float layerDepth) =>
            Rectangle(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height, color, layerDepth);

        /// <summary>
        ///     Draws a solid rectangle.
        /// </summary>
        /// <param name="position">
        ///     A <see cref="Microsoft.Xna.Framework.Vector2"/> value that defines the x- and y-coordinate location of
        ///     top-left corner of the rectangle to draw.
        /// </param>
        /// <param name="width">
        ///     The width, in pixels, of the rectangle to draw.
        /// </param>
        /// <param name="height">
        ///     The height, in pixels, of the rectangle to draw.
        /// </param>
        public static void Rectangle(Vector2 position, float width, float height) =>
            Rectangle(position.X, position.Y, width, height, Color.White, 0.0f);

        /// <summary>
        ///     Draws a solid rectangle.
        /// </summary>
        /// <param name="position">
        ///     A <see cref="Microsoft.Xna.Framework.Vector2"/> value that defines the x- and y-coordinate location of
        ///     top-left corner of the rectangle to draw.
        /// </param>
        /// <param name="width">
        ///     The width, in pixels, of the rectangle to draw.
        /// </param>
        /// <param name="height">
        ///     The height, in pixels, of the rectangle to draw.
        /// </param>
        /// <param name="color">
        ///     A <see cref="Microsoft.Xna.Framework.Color"/> value that defines the color mask to apply when drawing
        ///     the rectangle.
        /// </param>
        public static void Rectangle(Vector2 position, float width, float height, Color color) =>
            Rectangle(position.X, position.Y, width, height, color, 0.0f);

        /// <summary>
        ///     Draws a solid rectangle.
        /// </summary>
        /// <param name="position">
        ///     A <see cref="Microsoft.Xna.Framework.Vector2"/> value that defines the x- and y-coordinate location of
        ///     top-left corner of the rectangle to draw.
        /// </param>
        /// <param name="width">
        ///     The width, in pixels, of the rectangle to draw.
        /// </param>
        /// <param name="height">
        ///     The height, in pixels, of the rectangle to draw.
        /// </param>
        /// <param name="color">
        ///     A <see cref="Microsoft.Xna.Framework.Color"/> value that defines the color mask to apply when drawing
        ///     the rectangle.
        /// </param>
        /// <param name="layerDepth">
        ///     The layer depth to draw the rectangle at, between 0.0f (back) and 1.0f (front).
        /// </param>
        public static void Rectangle(Vector2 position, float width, float height, Color color, float layerDepth) =>
            Rectangle(position.X, position.Y, width, height, color, layerDepth);

        /// <summary>
        ///     Draws a solid rectangle.
        /// </summary>
        /// <param name="x">
        ///     The x-coordinate location of the top-left corner of the rectangle to draw.
        /// </param>
        /// <param name="y">
        ///     The y-coordinate location of the top-left corner of the rectangle to draw.
        /// </param>
        /// <param name="width">
        ///     The width, in pixels, of the rectangle to draw.
        /// </param>
        /// <param name="height">
        ///     The height, in pixels, of the rectangle to draw.
        /// </param>
        public static void Rectangle(float x, float y, float width, float height) =>
            Rectangle(x, y, width, height, Color.White, 0.0f);

        /// <summary>
        ///     Draws a solid rectangle.
        /// </summary>
        /// <param name="x">
        ///     The x-coordinate location of the top-left corner of the rectangle to draw.
        /// </param>
        /// <param name="y">
        ///     The y-coordinate location of the top-left corner of the rectangle to draw.
        /// </param>
        /// <param name="width">
        ///     The width, in pixels, of the rectangle to draw.
        /// </param>
        /// <param name="height">
        ///     The height, in pixels, of the rectangle to draw.
        /// </param>
        /// <param name="color">
        ///     A <see cref="Microsoft.Xna.Framework.Color"/> value that defines the color mask to apply when drawing
        ///     the rectangle.
        /// </param>
        public static void Rectangle(float x, float y, float width, float height, Color color) =>
            Rectangle(x, y, width, height, color, 0.0f);

        /// <summary>
        ///     Draws a solid rectangle.
        /// </summary>
        /// <param name="x">
        ///     The x-coordinate location of the top-left corner of the rectangle to draw.
        /// </param>
        /// <param name="y">
        ///     The y-coordinate location of the top-left corner of the rectangle to draw.
        /// </param>
        /// <param name="width">
        ///     The width, in pixels, of the rectangle to draw.
        /// </param>
        /// <param name="height">
        ///     The height, in pixels, of the rectangle to draw.
        /// </param>
        /// <param name="color">
        ///     A <see cref="Microsoft.Xna.Framework.Color"/> value that defines the color mask to apply when drawing
        ///     the rectangle.
        /// </param>
        /// <param name="layerDepth">
        ///     The layer depth to draw the rectangle at, between 0.0f (back) and 1.0f (front).
        /// </param>
        public static void Rectangle(float x, float y, float width, float height, Color color, float layerDepth)
        {
            _rect.X = (int)x;
            _rect.Y = (int)y;
            _rect.Width = (int)width;
            _rect.Height = (int)height;

            Ensure.NotNull(Pixel, "{0}.{1}.{2} is null", nameof(TurtleGraphics), nameof(TurtleGraphics.Draw), nameof(TurtleGraphics.Draw.Pixel));
            DrawTTexture(Pixel, _rect, color, 0.0f, Vector2.Zero, SpriteEffects.None, layerDepth);
        }

        #endregion Draw Rectangle

        #region Draw Hollow Rectangle

        /// <summary>
        ///     Draws a hollow rectangle.
        /// </summary>
        /// <param name="rectangle">
        ///     A <see cref="Microsoft.Xna.Framework.Rectangle"/> value that defines the location and extents of the
        ///     hollow rectangle to draw.
        /// </param>
        /// <param name="color">
        ///     A <see cref="Microsoft.Xna.Framework.Color"/> value that defines the color mask to apply when drawing
        ///     the hollow rectangle.
        /// </param>
        public static void HollowRectangle(Rectangle rectangle) =>
            HollowRectangle(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height, Color.White, 1.0f, 0.0f);

        /// <summary>
        ///     Draws a hollow rectangle.
        /// </summary>
        /// <param name="rectangle">
        ///     A <see cref="Microsoft.Xna.Framework.Rectangle"/> value that defines the location and extents of the
        ///     hollow rectangle to draw.
        /// </param>
        /// <param name="color">
        ///     A <see cref="Microsoft.Xna.Framework.Color"/> value that defines the color mask to apply when drawing
        ///     the hollow rectangle.
        /// </param>
        public static void HollowRectangle(Rectangle rectangle, Color color) =>
            HollowRectangle(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height, color, 1.0f, 0.0f);

        /// <summary>
        ///     Draws a hollow rectangle.
        /// </summary>
        /// <param name="rectangle">
        ///     A <see cref="Microsoft.Xna.Framework.Rectangle"/> value that defines the location and extents of the
        ///     hollow rectangle to draw.
        /// </param>
        /// <param name="color">
        ///     A <see cref="Microsoft.Xna.Framework.Color"/> value that defines the color mask to apply when drawing
        ///     the hollow rectangle.
        /// </param>
        /// <param name="thickness">
        ///     The thickness, in pixels, of the lines drawn for each edge of the hollow rectangle.
        /// </param>
        public static void HollowRectangle(Rectangle rectangle, Color color, float thickness) =>
            HollowRectangle(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height, color, thickness, 0.0f);

        /// <summary>
        ///     Draws a hollow rectangle.
        /// </summary>
        /// <param name="rectangle">
        ///     A <see cref="Microsoft.Xna.Framework.Rectangle"/> value that defines the location and extents of the
        ///     hollow rectangle to draw.
        /// </param>
        /// <param name="color">
        ///     A <see cref="Microsoft.Xna.Framework.Color"/> value that defines the color mask to apply when drawing
        ///     the hollow rectangle.
        /// </param>
        /// <param name="thickness">
        ///     The thickness, in pixels, of the lines drawn for each edge of the hollow rectangle.
        /// </param>
        /// <param name="layerDepth">
        ///     The layer depth to draw the hollow rectangle at, between 0.0f (back) and 1.0f (front).
        /// </param>
        public static void HollowRectangle(Rectangle rectangle, Color color, float thickness, float layerDepth) =>
            HollowRectangle(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height, color, thickness, layerDepth);

        /// <summary>
        ///     Draws a hollow rectangle.
        /// </summary>
        /// <param name="position">
        ///     A <see cref="Microsoft.Xna.Framework.Vector2"/> value that defines the x- and y-coordinate location of
        ///     top-left corner of the hollow rectangle to draw.
        /// </param>
        /// <param name="width">
        ///     The width, in pixels, of the hollow rectangle to draw.
        /// </param>
        /// <param name="height">
        ///     The height, in pixels, of the hollow rectangle to draw.
        /// </param>
        public static void HollowRectangle(Vector2 position, float width, float height) =>
            HollowRectangle(position.X, position.Y, width, height, Color.White, 1.0f, 0.0f);

        /// <summary>
        ///     Draws a hollow rectangle.
        /// </summary>
        /// <param name="position">
        ///     A <see cref="Microsoft.Xna.Framework.Vector2"/> value that defines the x- and y-coordinate location of
        ///     top-left corner of the hollow rectangle to draw.
        /// </param>
        /// <param name="width">
        ///     The width, in pixels, of the hollow rectangle to draw.
        /// </param>
        /// <param name="height">
        ///     The height, in pixels, of the hollow rectangle to draw.
        /// </param>
        /// <param name="color">
        ///     A <see cref="Microsoft.Xna.Framework.Color"/> value that defines the color mask to apply when drawing
        ///     the hollow rectangle.
        /// </param>
        public static void HollowRectangle(Vector2 position, float width, float height, Color color) =>
            HollowRectangle(position.X, position.Y, width, height, color, 1.0f, 0.0f);

        /// <summary>
        ///     Draws a hollow rectangle.
        /// </summary>
        /// <param name="position">
        ///     A <see cref="Microsoft.Xna.Framework.Vector2"/> value that defines the x- and y-coordinate location of
        ///     top-left corner of the hollow rectangle to draw.
        /// </param>
        /// <param name="width">
        ///     The width, in pixels, of the hollow rectangle to draw.
        /// </param>
        /// <param name="height">
        ///     The height, in pixels, of the hollow rectangle to draw.
        /// </param>
        /// <param name="color">
        ///     A <see cref="Microsoft.Xna.Framework.Color"/> value that defines the color mask to apply when drawing
        ///     the hollow rectangle.
        /// </param>
        /// <param name="thickness">
        ///     The thickness, in pixels, of the lines drawn for each edge of the hollow rectangle.
        /// </param>
        public static void HollowRectangle(Vector2 position, float width, float height, Color color, float thickness) =>
            HollowRectangle(position.X, position.Y, width, height, color, thickness, 0.0f);

        /// <summary>
        ///     Draws a hollow rectangle.
        /// </summary>
        /// <param name="position">
        ///     A <see cref="Microsoft.Xna.Framework.Vector2"/> value that defines the x- and y-coordinate location of
        ///     top-left corner of the hollow rectangle to draw.
        /// </param>
        /// <param name="width">
        ///     The width, in pixels, of the hollow rectangle to draw.
        /// </param>
        /// <param name="height">
        ///     The height, in pixels, of the hollow rectangle to draw.
        /// </param>
        /// <param name="color">
        ///     A <see cref="Microsoft.Xna.Framework.Color"/> value that defines the color mask to apply when drawing
        ///     the hollow rectangle.
        /// </param>
        /// <param name="thickness">
        ///     The thickness, in pixels, of the lines drawn for each edge of the hollow rectangle.
        /// </param>
        /// <param name="layerDepth">
        ///     The layer depth to draw the hollow rectangle at, between 0.0f (back) and 1.0f (front).
        /// </param>
        public static void HollowRectangle(Vector2 position, float width, float height, Color color, float thickness, float layerDepth) =>
            HollowRectangle(position.X, position.Y, width, height, color, thickness, layerDepth);

        /// <summary>
        ///     Draws a hollow rectangle.
        /// </summary>
        /// <param name="x">
        ///     The x-coordinate location of the top-left corner of the hollow rectangle to draw.
        /// </param>
        /// <param name="y">
        ///     The y-coordinate location of the top-left corner of the hollow rectangle to draw.
        /// </param>
        /// <param name="width">
        ///     The width, in pixels, of the hollow rectangle to draw.
        /// </param>
        /// <param name="height">
        ///     The height, in pixels, of the hollow rectangle to draw.
        /// </param>
        /// <param name="color">
        ///     A <see cref="Microsoft.Xna.Framework.Color"/> value that defines the color mask to apply when drawing
        ///     the hollow rectangle.
        /// </param>
        /// <param name="thickness">
        ///     The thickness, in pixels, of the lines drawn for each edge of the hollow rectangle.
        /// </param>
        /// <param name="layerDepth">
        ///     The layer depth to draw the hollow rectangle at, between 0.0f (back) and 1.0f (front).
        /// </param>
        public static void HollowRectangle(float x, float y, float width, float height) =>
            HollowRectangle(x, y, width, height, Color.White, 1.0f, 0.0f);

        /// <summary>
        ///     Draws a hollow rectangle.
        /// </summary>
        /// <param name="x">
        ///     The x-coordinate location of the top-left corner of the hollow rectangle to draw.
        /// </param>
        /// <param name="y">
        ///     The y-coordinate location of the top-left corner of the hollow rectangle to draw.
        /// </param>
        /// <param name="width">
        ///     The width, in pixels, of the hollow rectangle to draw.
        /// </param>
        /// <param name="height">
        ///     The height, in pixels, of the hollow rectangle to draw.
        /// </param>
        /// <param name="color">
        ///     A <see cref="Microsoft.Xna.Framework.Color"/> value that defines the color mask to apply when drawing
        ///     the hollow rectangle.
        /// </param>
        public static void HollowRectangle(float x, float y, float width, float height, Color color) =>
            HollowRectangle(x, y, width, height, color, 1.0f, 0.0f);

        /// <summary>
        ///     Draws a hollow rectangle.
        /// </summary>
        /// <param name="x">
        ///     The x-coordinate location of the top-left corner of the hollow rectangle to draw.
        /// </param>
        /// <param name="y">
        ///     The y-coordinate location of the top-left corner of the hollow rectangle to draw.
        /// </param>
        /// <param name="width">
        ///     The width, in pixels, of the hollow rectangle to draw.
        /// </param>
        /// <param name="height">
        ///     The height, in pixels, of the hollow rectangle to draw.
        /// </param>
        /// <param name="color">
        ///     A <see cref="Microsoft.Xna.Framework.Color"/> value that defines the color mask to apply when drawing
        ///     the hollow rectangle.
        /// </param>
        /// <param name="thickness">
        ///     The thickness, in pixels, of the lines drawn for each edge of the hollow rectangle.
        /// </param>
        public static void HollowRectangle(float x, float y, float width, float height, Color color, float thickness) =>
            HollowRectangle(x, y, width, height, color, thickness, 0.0f);

        /// <summary>
        ///     Draws a hollow rectangle.
        /// </summary>
        /// <param name="x">
        ///     The x-coordinate location of the top-left corner of the hollow rectangle to draw.
        /// </param>
        /// <param name="y">
        ///     The y-coordinate location of the top-left corner of the hollow rectangle to draw.
        /// </param>
        /// <param name="width">
        ///     The width, in pixels, of the hollow rectangle to draw.
        /// </param>
        /// <param name="height">
        ///     The height, in pixels, of the hollow rectangle to draw.
        /// </param>
        /// <param name="color">
        ///     A <see cref="Microsoft.Xna.Framework.Color"/> value that defines the color mask to apply when drawing
        ///     the hollow rectangle.
        /// </param>
        /// <param name="thickness">
        ///     The thickness, in pixels, of the lines drawn for each edge of the hollow rectangle.
        /// </param>
        /// <param name="layerDepth">
        ///     The layer depth to draw the hollow rectangle at, between 0.0f (back) and 1.0f (front).
        /// </param>
        public static void HollowRectangle(float x, float y, float width, float height, Color color, float thickness, float layerDepth)
        {
            Line(x, y, x + width, y, color, thickness, layerDepth);
            Line(x + width, y, x + width, y + height, color, thickness, layerDepth);
            Line(x + width, y + height, x, y + height, color, thickness, layerDepth);
            Line(x, y + height, x, y, color, thickness, layerDepth);
        }

        #endregion Draw Hollow Rectangle

        #region Draw Circle

        /// <summary>
        ///     Draws a circle.
        /// </summary>
        /// <param name="x">
        ///     The x-coordinate location of the center of the circle to draw.
        /// </param>
        /// <param name="y">
        ///     The y-coordinate location of the center of the circle to draw.
        /// </param>
        /// <param name="radius">
        ///     The radius, in pixels, of the circle to draw.
        /// </param>
        /// <param name="resolution">
        ///     <para>
        ///         The resolution of the circle to draw.
        ///     </para>
        ///     <para>
        ///         Determines the number of line segments to draw.  A higher resolution will result in a smoother
        ///         drawn circle but will require more line segments drawn.
        ///     </para>
        /// </param>
        public static void Circle(float x, float y, float radius, float resolution) =>
            Circle(new Vector2(x, y), radius, resolution, Color.White, 1.0f, 0.0f);

        /// <summary>
        ///     Draws a circle.
        /// </summary>
        /// <param name="x">
        ///     The x-coordinate location of the center of the circle to draw.
        /// </param>
        /// <param name="y">
        ///     The y-coordinate location of the center of the circle to draw.
        /// </param>
        /// <param name="radius">
        ///     The radius, in pixels, of the circle to draw.
        /// </param>
        /// <param name="resolution">
        ///     <para>
        ///         The resolution of the circle to draw.
        ///     </para>
        ///     <para>
        ///         Determines the number of line segments to draw.  A higher resolution will result in a smoother
        ///         drawn circle but will require more line segments drawn.
        ///     </para>
        /// </param>
        /// <param name="color">
        ///     A <see cref="Microsoft.Xna.Framework.Color"/> value that defines the color mask to apply when drawing
        ///     the circle.
        /// </param>
        public static void Circle(float x, float y, float radius, float resolution, Color color) =>
            Circle(new Vector2(x, y), radius, resolution, color, 1.0f, 0.0f);

        /// <summary>
        ///     Draws a circle.
        /// </summary>
        /// <param name="x">
        ///     The x-coordinate location of the center of the circle to draw.
        /// </param>
        /// <param name="y">
        ///     The y-coordinate location of the center of the circle to draw.
        /// </param>
        /// <param name="radius">
        ///     The radius, in pixels, of the circle to draw.
        /// </param>
        /// <param name="resolution">
        ///     <para>
        ///         The resolution of the circle to draw.
        ///     </para>
        ///     <para>
        ///         Determines the number of line segments to draw.  A higher resolution will result in a smoother
        ///         drawn circle but will require more line segments drawn.
        ///     </para>
        /// </param>
        /// <param name="color">
        ///     A <see cref="Microsoft.Xna.Framework.Color"/> value that defines the color mask to apply when drawing
        ///     the circle.
        /// </param>
        /// <param name="thickness">
        ///     The thickness, in pixels, of the lines drawn for each edge of the circle.
        /// </param>
        public static void Circle(float x, float y, float radius, float resolution, Color color, float thickness) =>
            Circle(new Vector2(x, y), radius, resolution, color, thickness, 0.0f);

        /// <summary>
        ///     Draws a circle.
        /// </summary>
        /// <param name="x">
        ///     The x-coordinate location of the center of the circle to draw.
        /// </param>
        /// <param name="y">
        ///     The y-coordinate location of the center of the circle to draw.
        /// </param>
        /// <param name="radius">
        ///     The radius, in pixels, of the circle to draw.
        /// </param>
        /// <param name="resolution">
        ///     <para>
        ///         The resolution of the circle to draw.
        ///     </para>
        ///     <para>
        ///         Determines the number of line segments to draw.  A higher resolution will result in a smoother
        ///         drawn circle but will require more line segments drawn.
        ///     </para>
        /// </param>
        /// <param name="color">
        ///     A <see cref="Microsoft.Xna.Framework.Color"/> value that defines the color mask to apply when drawing
        ///     the circle.
        /// </param>
        /// <param name="thickness">
        ///     The thickness, in pixels, of the lines drawn for each edge of the circle.
        /// </param>
        /// <param name="layerDepth">
        ///     The layer depth to draw the circle at, between 0.0f (back) and 1.0f (front).
        /// </param>
        public static void Circle(float x, float y, float radius, float resolution, Color color, float thickness, float layerDepth) =>
            Circle(new Vector2(x, y), radius, resolution, color, thickness, layerDepth);

        /// <summary>
        ///     Draws a circle.
        /// </summary>
        /// <param name="position">
        ///     A <see cref="Microsoft.Xna.Framework.Vector2"/> value that defines the x- and y-coordinate location of
        ///     center of the circle to draw.
        /// </param>
        /// <param name="radius">
        ///     The radius, in pixels, of the circle to draw.
        /// </param>
        /// <param name="resolution">
        ///     <para>
        ///         The resolution of the circle to draw.
        ///     </para>
        ///     <para>
        ///         Determines the number of line segments to draw.  A higher resolution will result in a smoother
        ///         drawn circle but will require more line segments drawn.
        ///     </para>
        /// </param>
        public static void Circle(Vector2 position, float radius, float resolution) =>
            Circle(position, radius, resolution, Color.White, 1.0f, 0.0f);

        /// <summary>
        ///     Draws a circle.
        /// </summary>
        /// <param name="position">
        ///     A <see cref="Microsoft.Xna.Framework.Vector2"/> value that defines the x- and y-coordinate location of
        ///     center of the circle to draw.
        /// </param>
        /// <param name="radius">
        ///     The radius, in pixels, of the circle to draw.
        /// </param>
        /// <param name="resolution">
        ///     <para>
        ///         The resolution of the circle to draw.
        ///     </para>
        ///     <para>
        ///         Determines the number of line segments to draw.  A higher resolution will result in a smoother
        ///         drawn circle but will require more line segments drawn.
        ///     </para>
        /// </param>
        /// <param name="color">
        ///     A <see cref="Microsoft.Xna.Framework.Color"/> value that defines the color mask to apply when drawing
        ///     the circle.
        /// </param>
        public static void Circle(Vector2 position, float radius, float resolution, Color color) =>
            Circle(position, radius, resolution, color, 1.0f, 0.0f);

        /// <summary>
        ///     Draws a circle.
        /// </summary>
        /// <param name="position">
        ///     A <see cref="Microsoft.Xna.Framework.Vector2"/> value that defines the x- and y-coordinate location of
        ///     center of the circle to draw.
        /// </param>
        /// <param name="radius">
        ///     The radius, in pixels, of the circle to draw.
        /// </param>
        /// <param name="resolution">
        ///     <para>
        ///         The resolution of the circle to draw.
        ///     </para>
        ///     <para>
        ///         Determines the number of line segments to draw.  A higher resolution will result in a smoother
        ///         drawn circle but will require more line segments drawn.
        ///     </para>
        /// </param>
        /// <param name="color">
        ///     A <see cref="Microsoft.Xna.Framework.Color"/> value that defines the color mask to apply when drawing
        ///     the circle.
        /// </param>
        /// <param name="thickness">
        ///     The thickness, in pixels, of the lines drawn for each edge of the circle.
        /// </param>
        public static void Circle(Vector2 position, float radius, float resolution, Color color, float thickness) =>
            Circle(position, radius, resolution, color, thickness, 0.0f);

        /// <summary>
        ///     Draws a circle.
        /// </summary>
        /// <param name="position">
        ///     A <see cref="Microsoft.Xna.Framework.Vector2"/> value that defines the x- and y-coordinate location of
        ///     center of the circle to draw.
        /// </param>
        /// <param name="radius">
        ///     The radius, in pixels, of the circle to draw.
        /// </param>
        /// <param name="resolution">
        ///     <para>
        ///         The resolution of the circle to draw.
        ///     </para>
        ///     <para>
        ///         Determines the number of line segments to draw.  A higher resolution will result in a smoother
        ///         drawn circle but will require more line segments drawn.
        ///     </para>
        /// </param>
        /// <param name="color">
        ///     A <see cref="Microsoft.Xna.Framework.Color"/> value that defines the color mask to apply when drawing
        ///     the circle.
        /// </param>
        /// <param name="thickness">
        ///     The thickness, in pixels, of the lines drawn for each edge of the circle.
        /// </param>
        /// <param name="layerDepth">
        ///     The layer depth to draw the circle at, between 0.0f (back) and 1.0f (front).
        /// </param>
        public static void Circle(Vector2 position, float radius, float resolution, Color color, float thickness, float layerDepth)
        {
            Vector2 last = Vector2.UnitX * radius;
            Vector2 lastPerpendicular = last.Perpendicular();

            for (int i = 1; i <= resolution; i++)
            {
                float angle = i * MathHelper.PiOver2 / resolution;
                Vector2 at = Maths.AngleToVector2(angle, radius);
                Vector2 atPerpendicular = at.Perpendicular();

                Line(position + last, position + at, color, thickness, layerDepth);
                Line(position - last, position - at, color, thickness, layerDepth);
                Line(position + lastPerpendicular, position + atPerpendicular, color, thickness, layerDepth);
                Line(position - lastPerpendicular, position - atPerpendicular, color, thickness, layerDepth);

                last = at;
                lastPerpendicular = atPerpendicular;
            }
        }

        public static void CirclePrecise(Vector2 position, float radius, float resolution, Color color, float thickness, float layerDepth)
        {
            // Calculate the angle between each line segment
            float angleStep = (float)(2 * Math.PI / resolution);

            // Loop through each segment and draw a line
            for (float angle = 0; angle < 2 * Math.PI; angle += angleStep)
            {
                Vector2 start = position + new Vector2((float)Math.Cos(angle) * radius, (float)Math.Sin(angle) * radius);
                Vector2 end = position + new Vector2((float)Math.Cos(angle + angleStep) * radius, (float)Math.Sin(angle + angleStep) * radius);

                Line(start, end, color, thickness, layerDepth);
            }
        }

        #endregion Draw Circle

        #region Draw TTexture

        public static void DrawTTexture(TTexture texture, Rectangle destinationRectangle, Color color) =>
            DrawTTexture(texture, destinationRectangle, color, 0.0f, Vector2.Zero, SpriteEffects.None, 0.0f);

        public static void DrawTTexture(TTexture texture, Rectangle destinationRectangle, Color color, float rotation, Vector2 origin, SpriteEffects effects, float layerDepth)
        {
            Ensure.IsFalse(texture.IsDisposed, "{0} has been disposed", nameof(texture));
            SpriteBatch.Draw(texture.Texture, destinationRectangle, texture.SourceRectangle, color, rotation, origin, effects, layerDepth);
        }

        public static void DrawTTexture(TTexture texture, Vector2 position) =>
            DrawTTexture(texture, position, Color.White, 0.0f, Vector2.Zero, Vector2.One, SpriteEffects.None, 0.0f);

        public static void DrawTTexture(TTexture texture, Vector2 position, Color color) =>
            DrawTTexture(texture, position, color, 0.0f, Vector2.Zero, Vector2.One, SpriteEffects.None, 0.0f);

        public static void DrawTTexture(TTexture texture, Vector2 position, Color color, Vector2 origin) =>
            DrawTTexture(texture, position, color, 0.0f, origin, Vector2.One, SpriteEffects.None, 0.0f);

        public static void DrawTTexture(TTexture texture, Vector2 position, Color color, float rotation, Vector2 origin, float scale) =>
            DrawTTexture(texture, position, color, rotation, origin, new Vector2(scale, scale), SpriteEffects.None, 0.0f);

        public static void DrawTTexture(TTexture texture, Vector2 position, Color color, float rotation, Vector2 origin, Vector2 scale) =>
            DrawTTexture(texture, position, color, rotation, origin, scale, SpriteEffects.None, 0.0f);

        public static void DrawTTexture(TTexture texture, Vector2 position, Color color, float rotation, Vector2 origin, float scale, SpriteEffects effects, float layerDepth) =>
            DrawTTexture(texture, position, color, rotation, origin, new Vector2(scale, scale), effects, layerDepth);

        public static void DrawTTexture(TTexture texture, Vector2 position, Color color, float rotation, Vector2 origin, Vector2 scale, SpriteEffects effects, float layerDepth)
        {
            Ensure.IsFalse(texture.IsDisposed, "{0} has been disposed", nameof(texture));
            SpriteBatch.Draw(texture.Texture, position, texture.SourceRectangle, color, rotation, origin, scale, effects, layerDepth);
        }


        public static void DrawTTextureOutlined(TTexture texture, Vector2 position) =>
            DrawTTextureOutlined(texture, position, Color.White, Color.Black, 0.0f, Vector2.Zero, Vector2.One, SpriteEffects.None, 0.0f);

        public static void DrawTTextureOutlined(TTexture texture, Vector2 position, Color color, Color outlineColor) =>
            DrawTTextureOutlined(texture, position, color, outlineColor, 0.0f, Vector2.Zero, Vector2.One, SpriteEffects.None, 0.0f);

        public static void DrawTTextureOutlined(TTexture texture, Vector2 position, Color color, Color outlineColor, Vector2 origin) =>
            DrawTTextureOutlined(texture, position, color, outlineColor, 0.0f, origin, Vector2.Zero, SpriteEffects.None, 0.0f);

        public static void DrawTTextureOutlined(TTexture texture, Vector2 position, Color color, Color outlineColor, float rotation, Vector2 origin, float scale) =>
            DrawTTextureOutlined(texture, position, color, outlineColor, rotation, origin, new Vector2(scale, scale), SpriteEffects.None, 0.0f);

        public static void DrawTTextureOutlined(TTexture texture, Vector2 position, Color color, Color outlineColor, float rotation, Vector2 origin, Vector2 scale) =>
            DrawTTextureOutlined(texture, position, color, outlineColor, rotation, origin, scale, SpriteEffects.None, 0.0f);

        public static void DrawTTextureOutlined(TTexture texture, Vector2 position, Color color, Color outlineColor, float rotation, Vector2 origin, float scale, SpriteEffects effects, float layerDepth) =>
            DrawTTextureOutlined(texture, position, color, outlineColor, rotation, origin, new Vector2(scale, scale), SpriteEffects.None, 0.0f);

        public static void DrawTTextureOutlined(TTexture texture, Vector2 position, Color color, Color outlineColor, float rotation, Vector2 origin, Vector2 scale, SpriteEffects effects, float layerDepth)
        {
            Ensure.IsFalse(texture.IsDisposed, "{0} has been disposed", nameof(texture));

            //  Draw offset by (-1, -1), (1, -1), (1, -1), (1, 1) with outline color
            DrawTTexture(texture, position + new Vector2(-1, -1), outlineColor, rotation, origin, scale, effects, layerDepth);
            DrawTTexture(texture, position + new Vector2(-1, 1), outlineColor, rotation, origin, scale, effects, layerDepth);
            DrawTTexture(texture, position + new Vector2(1, -1), outlineColor, rotation, origin, scale, effects, layerDepth);
            DrawTTexture(texture, position + new Vector2(1, 1), outlineColor, rotation, origin, scale, effects, layerDepth);

            //  Now draw normally
            DrawTTexture(texture, position, color, rotation, origin, scale, effects, layerDepth);
        }

        #endregion Draw TTexture
    }
}