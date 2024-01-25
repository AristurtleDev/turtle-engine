// Copyright (c) Christopher Whitley. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace Aristurtle.TurtleEngine;

/// <summary>
///     Represents a 2D size with 32-bit integer width and height component values.
/// </summary>
[DebuggerDisplay($"{{{nameof(DebugDisplayString)},nq}}")]
public struct Size : IEquatable<Size>
{
    internal string DebugDisplayString => $"(X:{Width}, Y:{Height})";

    private static readonly Size s_zero = new Size(0);

    /// <summary>
    ///     The width component of this <see cref="Size" />/
    /// </summary>
    public int Width;

    /// <summary>
    ///     The height component of this <see cref="Size" />/
    /// </summary>
    public int Height;

    /// <summary>
    ///     Gets a <see cref="Size"/> value with a <see cref="Width"/> and <see cref="Height"/> of <c>0</c>.
    /// </summary>
    public static Size Zero => s_zero;

    /// <summary>
    ///     Initializes a new <see cref="Size"/> value with the specified width and height.
    /// </summary>
    /// <param name="width">
    ///     The width component of the <see cref="Size"/> being created.
    /// </param>
    /// <param name="height">
    ///     THe height component of the <see cref="Size"/> being created.
    /// </param>
    public Size(int width, int height) => (Width, Height) = (width, height);

    /// <summary>
    ///     Initializes a new <see cref="Size"/> value with the width and height components set to the specified value.
    /// </summary>
    /// <param name="value">
    ///     The value to assign the width and height components of the <see cref="Size"/> being created.
    /// </param>
    public Size(int value) : this(value, value) { }

    /// <inheritdoc />
    public override readonly bool Equals([NotNullWhen(true)] object obj) => obj is Size other && Equals(other);

    /// <inheritdoc />
    public readonly bool Equals(Size other) => Width == other.Width && Height == other.Height;

    /// <inheritdoc />
    public override readonly int GetHashCode() => Width ^ Height;

    /// <summary>
    ///     <para>
    ///         Returns a string representation of this <see cref="Size"/> value in the following format:
    ///     </para>
    ///     <c>(X:Width, Y:height)</c>
    /// </summary>
    /// <returns>
    ///     The string representation of this <see cref="Size"/> value.
    /// </returns>
    public override string ToString() => DebugDisplayString;

    /// <summary>
    ///     Deconstruction method for this <see cref="Size"/> value.
    /// </summary>
    /// <param name="width">
    ///     When this method returns, will be equal to the <see cref="Width"/> component of this <see cref="Size"/>
    ///     value.
    /// </param>
    /// <param name="height">
    ///     When this method returns, will be equal to the <see cref="Height"/> component of this <see cref="Size"/>
    ///     value.
    /// </param>
    public readonly void Deconstruct(out int width, out int height) => (width, height) = (Width, Height);

    /// <inheritdoc />
    public static bool operator ==(Size a, Size b) => a.Equals(b);

    /// <inheritdoc />
    public static bool operator !=(Size a, Size b) => !a.Equals(b);

    /// <summary>
    ///     <para>
    ///         Returns a <see cref="Size"/> value by adding the <see cref="Width"/> and <see cref="Height"/> components
    ///         from <paramref name="b"/> to <paramref name="a"/>.
    ///     </para>
    ///     <c>new Size(a.Width + b.Width, a.Height + b.Height)</c>
    /// </summary>
    /// <param name="a">
    ///     The <see cref="Size"/> value that the <see cref="Width"/> and <see cref="Height"/> components of
    ///     <paramref name="b"/> will be added to.
    /// </param>
    /// <param name="b">
    ///     The <see cref="Size"/> value whos <see cref="Width"/> and <see cref="Height"/> components will be added to
    ///     <paramref name="a"/>.
    /// </param>
    /// <returns>
    ///     A new <see cref="Size"/> value that is the result of this addition operation.
    /// </returns>
    public static Size Add(Size a, Size b) => new Size(a.Width + b.Width, a.Height + b.Height);

    /// <summary>
    ///     <para>
    ///         Returns a <see cref="Size"/> value by subtracting the <see cref="Width"/> and <see cref="Height"/>
    ///         components of <paramref name="b"/> from <paramref name="a"/>.
    ///     </para>
    ///     <c>new Size(a.Width - b.Width, a.Height - b.Height)</c>
    /// </summary>
    /// <param name="a">
    ///     The <see cref="Size"/> value that the <see cref="Width"/> and <see cref="Height"/> components of
    ///     <paramref name="b"/> will be subtracted from.
    /// </param>
    /// <param name="b">
    ///     The <see cref="Size"/> value whos <see cref="Width"/> and <see cref="Height"/> components will be subtracted
    ///     from <paramref name="a"/>.
    /// </param>
    /// <returns>
    ///     A new <see cref="Size"/> value that is the result of this subtraction operation.
    /// </returns>
    public static Size Subtract(Size a, Size b) => new Size(a.Width - b.Width, a.Height - b.Height);

    /// <summary>
    ///     <para>
    ///         Returns a <see cref="Size"/> value by multiplying the <see cref="Width"/> and <see cref="Height"/>
    ///         components of <paramref name="b"/> to <paramref name="a"/>.
    ///     </para>
    ///     <c>new Size(a.Width * b.Width, a.Height * b.Height)</c>
    /// </summary>
    /// <param name="a">
    ///     The <see cref="Size"/> value that the <see cref="Width"/> and <see cref="Height"/> components of
    ///     <paramref name="b"/> will be multiplied to.
    /// </param>
    /// <param name="b">
    ///     The <see cref="Size"/> value whos <see cref="Width"/> and <see cref="Height"/> components will be multiplied
    ///     to <paramref name="a"/>.
    /// </param>
    /// <returns>
    ///     A new <see cref="Size"/> value that is the result of this multiplication operation.
    /// </returns>
    public static Size Multiply(Size a, Size b) => new Size(a.Width * b.Width, a.Height * b.Height);

