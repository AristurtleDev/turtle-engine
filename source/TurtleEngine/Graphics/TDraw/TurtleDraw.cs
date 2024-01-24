using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TurtleEngine.Debug;
using TurtleEngine.Graphics;

namespace TurtleEngine;


public static partial class TurtleDraw
{
    private static Rectangle _rect = Microsoft.Xna.Framework.Rectangle.Empty;

    /// <summary>
    ///     The sprite batch instance used for 2D rendering.
    /// </summary>
    public static SpriteBatch SpriteBatch { get; private set; }

    /// <summary>
    ///     Gets a 1x1 single pixel texture.
    /// </summary>
    public static TurtleTexture Pixel { get; private set; }

    internal static void Initialize(GraphicsDevice graphicsDevice)
    {
        SpriteBatch = new(graphicsDevice);
        Pixel = new(graphicsDevice, 1, 1, new Color[] { Color.White });
    }

    [Conditional("DEBUG")]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void EnsureInitialized()
    {
        if (SpriteBatch == null)
        {
            throw new InvalidOperationException("TurtleDraw.SpriteBatch is null");
        }

        if (Pixel is null)
        {
            throw new InvalidOperationException("TurtleDraw.Pixel is null");
        }
    }

    /***************************************************************************
    ***
    *** Point
    ***
    ***************************************************************************/
    #region Point

    /// <summary>
    ///     Draws a point.
    /// </summary>
    /// <param name="x">
    ///     The x-coordinate position of the point.
    /// </param>
    /// <param name="y">
    ///     The y-coordinate position of the point.
    /// </param>
    public static void Point(float x, float y) => Point(x, y, Color.White);

    /// <summary>
    ///     Draws a point.
    /// </summary>
    /// <param name="position">
    ///     The xy-coordinate position to draw the pixel at.
    /// </param>
    public static void Point(Vector2 position) => Point(position, Color.White);

    /// <summary>
    ///     Draws a point.
    /// </summary>
    /// <param name="x">
    ///     The x-coordinate position of the point.
    /// </param>
    /// <param name="y">
    ///     The y-coordinate position of the point.
    /// </param>
    /// <param name="color">
    ///     The color mask to apply when rendering the point.
    /// </param>
    public static void Point(float x, float y, Color color)
    {
        Vector2 position = new Vector2(x, y);
        Point(position, color);
    }

    /// <summary>
    ///     Draws a point.
    /// </summary>
    /// <param name="position">
    ///     The xy-coordinate position to draw the pixel at.
    /// </param>
    /// <param name="color">
    ///     The color mask to apply when rendering the point.
    /// </param>
    public static void Point(Vector2 position, Color color)
    {
        EnsureInitialized();
        SpriteBatch.Draw(Pixel.Texture, position, Pixel.SourceRectangle, color);
    }

    #endregion Point

    /***************************************************************************
    ***
    *** Line
    ***
    ***************************************************************************/
    #region Line

    /// <summary>
    ///     Draws a line.
    /// </summary>
    /// <param name="startX">
    ///     The starting x-coordinate position of the line.
    /// </param>
    /// <param name="startY">
    ///     The starting y-coordinate position of the line.
    /// </param>
    /// <param name="endX">
    ///     The ending x-coordinate position of the line.
    /// </param>
    /// <param name="endY">
    ///     The ending y-coordinate position of the line.
    /// </param>
    public static void Line(float startX, float startY, float endX, float endY) =>
        Line(startX, startY, endX, endY, Color.White, 1.0f);

    /// <summary>
    ///     Draws a line.
    /// </summary>
    /// <param name="start">
    ///     The starting xy-coordinate position of the line.
    /// </param>
    /// <param name="end">
    ///     The ending xy-coordinate position of the line.
    /// </param>
    public static void Line(Vector2 start, Vector2 end) =>
        Line(start, end, Color.White, 1.0f);

    /// <summary>
    ///     Draws a line.
    /// </summary>
    /// <param name="startX">
    ///     The starting x-coordinate position of the line.
    /// </param>
    /// <param name="startY">
    ///     The starting y-coordinate position of the line.
    /// </param>
    /// <param name="endX">
    ///     The ending x-coordinate position of the line.
    /// </param>
    /// <param name="endY">
    ///     The ending y-coordinate position of the line.
    /// </param>
    /// <param name="color">
    ///     The color mask to apply when rendering the line.
    /// </param>
    public static void Line(float startX, float startY, float endX, float endY, Color color) =>
        Line(startX, startY, endX, endY, color, 1.0f);

