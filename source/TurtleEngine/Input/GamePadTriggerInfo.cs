namespace TurtleEngine.Input;

/// <summary>
///     Defines the information regarding a physical trigger on a physical gamepad.
/// </summary>
public sealed class GamePadTriggerInfo
{
    private float _previousValue;
    private float _currentValue;

    internal GamePadTriggerInfo()
    {
        _previousValue = 0.0f;
        _currentValue = 0.0f;
    }

    internal void Update(float value)
    {
        _previousValue = _currentValue;
        _currentValue = value;
    }

    /// <summary>
    ///     Returns the value of this trigger during the previous update cycle.
    /// </summary>
    /// <param name="threshold">
    ///     The minimum value this trigger must reach to be considered a non-zero value.  Default is <c>0.0f</c>.
    /// </param>
    /// <returns>
    ///     The value of this trigger as <see cref="float"/>.
    /// </returns>
    public float PreviousValue(float threshold = 0.0f)
    {
        if (_previousValue < threshold)
        {
            return 0.0f;
        }

        return _previousValue;
    }

    /// <summary>
    ///     Returns the value of this trigger during the current update cycle.
    /// </summary>
    /// <param name="threshold">
    ///     The minimum value this trigger must reach to be considered a non-zero value.  Default is <c>0.0f</c>.
    /// </param>
    /// <returns>
    ///     The value of this trigger as a <see cref="float"/>.
    /// </returns>
    public float CurrentValue(float threshold = 0.0f)
    {
        if (_currentValue < threshold)
        {
            return 0.0f;
        }

        return _currentValue;
    }

    /// <summary>
    ///     Returns the difference in value of this trigger between the previous and current update cycle.
    /// </summary>
    /// <param name="threshold">
    ///     The minimum value this trigger must reach to be considered a non-zero value.  Default is <c>0.0f</c>.
    /// </param>
    /// <returns>
    ///     The difference in value of this trigger between the previous and current update cycle as a
    ///     <see cref="float"/>.
    /// </returns>
    public float DeltaValue(float threshold = 0.0f) => PreviousValue(threshold) - CurrentValue(threshold);

    /// <summary>
    ///     Returns a value that indicates whether this trigger has moved between the previous and current update cycle.
    /// </summary>
    /// <param name="threshold">
    ///     The minimum value this trigger must reach to be considered a non-zero value.  Default is <c>0.0f</c>.
    /// </param>
    /// <returns>
    ///     <see langword="true"/> if this trigger has moved; otherwise, <see langword="false"/>.
    /// </returns>
    public bool HasMoved(float threshold) => DeltaValue(threshold) > float.Epsilon;

    /// <summary>
    ///     Returns a value that indicates if this trigger is current pressed down.
    /// </summary>
    /// <param name="threshold">
    ///     The minimum value this trigger must reach to be considered a non-zero value.  Default is <c>0.0f</c>.
    /// </param>
    /// <returns>
    ///     <see langword="true"/> if this trigger is currently pressed down; otherwise, <see langword="false"/>.
    /// </returns>
    public bool Check(float threshold = 0.0f) => CurrentValue(threshold) > float.Epsilon;

    /// <summary>
    ///     Returns a value that indicates if this trigger was just pressed.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         This only returns <see langword="true"/> on the first frame this trigger was pressed.
    ///     </para>
    ///     <para>
    ///         This means if the trigger is being held down, this will only return <see langword="true"/> during the
    ///         first frame of the event and <see langword="false"/> for subsequent frames.
    ///     </para>
    /// </remarks>
    /// <param name="threshold">
    ///     The minimum value this trigger must reach to be considered a non-zero value.  Default is <c>0.0f</c>.
    /// </param>
    /// <returns>
    ///     <see langword="true"/> if this trigger was just pressed; otherwise, <see langword="false"/>.
    /// </returns>
    public bool JustPressed(float threshold = 0.0f) => Check(threshold) && PreviousValue(threshold) < float.Epsilon;

    /// <summary>
    ///     Returns a value that indicates if this trigger was just released.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         This only return <see langword="true"/> on the first frame this trigger was released.
    ///     </para>
    ///     <para>
    ///         This means if the trigger -was- pressed down and then released, this will only return
    ///         <see langword="true"/> on the first frame of the release event and <see langword="false"/> for
    ///         subsequent frames.
    ///     </para>
    /// </remarks>
    /// <param name="threshold">
    ///     The minimum value this trigger must reach to be considered a non-zero value.  Default is <c>0.0f</c>.
    /// </param>
    /// <returns>
    ///     <see langword="true"/> if this trigger was just released; otherwise, <see langword="false"/>.
    /// </returns>
    public bool JustReleased(float threshold = 0.0f) => !Check(threshold) && PreviousValue(threshold) > float.Epsilon;
}