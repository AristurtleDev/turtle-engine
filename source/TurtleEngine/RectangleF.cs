 using System.Diagnostics;
using Microsoft.Xna.Framework;

namespace TurtleEngine;

[DebuggerDisplay("{DebugDisplayString,nq}")]
public struct RectangleF : IEquatable<RectangleF>
{
    private static RectangleF empty = new RectangleF();

    /// <summary>
    ///     The x-coordinate of the top-left corner of this <see cref="RectangleF"/>
    /// </summary>
    public float X;

    /// <summary>
    ///     The y-coordinate of the top-left corner of this <see cref="RectangleF"/>
    /// </summary>
    public float Y;

    /// <summary>
    ///     The width of this <see cref="RectangleF"/>.
    /// </summary>
    public float Width;

    /// <summary>
    ///     The height of this <see cref="RectangleF"/>.
    /// </summary>
    public float Height;

    /// <summary>
    ///     Gets a <see cref="RectangleF"/> where <see cref="RectangleF.X"/> = 0, <see cref="RectangleF.Y"/> = 0, 
    ///     <see cref="RectangleF.Width"/> = 0, and <see cref="RectangleF.Height"/> = 0.
    /// </summary>
    public static RectangleF Empty => empty;

    /// <summary>
    ///     Gets the x-coordinate of the left edge of this <see cref="RectangleF"/>.
    /// </summary>
    public float Left => X;

    /// <summary>
    ///     Gets the x-coordinate of the right edge of this <see cref="RectangleF"/>.
    /// </summary>
    public float Right => X + Width;

    /// <summary>
    ///     Gets the y-coordinate of the top edge of this <see cref="RectangleF"/>.
    /// </summary>
    public float Top => Y;

    /// <summary>
    ///     Gets the y-coordinate of the bottom edge of this <see cref="RectangleF"/>.
    /// </summary>
    public float Bottom => Y + Height;

    /// <summary>
    ///     Gets a value that indicates whether the <see cref="X"/>, <see cref="Y"/>, <see cref="Width"/>, and
    ///     <see cref="Height"/> components of this <see cref="RectangleF"/> are all equal to 0.
    /// </summary>
    public bool IsEmpty => X == 0 && Y == 0 && Width == 0 && Height == 0;

    /// <summary>
    ///     Gets or Sets the top-left coordinate of this <see cref="RectangleF"/>.
    /// </summary>
    public Vector2 Location
    {
        get => new(X, Y);
        set => (X, Y) = (value.X, value.Y);
    }

    /// <summary>
    ///     Gets or Sets the width and height of this <see cref="RectangleF"/>.
    /// </summary>
    public Vector2 Size
    {
        get => new(X, Y);
        set => (Width, Height) = (value.X, value.Y);
    }

    /// <summary>
    ///     Gets the center coordinate of this <see cref="RectangleF"/>.
    /// </summary>
    public Vector2 Center => new(X + (Width / 2.0f), Y + (Height / 2.0f));

    internal string DebugDisplayString => $"({X}, {Y}, {Width}, {Height})";

    /// <summary>
    ///     Initializes a new instance of the <see cref="RectangleF"/> struct with the specified xy-coordinate position
    ///     and width and height extents.
    /// </summary>
    /// <param name="x">
    ///     The x-coordinate of the top-left corner of the <see cref="RectangleF"/> being created.
    /// </param>
    /// <param name="y">
    ///     The y-coordinate of the top-left corner of the <see cref="RectangleF"/> being created.
    /// </param>
    /// <param name="width">
    ///     The width of the <see cref="RectangleF"/> being created.
    /// </param>
    /// <param name="height">
    ///     The height of the <see cref="RectangleF"/> being created.
    /// </param>
    public RectangleF(float x, float y, float width, float height) => (X, Y, Width, Height) = (x, y, width, height);

    /// <summary>
    ///     Initializes a new instance of the <see cref="RectangleF"/> struct with the specified location and size.
    /// </summary>
    /// <param name="location">
    ///     The x- and y-coordinates of the top-left corner of the <see cref="RectangleF"/> being created.
    /// </param>
    /// <param name="size">
    ///     The width and height extents of the <see cref="RectangleF"/> being created.
    /// </param>
    public RectangleF(Vector2 location, Vector2 size) => (X, Y, Width, Height) = (location.X, location.Y, size.X, size.Y);

