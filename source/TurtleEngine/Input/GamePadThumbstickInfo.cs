using Microsoft.Xna.Framework;

namespace TurtleEngine.Input;

/// <summary>
///     Defines the information regarding a physical thumbstick on a physical gamepad.
/// </summary>
public sealed class GamePadThumbstickInfo
{
    private Vector2 _previousValue;
    private Vector2 _currentValue;

    internal GamePadThumbstickInfo()
    {
        _previousValue = Vector2.Zero;
        _currentValue = Vector2.Zero;
    }

    internal void Update(Vector2 value)
    {
        _previousValue = _currentValue;
        _currentValue = value;

        //  Flip the y-axis value
        _currentValue.Y = -_currentValue.Y;
    }

    /// <summary>
    ///     Returns the value of this thumbstick during the previous update cycle.
    /// </summary>
    /// <param name="deadZone">
    ///     The minimum value the thumbstick must exceed to be considered a non-zero value.  Default is <c>0.0f</c>.
    /// </param>
    /// <returns>
    ///     A <see cref="Microsoft.Xna.Framework.Vector2"/> value that represents the value of the x- and y-axes of this
    ///     thumbstick during the previous update cycle.
    /// </returns>
    public Vector2 PreviousValue(float deadZone = 0.0f)
    {
        if (_previousValue.LengthSquared() < deadZone * deadZone)
        {
            return Vector2.Zero;
        }

        return _previousValue;
    }

    /// <summary>
    ///     Returns the value of this thumbstick during the current update cycle.
    /// </summary>
    /// <param name="deadZone">
    ///     The minimum value the thumbstick must exceed to be considered a non-zero value.  Default is <c>0.0f</c>.
    /// </param>
    /// <returns>
    ///     A <see cref="Microsoft.Xna.Framework.Vector2"/> value that represents the value of the x- and y-axes of this
    ///     thumbstick during the current update cycle.
    /// </returns>
    public Vector2 CurrentValue(float deadZone = 0.0f)
    {
        if (_currentValue.LengthSquared() < deadZone * deadZone)
        {
            return Vector2.Zero;
        }

        return _currentValue;
    }

    /// <summary>
    ///     Returns the difference in the value of this thumbstick between the previous and current update cycle.
    /// </summary>
    /// <param name="deadZone">
    ///     The minimum value the thumbstick must exceed to be considered a non-zero value.  Default is <c>0.0f</c>.
    /// </param>
    /// <returns>
    ///     A <see cref="Microsoft.Xna.Framework.Vector2"/> value that represents the difference in the x- and y-axes 
    ///     values of this thumbstick between the previous and current update cycle.
    /// </returns>
    public Vector2 DeltaValue(float deadZone = 0.0f) => PreviousValue(deadZone) - CurrentValue(deadZone);

    /// <summary>
    ///     Returns a value that indicates if this thumbstick has moved between the previous and current update cycle.
    /// </summary>
    /// <param name="deadZone">
    ///     The minimum value the thumbstick must exceed to be considered a non-zero value.  Default is <c>0.0f</c>.
    /// </param>
    /// <returns>
    ///     <see langword="true"/> if this thumbstick has moved between the previous and current update cycle;
    ///     otherwise, <see langword="false"/>.
    /// </returns>
    public bool HasMoved(float deadZone = 0.0f) => DeltaValue(deadZone) != Vector2.Zero;

    /// <summary>
    ///     Returns a value that indicates if this thumbstick is currently pressed vertically upwards.
    /// </summary>
    /// <param name="deadZone">
    ///     The minimum value the thumbstick must exceed to be considered a non-zero value.  Default is <c>0.0f</c>.
    /// </param>
    /// <returns>
    ///     <see langword="true"/> if this thumbstick is currently pressed vertically upwards; otherwise, 
    ///     <see langword="false"/>.
    /// </returns>
    public bool CheckUp(float deadZone = 0.0f) => CurrentValue(deadZone).Y > float.Epsilon;

    /// <summary>
    ///     Returns a value that indicates if this thumbstick is currently pressed vertically downwards.
    /// </summary>
    /// <param name="deadZone">
    ///     The minimum value the thumbstick must exceed to be considered a non-zero value.  Default is <c>0.0f</c>.
    /// </param>
    /// <returns>
    ///     <see langword="true"/> if this thumbstick is currently pressed vertically downwards; otherwise, 
    ///     <see langword="false"/>.
    /// </returns>
    public bool CheckDown(float deadZone = 0.0f) => CurrentValue(deadZone).Y < float.Epsilon;

