using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TurtleEngine;

/// <summary>
///     Defines a wrapper around a <see cref="Microsoft.Xna.Framework.Graphics.Texture2D"/> that defines a source
///     rectangle and UVs
/// </summary>
public sealed class TurtleTexture2D : IDisposable
{
    /// <summary>
    ///     Gets a value that indicates if this <see cref="TurtleTexture2D"/> has been disposed of.
    /// </summary>
    public bool IsDisposed { get; private set; }

    /// <summary>
    ///     Gets the source <see cref="Texture2D"/> image of this <see cref="TurtleTexture2D"/>.
    /// </summary>
    public Texture2D Texture { get; }

    /// <summary>
    ///     Gets the source rectangle of this <see cref="TurtleTexture2D"/> that represents the bounds within the
    ///     source <see cref="Texture"/> to render.
    /// </summary>
    public Rectangle SourceRectangle { get; }

    /// <summary>
    ///     Gets the top edge UV coordinate of this <see cref="TurtleTexture2D"/>.
    /// </summary>
    public readonly float TopUV;

    /// <summary>
    ///     Gets the bottom edge UV coordinate of this <see cref="TurtleTexture2D"/>.
    /// </summary>
    public readonly float BottomUV;

    /// <summary>
    ///     Gets the left edge UV coordinate of this <see cref="TurtleTexture2D"/>.
    /// </summary>
    public readonly float LeftUV;

    /// <summary>
    ///     Gets the right edge UV coordinate of this <see cref="TurtleTexture2D"/>.
    /// </summary>
    public readonly float RightUV;

    /// <summary>
    ///     Gets the width, in pixels, of the image represented by this <see cref="TurtleTexture2D"/>.
    /// </summary>
    public int Width => SourceRectangle.Width;

    /// <summary>
    ///     Gets the height, in pixels, of the image represented by this <see cref="TurtleTexture2D"/>.
    /// </summary>
    public int Height => SourceRectangle.Height;

