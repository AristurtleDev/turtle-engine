using Microsoft.Xna.Framework;

namespace TurtleEngine;

/// <summary>
/// Utility class that contains various maths.
/// </summary>
public static class TurtleMath
{
    /// <summary>
    /// Returns the angle of the given vector
    /// </summary>
    /// <param name="vec2">The vector to calculate the angle of.</param>
    /// <returns>The angle of the given vector.</returns>
    public static float Angle(this Vector2 vec2) => (float)Math.Atan2(vec2.Y, vec2.X);

    /// <summary>
    /// Returns the angle between two vectors.
    /// </summary>
    /// <param name="start">
    /// The vector that represents the starting point.
    /// </param>
    /// <param name="end">The vector that represents the ending point.</param>
    /// <returns>
    /// The angle between the two specified vectors.</returns>
    public static float Angle(Vector2 start, Vector2 end)
    {
        Vector2 delta = end - start;
        return delta.Angle();
    }

    /// <summary>
    /// Translates the given angle and length into a vector.
    /// </summary>
    /// <param name="angle">The angle, in radians.</param>
    /// <param name="length">The length of the vector.</param>
    /// <returns>The vector with the specified angle and length.</returns>
    public static Vector2 AngleToVector2(float angle, float length)
    {
        float x = (float)Math.Cos(angle) * length;
        float y = (float)Math.Sin(angle) * length;
        return new(x, y);
    }

    /// <summary>
    /// Returns a vector that is perpendicular to the given vector.
    /// </summary>
    /// <param name="vec2">The vector to get the perpendicular of.</param>
    /// <returns>A vector that is perpendicular to the given vector.</returns>
    public static Vector2 Perpendicular(this Vector2 vec2) => new(-vec2.Y, vec2.X);
}
