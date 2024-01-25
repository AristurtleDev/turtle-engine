// Copyright (c) Christopher Whitley. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Aristurtle.TurtleEngine.Input;

/// <summary>
///     Represents a snapshot of the state of mouse input during the previous and current frame.
/// </summary>
public sealed class MouseInfo
{
    /// <summary>
    ///     The state of mouse input during the previous frame.
    /// </summary>
    public MouseState PreviousState;

    /// <summary>
    ///     The state of mouse input during the current frame.
    /// </summary>
    public MouseState CurrentState;

    /// <summary>
    ///     Gets the difference in the screen space xy-coordinate position of the mouse between the previous and current
    ///     frames.
    /// </summary>
    public Point PositionDelta => PreviousState.Position - CurrentState.Position;

    /// <summary>
    ///     Gets a value tat indicates whether the mouse moved position between the previous and current frames.
    /// </summary>
    public bool HasMoved => PositionDelta != Point.Zero;

    /// <summary>
    ///     Gets the current value of the mouse's scroll wheel.
    /// </summary>
    public int ScrollWheel => CurrentState.ScrollWheelValue;

    /// <summary>
    ///     Gets the difference in the mouse's scroll wheel value between the previous and current frames.
    /// </summary>
    public int ScrollWheelDelta => PreviousState.ScrollWheelValue - CurrentState.ScrollWheelValue;

    internal MouseInfo()
    {
        PreviousState = default(MouseState);
        CurrentState = default(MouseState);
    }

    /// <summary>
    ///     Updates the internal state values.
    /// </summary>
    public void Update()
    {
        PreviousState = CurrentState;
        CurrentState = Mouse.GetState();
    }

    /// <summary>
    ///     Gets the screen space xy-coordinate position of the mouse during the previous frame.
    /// </summary>
    /// <returns>
    ///     The screen space xy-coordinate position of the mouse during the previous frame.
    /// </returns>
    public Point GetPreviousPosition() => PreviousState.Position;

    /// <summary>
    ///     Gets the xy-coordinate position of the mouse during the previous frame converted to the coordinate system
    ///     specified by the provided translation matrix.
    /// </summary>
    /// <param name="translationMatrix">
    ///     The matrix to translate the screen space xy-coordinate position of the mouse to the coordinate system
    ///     needed.
    /// </param>
    /// <returns>
    ///     he xy-coordinate position of the mouse during the previous frame in the coordinate system specified by the
    ///     provided translation matrix.
    /// </returns>
    public Point GetPreviousPosition(Matrix translationMatrix) =>
        Vector2.Transform(PreviousState.Position.ToVector2(), translationMatrix).ToPoint();


    /// <summary>
    ///     Gets the current screen space xy-coordinate position of the mouse.
    /// </summary>
    /// <returns>
    ///     The current screen space xy-coordinate position of the mouse.
    /// </returns>
    public Point GetPosition() => CurrentState.Position;

    /// <summary>
    ///     Gets the current xy-coordinate position of the mouse converted to the coordinate system specified by the
    ///     provided translation matrix.
    /// </summary>
    /// <param name="translationMatrix">
    ///     The matrix to translate the screen space xy-coordinate position of the mouse to the coordinate system
    ///     needed.
    /// </param>
    /// <returns>
    ///     The current xy-coordinate position of the mouse in the coordinate system specified by the provided
    ///     translation matrix.
    /// </returns>
    public Point GetPosition(Matrix translationMatrix) =>
         Vector2.Transform(CurrentState.Position.ToVector2(), translationMatrix).ToPoint();


    /// <summary>
    ///     Sets the screen space xy-coordinate position of the mouse.
    /// </summary>
    /// <param name="position">
    /// T   he xy-coordinate position in screen space to set the mouse to.
    /// </param>
    /// <param name="updateState">
    ///     When <see langword="true"/>, updates the <see cref="CurrentState"/> for this frame.
    /// </param>
    public void SetPosition(Point position, bool updateState = true)
    {
        Mouse.SetPosition(position.X, position.Y);

        if (updateState)
        {
            CurrentState = Mouse.GetState();
        }
    }