    /// <summary>
    ///     Draws a line.
    /// </summary>
    /// <param name="start">
    ///     The starting xy-coordinate position of the line.
    /// </param>
    /// <param name="end">
    ///     The ending xy-coordinate position of the line.
    /// </param>
    /// <param name="color">
    ///     The color mask to apply when rendering the line.
    /// </param>
    public static void Line(Vector2 start, Vector2 end, Color color) =>
        Line(start, end, color, 1.0f);

    /// <summary>
    ///     Draws a line.
    /// </summary>
    /// <param name="startX">
    ///     The starting x-coordinate position of the line.
    /// </param>
    /// <param name="startY">
    ///     The starting y-coordinate position of the line.
    /// </param>
    /// <param name="endX">
    ///     The ending x-coordinate position of the line.
    /// </param>
    /// <param name="endY">
    ///     The ending y-coordinate position of the line.
    /// </param>
    /// <param name="color">
    ///     The color mask to apply when rendering the line.
    /// </param>
    /// <param name="thickness">
    ///     The thickness, in pixels, of the line.
    /// </param>
    public static void Line(float startX, float startY, float endX, float endY, Color color, float thickness)
    {
        Vector2 start = new Vector2(startX, startY);
        Vector2 end = new Vector2(endX, endY);
        Line(start, end, color, thickness);
    }

    /// <summary>
    ///     Draws a line.
    /// </summary>
    /// <param name="start">
    ///     The starting xy-coordinate position of the line.
    /// </param>
    /// <param name="end">
    ///     The ending xy-coordinate position of the line.
    /// </param>
    /// <param name="color">
    ///     The color mask to apply when rendering the line.
    /// </param>
    /// <param name="thickness">
    ///     The thickness, in pixels, of the line.
    /// </param>
    public static void Line(Vector2 start, Vector2 end, Color color, float thickness)
    {
        float angle = TurtleMath.Angle(start, end);
        float length = Vector2.Distance(start, end);
        LineAngle(start, angle, length, color, thickness);
    }

    #endregion Line

    /***************************************************************************
    ***
    *** Line Angle
    ***
    ***************************************************************************/
    #region Line Angle

    /// <summary>
    ///     Draws a line at an angle.
    /// </summary>
    /// <param name="start">
    ///     The starting xy-coordinate point of the line.
    /// </param>
    /// <param name="angle">
    ///     The angle, in radians, to draw the line at.
    /// </param>
    /// <param name="length">
    ///     The length, in pixels, of the line to draw.
    /// </param>
    public static void LineAngle(Vector2 start, float angle, float length) =>
        LineAngle(start, angle, length, Color.White, 1.0f);

    /// <summary>
    ///     Draws a line at an angle.
    /// </summary>
    /// <param name="start">
    ///     The starting xy-coordinate point of the line.
    /// </param>
    /// <param name="angle">
    ///     The angle, in radians, to draw the line at.
    /// </param>
    /// <param name="length">
    ///     The length, in pixels, of the line to draw.
    /// </param>
    /// <param name="color">
    ///     The color mask to apply when rendering the line.
    /// </param>
    public static void LineAngle(Vector2 start, float angle, float length, Color color) =>
        LineAngle(start, angle, length, color, 1.0f);

    /// <summary>
    ///     Draws a line at an angle.
    /// </summary>
    /// <param name="start">
    ///     The starting xy-coordinate point of the line.
    /// </param>
    /// <param name="angle">
    ///     The angle, in radians, to draw the line at.
    /// </param>
    /// <param name="length">
    ///     The length, in pixels, of the line to draw.
    /// </param>
    /// <param name="color">
    ///     The color mask to apply when rendering the line.
    /// </param>
    /// <param name="thickness">
    ///     The thickness, in pixels, of the line.
    /// </param>
    public static void LineAngle(Vector2 start, float angle, float length, Color color, float thickness)
    {
        EnsureInitialized();
        SpriteBatch.Draw(Pixel.Texture, start, Pixel.SourceRectangle, color, angle, new Vector2(0, 0.5f), new Vector2(length, thickness), SpriteEffects.None, 0.0f);
    }

