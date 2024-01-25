// Copyright (c) Christopher Whitley. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.

using Microsoft.Xna.Framework;

namespace Aristurtle.TurtleEngine.Input;

/// <summary>
///     Represents a snapshot of the state of a gamepad thumb stick during the previous and current frames.
/// </summary>
public sealed class GamePadThumbStickInfo
{
    private Vector2 _previousValue;
    private Vector2 _currentValue;

    internal GamePadThumbStickInfo()
    {
        _previousValue = default(Vector2);
        _currentValue = default(Vector2);
    }

    internal void Update(Vector2 value)
    {
        _previousValue = _currentValue;
        _currentValue = value;

        //  Flip the y-axis value
        _currentValue.Y = -_currentValue.Y;
    }

    /// <summary>
    ///     Returns the value of this thumb stick during the previous frame.
    /// </summary>
    /// <param name="deadZone">
    ///     The minimum value the thumb stick must exceed to be considered a non-zero value.
    /// </param>
    /// <returns>
    ///     A <see cref="Microsoft.Xna.Framework.Vector2"/> value who's <see cref="Microsoft.Xna.Framework.Vector2.X"/>
    ///     and <see cref="Microsoft.Xna.Framework.Vector2.Y"/> component values represent the x- and y-axis values of
    ///     this thumb stick during the previous frame.
    /// </returns>
    public Vector2 PreviousValue(float deadZone = 0.0f)
    {
        if (_previousValue.LengthSquared() >= deadZone * deadZone)
        {
            return _previousValue;
        }

        return Vector2.Zero;
    }

    /// <summary>
    ///     Returns the value of this thumb stick during the current frame.
    /// </summary>
    /// <param name="deadZone">
    ///  The minimum value the thumb stick must exceed to be considered a non-zero value.
    /// </param>
    /// <returns>
    ///     A <see cref="Microsoft.Xna.Framework.Vector2"/> value who's <see cref="Microsoft.Xna.Framework.Vector2.X"/>
    ///     and <see cref="Microsoft.Xna.Framework.Vector2.Y"/> component values represent the x- and y-axis values of
    ///     this thumb stick during the current frame.
    /// </returns>
    public Vector2 CurrentValue(float deadZone = 0.0f)
    {
        if (_currentValue.LengthSquared() >= deadZone * deadZone)
        {
            return _currentValue;
        }

        return Vector2.Zero;
    }

    /// <summary>
    ///     Returns the difference in this thumb stick's value between the previous and current frame.
    /// </summary>
    /// <param name="deadZone">
    ///     The minimum value the thumb stick must exceed to be considered a non-zero value.
    /// </param>
    /// <returns>
    ///     A <see cref="Microsoft.Xna.Framework.Vector2"/> value who's <see cref="Microsoft.Xna.Framework.Vector2.X"/>
    ///     and <see cref="Microsoft.Xna.Framework.Vector2.Y"/> component values represent the difference in the x- and
    ///     y-axis values of this thumb stick between the previous and current frames.
    /// </returns>
    public Vector2 DeltaValue(float deadZone = 0.0f)
    {
        return PreviousValue(deadZone) - CurrentValue(deadZone);
    }

    /// <summary>
    ///     Returns a value that indicates if this thumb stick has moved between the previous and current frame.
    /// </summary>
    /// <param name="deadZone">
    ///     The minimum value the thumb stick must exceed to be considered a non-zero value.
    /// </param>
    /// <returns>
    ///     <see langword="true"/> if this thumb stick has moved between the previous and current frame; otherwise,
    ///     <see langword="false"/>.
    /// </returns>
    public bool HasMoved(float deadZone = 0.0f)
    {
        return DeltaValue(deadZone) != Vector2.Zero;
    }