    /// <summary>
    ///     <para>
    ///         Returns a <see cref="Size"/> value by dividing the <see cref="Width"/> and <see cref="Height"/>
    ///         components of <paramref name="b"/> from <paramref name="a"/>.
    ///     </para>
    ///     <c>new Size(a.Width / b.Width, a.Height / b.Height)</c>
    /// </summary>
    /// <param name="a">
    ///     The <see cref="Size"/> value that the <see cref="Width"/> and <see cref="Height"/> components of
    ///     <paramref name="b"/> will be divided from.
    /// </param>
    /// <param name="b">
    ///     The <see cref="Size"/> value whos <see cref="Width"/> and <see cref="Height"/> components will be divided
    ///     from <paramref name="a"/>.
    /// </param>
    /// <returns>
    ///     A new <see cref="Size"/> value that is the result of this division operation.
    /// </returns>
    /// <exception cref="DivideByZeroException">
    ///     Thrown if the <see cref="Width"/> and/or <see cref="Height"/> component of <paramref name="b"/> are equal
    ///     to zero.
    /// </exception>
    public static Size Divide(Size a, Size b) => new Size(a.Width / b.Width, a.Height / b.Height);

    /// <summary>
    ///     <para>
    ///         Returns a <see cref="Size"/> value by adding the <see cref="Width"/> and <see cref="Height"/> components
    ///         from <paramref name="b"/> to <paramref name="a"/>.
    ///     </para>
    ///     <c>new Size(a.Width + b.Width, a.Height + b.Height)</c>
    /// </summary>
    /// <param name="a">
    ///     The <see cref="Size"/> value that the <see cref="Width"/> and <see cref="Height"/> components of
    ///     <paramref name="b"/> will be added to.
    /// </param>
    /// <param name="b">
    ///     The <see cref="Size"/> value whos <see cref="Width"/> and <see cref="Height"/> components will be added to
    ///     <paramref name="a"/>.
    /// </param>
    /// <returns>
    ///     A new <see cref="Size"/> value that is the result of this addition operation.
    /// </returns>
    public static Size operator +(Size a, Size b) => Add(a, b);

    /// <summary>
    ///     <para>
    ///         Returns a <see cref="Size"/> value by subtracting the <see cref="Width"/> and <see cref="Height"/>
    ///         components of <paramref name="b"/> from <paramref name="a"/>.
    ///     </para>
    ///     <c>new Size(a.Width - b.Width, a.Height - b.Height)</c>
    /// </summary>
    /// <param name="a">
    ///     The <see cref="Size"/> value that the <see cref="Width"/> and <see cref="Height"/> components of
    ///     <paramref name="b"/> will be subtracted from.
    /// </param>
    /// <param name="b">
    ///     The <see cref="Size"/> value whos <see cref="Width"/> and <see cref="Height"/> components will be subtracted
    ///     from <paramref name="a"/>.
    /// </param>
    /// <returns>
    ///     A new <see cref="Size"/> value that is the result of this subtraction operation.
    /// </returns>
    public static Size operator -(Size a, Size b) => Subtract(a, b);

    /// <summary>
    ///     <para>
    ///         Returns a <see cref="Size"/> value by multiplying the <see cref="Width"/> and <see cref="Height"/>
    ///         components of <paramref name="b"/> to <paramref name="a"/>.
    ///     </para>
    ///     <c>new Size(a.Width * b.Width, a.Height * b.Height)</c>
    /// </summary>
    /// <param name="a">
    ///     The <see cref="Size"/> value that the <see cref="Width"/> and <see cref="Height"/> components of
    ///     <paramref name="b"/> will be multiplied to.
    /// </param>
    /// <param name="b">
    ///     The <see cref="Size"/> value whos <see cref="Width"/> and <see cref="Height"/> components will be multiplied
    ///     to <paramref name="a"/>.
    /// </param>
    /// <returns>
    ///     A new <see cref="Size"/> value that is the result of this multiplication operation.
    /// </returns>
    public static Size operator *(Size a, Size b) => Multiply(a, b);

    /// <summary>
    ///     <para>
    ///         Returns a <see cref="Size"/> value by dividing the <see cref="Width"/> and <see cref="Height"/>
    ///         components of <paramref name="b"/> from <paramref name="a"/>.
    ///     </para>
    ///     <c>new Size(a.Width / b.Width, a.Height / b.Height)</c>
    /// </summary>
    /// <param name="a">
    ///     The <see cref="Size"/> value that the <see cref="Width"/> and <see cref="Height"/> components of
    ///     <paramref name="b"/> will be divided from.
    /// </param>
    /// <param name="b">
    ///     The <see cref="Size"/> value whos <see cref="Width"/> and <see cref="Height"/> components will be divided
    ///     from <paramref name="a"/>.
    /// </param>
    /// <returns>
    ///     A new <see cref="Size"/> value that is the result of this division operation.
    /// </returns>
    /// <exception cref="DivideByZeroException">
    ///     Thrown if the <see cref="Width"/> and/or <see cref="Height"/> component of <paramref name="b"/> are equal
    ///     to zero.
    /// </exception>
    public static Size operator /(Size a, Size b) => Divide(a, b);

}