    /// <summary>
    ///     Returns a value that indicates whether two <see cref="RectangleF"/> instances are equal.
    /// </summary>
    /// <param name="a">
    ///     The <see cref="RectangleF"/> instance on the left of the equal sign.
    /// </param>
    /// <param name="b">
    ///     The <see cref="RectangleF"/> instance on the right of the equal sign.
    /// </param>
    /// <returns>
    ///     <see langword="true"/> if the two <see cref="RectangleF"/> instances are equal; otherwise, 
    ///     <see langword="false"/>.
    /// </returns>
    public static bool operator ==(RectangleF a, RectangleF b) => a.X == b.X &&
                                                                  a.Y == b.Y &&
                                                                  a.Width == b.Width &&
                                                                  a.Height == b.Height;

    /// <summary>
    ///     Returns a value that indicates whether two <see cref="RectangleF"/> instances are not equal.
    /// </summary>
    /// <param name="a">
    ///     The <see cref="RectangleF"/> instance on the left of the not equal sign.
    /// </param>
    /// <param name="b">
    ///     The <see cref="RectangleF"/> instance on the right of the not equal sign.
    /// </param>
    /// <returns>
    ///     <see langword="true"/> if the two <see cref="RectangleF"/> instances are not equal; otherwise,
    ///     <see langword="false"/>.
    public static bool operator !=(RectangleF a, RectangleF b) => !(a == b);


    /// <summary>
    ///     Returns a value that indicates whether the specified xy-coordinate location lies within the bounds of this
    ///     <see cref="RectangleF"/> instance.
    /// </summary>
    /// <param name="x">
    ///     The x-coordinate of the location to check.
    /// </param>
    /// <param name="y">
    ///     The y-coordinate of the location to check.
    /// </param>
    /// <returns>
    ///     <see langword="true"/> if the specified xy-coordinate location lies within the bounds of this
    ///     <see cref="RectangleF"/> instance; otherwise, <see langword="false"/>.
    /// </returns>
    public bool Contains(int x, int y) => X <= x &&
                                          x < (X + Width) &&
                                          Y <= y &&
                                          y < (Y + Height);

    /// <summary>
    ///     Returns a value that indicates whether the specified xy-coordinate location lies within the bounds of this
    ///     <see cref="RectangleF"/> instance.
    /// </summary>
    /// <param name="x">
    ///     The x-coordinate of the location to check.
    /// </param>
    /// <param name="y">
    ///     The y-coordinate of the location to check.
    /// </param>
    /// <returns>
    ///     <see langword="true"/> if the specified xy-coordinate location lies within the bounds of this
    ///     <see cref="RectangleF"/> instance; otherwise, <see langword="false"/>.
    /// </returns>
    public bool Contains(float x, float y) => X <= x &&
                                              x < (X + Width) &&
                                              Y <= y &&
                                              y < (Y + Height);

    /// <summary>
    ///     Returns a value that indicates whether the specified <see cref="Point"/> value lies within the bounds of
    ///     this <see cref="RectangleF"/> instance.
    /// </summary>
    /// <param name="value">
    ///     The <see cref="Point"/> value to check.
    /// </param>
    /// <returns>
    ///     <see langword="true"/> if the specified <see cref="Point"/> value lies within the bounds of this
    ///     <see cref="RectangleF"/>; otherwise, <see langword="false"/>.
    /// </returns>
    public bool Contains(Point value) => X <= value.X &&
                                         value.X < (X + Width) &&
                                         Y <= value.Y &&
                                         value.Y < (Y + Height);

    /// <summary>
    ///     Gets whether the specified <see cref="Point"/> value lies within the bounds of this <see cref="RectangleF"/>
    ///     instance.
    /// </summary>
    /// <param name="value">
    ///     The <see cref="Point"/> value to check.
    /// </param>
    /// <param name="result">
    ///     When this method returns, will be <see langword="true"/> if the specified <see cref="Point"/> value lies
    ///     within the bounds of this <see cref="RectangleF"/> instance; otherwise, <see langword="false"/>.
    /// </param>
    public void Contains(ref Point value, out bool result) => result = X <= value.X &&
                                                                       value.X < (X + Width) &&
                                                                       Y <= value.Y &&
                                                                       value.Y < (Y + Height);

