// Copyright (c) Christopher Whitley. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.

namespace TurtleEngine.Input;

using System.Diagnostics.CodeAnalysis;

/// <summary>
///     Defines a class that represents information about the state of input for a gamepad trigger.
/// </summary>
public readonly struct GamePadTriggerInfo : IEquatable<GamePadTriggerInfo>
{
    private readonly float _previousValue;
    private readonly float _currentValue;

    /// <summary>
    /// Initializes a new <see cref="GamePadTriggerInfo"/> value.
    /// </summary>
    public GamePadTriggerInfo()
    {
        _previousValue = default(float);
        _currentValue = default(float);
    }

    /// <summary>
    /// Initializes a new <see cref="GamePadTriggerInfo"/> value with the
    /// specified previous and current state values.
    /// </summary>
    /// <param name="previousValue">
    /// The value of this trigger during the previous frame.
    /// </param>
    /// <param name="currentValue">
    /// The value of this trigger during the current frame.
    /// </param>
    public GamePadTriggerInfo(float previousValue, float currentValue)
    {
        _previousValue = previousValue;
        _currentValue = currentValue;
    }

    /// <summary>
    ///Returns the value of the trigger during the previous frame.
    /// </summary>
    /// <param name="threshold">
    /// The minimum value the trigger must reach to be considered a non-zero
    /// value.
    /// </param>
    /// <returns>The value of the trigger during the previous frame.</returns>
    public readonly float PreviousValue(float threshold = default(float))
    {
        if (_previousValue >= threshold)
        {
            return _previousValue;
        }

        return default(float);
    }

    /// <summary>
    /// Returns the value of the trigger during the current frame.
    /// </summary>
    /// <param name="threshold">
    /// The minimum value the trigger must reach to be considered a non-zero
    /// value.
    /// </param>
    /// <returns>The value of the trigger during the current frame.</returns>
    public readonly float CurrentValue(float threshold = default(float))
    {
        if (_currentValue >= threshold)
        {
            return _currentValue;
        }

        return default(float);
    }

    /// <summary>
    /// Returns the difference in value of the trigger between the previous and
    /// current frames.
    /// </summary>
    /// <param name="threshold">
    /// The minimum threshold the trigger must reach to be considered a non-zero
    /// value.
    /// </param>
    /// <returns>
    /// The difference in value of the trigger between the previous and current
    /// frames.
    /// </returns>
    public readonly float DeltaValue(float threshold = default(float))
    {
        return PreviousValue(threshold) - CurrentValue(threshold);
    }

    /// <summary>
    /// Returns a value that indicates whether the trigger value changed between
    /// the previous and current frames.
    /// </summary>
    /// <param name="threshold">
    /// The minimum value the trigger must reach to be considered a non-zero
    /// value.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if the trigger value changed between the previous
    /// and current frames; otherwise, <see langword="false"/>.
    /// </returns>
    public readonly bool HasMoved(float threshold = default(float))
    {
        return DeltaValue(threshold) > float.Epsilon;
    }

    /// <summary>
    /// Returns a value that indicates whether the trigger is current down.
    /// </summary>
    /// <param name="threshold">
    /// The minimum value the trigger must reach to be considered a non-zero
    /// value.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if the trigger is currently down; otherwise,
    /// <see langword="false"/>.  This will return <see langword="true"/> for
    /// every frame the trigger is down.
    /// </returns>
    public readonly bool Check(float threshold = default(float))
    {
        return CurrentValue(threshold) > float.Epsilon;
    }

    /// <summary>
    /// Returns a value that indicates whether the trigger was just pressed
    /// </summary>
    /// <param name="threshold">
    /// The minimum value the trigger must reach to be considered a non-zero
    /// value.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if the trigger was just pressed; otherwise,
    /// <see langword="false"/>.  This only returns <see langword="true"/> on
    /// the first frame the trigger was pressed.
    /// </returns>
    public readonly bool Pressed(float threshold = default(float))
    {
        return Check(threshold) && PreviousValue(threshold) < float.Epsilon;
    }

    /// <summary>
    /// Returns a value that indicates whether the trigger was just released
    /// </summary>
    /// <param name="threshold">
    /// The minimum value the trigger must reach to be considered a non-zero
    /// value.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if the trigger was just released; otherwise,
    /// <see langword="false"/>.  This only returns <see langword="true"/> on
    /// the first frame the trigger was released.
    /// </returns>
    public readonly bool Released(float threshold = default(float))
    {
        return !Check(threshold) && PreviousValue(threshold) > float.Epsilon;
    }

    /// <inheritdoc/>
    public readonly bool Equals(GamePadTriggerInfo other)
    {
        return GetHashCode() == other.GetHashCode();
    }

    /// <inheritdoc />
    public override readonly bool Equals([NotNullWhen(true)] object? obj)
    {
        return obj is GamePadTriggerInfo other && Equals(other);
    }

    /// <inheritdoc />
    public override readonly int GetHashCode()
    {
        return HashCode.Combine(_previousValue.GetHashCode(), _currentValue.GetHashCode());
    }

    /// <inheritdoc />
    public static bool operator ==(GamePadTriggerInfo left, GamePadTriggerInfo right)
    {
        return left.Equals(right);
    }

    /// <inheritdoc />
    public static bool operator !=(GamePadTriggerInfo left, GamePadTriggerInfo right)
    {
        return !(left == right);
    }
}