    #endregion

    /***************************************************************************
    ***
    *** Rectangle
    ***
    ***************************************************************************/
    #region Rectangle

    /// <summary>
    ///     Draws a filled rectangle.
    /// </summary>
    /// <param name="rectangle">
    ///     The rectangle to draw.
    /// </param>
    public static void Rectangle(Rectangle rectangle) =>
        Rectangle(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height, Color.White);

    /// <summary>
    ///     Draws a filled rectangle.
    /// </summary>
    /// <param name="rectangle">
    ///     The rectangle to draw.
    /// </param>
    /// <param name="color">
    ///     The color mask to apply when rendering the rectangle.
    /// </param>
    public static void Rectangle(Rectangle rectangle, Color color) =>
        Rectangle(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height, color);

    /// <summary>
    ///     Draws a filled rectangle.
    /// </summary>
    /// <param name="position">
    ///     The top-left xy-coordinate position of the rectangle.
    /// </param>
    /// <param name="width">
    ///     The width, in pixels, of the rectangle.
    /// </param>
    /// <param name="height">
    ///     The height, in pixels, of the rectangle.
    /// </param>
    public static void Rectangle(Vector2 position, float width, float height) =>
        Rectangle(position.X, position.X, width, height, Color.White);

    /// <summary>
    ///     Draws a filled rectangle.
    /// </summary>
    /// <param name="position">
    ///     The top-left xy-coordinate position of the rectangle.
    /// </param>
    /// <param name="width">
    ///     The width, in pixels, of the rectangle.
    /// </param>
    /// <param name="height">
    ///     The height, in pixels, of the rectangle.
    /// </param>
    /// <param name="color">
    ///     The color mask to apply when rendering the rectangle.
    /// </param>
    public static void Rectangle(Vector2 position, float width, float height, Color color) =>
        Rectangle(position.X, position.Y, width, height, color);

    /// <summary>
    ///     Draws a filled rectangle.
    /// </summary>
    /// <param name="x">
    ///     The top-left x-coordinate position of the rectangle.
    /// </param>
    /// <param name="y">
    ///     The top-left y-coordinate position of the rectangle.
    /// </param>
    /// <param name="width">
    ///     The width, in pixels, of the rectangle.
    /// </param>
    /// <param name="height">
    ///     The height, in pixels, of the rectangle.
    /// </param>
    public static void Rectangle(float x, float y, float width, float height) =>
        Rectangle(x, y, width, height, Color.White);

    /// <summary>
    ///     Draws a filled rectangle.
    /// </summary>
    /// <param name="x">
    ///     The top-left x-coordinate position of the rectangle.
    /// </param>
    /// <param name="y">
    ///     The top-left y-coordinate position of the rectangle.
    /// </param>
    /// <param name="width">
    ///     The width, in pixels, of the rectangle.
    /// </param>
    /// <param name="height">
    ///     The height, in pixels, of the rectangle.
    /// </param>
    /// <param name="color">
    ///     The color mask to apply when rendering the rectangle.
    /// </param>
    public static void Rectangle(float x, float y, float width, float height, Color color)
    {
        EnsureInitialized();

        _rect.X = (int)x;
        _rect.Y = (int)y;
        _rect.Width = (int)width;
        _rect.Height = (int)height;

        SpriteBatch.Draw(Pixel.Texture, _rect, Pixel.SourceRectangle, color);
    }

    #endregion Rectangle

    /***************************************************************************
    ***
    *** Hollow Rectangle
    ***
    ***************************************************************************/
    #region Hollow Rectangle

    /// <summary>
    ///     Draws a hollow rectangle.
    /// </summary>
    /// <param name="rectangle">
    ///     The rectangle to draw.
    /// </param>
    public static void HollowRectangle(Rectangle rectangle) =>
        HollowRectangle(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height, Color.White);

    /// <summary>
    ///     Draws a hollow rectangle.
    /// </summary>
    /// <param name="rectangle">
    ///     The rectangle to draw.
    /// </param>
    /// <param name="color">
    ///     The color mask to apply when rendering the rectangle.
    /// </param>
    public static void HollowRectangle(Rectangle rectangle, Color color) =>
        HollowRectangle(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height, color);

