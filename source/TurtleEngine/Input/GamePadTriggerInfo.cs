// Copyright (c) Christopher Whitley. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.

namespace Aristurtle.TurtleEngine.Input;

/// <summary>
///     Represents a snapshot of the state of a gamepad trigger during the previous and current frame.
/// </summary>
public sealed class GamePadTriggerInfo
{
    private float _previousValue;
    private float _currentValue;

    internal GamePadTriggerInfo()
    {
        _previousValue = default(float);
        _currentValue = default(float);
    }

    internal void Update(float value)
    {
        _previousValue = _currentValue;
        _currentValue = value;
    }

    /// <summary>
    ///     Returns the value of the trigger during the previous frame.
    /// </summary>
    /// <param name="threshold">
    ///     The minimum value the trigger must reach to be considered a non-zero value.
    /// </param>
    /// <returns>
    ///     The value of the trigger during the previous frame.
    /// </returns>
    public float PreviousValue(float threshold = default(float))
    {
        if (_previousValue >= threshold)
        {
            return _previousValue;
        }

        return default(float);
    }

    /// <summary>
    ///     Returns the value of the trigger during the current frame.
    /// </summary>
    /// <param name="threshold">
    ///     The minimum value the trigger must reach to be considered a non-zero value.
    /// </param>
    /// <returns>
    ///     The value of the trigger during the current frame.
    /// </returns>
    public float CurrentValue(float threshold = default(float))
    {
        if (_currentValue >= threshold)
        {
            return _currentValue;
        }

        return default(float);
    }

    /// <summary>
    ///     Returns the difference in value of the trigger between the previous and current frames.
    /// </summary>
    /// <param name="threshold">
    ///     The minimum threshold the trigger must reach to be considered a non-zero value.
    /// </param>
    /// <returns>
    ///     The difference in value of the trigger between the previous and current frames.
    /// </returns>
    public float DeltaValue(float threshold = default(float))
    {
        return PreviousValue(threshold) - CurrentValue(threshold);
    }

    /// <summary>
    ///     Returns a value that indicates whether the trigger value changed between the previous and current frames.
    /// </summary>
    /// <param name="threshold">
    ///     The minimum value the trigger must reach to be considered a non-zero value.
    /// </param>
    /// <returns>
    ///     <see langword="true"/> if the trigger value changed between the previous and current frames; otherwise,
    ///     <see langword="false"/>.
    /// </returns>
    public bool HasMoved(float threshold = default(float))
    {
        return DeltaValue(threshold) > float.Epsilon;
    }

    /// <summary>
    ///     Returns a value that indicates whether the trigger is current down.
    /// </summary>
    /// <param name="threshold">
    ///     The minimum value the trigger must reach to be considered a non-zero value.
    /// </param>
    /// <returns>
    ///     <see langword="true"/> if the trigger is currently down; otherwise, <see langword="false"/>.  This will
    ///     return <see langword="true"/> for every frame the trigger is down.
    /// </returns>
    public bool Check(float threshold = default(float))
    {
        return CurrentValue(threshold) > float.Epsilon;
    }

    /// <summary>
    ///     Returns a value that indicates whether the trigger was just pressed
    /// </summary>
    /// <param name="threshold">
    ///     The minimum value the trigger must reach to be considered a non-zero value.
    /// </param>
    /// <returns>
    ///     <see langword="true"/> if the trigger was just pressed; otherwise, <see langword="false"/>.  This only
    ///     returns <see langword="true"/> on the first frame the trigger was pressed.
    /// </returns>
    public bool Pressed(float threshold = default(float))
    {
        return Check(threshold) && PreviousValue(threshold) < float.Epsilon;
    }

    /// <summary>
    ///     Returns a value that indicates whether the trigger was just released
    /// </summary>
    /// <param name="threshold">
    ///     The minimum value the trigger must reach to be considered a non-zero value.
    /// </param>
    /// <returns>
    ///     <see langword="true"/> if the trigger was just released; otherwise, <see langword="false"/>.  This only
    ///     returns <see langword="true"/> on the first frame the trigger was released.
    /// </returns>
    public bool Released(float threshold = default(float))
    {
        return !Check(threshold) && PreviousValue(threshold) > float.Epsilon;
    }
}
