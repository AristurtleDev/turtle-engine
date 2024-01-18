// Copyright (c) Christopher Whitley. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace TurtleEngine.Input;

/// <summary>
///     Represents a snapshot of the state of mouse input.
/// </summary>
public sealed class MouseInfo
{
    /// <summary>
    ///     Gets the state of mouse input during the previous frame.
    /// </summary>
    public MouseState PreviousState { get; private set; }

    /// <summary>
    ///     Gets the state of mouse input during the current frame.
    /// </summary>
    public MouseState CurrentState { get; private set; }

    /// <summary>
    ///     Gets or Sets the screen space xy-coordinate position of the mouse.
    /// </summary>
    public Point Position
    {
        get => CurrentState.Position;
        set => Mouse.SetPosition(value.X, value.Y);
    }

    /// <summary>
    ///     Gets or Sets the screen space x-coordinate position of the mouse.
    /// </summary>
    public int X
    {
        get => Position.X;
        set => Position = new Point(value, Position.Y);
    }

    /// <summary>
    ///     Gets or Sets the screen space y-coordinate position of the mouse.
    /// </summary>
    public int Y
    {
        get => Position.Y;
        set => Position = new Point(Position.X, value);
    }

    /// <summary>
    ///     Gets the difference of the screen space xy-coordinate position of
    ///     the mouse between the previous and current frames.
    /// </summary>
    public Point PositionDelta => PreviousState.Position - CurrentState.Position;

    /// <summary>
    ///     Gets the difference of the screen space x-coordinate position of the
    ///     mouse between the previous and current frames.
    /// </summary>
    public int DeltaX => PositionDelta.X;

    /// <summary>
    ///     Gets the difference of the screen space y-coordinate position of the
    ///     mouse between the previous and current frames.
    /// </summary>
    public int DeltaY => PositionDelta.Y;

    /// <summary>
    ///     Gets a value that indicates if the mouse has moved position between
    ///     the previous and current frames.
    /// </summary>
    public bool HasMoved => PositionDelta != Point.Zero;

    /// <summary>
    ///     Gets the value of the mouse's scroll wheel.
    /// </summary>
    public int ScrollWheel => CurrentState.ScrollWheelValue;

    /// <summary>
    ///     Gets the difference in the mouse's scroll wheel value between the
    ///     previous and current frames.
    /// </summary>
    public int ScrollWheelDelta => PreviousState.ScrollWheelValue - CurrentState.ScrollWheelValue;

    /// <summary>
    ///     Creates a new instance of the <see cref="MouseInfo"/> class.
    /// </summary>
    public MouseInfo()
    {
        PreviousState = new();
        CurrentState = Mouse.GetState();
    }

    /// <summary>
    ///     Updates the state of this instance of the <see cref="MouseInfo"/>
    ///     class.
    /// </summary>
    public void Update()
    {
        PreviousState = CurrentState;
        CurrentState = Mouse.GetState();
    }

    /// <summary>
    ///     Returns a value that indicates whether the specified mouse button is
    ///     currently held down.
    /// </summary>
    /// <param name="button">
    ///     The mouse button to check.
    /// </param>
    /// <returns>
    ///     <see langword="true"/> if the specified mouse button is currently
    ///     held down; otherwise, <see langword="false"/>.
    /// </returns>
    public bool ButtonDown(MouseButton button)
    {
        switch (button)
        {
            case MouseButton.Left:
                return CurrentState.LeftButton == ButtonState.Pressed;

            case MouseButton.Middle:
                return CurrentState.MiddleButton == ButtonState.Pressed;

            case MouseButton.Right:
                return CurrentState.RightButton == ButtonState.Pressed;

            case MouseButton.XButton1:
                return CurrentState.XButton1 == ButtonState.Pressed;

            case MouseButton.XButton2:
                return CurrentState.XButton2 == ButtonState.Pressed;

            default:
                throw new InvalidOperationException($"{nameof(MouseInfo)}.{nameof(ButtonDown)} encountered an unknown {nameof(MouseButton)}: {button}");
        }
    }

    /// <summary>
    ///     Returns a value that indicates whether the specified mouse button
    ///     was just pressed.
    /// </summary>
    /// <remarks>
    ///     "Just pressed" means the mouse button was released on the previous
    ///     frame and pressed on the current frame.
    /// </remarks>
    /// <param name="button">
    ///     The mouse button to check.
    /// </param>
    /// <returns>
    ///     <see langword="true"/> if the specified mouse button was just
    ///     pressed; otherwise, <see langword="false"/>.
    /// </returns>
    public bool ButtonPressed(MouseButton button)
    {
        switch (button)
        {
            case MouseButton.Left:
                return CurrentState.LeftButton == ButtonState.Pressed &&
                       PreviousState.LeftButton == ButtonState.Released;

            case MouseButton.Middle:
                return CurrentState.MiddleButton == ButtonState.Pressed &&
                       PreviousState.MiddleButton == ButtonState.Released;

            case MouseButton.Right:
                return CurrentState.RightButton == ButtonState.Pressed &&
                       PreviousState.RightButton == ButtonState.Released;

            case MouseButton.XButton1:
                return CurrentState.XButton1 == ButtonState.Pressed &&
                       PreviousState.XButton1 == ButtonState.Released;

            case MouseButton.XButton2:
                return CurrentState.XButton2 == ButtonState.Pressed &&
                       PreviousState.XButton2 == ButtonState.Released;

            default:
                throw new InvalidOperationException($"{nameof(MouseInfo)}.{nameof(ButtonPressed)} encountered an unknown {nameof(MouseButton)}: {button}");
        }
    }

    /// <summary>
    ///     Returns a value that indicates whether the specified mouse button
    ///     was just released.
    /// </summary>
    /// <remarks>
    ///     "Just released" means the mouse button was pressed on the previous
    ///     frame and released on the current frame.
    /// </remarks>
    /// <param name="button">
    ///     The mouse button to check.
    /// </param>
    /// <returns>
    ///     <see langword="true"/> if the specified mouse button was just
    ///     released; otherwise, <see langword="false"/>.
    /// </returns>
    public bool ButtonReleased(MouseButton button)
    {
        switch (button)
        {
            case MouseButton.Left:
                return CurrentState.LeftButton == ButtonState.Released &&
                       PreviousState.LeftButton == ButtonState.Pressed;

            case MouseButton.Middle:
                return CurrentState.MiddleButton == ButtonState.Released &&
                       PreviousState.MiddleButton == ButtonState.Pressed;

            case MouseButton.Right:
                return CurrentState.RightButton == ButtonState.Released &&
                       PreviousState.RightButton == ButtonState.Pressed;

            case MouseButton.XButton1:
                return CurrentState.XButton1 == ButtonState.Released &&
                       PreviousState.XButton1 == ButtonState.Pressed;

            case MouseButton.XButton2:
                return CurrentState.XButton2 == ButtonState.Released &&
                       PreviousState.XButton2 == ButtonState.Pressed;

            default:
                throw new InvalidOperationException($"{nameof(MouseInfo)}.{nameof(ButtonReleased)} encountered an unknown {nameof(MouseButton)}: {button}");
        }
    }
}
