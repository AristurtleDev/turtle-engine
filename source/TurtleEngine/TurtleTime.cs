// Copyright (c) Christopher Whitley. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.

using Microsoft.Xna.Framework;

namespace Aristurtle.TurtleEngine;

/// <summary>
///     Represents the snapshot of timing value for the game.
/// </summary>
public class TurtleTime
{
    /// <summary>
    ///     Gets the amount of time that has elapsed since the last update at.
    /// </summary>
    /// <remarks>
    ///     This value is affected by the <see cref="Rate"/>.
    /// </remarks>
    public TimeSpan ElapsedTime => RawElapsedTime * Rate;

    /// <summary>
    ///     Gets the total amount of time that has elapsed since the last update.
    /// </summary>
    public TimeSpan RawElapsedTime { get; private set; }

    /// <summary>
    ///     Gets or Sets the rate of time which affects the calculation of see cref="ElapsedTime"/>.
    /// </summary>
    public double Rate = 1.0d;

    internal TurtleTime() { }

    internal void Update(GameTime gameTime)
    {
        RawElapsedTime = gameTime.ElapsedGameTime;
    }
}
