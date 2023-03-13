using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace TurtleEngine.Input;

/// <summary>
///     Defines the information regarding mouse input.
/// </summary>
public sealed class MouseInfo
{
    /// <summary>
    ///     Gets the state of mouse input during the previous update cycle.
    /// </summary>
    /// <value>
    ///     A <see cref="Microsoft.Xna.Framework.Input.MouseState"/> value that represents the state of mouse input
    ///     during the previous update cycle.
    /// </value>
    public MouseState PreviousState { get; set; }

    /// <summary>
    ///     Gets the state of mouse input during the current update cycle.
    /// </summary>
    /// <value>
    ///     A <see cref="Microsoft.Xna.Framework.Input.MouseState"/> value that represents the state of mouse input
    ///     during the current update cycle.
    /// </value>
    public MouseState CurrentState { get; set; }

    /// <summary>
    ///     Gets or Sets the current position of the mouse.
    /// </summary>
    /// <value>
    ///     A <see cref="Microsoft.Xna.Framework.Point"/> value that defines the x- and y-coordinate position of the
    ///     mouse.
    /// </value>
    public Point Position
    {
        get => CurrentState.Position;
        set => Mouse.SetPosition(value.X, value.Y);
    }

    /// <summary>
    ///     Gets or Sets the current x-coordinate position of the mouse.
    /// </summary>
    /// <value>
    ///     A 32-bit signed integer value that defines the x-coordinate position of the mouse.
    /// </value>
    public int X
    {
        get => Position.X;
        set => Position = new Point(value, Position.Y);
    }

    /// <summary>
    ///     Gets or Sets the current y-coordinate position of the mouse.
    /// </summary>
    /// <value>
    ///     A 32-bit signed integer value that defines the y-coordinate position of the mouse.
    /// </value>
    public int Y
    {
        get => Position.Y;
        set => Position = new Point(Position.X, value);
    }

    /// <summary>
    ///     Gets the difference in the mouse position between the previous frame and the current frame.
    /// </summary>
    /// <value>
    ///     A <see cref="Microsoft.Xna.Framework.Point"/> value that defines the difference in the x-coordinate and
    ///     y-coordinate position of the mouse between the previous and current frames.
    /// </value>
    public Point PositionDelta => PreviousState.Position - CurrentState.Position;

    /// <summary>
    ///     Gets a value that indicates if the mouse has changed position between the previous and current frame.
    /// </summary>
    /// <value>
    ///     <see langword="true"/> if the mouse has changed position between the previous and current frame; otherwise,
    ///     <see langword="false"/>.
    /// </value>
    public bool HasMoved => PositionDelta != Point.Zero;

    /// <summary>
    ///     Gets the value of the scroll wheel for the mouse during the current frame.
    /// </summary>
    /// <value>
    ///     A 32-bit signed integer that represents the value of the scroll wheel for the mouse during the current
    ///     frame.
    /// </value>
    public int ScrollWheel => CurrentState.ScrollWheelValue;

    /// <summary>
    ///     Gets the difference in the value of the scroll wheel for the mouse between the previous frame and the
    ///     current frame.
    /// </summary>
    /// <value> 
    ///     A 32-bit signed integer value that represents the difference in the value of the scroll wheel for the mouse
    ///     between the previous frame and the current frame.
    /// </value>
    public int ScrollWheelDelta => PreviousState.ScrollWheelValue - CurrentState.ScrollWheelValue;

    /// <summary>
    ///     Initializes a new instance of the <see cref="MouseInfo"/> class.
    /// </summary>
    public MouseInfo()
    {
        PreviousState = new();
        CurrentState = Mouse.GetState();
    }

    /// <summary>
    ///     Updates the state of this instance of the <see cref="MouseInfo"/> class.
    /// </summary>
    /// <remarks>
    ///     This should only be called once per update cycle.
    /// </remarks>
    public void Update()
    {
        PreviousState = CurrentState;
        CurrentState = Mouse.GetState();
    }