    /// <summary>
    ///     Returns a value that indicates whether the specified <see cref="Vector2"/> value lies within the bounds of
    ///     this <see cref="RectangleF"/> instance.
    /// </summary>
    /// <param name="value">
    ///     The <see cref="Vector2"/> value to check.
    /// </param>
    /// <returns>
    ///     <see langword="true"/> if the specified <see cref="Vector2"/> value lies within the bounds of this
    ///     <see cref="RectangleF"/>; otherwise, <see langword="false"/>.
    /// </returns>
    public bool Contains(Vector2 value) => X <= value.X &&
                                           value.X < (X + Width) &&
                                           Y <= value.Y &&
                                           value.Y < (Y + Height);

    /// <summary>
    ///     Gets whether the specified <see cref="Vector2"/> value lies within the bounds of this 
    ///     <see cref="RectangleF"/> instance.
    /// </summary>
    /// <param name="value">
    ///     The <see cref="Vector2"/> value to check.
    /// </param>
    /// <param name="result">
    ///     When this method returns, will be <see langword="true"/> if the specified <see cref="Vector2"/> value lies
    ///     within the bounds of this <see cref="RectangleF"/> instance; otherwise, <see langword="false"/>.
    /// </param>
    public void Contains(ref Vector2 value, out bool result) => result = X <= value.X &&
                                                                         value.X < (X + Width) &&
                                                                         Y <= value.Y &&
                                                                         value.Y < (Y + Height);

    /// <summary>
    ///     Returns a value that indicates whether the specified <see cref="Rectangle"/> value lies within the bounds of
    ///     this <see cref="RectangleF"/> instance.
    /// </summary>
    /// <param name="value">
    ///     The <see cref="Rectangle"/> value to check.
    /// </param>
    /// <returns>
    ///     <see langword="true"/> if the specified <see cref="Rectangle"/> value lies within the bounds of this
    ///     <see cref="RectangleF"/>; otherwise, <see langword="false"/>.
    /// </returns>
    public bool Contains(Rectangle value) => X <= value.X &&
                                             value.X + value.Width <= X + Width &&
                                             Y <= value.Y &&
                                             value.Y + value.Height <= Y + Height;

    /// <summary>
    ///     Gets whether the specified <see cref="Rectangle"/> value lies within the bounds of this 
    ///     <see cref="RectangleF"/> instance.
    /// </summary>
    /// <param name="value">
    ///     The <see cref="Rectangle"/> value to check.
    /// </param>
    /// <param name="result">
    ///     When this method returns, will be <see langword="true"/> if the specified <see cref="Rectangle"/> value lies
    ///     within the bounds of this <see cref="RectangleF"/> instance; otherwise, <see langword="false"/>.
    /// </param>
    public bool Contains(ref Rectangle value, out bool result) => result = X <= value.X &&
                                                                           value.X + value.Width <= X + Width &&
                                                                           Y <= value.Y &&
                                                                           value.Y + value.Height <= Y + Height;

    /// <summary>
    ///     Returns a value that indicates whether the specified <see cref="RectangleF"/> value lies within the bounds 
    ///     of this <see cref="RectangleF"/> instance.
    /// </summary>
    /// <param name="value">
    ///     The <see cref="Rectangle"/> value to check.
    /// </param>
    /// <returns>
    ///     <see langword="true"/> if the specified <see cref="RectangleF"/> value lies within the bounds of this
    ///     <see cref="RectangleF"/>; otherwise, <see langword="false"/>.
    /// </returns>
    public bool Contains(RectangleF value) => X <= value.X &&
                                             value.X + value.Width <= X + Width &&
                                             Y <= value.Y &&
                                             value.Y + value.Height <= Y + Height;

    /// <summary>
    ///     Gets whether the specified <see cref="RectangleF"/> value lies within the bounds of this 
    ///     <see cref="RectangleF"/> instance.
    /// </summary>
    /// <param name="value">
    ///     The <see cref="RectangleF"/> value to check.
    /// </param>
    /// <param name="result">
    ///     When this method returns, will be <see langword="true"/> if the specified <see cref="RectangleF"/> value 
    ///     lies within the bounds of this <see cref="RectangleF"/> instance; otherwise, <see langword="false"/>.
    /// </param>
    public bool Contains(ref RectangleF value, out bool result) => result = X <= value.X &&
                                                                            value.X + value.Width <= X + Width &&
                                                                            Y <= value.Y &&
                                                                            value.Y + value.Height <= Y + Height;