    /// <summary>
    ///     Returns a value that indicates if this thumb stick is currently pressed upward.
    /// </summary>
    /// <param name="deadZone">
    ///     The minimum value the thumb stick must exceed to be considered a non-zero value.
    /// </param>
    /// <returns>
    ///     <see langword="true"/> if this thumb stick is currently pressed upward; otherwise, <see langword="false"/>.
    ///     This method returns <see langword="true"/> for every frame that the thumb stick is pressed upward.
    /// </returns>
    public bool CheckUp(float deadZone = 0.0f)
    {
        return CurrentValue(deadZone).Y > float.Epsilon;
    }

    /// <summary>
    ///     Returns a value that indicates if this thumb stick is currently pressed downward.
    /// </summary>
    /// <param name="deadZone">
    ///     The minimum value the thumb stick must exceed to be considered a non-zero value.
    /// </param>
    /// <returns>
    ///     <see langword="true"/> if this thumb stick is currently pressed downward; otherwise,
    ///     <see langword="false"/>. This method returns <see langword="true"/> for every frame that the thumb stick is
    ///     pressed downward.
    /// </returns>
    public bool CheckDown(float deadZone = 0.0f)
    {
        return CurrentValue(deadZone).Y < float.Epsilon;
    }

    /// <summary>
    ///     Returns a value that indicates if this thumb stick is currently pressed to the left.
    /// </summary>
    /// <param name="deadZone">
    ///     The minimum value the thumb stick must exceed to be considered a on-zero value.
    /// </param>
    /// <returns>
    ///     <see langword="true"/> if this thumb stick is currently pressed to the left; otherwise,
    ///     <see langword="false"/>.  This method returns <see langword="true"/> for every frame that the thumb stick is
    ///     pressed to the left.
    /// </returns>
    public bool CheckLeft(float deadZone = 0.0f)
    {
        return CurrentValue(deadZone).X < float.Epsilon;
    }

    /// <summary>
    ///     Returns a value that indicates if this thumb stick is currently pressed to the right.
    /// </summary>
    /// <param name="deadZone">
    ///     The minimum value the thumb stick must exceed to be considered a non-zero value.
    /// </param>
    /// <returns>
    ///     <see langword="true"/> if this thumb stick is currently pressed to the right; otherwise,
    ///     <see langword="false"/>.  This method returns <see langword="true"/> for every frame that the thumb stick is
    ///     pressed to the right.
    /// </returns>
    public bool CheckRight(float deadZone = 0.0f)
    {
        return CurrentValue(deadZone).X > float.Epsilon;
    }

    /// <summary>
    ///     Returns a value that indicates if this thumb stick was just pressed upward.
    /// </summary>
    /// <param name="deadZone">
    ///     The minimum value the thumb stick must exceed to be considered a non-zero value.
    /// </param>
    /// <returns>
    ///     <see langword="true"/> if this thumb stick was just pressed upward; otherwise, <see langword="false"/>.
    ///     This only returns <see langword="true"/> for the first frame the thumb stick was pressed upward.
    /// </returns>
    public bool PressedUp(float deadZone = 0.0f)
    {
        return CheckUp(deadZone) && PreviousValue(deadZone).Y <= float.Epsilon;
    }

    /// <summary>
    ///     Returns a value that indicates if this thumb stick was just pressed downward.
    /// </summary>
    /// <param name="deadZone">
    ///     The minimum value the thumb stick must exceed to be considered a non-zero value.
    /// </param>
    /// <returns>
    ///     <see langword="true"/> if this thumb stick was just pressed downward; otherwise, <see langword="false"/>.
    ///     This only returns see langword="true"/> for the first frame the thumb stick was pressed downward.
    /// </returns>
    public bool PressedDown(float deadZone = 0.0f)
    {
        return CheckDown(deadZone) && PreviousValue(deadZone).Y >= float.Epsilon;
    }