    /// <summary>
    ///     Returns a value that indicates if the specified mouse button is currently pressed.
    /// </summary>
    /// <param name="button">
    ///     The <see cref="MouseButtons"/> value that represents the mouse button to check.
    /// </param>
    /// <returns>
    ///     <see langword="true"/> if the mouse button is currently pressed; otherwise, <see langword="false"/>.  
    /// </returns>
    public bool ButtonCheck(MouseButtons button) => button switch
    {
        MouseButtons.Left => ButtonCheck(b => b.LeftButton),
        MouseButtons.Middle => ButtonCheck(b => b.MiddleButton),
        MouseButtons.Right => ButtonCheck(b => b.RightButton),
        MouseButtons.XButton1 => ButtonCheck(b => b.XButton1),
        MouseButtons.XButton2 => ButtonCheck(b => b.XButton2),
        _ => throw new Exception($"{nameof(MouseInfo)}.{nameof(ButtonCheck)} encountered an unknown mouse button: {button}")
    };

    /// <summary>
    ///     Returns a value that indicates if the specified mouse button was just pressed.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         This only returns <see langword="true"/> on the first frame the specified mouse button was pressed.
    ///     </para>
    ///     <para>
    ///         This means if the mouse button is pressed down for repeated frames, it will only return
    ///         <see langword="true"/> on the first frame of the event and <see langword="false"/> for subsequent
    ///         frame.
    ///     </para>
    /// </remarks>
    /// <param name="button">
    ///     The <see cref="MouseButtons"/> value that represents the mouse button to check.
    /// </param>
    /// <returns>
    ///     <see langword="true"/> if the specified mouse button was just pressed; otherwise, <see langword="false"/>.
    /// </returns>
    public bool ButtonJustPressed(MouseButtons button) => button switch
    {
        MouseButtons.Left => ButtonJustPressed(b => b.LeftButton),
        MouseButtons.Middle => ButtonJustPressed(b => b.MiddleButton),
        MouseButtons.Right => ButtonJustPressed(b => b.RightButton),
        MouseButtons.XButton1 => ButtonJustPressed(b => b.XButton1),
        MouseButtons.XButton2 => ButtonJustPressed(b => b.XButton2),
        _ => throw new Exception($"{nameof(MouseInfo)}.{nameof(ButtonJustPressed)} encountered an unknown mouse button: {button}")
    };

    /// <summary>
    ///     Returns a value that indicates if the specified mouse button was just released.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         This only returns <see langword="true"/> on the first frame the specified mouse button was released.
    ///     </para>
    ///     <para>
    ///         This means if the mouse button is not pressed down for repeated frames, it will only return
    ///         <see langword="true"/> for the first frame after it was released and <see langword="false"/> for 
    ///         subsequent frames.
    ///     </para>
    /// </remarks>
    /// <param name="button">
    ///     The <see cref="MouseButtons"/> value that represents the mouse button to check.
    /// </param>
    /// <returns>
    ///     <see langword="true"/> if the specified mouse button was just released; otherwise, <see langword="false"/>.
    /// </returns>
    public bool ButtonJustReleased(MouseButtons button) => button switch
    {
        MouseButtons.Left => ButtonJustReleased(b => b.LeftButton),
        MouseButtons.Middle => ButtonJustReleased(b => b.MiddleButton),
        MouseButtons.Right => ButtonJustReleased(b => b.RightButton),
        MouseButtons.XButton1 => ButtonJustReleased(b => b.XButton1),
        MouseButtons.XButton2 => ButtonJustReleased(b => b.XButton2),
        _ => throw new Exception($"{nameof(MouseInfo)}.{nameof(ButtonJustReleased)} encountered an unknown mouse button: {button}")
    };

    private bool ButtonCheck(Func<MouseState, ButtonState> func) => func(CurrentState) == ButtonState.Pressed;
    private bool ButtonJustPressed(Func<MouseState, ButtonState> func) => func(CurrentState) == ButtonState.Pressed && func(PreviousState) == ButtonState.Released;
    private bool ButtonJustReleased(Func<MouseState, ButtonState> func) => func(CurrentState) == ButtonState.Released && func(PreviousState) == ButtonState.Pressed;
}