    /// <summary>
    ///     Returns a value that indicates whether this <see cref="RectangleF"/> instance is equal to the specified
    ///     <see cref="object"/>.
    /// </summary>
    /// <param name="obj">
    ///     The <see cref="object"/> to compare for equality with this <see cref="RectangleF"/> instance.
    /// </param>
    /// <returns>
    ///     <see langword="true"/> if the specified <see cref="object"/> is equal to this <see cref="RectangleF"/>
    ///     instance; otherwise, <see langword="false"/>.
    /// </returns>
    public override bool Equals(object obj) => obj is RectangleF other && Equals(other);

    /// <summary>
    ///     Returns a value that indicates whether this <see cref="RectangleF"/> instance is equal to the specified
    ///     <see cref="RectangleF"/> instance.
    /// </summary>
    /// <param name="other">
    ///     The <see cref="RectangleF"/> to compare for equality with this <see cref="RectangleF"/> instance.
    /// </param>
    /// <returns>
    ///     <see langword="true"/> if the specified <see cref="RectangleF"/> is equal to this <see cref="RectangleF"/>
    ///     instance; otherwise, <see langword="false"/>.
    /// </returns>
    public bool Equals(RectangleF other) => this == other;

    /// <summary>
    ///     Returns the hash code for this <see cref="RectangleF"/> instance.
    /// </summary>
    /// <returns>
    ///     The 32-bit signed integer hash code for this <see cref="RectangleF"/> instance.
    /// </returns>
    public override int GetHashCode() => (int)X ^ (int)Y ^ (int)Width ^ (int)Height;

    /// <summary>
    ///     Expands the edges of this <see cref="RectangleF"/> by the specified horizontal and vertical amounts.
    /// </summary>
    /// <param name="horizontalAmount">
    ///     The amount of adjust the left and right edges by.
    /// </param>
    /// <param name="verticalAmount">
    ///     The amount to adjust the top and bottom edges by.
    /// </param>
    public void Inflate(int horizontalAmount, int verticalAmount)
    {
        X -= horizontalAmount;
        Y -= verticalAmount;
        Width += horizontalAmount * 2;
        Height += verticalAmount * 2;
    }

    /// <summary>
    ///     Expands the edges of this <see cref="RectangleF"/> by the specified horizontal and vertical amounts.
    /// </summary>
    /// <param name="horizontalAmount">
    ///     The amount of adjust the left and right edges by.
    /// </param>
    /// <param name="verticalAmount">
    ///     The amount to adjust the top and bottom edges by.
    /// </param>
    public void Inflate(float horizontalAmount, float verticalAmount)
    {
        X -= horizontalAmount;
        Y -= verticalAmount;
        Width += horizontalAmount * 2.0f;
        Height += verticalAmount * 2.0f;
    }

    /// <summary>
    ///     Returns a value that indicates whether the specified <see cref="Rectangle"/> intersects with this
    ///     <see cref="RectangleF"/>.
    /// </summary>
    /// <param name="value">
    ///     The <see cref="Rectangle"/> to check.
    /// </param>
    /// <returns>
    ///     <see langword="true"/> if the specified <see cref="Rectangle"/> intersects with this
    ///     <see cref="RectangleF"/>; otherwise, <see langword="false"/>.
    /// </returns>
    public bool Intersects(Rectangle value) => value.Left < Right &&
                                               Left < value.Right &&
                                               value.Top < Bottom &&
                                               Top < value.Bottom;

    /// <summary>
    ///     Returns a value that indicates whether the specified <see cref="Rectangle"/> intersects with this
    ///     <see cref="RectangleF"/>.
    /// </summary>
    /// <param name="value">
    ///     The <see cref="Rectangle"/> to check.
    /// </param>
    /// <param name="result">
    ///     When this method returns, will be <see langword="true"/> if the specified <see cref="Rectangle"/> 
    ///     intersects with this <see cref="RectangleF"/>; otherwise, <see langword="false"/>.
    /// </param>
    public void Intersects(ref Rectangle value, out bool result) => result = value.Left < Right &&
                                                                             Left < value.Right &&
                                                                             value.Top < Bottom &&
                                                                             Top < value.Bottom;