    /// <summary>
    ///     Returns a value that indicates if this thumb stick was just pressed to the left.
    /// </summary>
    /// <param name="deadZone">
    ///     The minimum value the thumb stick must exceed to be considered a non-zero value.
    /// </param>
    /// <returns>
    ///     <see langword="true"/> if this thumb stick was just pressed to the left; otherwise, <see langword="false"/>.
    ///     This only returns <see langword="true"/> for the first frame the thumb stick was pressed to the left.
    /// </returns>
    public bool PressedLeft(float deadZone = 0.0f)
    {
        return CheckLeft(deadZone) && PreviousValue(deadZone).X >= float.Epsilon;
    }

    /// <summary>
    ///     Returns a value that indicates if this thumb stick was just pressed to the right.
    /// </summary>
    /// <param name="deadZone">
    ///     The minimum value the thumb stick must exceed to be considered a on-zero value.
    /// </param>
    /// <returns>
    ///     <see langword="true"/> if this thumb stick was just pressed to the right; otherwise,
    ///     <see langword="false"/>. This only returns <see langword="true"/> for the first frame the thumb stick was
    ///     pressed to the right.
    /// </returns>
    public bool PressedRight(float deadZone = 0.0f)
    {
        return CheckRight(deadZone) && PreviousValue(deadZone).X <= float.Epsilon;
    }

    /// <summary>
    ///     Returns a value that indicates if this thumb stick was just released from being pressed upward.
    /// </summary>
    /// <param name="deadZone">
    ///     The minimum value the thumb stick must exceed to be considered a non-zero value.
    /// </param>
    /// <returns>
    ///     <see langword="true"/> if this thumb stick was just released from being pressed upward; otherwise,
    ///     <see langword="false"/>.  This only returns <see langword="true"/> for the first frame the thumb stick was
    ///     released from being pressed upward.
    /// </returns>
    public bool ReleasedUp(float deadZone = 0.0f)
    {
        return !CheckUp(deadZone) && PreviousValue(deadZone).Y >= float.Epsilon;
    }

    /// <summary>
    ///     Returns a value that indicates if this thumb stick was just released from being pressed downward.
    /// </summary>
    /// <param name="deadZone">
    ///     The minimum value the thumb stick must exceed to be considered a non-zero value.
    /// </param>
    /// <returns>
    ///     <see langword="true"/> if this thumb stick was just released from being pressed downward; otherwise,
    ///     <see langword="false"/>.  This only returns  <see langword="true"/> for the first frame the thumb stick was
    ///     released from being pressed downward.
    /// </returns>
    public bool ReleasedDown(float deadZone = 0.0f)
    {
        return !CheckDown(deadZone) && PreviousValue(deadZone).Y <= float.Epsilon;
    }

    /// <summary>
    ///     Returns a value that indicates if this thumb stick was just released from being pressed to the left.
    /// </summary>
    /// <param name="deadZone">
    ///     The minimum value the thumb stick must exceed to be considered a non-zero value.
    /// </param>
    /// <returns>
    ///     <see langword="true"/> if this thumb stick was just released from being pressed to the left; otherwise,
    ///     <see langword="false"/>.  This only returns <see langword="true"/> for the first frame the thumb stick was
    ///     released from being pressed to the left.
    /// </returns>
    public bool ReleasedLeft(float deadZone = 0.0f)
    {
        return !CheckLeft(deadZone) && PreviousValue(deadZone).X <= float.Epsilon;
    }

    /// <summary>
    ///     Returns a value that indicates if this thumb stick was just released from being pressed to the right.
    /// </summary>
    /// <param name="deadZone">
    ///     The minimum value the thumb stick must exceed to be considered a non-zero value.
    /// </param>
    /// <returns>
    ///     <see langword="true"/> if this thumb stick was just released from being pressed to the right; otherwise,
    ///     <see langword="false"/>.  This only returns <see langword="true"/> for the first frame the thumb stick was
    ///     released from being pressed to the right.
    /// </returns>
    public bool ReleasedRight(float deadZone = 0.0f)
    {
        return !CheckRight(deadZone) && PreviousValue(deadZone).X >= float.Epsilon;
    }
}