    /// <summary>
    ///     Draws a hollow rectangle.
    /// </summary>
    /// <param name="position">
    ///     The top-left xy-coordinate position of the rectangle.
    /// </param>
    /// <param name="width">
    ///     The width, in pixels, of the rectangle.
    /// </param>
    /// <param name="height">
    ///     The height, in pixels, of the rectangle.
    /// </param>
    public static void HollowRectangle(Vector2 position, float width, float height) =>
        HollowRectangle(position.X, position.X, width, height, Color.White);

    /// <summary>
    ///     Draws a hollow rectangle.
    /// </summary>
    /// <param name="position">
    ///     The top-left xy-coordinate position of the rectangle.
    /// </param>
    /// <param name="width">
    ///     The width, in pixels, of the rectangle.
    /// </param>
    /// <param name="height">
    ///     The height, in pixels, of the rectangle.
    /// </param>
    /// <param name="color">
    ///     The color mask to apply when rendering the rectangle.
    /// </param>
    public static void HollowRectangle(Vector2 position, float width, float height, Color color) =>
        HollowRectangle(position.X, position.Y, width, height, color);

    /// <summary>
    ///     Draws a hollow rectangle.
    /// </summary>
    /// <param name="x">
    ///     The top-left x-coordinate position of the rectangle.
    /// </param>
    /// <param name="y">
    ///     The top-left y-coordinate position of the rectangle.
    /// </param>
    /// <param name="width">
    ///     The width, in pixels, of the rectangle.
    /// </param>
    /// <param name="height">
    ///     The height, in pixels, of the rectangle.
    /// </param>
    public static void HollowRectangle(float x, float y, float width, float height) =>
        HollowRectangle(x, y, width, height, Color.White);

    /// <summary>
    ///     Draws a hollow rectangle.
    /// </summary>
    /// <param name="x">
    ///     The top-left x-coordinate position of the rectangle.
    /// </param>
    /// <param name="y">
    ///     The top-left y-coordinate position of the rectangle.
    /// </param>
    /// <param name="width">
    ///     The width, in pixels, of the rectangle.
    /// </param>
    /// <param name="height">
    ///     The height, in pixels, of the rectangle.
    /// </param>
    /// <param name="color">
    ///     The color mask to apply when rendering the rectangle.
    /// </param>
    public static void HollowRectangle(float x, float y, float width, float height, Color color)
    {
        Line(x, y, x + width, y, color, 1.0f);
        Line(x + width, y, x + width, y + height, color, 1.0f);
        Line(x + width, y + height, x, y + height, color, 1.0f);
        Line(x, y + height, x, y, color, 1.0f);
    }

    #endregion Hollow Rectangle

    /***************************************************************************
    ***
    *** Circle
    ***
    ***************************************************************************/
    #region Circle

    /// <summary>
    ///     Draws a circle.
    /// </summary>
    /// <param name="centerX">
    ///     The x-coordinate position of the center of the circle.
    /// </param>
    /// <param name="centerY">
    ///     The y-coordinate position of the center of the circle.
    /// </param>
    /// <param name="radius">
    ///     The radius, in pixels, of the circle.
    /// </param>
    /// <param name="resolution">
    ///     <para>
    ///         The resolution of the circle to draw.
    ///     </para>
    ///     <para>
    ///         Determines the number of line segments to draw.  A higher resolution will result in a smoother circle,
    ///         but will require more draw calls to perform.
    ///     </para>
    /// </param>
    public static void Circle(float centerX, float centerY, float radius, float resolution) =>
        Circle(centerX, centerY, radius, resolution, Color.White, 1.0f);

    /// <summary>
    ///     Draws a circle
    /// </summary>
    /// <param name="center">
    ///     The xy-coordinate position of the center of the circle.
    /// </param>
    /// <param name="radius">
    ///     The radius, in pixels, of the circle.
    /// </param>
    /// <param name="resolution">
    ///     <para>
    ///         The resolution of the circle to draw.
    ///     </para>
    ///     <para>
    ///         Determines the number of line segments to draw.  A higher resolution will result in a smoother circle,
    ///         but will require more draw calls to perform.
    ///     </para>
    /// </param>
    public static void Circle(Vector2 center, float radius, float resolution) =>
        Circle(center, radius, resolution, Color.White, 1.0f);

