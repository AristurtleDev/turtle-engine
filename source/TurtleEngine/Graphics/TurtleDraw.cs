using System.Diagnostics.CodeAnalysis;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TurtleEngine;

public static class TurtleDraw
{
    private static Rectangle _rect = Microsoft.Xna.Framework.Rectangle.Empty;

    public static SpriteBatch SpriteBatch { get; private set; }
    public static TurtleTexture2D Pixel { get; private set; }

    public static void Initialize(GraphicsDevice graphicsDevice)
    {
        SpriteBatch = new(graphicsDevice);

        Texture2D pixel = new(graphicsDevice, 1, 1);
        pixel.SetData<Color>(new Color[] { Color.White });
        pixel.Name = "pixel";
        Pixel = new(pixel);
    }

    public static void Begin(SpriteSortMode sortMode = SpriteSortMode.Deferred, BlendState? blendState = default, SamplerState? samplerState = default, DepthStencilState? depthStencilState = default, RasterizerState? rasterizerState = default, Effect? effect = default, Matrix? transformMatrix = default)
    {
        EnsureSpriteBatch();
        SpriteBatch.Begin(sortMode, blendState, samplerState, depthStencilState, rasterizerState, effect, transformMatrix);
    }

    public static void End()
    {
        EnsureSpriteBatch();
        SpriteBatch.End();
    }

    private static void EnsureSpriteBatch()
    {
        if (SpriteBatch is null)
        {
            throw new InvalidOperationException($"{nameof(TurtleDraw)}.{nameof(TurtleDraw.SpriteBatch)} is null.  Did you forget to call {nameof(TurtleDraw)}.{nameof(TurtleDraw.Initialize)}");
        }

        if (SpriteBatch.IsDisposed)
        {
            throw new InvalidOperationException($"{nameof(TurtleDraw)}.{nameof(SpriteBatch)} is disposed");
        }
    }

    public static void Point(Vector2 position, Color color) => Point(position, color, 0.0f);

    public static void Point(Vector2 position, Color color, float layerDepth)
    {
        EnsurePixel();
        Pixel.Draw(position, color, 0.0f, Vector2.Zero, Vector2.One, SpriteEffects.None, 0.0f);
    }

    public static void Line(float x1, float y1, float x2, float y2, Color color) =>
        Line(new Vector2(x1, y1), new Vector2(x2, y2), color, 1.0f, 0.0f);

    public static void Line(float x1, float y1, float x2, float y2, Color color, float thickness) =>
        Line(new Vector2(x1, y1), new Vector2(x2, y2), color, thickness, 0.0f);

    public static void Line(float x1, float y1, float x2, float y2, Color color, float thickness, float layerDepth) =>
        Line(new Vector2(x1, y1), new Vector2(x2, y2), color, thickness, layerDepth);

    public static void Line(Vector2 start, Vector2 end, Color color) => Line(start, end, color, 1.0f, 0.0f);

    public static void Line(Vector2 start, Vector2 end, Color color, float thickness) =>
        LineAngle(start, Maths.Angle(start, end), Vector2.Distance(start, end), color, thickness, 0.0f);

    public static void Line(Vector2 start, Vector2 end, Color color, float thickness, float layerDepth) =>
        LineAngle(start, Maths.Angle(start, end), Vector2.Distance(start, end), color, thickness, layerDepth);

    public static void LineAngle(float startX, float startY, float angle, float length, Color color) =>
        LineAngle(startX, startY, angle, length, color, 1.0f, 0.0f);

    public static void LineAngle(float startX, float startY, float angle, float length, Color color, float thickness) =>
        LineAngle(new Vector2(startX, startY), angle, length, color, thickness, 0.0f);

    public static void LineAngle(float startX, float startY, float angle, float length, Color color, float thickness, float layerDepth) =>
        LineAngle(new Vector2(startX, startY), angle, length, color, thickness, layerDepth);

    public static void LineAngle(Vector2 start, float angle, float length, Color color) =>
        LineAngle(start, angle, length, color, 1.0f, 0.0f);

    public static void LineAngle(Vector2 start, float angle, float length, Color color, float thickness) =>
        LineAngle(start, angle, length, color, thickness, 0.0f);

    public static void LineAngle(Vector2 start, float angle, float length, Color color, float thickness, float layerDepth)
    {
        EnsurePixel();
        Pixel.Draw(start, color, angle, new Vector2(0, 0.5f), new Vector2(length, thickness), SpriteEffects.None, layerDepth);
    }

    public static void Rectangle(Rectangle rectangle, Color color) =>
        Rectangle(rectangle, color, 0.0f);