    /// <summary>
    ///     Returns a value that indicates whether the specified <see cref="RectangleF"/> intersects with this
    ///     <see cref="RectangleF"/>.
    /// </summary>
    /// <param name="value">
    ///     The <see cref="RectangleF"/> to check.
    /// </param>
    /// <returns>
    ///     <see langword="true"/> if the specified <see cref="RectangleF"/> intersects with this
    ///     <see cref="RectangleF"/>; otherwise, <see langword="false"/>.
    /// </returns>
    public bool Intersects(RectangleF value) => value.Left < Right &&
                                                Left < value.Right &&
                                                value.Top < Bottom &&
                                                Top < value.Bottom;

    /// <summary>
    ///     Returns a value that indicates whether the specified <see cref="RectangleF"/> intersects with this
    ///     <see cref="RectangleF"/>.
    /// </summary>
    /// <param name="value">
    ///     The <see cref="RectangleF"/> to check.
    /// </param>
    /// <param name="result">
    ///     When this method returns, will be <see langword="true"/> if the specified <see cref="RectangleF"/> 
    ///     intersects with this <see cref="RectangleF"/>; otherwise, <see langword="false"/>.
    /// </param>
    public void Intersects(ref RectangleF value, out bool result) => result = value.Left < Right &&
                                                                              Left < value.Right &&
                                                                              value.Top < Bottom &&
                                                                              Top < value.Bottom;

    /// <summary>
    ///     Returns a new <see cref="RectangleF"/> that contains the overlapping region of the two specified 
    ///     <see cref="RectangleF"/> instances.
    /// </summary>
    /// <param name="value1">
    ///     The first <see cref="RectangleF"/>.
    /// </param>
    /// <param name="value2">
    ///     The second <see cref="RectangleF"/>.
    /// </param>
    /// <returns>
    ///     A new <see cref="RectangleF"/> that represents the overlapping region of the two specified
    ///     <see cref="RectangleF"/> instances, if they overlap; otherwise, <see cref="RectangleF.Empty"/>.
    /// </returns>
    public static RectangleF Intersect(RectangleF value1, RectangleF value2)
    {
        RectangleF result;
        Intersect(ref value1, ref value2, out result);
        return result;
    }

    /// <summary>
    ///     Returns a new <see cref="RectangleF"/> that contains the overlapping region of the two specified 
    ///     <see cref="RectangleF"/> instances.
    /// </summary>
    /// <param name="value1">
    ///     The first <see cref="RectangleF"/>.
    /// </param>
    /// <param name="value2">
    ///     The second <see cref="RectangleF"/>.
    /// </param>
    /// <param name="result">
    ///     When this method returns, contains the new <see cref="RectangleF"/> that represents the overlapping region
    ///     of the two specified <see cref="RectangleF"/> instances, if they overlap; otherwise, 
    ///     <see cref="RectangleF.Empty"/>.
    /// </param>
    public static void Intersect(ref RectangleF value1, ref RectangleF value2, out RectangleF result)
    {
        if (value1.Intersects(value2))
        {
            float right_side = Math.Min(value1.X + value1.Width, value2.X + value2.Width);
            float left_side = Math.Max(value1.X, value2.X);
            float top_side = Math.Max(value1.Y, value2.Y);
            float bottom_side = Math.Min(value1.Y + value1.Height, value2.Y + value2.Height);
            result = new RectangleF(left_side, top_side, right_side - left_side, bottom_side - top_side);
        }
        else
        {
            result = new RectangleF(0, 0, 0, 0);
        }
    }

    /// <summary>
    ///     Changes the x- and y-coordinate location of this <see cref="RectangleF"/> by the specified amounts.
    /// </summary>
    /// <param name="offsetX">
    ///     The amount to offset the x-coordinate location of this <see cref="RectangleF"/>.
    /// </param>
    /// <param name="offsetY">
    ///     The amount to offset the y-coordinate location of this <see cref="RectangleF"/>.
    /// </param>
    public void Offset(int offsetX, int offsetY)
    {
        X += offsetX;
        Y += offsetY;
    }