    /// <summary>
    ///     Draws a circle.
    /// </summary>
    /// <param name="centerX">
    ///     The x-coordinate position of the center of the circle.
    /// </param>
    /// <param name="centerY">
    ///     The y-coordinate position of the center of the circle.
    /// </param>
    /// <param name="radius">
    ///     The radius, in pixels, of the circle.
    /// </param>
    /// <param name="resolution">
    ///     <para>
    ///         The resolution of the circle to draw.
    ///     </para>
    ///     <para>
    ///         Determines the number of line segments to draw.  A higher resolution will result in a smoother circle,
    ///         but will require more draw calls to perform.
    ///     </para>
    /// </param>
    /// <param name="color">
    ///     The color mask to apply when rendering the circle.
    /// </param>
    public static void Circle(float centerX, float centerY, float radius, float resolution, Color color) =>
        Circle(centerX, centerY, radius, resolution, color, 1.0f);

    /// <summary>
    ///     Draws a circle
    /// </summary>
    /// <param name="center">
    ///     The xy-coordinate position of the center of the circle.
    /// </param>
    /// <param name="radius">
    ///     The radius, in pixels, of the circle.
    /// </param>
    /// <param name="resolution">
    ///     <para>
    ///         The resolution of the circle to draw.
    ///     </para>
    ///     <para>
    ///         Determines the number of line segments to draw.  A higher resolution will result in a smoother circle,
    ///         but will require more draw calls to perform.
    ///     </para>
    /// </param>
    /// <param name="color">
    ///     The color mask to apply when rendering the circle.
    /// </param>
    public static void Circle(Vector2 center, float radius, float resolution, Color color) =>
        Circle(center, radius, resolution, color, 1.0f);

    /// <summary>
    ///     Draws a circle.
    /// </summary>
    /// <param name="centerX">
    ///     The x-coordinate position of the center of the circle.
    /// </param>
    /// <param name="centerY">
    ///     The y-coordinate position of the center of the circle.
    /// </param>
    /// <param name="radius">
    ///     The radius, in pixels, of the circle.
    /// </param>
    /// <param name="resolution">
    ///     <para>
    ///         The resolution of the circle to draw.
    ///     </para>
    ///     <para>
    ///         Determines the number of line segments to draw.  A higher resolution will result in a smoother circle,
    ///         but will require more draw calls to perform.
    ///     </para>
    /// </param>
    /// <param name="color">
    ///     The color mask to apply when rendering the circle.
    /// </param>
    /// <param name="thickness">
    ///     The thickness, pixels, of the edge of the circle.
    /// </param>
    public static void Circle(float centerX, float centerY, float radius, float resolution, Color color, float thickness)
    {
        Vector2 center = new Vector2(centerX, centerY);
        Circle(center, radius, resolution, color, thickness);
    }

    /// <summary>
    ///     Draws a circle
    /// </summary>
    /// <param name="center">
    ///     The xy-coordinate position of the center of the circle.
    /// </param>
    /// <param name="radius">
    ///     The radius, in pixels, of the circle.
    /// </param>
    /// <param name="resolution">
    ///     <para>
    ///         The resolution of the circle to draw.
    ///     </para>
    ///     <para>
    ///         Determines the number of line segments to draw.  A higher resolution will result in a smoother circle,
    ///         but will require more draw calls to perform.
    ///     </para>
    /// </param>
    /// <param name="color">
    ///     The color mask to apply when rendering the circle.
    /// </param>
    /// <param name="thickness">
    ///     The thickness, pixels, of the edge of the circle.
    /// </param>
    public static void Circle(Vector2 center, float radius, float resolution, Color color, float thickness)
    {
        Vector2 last = Vector2.UnitX * radius;
        Vector2 lastPerpendicular = last.Perpendicular();

        for (int i = 0; i <= resolution; i++)
        {
            float angle = i * MathHelper.PiOver2 / resolution;
            Vector2 at = TurtleMath.AngleToVector2(angle, radius);
            Vector2 atPerpendicular = at.Perpendicular();

            Line(center + last, center + at, color, thickness);
            Line(center - last, center - at, color, thickness);
            Line(center + lastPerpendicular, center + atPerpendicular, color, thickness);
            Line(center - lastPerpendicular, center - atPerpendicular, color, thickness);

            last = at;
            lastPerpendicular = atPerpendicular;
        }
    }

    #endregion Circle
}