    /// <summary>
    ///     Returns a value that indicates if this thumbstick is currently pressed horizontally to the left
    /// </summary>
     /// <param name="deadZone">
    ///     The minimum value the thumbstick must exceed to be considered a non-zero value.  Default is <c>0.0f</c>.
    /// </param>
    /// <returns>
    ///     <see langword="true"/> if this thumbstick is currently pressed horizontally to the left; otherwise, 
    ///     <see langword="false"/>.
    /// </returns>
    public bool CheckLeft(float deadZone = 0.0f) => CurrentValue(deadZone).X < float.Epsilon;

    /// <summary>
    ///     Returns a value that indicates if this thumbstick is currently pressed horizontally to the right.
    /// </summary>
    /// <param name="deadZone">
    ///     The minimum value the thumbstick must exceed to be considered a non-zero value.  Default is <c>0.0f</c>.
    /// </param>
    /// <returns>
    ///     <see langword="true"/> if this thumbstick is currently pressed horizontally to the right; otherwise, 
    ///     <see langword="false"/>.
    /// </returns>
    public bool CheckRight(float deadZone = 0.0f) => CurrentValue(deadZone).X > float.Epsilon;

    /// <summary>
    ///     Returns a value that indicates if this thumbstick was just pressed vertically upwards.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         This only returns <see langword="true"/> on the first frame this thumbstick was pressed vertically 
    ///         upwards.
    ///     </para>
    ///     <para>
    ///         This means if the thumbstick is being held vertically upwards, it will only return 
    ///         <see langword="true"/> on the first frame of the event and <see langword="false"/> for subsequent
    ///         frames.
    ///     </para>
    /// </remarks>
    /// <param name="deadZone">
    ///     The minimum value the thumbstick must exceed to be considered a non-zero value.  Default is <c>0.0f</c>.
    /// </param>
    /// <returns>
    ///     <see langword="true"/> if this thumbstick was just pressed vertically upwards; otherwise,
    ///     <see langword="false"/>.
    /// </returns>
    public bool JustPressedUp(float deadZone = 0.0f) => CheckUp(deadZone) && PreviousValue(deadZone).Y <= float.Epsilon;

    /// <summary>
    ///     Returns a value that indicates if this thumbstick was just pressed vertically downwards.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         This only returns <see langword="true"/> on the first frame this thumbstick was pressed vertically 
    ///         downwards.
    ///     </para>
    ///     <para>
    ///         This means if the thumbstick is being held vertically downwards, it will only return 
    ///         <see langword="true"/> on the first frame of the event and <see langword="false"/> for subsequent
    ///         frames.
    ///     </para>
    /// </remarks>
    /// <param name="deadZone">
    ///     The minimum value the thumbstick must exceed to be considered a non-zero value.  Default is <c>0.0f</c>.
    /// </param>
    /// <returns>
    ///     <see langword="true"/> if this thumbstick was just pressed vertically downwards; otherwise,
    ///     <see langword="false"/>.
    /// </returns>
    public bool JustPressedDown(float deadZone = 0.0f) => CheckDown(deadZone) && PreviousValue(deadZone).Y >= float.Epsilon;

    /// <summary>
    ///     Returns a value that indicates if this thumbstick was just pressed horizontally to the left.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         This only returns <see langword="true"/> on the first frame this thumbstick was pressed horizontally to
    ///         the left.
    ///     </para>
    ///     <para>
    ///         This means if the thumbstick is being held horizontally to the left, it will only return
    ///         <see langword="true"/> on the first frame of the event and <see langword="false"/> for subsequent
    ///         frames.
    ///     </para>
    /// </remarks>
    /// <param name="deadZone">
    ///     The minimum value the thumbstick must exceed to be considered a non-zero value.  Default is <c>0.0f</c>.
    /// </param>
    /// <returns>
    ///     <see langword="true"/> if this thumbstick was just pressed horizontally to the left; otherwise,
    ///     <see langword="false"/>.
    /// </returns>
    public bool JustPressedLeft(float deadZone = 0.0f) => CheckLeft(deadZone) && PreviousValue(deadZone).X >= float.Epsilon;

    /// <summary>
    ///     Returns a value that indicates if this thumbstick was just pressed horizontally to the right.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         This only returns <see langword="true"/> on the first frame this thumbstick was pressed horizontally to
    ///         the right.
    ///     </para>
    ///     <para>
    ///         This means if the thumbstick is being held horizontally to the right, it will only return
    ///         <see langword="true"/> on the first frame of the event and <see langword="false"/> for subsequent
    ///         frames.
    ///     </para>
    /// </remarks>
    /// <param name="deadZone">
    ///     The minimum value the thumbstick must exceed to be considered a non-zero value.  Default is <c>0.0f</c>.
    /// </param>
    /// <returns>
    ///     <see langword="true"/> if this thumbstick was just pressed horizontally to the right; otherwise,
    ///     <see langword="false"/>.
    /// </returns>
    public bool JustPressedRight(float deadZone = 0.0f) => CheckRight(deadZone) && PreviousValue(deadZone).X <= float.Epsilon;


