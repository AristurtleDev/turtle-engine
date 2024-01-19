// Copyright (c) Christopher Whitley. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace TurtleEngine.Input;

/// <summary>
/// Represents a snapshot of the state of mouse input during the previous and
/// current frame.
/// </summary>
public sealed class MouseInfo
{
    /// <summary>
    /// Gets the state of mouse input during the previous frame.
    /// </summary>
    public MouseState PreviousState { get; private set; }

    /// <summary>
    /// Gets the state of mouse input during the current frame.
    /// </summary>
    public MouseState CurrentState { get; private set; }

    /// <summary>
    /// Gets or Sets the screen space xy-coordinate position of the mouse.
    /// </summary>
    public Point Position
    {
        get
        {
            return CurrentState.Position;
        }
        set
        {
            Mouse.SetPosition(value.X, value.Y);
        }
    }

    /// <summary>
    /// Gets or Sets the screen space x-coordinate position of the mouse.
    /// </summary>
    public int X
    {
        get
        {
            return Position.X;
        }
        set
        {
            Position = new Point(value, Position.Y);
        }
    }

    /// <summary>
    /// Gets or Sets the screen space y-coordinate position of the mouse.
    /// </summary>
    public int Y
    {
        get
        {
            return Position.Y;
        }
        set
        {
            Position = new Point(Position.X, value);
        }
    }

    /// <summary>
    /// Gets the difference in the screen space xy-coordinate position of the
    /// mouse between the previous and current frames.
    /// </summary>
    public Point PositionDelta
    {
        get
        {
            return PreviousState.Position - CurrentState.Position;
        }
    }

    /// <summary>
    /// Gets the difference in the screen space x-coordinate position of the
    /// mouse between the previous and current frames.
    /// </summary>
    public int DeltaX
    {
        get
        {
            return PositionDelta.X;
        }
    }

    /// <summary>
    /// Gets the difference in the screen space y-coordinate position of the
    /// mouse between the previous and current frames.
    /// </summary>
    public int DeltaY
    {
        get
        {
            return PositionDelta.Y;
        }
    }

    /// <summary>
    /// Gets a value tat indicates whether the mouse moved position between the
    /// previous and current frames.
    /// </summary>
    public bool HasMoved
    {
        get
        {
            return PositionDelta != Point.Zero;
        }
    }

    /// <summary>
    /// Gets the current value of the mouse's scroll wheel.
    /// </summary>
    public int ScrollWheel
    {
        get
        {
            return CurrentState.ScrollWheelValue;
        }
    }

    /// <summary>
    /// Gets the difference in the mouse's scroll wheel value between the
    /// previous and current frames.
    /// </summary>
    public int ScrollWheelDelta
    {
        get
        {
            return PreviousState.ScrollWheelValue - CurrentState.ScrollWheelValue;
        }
    }

    internal MouseInfo()
    {
        PreviousState = default(MouseState);
        CurrentState = default(MouseState);
    }

    /// <summary>
    /// Updates the internal state values.
    /// </summary>
    public void Update()
    {
        PreviousState = CurrentState;
        CurrentState = Mouse.GetState();
    }

    /// <summary>
    /// Returns a value that indicates whether the specified button is currently
    /// down.
    /// </summary>
    /// <param name="button">The mouse button to check.</param>
    /// <returns>
    /// <see langword="true"/> if the specified button is currently down;
    /// otherwise, <see langword="false"/>.  This returns <see langword="true"/>
    /// for every frame the button is down.
    /// </returns>
    public bool Check(MouseButton button)
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
                throw new InvalidOperationException($"{nameof(MouseInfo)}.{nameof(Check)} encountered an unknown {nameof(MouseButton)}: {button}");
        }
    }

    /// <summary>
    /// Returns a value that indicates whether the specified button was just
    /// pressed.
    /// </summary>
    /// <param name="button">The mouse button to check.</param>
    /// <returns>
    /// <see langword="true"/> if the specified button was just pressed;
    /// otherwise, <see langword="false"/>.  This only returns
    /// <see langword="true"/> on the first frame the button was pressed.
    /// </returns>
    public bool Pressed(MouseButton button)
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
                throw new InvalidOperationException($"{nameof(MouseInfo)}.{nameof(Pressed)} encountered an unknown {nameof(MouseButton)}: {button}");
        }
    }

    /// <summary>
    /// Returns a value that indicates whether the specified button was just
    /// released.
    /// </summary>
    /// <param name="button">The button to check.</param>
    /// <returns>
    /// <see langword="true"/> if the specified button was just released;
    /// otherwise, <see langword="false"/>.  This only returns
    /// <see langword="true"/> on the first frame the button was released.
    /// </returns>
    public bool Released(MouseButton button)
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
                throw new InvalidOperationException($"{nameof(MouseInfo)}.{nameof(Released)} encountered an unknown {nameof(MouseButton)}: {button}");
        }
    }
}
