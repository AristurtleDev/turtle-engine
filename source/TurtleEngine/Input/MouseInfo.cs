// Copyright (c) Christopher Whitley. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.

using System.Diagnostics.CodeAnalysis;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace TurtleEngine.Input;

/// <summary>
/// Represents a snapshot of the state of mouse input.
/// </summary>
public struct MouseInfo : IEquatable<MouseInfo>
{
    /// <summary>
    /// A <see cref="Microsoft.Xna.Framework.Input.MouseState"/> value that
    /// represents the state of mouse input during the previous frame.
    /// </summary>
    public MouseState PreviousState;

    /// <summary>
    /// A <see cref="Microsoft.Xna.Framework.Input.MouseState"/> value that
    /// represents the state of mouse input during the previous frame.
    /// </summary>
    public MouseState CurrentState;

    /// <summary>
    /// Gets the current screen space xy-coordinate position of the mouse.
    /// </summary>
    public readonly Point Position
    {
        get
        {
            return CurrentState.Position;
        }
    }

    /// <summary>
    /// Gets the current screen space x-coordinate position of the mouse.
    /// </summary>
    public readonly int X
    {
        get
        {
            return Position.X;
        }
    }

    /// <summary>
    /// Gets the current screen space y-coordinate position of the mouse.
    /// </summary>
    public readonly int Y
    {
        get
        {
            return Position.Y;
        }
    }

    /// <summary>
    /// Gets the difference in the screen space xy-coordinate position of the
    /// mouse between the previous and current frames.
    /// </summary>
    public readonly Point PositionDelta
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
    public readonly int DeltaX
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
    public readonly int DeltaY
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
    public readonly bool HasMoved
    {
        get
        {
            return PositionDelta != Point.Zero;
        }
    }

    /// <summary>
    /// Gets the current value of the mouse's scroll wheel.
    /// </summary>
    public readonly int ScrollWheel
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
    public readonly int ScrollWheelDelta
    {
        get
        {
            return PreviousState.ScrollWheelValue - CurrentState.ScrollWheelValue;
        }
    }

    /// <summary>
    /// Initializes a new <see cref="MouseInfo"/> value.
    /// </summary>
    public MouseInfo()
    {
        PreviousState = new MouseState();
        CurrentState = new MouseState();
    }

    /// <summary>
    /// Initializes a new <see cref="MouseInfo"/> value with the specified
    /// previous and current states.
    /// </summary>
    /// <param name="previousState">
    /// A <see cref="Microsoft.Xna.Framework.Input.MouseState"/> value that
    /// represents the state of mouse input during the previous frame.
    /// </param>
    /// <param name="currentState">
    /// A <see cref="Microsoft.Xna.Framework.Input.MouseState"/> value that
    /// represents the state of mouse input during the current frame.
    /// </param>
    public MouseInfo(MouseState previousState, MouseState currentState)
    {
        PreviousState = previousState;
        CurrentState = currentState;
    }

    /// <summary>
    /// Returns a value that indicates whether the specified mouse button is
    /// current held down.
    /// </summary>
    /// <param name="button">The mouse button to check.</param>
    /// <returns>
    /// <see langword="true"/> if the specified mouse button is currently held
    /// down; otherwise, <see langword="false"/>.
    /// </returns>
    public readonly bool ButtonDown(MouseButton button)
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
    /// Returns a value that indicates whether the specified mouse button was
    /// just pressed.
    /// </summary>
    /// <remarks>
    /// "Just pressed" means the mouse button was released on the previous frame
    /// and pressed on the current frame.
    /// </remarks>
    /// <param name="button">The mouse button to check.</param>
    /// <returns>
    /// <see langword="true"/> if the specified mouse button was just pressed;
    /// otherwise, <see langword="false"/>.
    /// </returns>
    public readonly bool ButtonPressed(MouseButton button)
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
    /// Returns a value that indicates whether the specified mouse button was
    /// just released.
    /// </summary>
    /// <remarks>
    /// "Just released" means the mouse button was pressed on the previous frame
    /// and released on the current frame.
    /// </remarks>
    /// <param name="button">The mouse button to check.</param>
    /// <returns>
    /// <see langword="true"/> if the specified mouse button was just released;
    /// otherwise, <see langword="false"/>.
    /// </returns>
    public readonly bool ButtonReleased(MouseButton button)
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

    /// <inheritdoc/>
    public readonly bool Equals(MouseInfo other)
    {
        return GetHashCode() == other.GetHashCode();
    }

    /// <inheritdoc />
    public override readonly bool Equals([NotNullWhen(true)] object? obj)
    {
        return obj is MouseInfo other && Equals(other);
    }

    /// <inheritdoc />
    public override readonly int GetHashCode()
    {
        return HashCode.Combine(PreviousState.GetHashCode(), CurrentState.GetHashCode());
    }

    /// <inheritdoc />
    public static bool operator ==(MouseInfo left, MouseInfo right)
    {
        return left.Equals(right);
    }

    /// <inheritdoc />
    public static bool operator !=(MouseInfo left, MouseInfo right)
    {
        return !(left == right);
    }
}
