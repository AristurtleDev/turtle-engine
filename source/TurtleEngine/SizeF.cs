using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Microsoft.Xna.Framework
{
    /// <summary>
    ///     Describes a 2D size with single-precision floating-point component values. 
    ///     Describes a 2D SizeF.
    /// </summary>
    [DebuggerDisplay("{DebugDisplayString,nq}")]
    public struct SizeF : IEquatable<SizeF>
    {
        private static readonly SizeF _zero = new();

        /// <summary>
        ///     The width of this <see cref="SizeF"/>.
        /// </summary>
        public float Width;

        /// <summary>
        ///     The height of this <see cref="SizeF"/>.
        /// </summary>
        public float Height;

        /// <summary>
        ///     Gets a <see cref="SizeF"/> value with <see cref="Width"/> and <see cref="Height"/> equal to 0.0f
        /// </summary>
        public static SizeF Zero => _zero;

        internal string DebugDisplayString => $"({Width}, {Height})";

        /// <summary>
        ///     Initializes a new instance of the <see cref="SizeF"/> struct with the specified width and height.
        /// </summary>
        /// <param name="width">
        ///     The width of the <see cref="SizeF"/> being created.
        /// </param>
        /// <param name="height">
        ///     The height of the <see cref="SizeF"/> being created.
        /// </param>
        public SizeF(int width, int height) => (Width, Height) = (width, height);

        /// <summary>
        ///     Initializes a new instance of the <see cref="SizeF"/> struct with the specified width and height.
        /// </summary>
        /// <param name="width">
        ///     The width of the <see cref="SizeF"/> being created.
        /// </param>
        /// <param name="height">
        ///     The height of the <see cref="SizeF"/> being created.
        /// </param>
        public SizeF(float width, float height) => (Width, Height) = (width, height);

        /// <summary>
        ///     Initializes a new instance of the <see cref="SizeF"/> struct with the width and height equal to the 
        ///     specified value.
        /// </summary>
        /// <param name="value">
        ///     The value to set the width and height of the <see cref="SizeF"/> being created.
        /// </param>
        public SizeF(int value) => (Width, Height) = (value, value);

        /// <summary>
        ///     Initializes a new instance of the <see cref="SizeF"/> struct with the width and height equal to the 
        ///     specified value.
        /// </summary>
        /// <param name="value">
        ///     The value to set the width and height of the <see cref="SizeF"/> being created.
        /// </param>
        public SizeF(float value) => (Width, Height) = (value, value);

        /// <summary>
        ///     Returns the sum of the two specified <see cref="SizeF"/> values.
        /// </summary>
        /// <param name="a">
        ///     The <see cref="SizeF"/> value on the left side of the addition sign.
        /// </param>
        /// <param name="b">
        ///     The <see cref="SizeF"/> value on the right side of the addition sign.
        /// </param>
        /// <returns>
        ///     The sum of the two specified <see cref="SizeF"/> values.
        /// </returns>
        public static SizeF operator +(SizeF a, SizeF b) => new(a.Width + b.Width, a.Height + b.Height);

        /// <summary>
        ///     Returns the difference of the two specified <see cref="SizeF"/> values.
        /// </summary>
        /// <param name="a">
        ///     The <see cref="SizeF"/> value on the left side of the subtraction sign.
        /// </param>
        /// <param name="b">
        ///     The <see cref="SizeF"/> value on the right side of the subtraction sign.
        /// </param>
        /// <returns>
        ///     The difference of the two specified <see cref="SizeF"/> values.
        /// </returns>
        public static SizeF operator -(SizeF a, SizeF b) => new(a.Width - b.Width, a.Height - b.Height);

        /// <summary>
        ///     Returns the result of multiplying the components of the two specified <see cref="SizeF"/> values.
        /// </summary>
        /// <param name="a">
        ///     The <see cref="SizeF"/> value on the left side of the multiplication sign.
        /// </param>
        /// <param name="b">
        ///     The <see cref="SizeF"/> value on the right side of the multiplication sign.
        /// </param>
        /// <returns>
        ///     The result of multiplying the components of the two specified <see cref="SizeF"/> values.
        /// </returns>
        public static SizeF operator *(SizeF a, SizeF b) => new(a.Width * b.Width, a.Height * b.Height);

        /// <summary>
        ///     Returns the result of dividing the components of the two specified <see cref="SizeF"/> values.
        /// </summary>
        /// <param name="a">
        ///     The <see cref="SizeF"/> value on the left side of the division sign.
        /// </param>
        /// <param name="b">
        ///     The <see cref="SizeF"/> value on the right side of the division sign.
        /// </param>
        /// <returns>
        ///     The result of dividing the components of the two specified <see cref="SizeF"/> values.
        /// </returns>
        public static SizeF operator /(SizeF a, SizeF b) => new(a.Width / b.Width, a.Height / b.Height);

        /// <summary>
        ///     Returns a value that indicates whether the two specified <see cref="SizeF"/> values are equal.
        /// </summary>
        /// <param name="a">
        ///     The <see cref="SizeF"/> value on the left side of the equal sign.
        /// </param>
        /// <param name="b">
        ///     The <see cref="SizeF"/> value on the right side of the equal sign.
        /// </param>
        /// <returns>
        ///     <see langword="true"/> if the two specified <see cref="SizeF"/> values are equal; otherwise,
        ///     <see langword="false"/>.
        /// </returns>
        public static bool operator ==(SizeF a, SizeF b) => a.Equals(b);

        /// <summary>
        ///     Returns a value that indicates whether the two specified <see cref="SizeF"/> values are not equal.
        /// </summary>
        /// <param name="a">
        ///     The <see cref="SizeF"/> value on the left side of the not equal sign.
        /// </param>
        /// <param name="b">
        ///     The <see cref="SizeF"/> value on the right side of the not equal sign.
        /// </param>
        /// <returns>
        ///     <see langword="true"/> if the two specified <see cref="SizeF"/> values are not equal; otherwise,
        ///     <see langword="false"/>.
        /// </returns>
        public static bool operator !=(SizeF a, SizeF b) => !a.Equals(b);

        /// <summary>
        ///     Returns a value that indicates whether the specified <see cref="object"/> is equal to this 
        ///     <see cref="SizeF"/> value.
        /// </summary>
        /// <param name="obj">
        ///     The <see cref="object"/> to check for equality with this <see cref="SizeF"/> value.
        /// </param>
        /// <returns>
        ///     <see langword="true"/> if the specified <see cref="object"/> is equal to this <see cref="SizeF"/> value;
        ///     otherwise, <see langword="false"/>.
        /// </returns>
        public override bool Equals(object obj) => obj is SizeF other && Equals(other);

        /// <summary>
        ///     Returns a value that indicates whether the specified <see cref="SizeF"/> value is equal to this 
        ///     <see cref="SizeF"/> value.
        /// </summary>
        /// <param name="obj">
        ///     The <see cref="SizeF"/> value to check for equality with this <see cref="SizeF"/> value.
        /// </param>
        /// <returns>
        ///     <see langword="true"/> if the specified <see cref="SizeF"/> value is equal to this <see cref="SizeF"/> value;
        ///     otherwise, <see langword="false"/>.
        /// </returns>
        public bool Equals(SizeF other) => Width == other.Width && Height == other.Height;

        /// <summary>
        ///     Returns the hash code for this <see cref="SizeF"/> value.
        /// </summary>
        /// <returns>
        ///     The 32-bit signed integer hash code for this <see cref="SizeF"/> value.
        /// </returns>
        public override int GetHashCode() => (int)Width ^ (int)Height;

        /// <summary>
        ///     Returns a <see cref="string"/> representation of this <see cref="SizeF"/> value in the following format:
        ///     {X:[<see cref="Width"/>], Y:[<see cref="Height"/>]}
        /// </summary>
        /// <returns>
        ///     A <see cref="string"/> representation of this <see cref="SizeF"/> value.
        /// </returns>
        public override string ToString() => $"{{Width:{Width}, Height:{Height}}}";

        /// <summary>
        ///     Deconstruction method for this <see cref="SizeF"/> value.
        /// </summary>
        /// <param name="width">
        ///     When this method returns, contains the <see cref="SizeF.Width"/> component value of this 
        ///     <see cref="SizeF"/> value.
        /// </param>
        /// <param name="height">
        ///     When this method returns, contains the <see cref="SizeF.Height"/> component value of this 
        ///     <see cref="SizeF"/> value.
        /// </param>
        public void Deconstruct(out float width, out float height) => (width, height) = (Width, Height);
    }
}