    /// <summary>
    ///     Sets the screen space xy-coordinate position of the mouse based on the given position in a coordinate system
    ///     and a translation matrix to that is used to translate from that coordinate system to screen space.
    /// </summary>
    /// <param name="position">
    ///     The xy-coordinate position in a non-screen space coordinate system.
    /// </param>
    /// <param name="translationMatrix">
    ///     The matrix to use for translating between the coordinate system of the given position to screen space.
    /// </param>
    /// <param name="updateState">
    ///     When <see langword="true"/>, updates the <see cref="CurrentState"/> for this frame.
    /// </param>
    public void SetPosition(Point position, Matrix translationMatrix, bool updateState)
    {
        Vector2 v = Vector2.Transform(position.ToVector2(), translationMatrix);
        Mouse.SetPosition((int)Math.Round(v.X), (int)Math.Round(v.Y));

        if (updateState)
        {
            CurrentState = Mouse.GetState();
        }
    }

    /// <summary>
    ///     Returns a value that indicates whether the specified button is currently down.
    /// </summary>
    /// <param name="button">
    ///     The mouse button to check.
    /// </param>
    /// <returns>
    ///     <see langword="true"/> if the specified button is currently down; otherwise, <see langword="false"/>.
    ///     This returns <see langword="true"/> or every frame the button is down.
    /// </returns>
    public bool Check(MouseButton button) => button switch
    {
        MouseButton.Left => CurrentState.LeftButton == ButtonState.Pressed,
        MouseButton.Middle => CurrentState.MiddleButton == ButtonState.Pressed,
        MouseButton.Right => CurrentState.RightButton == ButtonState.Pressed,
        MouseButton.XButton1 => CurrentState.XButton1 == ButtonState.Pressed,
        MouseButton.XButton2 => CurrentState.XButton2 == ButtonState.Pressed,
        _ => throw new InvalidOperationException($"{nameof(MouseInfo)}.{nameof(Check)} encountered an unknown {nameof(MouseButton)}: {button}"),
    };


    /// <summary>
    ///     Returns a value that indicates whether the specified button was just pressed.
    /// </summary>
    /// <param name="button">
    ///     The mouse button to check.
    /// </param>
    /// <returns>
    ///     <see langword="true"/> if the specified button was just pressed; otherwise, <see langword="false"/>.
    ///     This only returns see langword="true"/> on the first frame the button was pressed.
    /// </returns>
    public bool Pressed(MouseButton button) => button switch
    {
        MouseButton.Left => CurrentState.LeftButton == ButtonState.Pressed &&
                               PreviousState.LeftButton == ButtonState.Released,

        MouseButton.Middle => CurrentState.MiddleButton == ButtonState.Pressed &&
                               PreviousState.MiddleButton == ButtonState.Released,

        MouseButton.Right => CurrentState.RightButton == ButtonState.Pressed &&
                               PreviousState.RightButton == ButtonState.Released,

        MouseButton.XButton1 => CurrentState.XButton1 == ButtonState.Pressed &&
                               PreviousState.XButton1 == ButtonState.Released,

        MouseButton.XButton2 => CurrentState.XButton2 == ButtonState.Pressed &&
                               PreviousState.XButton2 == ButtonState.Released,

        _ => throw new InvalidOperationException($"{nameof(MouseInfo)}.{nameof(Pressed)} encountered an unknown {nameof(MouseButton)}: {button}"),
    };


    /// <summary>
    ///     Returns a value that indicates whether the specified button was just released.
    /// </summary>
    /// <param name="button">
    ///     The button to check.
    /// </param>
    /// <returns>
    ///     <see langword="true"/> if the specified button was just released; otherwise, <see langword="false"/>.
    ///     This only returns <see langword="true"/> on the first frame the button was released.
    /// </returns>
    public bool Released(MouseButton button) => button switch
    {
        MouseButton.Left => CurrentState.LeftButton == ButtonState.Released &&
                               PreviousState.LeftButton == ButtonState.Pressed,

        MouseButton.Middle => CurrentState.MiddleButton == ButtonState.Released &&
                               PreviousState.MiddleButton == ButtonState.Pressed,

        MouseButton.Right => CurrentState.RightButton == ButtonState.Released &&
                               PreviousState.RightButton == ButtonState.Pressed,

        MouseButton.XButton1 => CurrentState.XButton1 == ButtonState.Released &&
                               PreviousState.XButton1 == ButtonState.Pressed,

        MouseButton.XButton2 => CurrentState.XButton2 == ButtonState.Released &&
                               PreviousState.XButton2 == ButtonState.Pressed,

        _ => throw new InvalidOperationException($"{nameof(MouseInfo)}.{nameof(Released)} encountered an unknown {nameof(MouseButton)}: {button}"),
    };
}
