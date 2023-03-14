using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TurtleEngine;

/// <summary>
///     Defines a wrapper around a <see cref="Microsoft.Xna.Framework.Graphics.Texture2D"/> that defines a source
///     rectangle and UVs
/// </summary>
public sealed class TTexture : IDisposable
{
    /// <summary>
    ///     Gets a value that indicates if this <see cref="TTexture"/> has been disposed of.
    /// </summary>
    public bool IsDisposed { get; private set; }

    /// <summary>
    ///     Gets the source <see cref="Texture2D"/> image of this <see cref="TTexture"/>.
    /// </summary>
    public Texture2D Texture { get; }

    /// <summary>
    ///     Gets the source rectangle of this <see cref="TTexture"/> that represents the bounds within the
    ///     source <see cref="Texture"/> to render.
    /// </summary>
    public Rectangle SourceRectangle { get; }

    /// <summary>
    ///     Gets the top edge UV coordinate of this <see cref="TTexture"/>.
    /// </summary>
    public readonly float TopUV;

    /// <summary>
    ///     Gets the bottom edge UV coordinate of this <see cref="TTexture"/>.
    /// </summary>
    public readonly float BottomUV;

    /// <summary>
    ///     Gets the left edge UV coordinate of this <see cref="TTexture"/>.
    /// </summary>
    public readonly float LeftUV;

    /// <summary>
    ///     Gets the right edge UV coordinate of this <see cref="TTexture"/>.
    /// </summary>
    public readonly float RightUV;

    /// <summary>
    ///     Gets the width, in pixels, of the image represented by this <see cref="TTexture"/>.
    /// </summary>
    public int Width => SourceRectangle.Width;

    /// <summary>
    ///     Gets the height, in pixels, of the image represented by this <see cref="TTexture"/>.
    /// </summary>
    public int Height => SourceRectangle.Height;

    /// <summary>
    ///     Initializes a new instance of the <see cref="TTexture"/> class with the specified 
    ///     <see cref="Texture2D"/> as the source image.
    /// </summary>
    /// <param name="texture">
    ///     The <see cref="Texture2D"/> to use as the source image for the <see cref="TTexture"/> being created.
    /// </param>
    public TTexture(Texture2D texture)
    {
        Texture = texture;
        SourceRectangle = texture.Bounds;
        SetUVs(out TopUV, out BottomUV, out LeftUV, out RightUV);
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="TTexture"/> class as a sub texture region of an exiting
    ///     <see cref="TTexture"/>.
    /// </summary>
    /// <param name="parent">
    ///     The existing instance of the <see cref="TTexture"/> class that will be used to create the sub texture
    ///     region for this <see cref="TTexture"/> being created.
    /// </param>
    /// <param name="sourceRectangle">
    ///     A <see cref="Rectangle"/> value that defines the source rectangle of the sub texture region for the
    ///     <see cref="TTexture"/> being created, relative to the <see cref="TTexture.SourceRectangle"/>
    ///     of the <paramref name="parent"/>.
    /// </param>
    public TTexture(TTexture parent, Rectangle sourceRectangle)
    {
        Texture = parent.Texture;
        SourceRectangle = GetRelativeSourceRectangle(sourceRectangle);
        SetUVs(out TopUV, out BottomUV, out LeftUV, out RightUV);
    }

    ~TTexture() => Dispose(false);

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

    /// <summary>
    ///     Disposes of resources used by this instance of the <see cref="TTexture"/> class.
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    private void Dispose(bool isDisposing)
    {
        if (IsDisposed) return;

        if (isDisposing)
        {
            Texture.Dispose();
        }

        IsDisposed = true;
    }
}