    public static void Rectangle(Rectangle rectangle, Color color, float layerDepth)
    {
        EnsurePixel();
        Pixel.Draw(rectangle, color, 0.0f, Vector2.Zero, SpriteEffects.None, layerDepth);
    }

    public static void Rectangle(Vector2 position, float width, float height, Color color) =>
        Rectangle(position.X, position.Y, width, height, color, 0.0f);

    public static void Rectangle(Vector2 position, float width, float height, Color color, float layerDepth) =>
        Rectangle(position.X, position.Y, width, height, color, layerDepth);

    public static void Rectangle(float x, float y, float width, float height, Color color) =>
        Rectangle(x, y, width, height, color, 0.0f);

    public static void Rectangle(float x, float y, float width, float height, Color color, float layerDepth)
    {
        _rect.X = (int)x;
        _rect.Y = (int)y;
        _rect.Width = (int)width;
        _rect.Height = (int)height;

        EnsurePixel();
        Pixel.Draw(_rect, color, 0.0f, Vector2.Zero, SpriteEffects.None, layerDepth);
    }

    public static void HollowRectangle(Rectangle rectangle, Color color) =>
        HollowRectangle(rectangle, color, 1.0f, 0.0f);

    public static void HollowRectangle(Rectangle rectangle, Color color, float thickness) =>
        HollowRectangle(rectangle, color, thickness, 0.0f);

    public static void HollowRectangle(Rectangle rectangle, Color color, float thickness, float layerDepth)
    {
        Line(rectangle.Left, rectangle.Top, rectangle.Right, rectangle.Top, color, thickness, layerDepth);
        Line(rectangle.Right, rectangle.Top, rectangle.Right, rectangle.Bottom, color, thickness, layerDepth);
        Line(rectangle.Right, rectangle.Bottom, rectangle.Left, rectangle.Bottom, color, thickness, layerDepth);
        Line(rectangle.Left, rectangle.Bottom, rectangle.Left, rectangle.Top, color, thickness, layerDepth);
    }

    public static void HollowRectangle(Vector2 position, float width, float height, Color color) =>
        HollowRectangle(position.X, position.Y, width, height, color, 1.0f, 0.0f);

    public static void HollowRectangle(Vector2 position, float width, float height, Color color, float thickness) =>
        HollowRectangle(position.X, position.Y, width, height, color, thickness, 0.0f);

    public static void HollowRectangle(Vector2 position, float width, float height, Color color, float thickness, float layerDepth) =>
        HollowRectangle(position.X, position.Y, width, height, color, thickness, layerDepth);

    public static void HollowRectangle(float x, float y, float width, float height, Color color) =>
        HollowRectangle(x, y, width, height, color, 1.0f, 0.0f);

    public static void HollowRectangle(float x, float y, float width, float height, Color color, float thickness) =>
        HollowRectangle(x, y, width, height, color, thickness, 0.0f);

    public static void HollowRectangle(float x, float y, float width, float height, Color color, float thickness, float layerDepth)
    {
        Line(x, y, x + width, y, color, thickness, layerDepth);
        Line(x + width, y, x + width, y + height, color, thickness, layerDepth);
        Line(x + width, y + height, x, y + height, color, thickness, layerDepth);
        Line(x, y + height, x, y, color, thickness, layerDepth);
    }

    public static void Circle(float x, float y, float radius, Color color, float resolution) =>
        Circle(new Vector2(x, y), radius, color, resolution, 1.0f, 0.0f);

    public static void Circle(float x, float y, float radius, Color color, float resolution, float thickness) =>
        Circle(new Vector2(x, y), radius, color, resolution, thickness, 0.0f);

    public static void Circle(float x, float y, float radius, Color color, float resolution, float thickness, float layerDepth) =>
        Circle(new Vector2(x, y), radius, color, resolution, thickness, layerDepth);

    public static void Circle(Vector2 position, float radius, Color color, float resolution) =>
        Circle(position, radius, color, 1.0f, resolution, 0.0f);

    public static void Circle(Vector2 position, float radius, Color color, float resolution, float thickness) =>
        Circle(position, radius, color, thickness, resolution, 0.0f);

    public static void Circle(Vector2 position, float radius, Color color, float resolution, float thickness, float layerDepth)
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

    [MemberNotNull(nameof(Pixel))]
    private static void EnsurePixel()
    {
        if (Pixel is null)
        {
            throw new InvalidOperationException($"{nameof(TurtleDraw)}.{nameof(TurtleDraw.Pixel)} is null.  You may have forgot to call {nameof(TurtleDraw)}.{nameof(TurtleDraw.Initialize)}");
        }

        if (Pixel.IsDisposed)
        {
            throw new InvalidOperationException($"{nameof(TurtleDraw)}.{nameof(TurtleDraw.Pixel)} is disposed");
        }
    }
}