    /// <summary>
    ///     Returns a value that indicates if this thumbstick was just released from being pressed vertically upwards.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         This only returns <see langword="true"/> on the first frame this thumbstick was released from being
    ///         pressed vertically upwards.
    ///     </para>
    ///     <para>
    ///         This means if the thumbstick -was- being held vertically upwards and then released, this will only
    ///         return <see langword="true"/> on the first frame of the release event and <see langword="false"/> for
    ///         subsequent frames.
    ///     </para>
    /// </remarks>
    /// <param name="deadZone">
    ///     The minimum value the thumbstick must exceed to be considered a non-zero value.  Default is <c>0.0f</c>.
    /// </param>
    /// <returns>
    ///     <see langword="true"/> if this thumbstick was just released from being pressed vertically upwards; 
    ///     otherwise, <see langword="false"/>.
    /// </returns>
    public bool JustReleasedUp(float deadZone = 0.0f) => !CheckUp(deadZone) && PreviousValue(deadZone).Y >= float.Epsilon;

    /// <summary>
    ///     Returns a value that indicates if this thumbstick was just released from being pressed vertically downwards.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         This only returns <see langword="true"/> on the first frame this thumbstick was released from being
    ///         pressed vertically downwards.
    ///     </para>
    ///     <para>
    ///         This means if the thumbstick -was- being held vertically downwards and then released, this will only
    ///         return <see langword="true"/> on the first frame of the release event and <see langword="false"/> for
    ///         subsequent frames.
    ///     </para>
    /// </remarks>
    /// <param name="deadZone">
    ///     The minimum value the thumbstick must exceed to be considered a non-zero value.  Default is <c>0.0f</c>.
    /// </param>
    /// <returns>
    ///     <see langword="true"/> if this thumbstick was just released from being pressed vertically downwards; 
    ///     otherwise, <see langword="false"/>.
    /// </returns>
    public bool JustReleasedDown(float deadZone = 0.0f) => !CheckDown(deadZone) && PreviousValue(deadZone).Y <= float.Epsilon;

    /// <summary>
    ///     Returns a value that indicates if this thumbstick was just released from being pressed horizontally to the
    ///     left.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         This only returns <see langword="true"/> on the first frame this thumbstick was released from being
    ///         pressed horizontally to the left.
    ///     </para>
    ///     <para>
    ///         This means if the thumbstick -was- being held horizontally to the left and then released, this will only
    ///         return <see langword="true"/> on the first frame of the release event and <see langword="false"/> for
    ///         subsequent frames.
    ///     </para>
    /// </remarks>
    /// <param name="deadZone">
    ///     The minimum value the thumbstick must exceed to be considered a non-zero value.  Default is <c>0.0f</c>.
    /// </param>
    /// <returns>
    ///     <see langword="true"/> if this thumbstick was just released from being pressed horizontally to the left; 
    ///     otherwise, <see langword="false"/>.
    /// </returns>
    public bool JustReleasedLeft(float deadZone = 0.0f) => !CheckLeft(deadZone) && PreviousValue(deadZone).X <= float.Epsilon;

    /// <summary>
    ///     Returns a value that indicates if this thumbstick was just released from being pressed horizontally to the
    ///     right.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         This only returns <see langword="true"/> on the first frame this thumbstick was released from being
    ///         pressed horizontally to the right.
    ///     </para>
    ///     <para>
    ///         This means if the thumbstick -was- being held horizontally to the right and then released, this will 
    ///         only return <see langword="true"/> on the first frame of the release event and <see langword="false"/> 
    ///         for subsequent frames.
    ///     </para>
    /// </remarks>
    /// <param name="deadZone">
    ///     The minimum value the thumbstick must exceed to be considered a non-zero value.  Default is <c>0.0f</c>.
    /// </param>
    /// <returns>
    ///     <see langword="true"/> if this thumbstick was just released from being pressed horizontally to the right; 
    ///     otherwise, <see langword="false"/>.
    /// </returns>
    public bool JustReleasedRight(float deadZone = 0.0f) => !CheckRight(deadZone) && PreviousValue(deadZone).X >= float.Epsilon;
}