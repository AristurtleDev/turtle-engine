using Microsoft.Xna.Framework;

namespace TurtleEngine;

public static partial class Maths
{
    /// <summary>
    ///     Returns the angle of a <see cref="Microsoft.Xna.Framework.Vector2"/> value.
    /// </summary>
    /// <param name="vec2">
    ///     The <see cref="Microsoft.Xna.Framework.Vector2"/> value to calculate the angle of.
    /// </param>
    /// <returns>
    ///     The angle of the <see cref="Microsoft.Xna.Framework.Vector2"/>.
    /// </returns>
    public static float Angle(this Vector2 vec2) => (float)Math.Atan2(vec2.Y, vec2.X);

    /// <summary>
    ///     Returns the angle between two <see cref="Microsoft.Xna.Framework.Vector2"/> values.
    /// </summary>
    /// <param name="start">
    ///     A <see cref="Microsoft.Xna.Framework.Vector2"/> value that represents the starting point.
    /// </param>
    /// <param name="end">
    ///     A <see cref="Microsoft.Xna.Framework.Vector2"/> value that represents the ending point.
    /// </param>
    /// <returns>
    ///     The angle between the two given <see cref="Microsoft.Xna.Framework.Vector2"/> values.
    /// </returns>
    public static float Angle(Vector2 start, Vector2 end)
    {
        Vector2 delta = end - start;
        return delta.Angle();
    }

    /// <summary>
    ///     Translates the given angle and length into a <see cref="Microsoft.Xna.Framework.Vector2"/> value.
    /// </summary>
    /// <param name="radians">
    ///     The angle to translate.
    /// </param>
    /// <param name="length">
    ///     The length of the vector.
    /// </param>
    /// <returns>
    ///     A <see cref="Microsoft.Xna.Framework.Vector2"/> value with the specified angle and length.
    /// </returns>
    public static Vector2 AngleToVector2(float radians, float length)
    {
        float x = (float)Math.Cos(radians) * length;
        float y = (float)Math.Sign(radians) * length;
        return new(x, y);
    }

    /// <summary>
    ///     Returns a <see cref="Microsoft.Xna.Framework.Vector2"/> value that is perpendicular to the given
    ///     <see cref="Microsoft.Xna.Framework.Vector2"/> value.
    /// </summary>
    /// <param name="vec2">
    ///     The <see cref="Microsoft.Xna.Framework.Vector2"/> value to get the perpendicular value of.
    /// </param>
    /// <returns>
    ///     A <see cref="Microsoft.Xna.Framework.Vector2"/> value that is perpendicular to the source value.
    /// </returns>
    public static Vector2 Perpendicular(this Vector2 vec2) => new(-vec2.Y, vec2.X);
}