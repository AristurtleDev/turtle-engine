using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TurtleEngine.Graphics;

/// <summary>
///     Defines a 2D texture with a source rectangle and UVs.
/// </summary>
public sealed class TurtleTexture : IDisposable
{
    private bool _disposed;

    /// <summary>
    ///     Gets the underlying source texture to render.
    /// </summary>
    public Texture2D Texture { get; }

    /// <summary>
    ///     Gets the source rectangle that defines the bound within the underlying source texture to render.
    /// </summary>
    public Rectangle SourceRectangle { get; }

    /// <summary>
    ///     The top edge UV coordinate of this texture.
    /// </summary>
    public readonly float TopUV;

    /// <summary>
    ///     The bottom edge UV coordinate of this texture.
    /// </summary>
    public readonly float BottomUV;

    /// <summary>
    ///     The left edge UV coordinate of this texture.
    /// </summary>
    public readonly float LeftUV;

    /// <summary>
    ///     The right edge UV coordinate of this texture.
    /// </summary>
    public readonly float RightUV;

    /// <summary>
    ///     Gets the width, in pixels, of this texture.
    /// </summary>
    public int Width => SourceRectangle.Width;

    /// <summary>
    ///     Gets the height, in pixels, of the this texture.
    /// </summary>
    public int Height => SourceRectangle.Height;

    /// <summary>
    ///     Creates a new instance of the <see cref="TurtleTexture"/> class with the specified texture as the source
    ///     image.
    /// </summary>
    /// <param name="texture">
    ///     The texture to use as the underlying source image.
    /// </param>
    /// <exception cref="ArgumentNullException">
    ///     Thrown if the <paramref name="texture"/> given is<see langword="null"/>.
    /// </exception>
    /// <exception cref="ObjectDisposedException">
    ///     Thrown if the <paramref name="texture"/> given has already been disposed of.
    /// </exception>
    public TurtleTexture(Texture2D texture)
    {
        ArgumentNullException.ThrowIfNull(texture);
        if (texture.IsDisposed)
        {
            throw new ObjectDisposedException(nameof(texture), $"{nameof(texture)} has been disposed of previously.");
        }

        Texture = texture;
        SourceRectangle = texture.Bounds;
        SetUVs(out TopUV, out BottomUV, out LeftUV, out RightUV);
    }

    /// <summary>
    ///     Creates a new instance of the <see cref="TurtleTexture"/> class initialized with the given data.
    /// </summary>
    /// <param name="graphicsDevice">
    ///     The graphics device instance used for creating graphical resources.
    /// </param>
    /// <param name="width">
    ///     The width, in pixels, of the texture being created.
    /// </param>
    /// <param name="height">
    ///     The height, in pixels, of the texture being created.
    /// </param>
    /// <param name="color">
    ///     The color data to set for the texture.
    /// </param>
    /// <exception cref="ArgumentNullException">
    ///     Thrown if <paramref name="color"/> is <see langword="null"/></exception>
    /// <exception cref="InvalidOperationException">
    ///     Thrown if the length of <paramref name="color"/> is not equal to
    ///     <paramref name="width"/>*<paramref name="height"/>
    /// </exception>
    public TurtleTexture(GraphicsDevice graphicsDevice, int width, int height, Color[] color)
    {
        ArgumentNullException.ThrowIfNull(color);

        if (color.Length != width * height)
        {
            throw new InvalidOperationException($"{nameof(color)} array length does not match {nameof(width)} * {nameof(height)}");
        }

        Texture = new(graphicsDevice, width, height, mipmap: false, SurfaceFormat.Color);
        Texture.SetData<Color>(color);
        SetUVs(out TopUV, out BottomUV, out LeftUV, out RightUV);
    }

    /// <summary>
    ///     Creates a new instance of the <see cref="TurtleTexture"/> class as a sub texture region of an existing
    ///     <see cref="TurtleTexture"/> instance.
    /// </summary>
    /// <param name="parent">
    ///     The existing instance of the <see cref="TurtleTexture"/> class that will be used to create the sub texture
    ///     region for this <see cref="TurtleTexture"/> being created.
    /// </param>
    /// <param name="sourceRectangle">
    ///     <para>
    ///         The bounds within the <paramref name="parent"/> texture that defines the sub texture region for for the
    ///         <see cref="TurtleTexture"/> being created.
    ///     </para>
    ///     <para>
    ///         The bounds are relative to the <see cref="TurtleTexture.SourceRectangle"/> of the
    ///         <paramref name="parent"/>.
    ///     </para>
    /// </param>
    /// <exception cref="ArgumentNullException">
    ///     Thrown if <paramref name="parent"/> is <see langword="null"/>.
    /// </exception>
    public TurtleTexture(TurtleTexture parent, Rectangle sourceRectangle)
    {
        ArgumentNullException.ThrowIfNull(parent);
        Texture = parent.Texture;
        SourceRectangle = GetRelativeSourceRectangle(sourceRectangle);
        SetUVs(out TopUV, out BottomUV, out LeftUV, out RightUV);
    }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    ~TurtleTexture() => Dispose(false);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

    private void SetUVs(out float top, out float bottom, out float left, out float right)
    {
        top = SourceRectangle.Top / (float)Texture.Height;
        bottom = SourceRectangle.Bottom / (float)Texture.Height;
        left = SourceRectangle.Left / (float)Texture.Width;
        right = SourceRectangle.Right / (float)Texture.Width;
    }

    private Rectangle GetRelativeSourceRectangle(Rectangle rect)
    {
        int x = SourceRectangle.X + rect.X;
        int y = SourceRectangle.Y + rect.Y;

        int relativeX = MathHelper.Clamp(x, SourceRectangle.Left, SourceRectangle.Right);
        int relativeY = MathHelper.Clamp(y, SourceRectangle.Top, SourceRectangle.Bottom);
        int relativeWidth = Math.Max(0, Math.Min(x + rect.Width, SourceRectangle.Right) - relativeX);
        int relativeHeight = Math.Max(0, Math.Min(y + rect.Height, SourceRectangle.Bottom) - relativeY);

        return new(relativeX, relativeY, relativeWidth, relativeHeight);
    }

   /// <inheritdoc />
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    private void Dispose(bool isDisposing)
    {
        if (_disposed) return;

        if (isDisposing)
        {
            Texture.Dispose();
        }

        _disposed = true;
    }
}
