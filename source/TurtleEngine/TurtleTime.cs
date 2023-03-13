using Microsoft.Xna.Framework;

namespace TurtleEngine;

/// <summary>
///     Defines a snapshot of the timing values for the game.
/// </summary>
public class TurtleTime
{
    /// <summary>
    ///     Gets the amount of time that has elapsed since the last update, multiplied by the <see cref="Rate"/>
    ///     property value.
    /// </summary>
    /// <value>
    ///     A <see cref="TimeSpan"/> that represents the amount of time that has elapsed since the last update,
    ///     multiplied by the <see cref="Rate"/> property value.
    /// </value>
    public TimeSpan ElapsedTime => RawElapsedTime * Rate;

    /// <summary>
    ///     Gets or Sets the amount of time that has elapsed since the last update.
    /// </summary>
    /// <value>
    ///     A <see cref="TimeSpan"/> that represents the amount of time that has elapsed since the last update.
    /// </value>
    public TimeSpan RawElapsedTime { get; private set; }

    /// <summary>
    ///     Gets or Sets the rate of time that affects the <see cref="ElapsedTime"/> property value.
    /// </summary>
    /// <value>
    ///     A <see cref="double"/> value that represents the rate of time that affects the <see cref="ElapsedTime"/>
    ///     property value.
    /// </value>
    public double Rate { get; set; } = 1.0d;

    internal TurtleTime() { }

    internal void Update(GameTime gameTime)
    {
        RawElapsedTime = gameTime.ElapsedGameTime;
    }
}