    /// <summary>
    ///     Changes the x- and y-coordinate location of this <see cref="RectangleF"/> by the specified amounts.
    /// </summary>
    /// <param name="offsetX">
    ///     The amount to offset the x-coordinate location of this <see cref="RectangleF"/>.
    /// </param>
    /// <param name="offsetY">
    ///     The amount to offset the y-coordinate location of this <see cref="RectangleF"/>.
    /// </param>
    public void Offset(float offsetX, float offsetY)
    {
        X += offsetX;
        Y += offsetY;
    }

    /// <summary>
    ///     Changes the x- and y-coordinate location of this <see cref="RectangleF"/> by the specified amount.
    /// </summary>
    /// <param name="amount">
    ///     The amount to offset the x- and y-coordinate location of this <see cref="RectangleF"/>.
    ///</param>
    public void Offset(Point amount)
    {
        X += amount.X;
        Y += amount.Y;
    }

    /// <summary>
    ///     Changes the x- and y-coordinate location of this <see cref="RectangleF"/> by the specified amount.
    /// </summary>
    /// <param name="amount">
    ///     The amount to offset the x- and y-coordinate location of this <see cref="RectangleF"/>.
    ///</param>
    public void Offset(Vector2 amount)
    {
        X += amount.X;
        Y += amount.Y;
    }

    /// <summary>
    ///     Returns a <see cref="string"/> representation of this <see cref="RectangleF"/> in the format:
    ///     {X:[<see cref="X"/>], Y:[<see cref="Y"/>], Width:[<see cref="Width"/>], Height:[<see cref="Height"/>]}
    /// </summary>
    /// <returns>
    ///     The <see cref="string"/> representation of this <see cref="RectangleF"/>.
    /// </returns>
    public override string ToString() => $"{{X:{X}, Y:{Y}, Width:{Width}, Height:{Height}}}";

    /// <summary>
    ///     Creates a new <see cref="RectangleF"/> that completely contains the specified <see cref="RectangleF"/> 
    ///     instances.
    /// </summary>
    /// <param name="value1">
    ///     The first <see cref="RectangleF"/>.
    /// </param>
    /// <param name="value2">
    ///     The second <see cref="RectangleF"/>.
    /// </param>
    /// <returns>
    ///     A new <see cref="RectangleF"/> that completely contains the specified <see cref="RectangleF"/> 
    ///     instances.
    /// </returns>
    public static RectangleF Union(RectangleF value1, RectangleF value2)
    {
        float x = Math.Min(value1.X, value2.X);
        float y = Math.Min(value1.Y, value2.Y);
        return new(x, y, Math.Max(value1.Right, value2.Right) - x, Math.Max(value1.Bottom, value2.Bottom) - y);
    }

    /// <summary>
    ///     Creates a new <see cref="RectangleF"/> that completely contains the specified <see cref="RectangleF"/> 
    ///     instances.
    /// </summary>
    /// <param name="value1">
    ///     The first <see cref="RectangleF"/>.
    /// </param>
    /// <param name="value2">
    ///     The second <see cref="RectangleF"/>.
    /// </param>
    /// <param name="result">
    ///     When this method returns, contains a new <see cref="RectangleF"/> that completely contains the specified 
    ///     <see cref="RectangleF"/>.
    /// </param>
    public static void Union(ref RectangleF value1, ref RectangleF value2, out RectangleF result)
    {
        result.X = Math.Min(value1.X, value2.X);
        result.Y = Math.Min(value1.Y, value2.Y);
        result.Width = Math.Max(value1.Right, value2.Right) - result.X;
        result.Height = Math.Max(value1.Bottom, value2.Bottom) - result.Y;
    }

    /// <summary>
    ///     Deconstruction method for <see cref="RectangleF"/>.
    /// </summary>
    /// <param name="x">
    ///     When this method returns, contains the <see cref="X"/> value of this <see cref="RectangleF"/> instance.
    /// </param>
    /// <param name="y">
    ///     When this method returns, contains the <see cref="Y"/> value of this <see cref="RectangleF"/> instance.
    /// </param>
    /// <param name="width">
    ///     When this method returns, contains the <see cref="Width"/> value of this <see cref="RectangleF"/> instance.
    /// </param>
    /// <param name="height">
    ///     When this method returns, contains the <see cref="Height"/> value of this <see cref="RectangleF"/> instance.
    /// </param>
    public void Deconstruct(out float x, out float y, out float width, out float height)
    {
        x = X;
        y = Y;
        width = Width;
        height = Height;
    }
}