    /// <summary>
    ///     Initializes a new instance of the <see cref="TurtleTexture2D"/> class with the specified 
    ///     <see cref="Texture2D"/> as the source image.
    /// </summary>
    /// <param name="texture">
    ///     The <see cref="Texture2D"/> to use as the source image for the <see cref="TurtleTexture2D"/> being created.
    /// </param>
    public TurtleTexture2D(Texture2D texture)
    {
        Texture = texture;
        SourceRectangle = texture.Bounds;
        SetUVs(out TopUV, out BottomUV, out LeftUV, out RightUV);
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="TurtleTexture2D"/> class as a sub texture region of an exiting
    ///     <see cref="TurtleTexture2D"/>.
    /// </summary>
    /// <param name="parent">
    ///     The existing instance of the <see cref="TurtleTexture2D"/> class that will be used to create the sub texture
    ///     region for this <see cref="TurtleTexture2D"/> being created.
    /// </param>
    /// <param name="sourceRectangle">
    ///     A <see cref="Rectangle"/> value that defines the source rectangle of the sub texture region for the
    ///     <see cref="TurtleTexture2D"/> being created, relative to the <see cref="TurtleTexture2D.SourceRectangle"/>
    ///     of the <paramref name="parent"/>.
    /// </param>
    public TurtleTexture2D(TurtleTexture2D parent, Rectangle sourceRectangle)
    {
        Texture = parent.Texture;
        SourceRectangle = GetRelativeSourceRectangle(sourceRectangle);
        SetUVs(out TopUV, out BottomUV, out LeftUV, out RightUV);
    }

    ~TurtleTexture2D() => Dispose(false);

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
    ///     Disposes of resources used by this instance of the <see cref="TurtleTexture2D"/> class.
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

    public void Draw(Rectangle destinationRectangle, Color color) =>
        Draw(destinationRectangle, color, 0.0f, Vector2.Zero, SpriteEffects.None, 0.0f);

    public void Draw(Rectangle destinationRectangle, Color color, float rotation, Vector2 origin, SpriteEffects effects, float layerDepth)
    {
#if DEBUG
        if (Texture.IsDisposed)
        {
            throw new InvalidOperationException($"Texture '{Texture.Name}' is disposed");
        }
#endif
        TurtleEngine.TurtleDraw.SpriteBatch?.Draw(Texture, destinationRectangle, SourceRectangle, color, rotation, origin, effects, layerDepth);
    }

    public void Draw(Vector2 position) =>
        TurtleEngine.TurtleDraw.SpriteBatch?.Draw(Texture, position, SourceRectangle, Color.White);

    public void Draw(Vector2 position, Color color) =>
        Draw(position, color, 0.0f, Vector2.Zero, Vector2.One, SpriteEffects.None, 0.0f);

    public void Draw(Vector2 position, Color color, Vector2 origin) =>
        Draw(position, color, 0.0f, origin, Vector2.One, SpriteEffects.None, 0.0f);

    public void Draw(Vector2 position, Color color, float rotation, Vector2 origin, float scale) =>
        Draw(position, color, rotation, origin, new Vector2(scale, scale), SpriteEffects.None, 0.0f);

    public void Draw(Vector2 position, Color color, float rotation, Vector2 origin, Vector2 scale) =>
        Draw(position, color, rotation, origin, scale, SpriteEffects.None, 0.0f);

    public void Draw(Vector2 position, Color color, float rotation, Vector2 origin, float scale, SpriteEffects effects, float layerDepth) =>
        Draw(position, color, rotation, origin, new Vector2(scale, scale), effects, layerDepth);

    public void Draw(Vector2 position, Color color, float rotation, Vector2 origin, Vector2 scale, SpriteEffects effects, float layerDepth)
    {
#if DEBUG
        if (Texture.IsDisposed)
        {
            throw new InvalidOperationException($"Texture '{Texture.Name}' is disposed");
        }
#endif
        TurtleEngine.TurtleDraw.SpriteBatch?.Draw(Texture, position, SourceRectangle, color, rotation, origin, scale, effects, layerDepth);
    }


    public void DrawOutlined(Vector2 position) =>
        DrawOutlined(position, Color.White, Color.Black, 0.0f, Vector2.Zero, Vector2.One, SpriteEffects.None, 0.0f);

    public void DrawOutlined(Vector2 position, Color color, Color outlineColor) =>
        DrawOutlined(position, color, outlineColor, 0.0f, Vector2.Zero, Vector2.One, SpriteEffects.None, 0.0f);

    public void DrawOutlined(Vector2 position, Color color, Color outlineColor, Vector2 origin) =>
        DrawOutlined(position, color, outlineColor, 0.0f, origin, Vector2.Zero, SpriteEffects.None, 0.0f);

    public void DrawOutlined(Vector2 position, Color color, Color outlineColor, float rotation, Vector2 origin, float scale) =>
        DrawOutlined(position, color, outlineColor, rotation, origin, new Vector2(scale, scale), SpriteEffects.None, 0.0f);

    public void DrawOutlined(Vector2 position, Color color, Color outlineColor, float rotation, Vector2 origin, Vector2 scale) =>
        DrawOutlined(position, color, outlineColor, rotation, origin, scale, SpriteEffects.None, 0.0f);

    public void DrawOutlined(Vector2 position, Color color, Color outlineColor, float rotation, Vector2 origin, float scale, SpriteEffects effects, float layerDepth) =>
        DrawOutlined(position, color, outlineColor, rotation, origin, new Vector2(scale, scale), SpriteEffects.None, 0.0f);

    public void DrawOutlined(Vector2 position, Color color, Color outlineColor, float rotation, Vector2 origin, Vector2 scale, SpriteEffects effects, float layerDepth)
    {
#if DEBUG
        if (Texture.IsDisposed)
        {
            throw new InvalidOperationException($"Texture '{Texture.Name}' is disposed");
        }
#endif
        //  Draw offset by (-1, -1), (1, -1), (1, -1), (1, 1) with outline color
        Draw(position + new Vector2(-1, -1), outlineColor, rotation, origin, scale, effects, layerDepth);
        Draw(position + new Vector2(-1, 1), outlineColor, rotation, origin, scale, effects, layerDepth);
        Draw(position + new Vector2(1, -1), outlineColor, rotation, origin, scale, effects, layerDepth);
        Draw(position + new Vector2(1, 1), outlineColor, rotation, origin, scale, effects, layerDepth);

        //  Now draw normally
        Draw(position, color, rotation, origin, scale, effects, layerDepth);
    }
}