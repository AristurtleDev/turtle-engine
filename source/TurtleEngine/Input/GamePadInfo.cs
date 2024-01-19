// Copyright (c) Christopher Whitley. All rights reserved.
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace TurtleEngine.Input;

/// <summary>
/// Represents a snapshot of the state of a gamepad input during the previous
/// and current frame.
/// </summary>
public sealed class GamePadInfo
{
    private float _leftMotorSpeed;
    private float _rightMotorSpeed;
    private float _leftTriggerMotorSpeed;
    private float _rightTriggerMotorSpeed;
    private TimeSpan _leftMotorTimeRemaining = TimeSpan.Zero;
    private TimeSpan _rightMotorTimeRemaining = TimeSpan.Zero;
    private TimeSpan _leftTriggerMotorTimeRemaining = TimeSpan.Zero;
    private TimeSpan _rightTriggerMotorTimeRemaining = TimeSpan.Zero;

    /// <summary>
    /// Gets the index of the player this gamepad is for.
    /// </summary>
    public PlayerIndex PlayerIndex { get; }

    /// <summary>
    /// Gets the state of this gamepad during the previous frame.
    /// </summary>
    public GamePadState PreviousState { get; private set; }

    /// <summary>
    /// Gets the state of this gamepad during the current frame.
    /// </summary>
    public GamePadState CurrentState { get; private set; }

    /// <summary>
    /// Gets a value that defines whether this gamepad is attached.
    /// </summary>
    public bool IsAttached { get; private set; }

    /// <summary>
    /// Gets the state of the left thumb stick for this gamepad.
    /// </summary>
    public GamePadThumbStickInfo LeftThumbstick { get; private set; }

    /// <summary>
    /// Gets the state of the right thumbstick for this gamepad.
    /// </summary>
    public GamePadThumbStickInfo RightThumbstick { get; private set; }

    /// <summary>
    /// Gets the state of the left trigger for this gamepad.
    /// </summary>
    public GamePadTriggerInfo LeftTrigger { get; private set; }

    /// <summary>
    /// Gets the state of the right trigger for this gamepad.
    /// </summary>
    public GamePadTriggerInfo RightTrigger { get; private set; }

    internal GamePadInfo(PlayerIndex playerIndex)
    {
        PlayerIndex = playerIndex;
        PreviousState = default(GamePadState);
        CurrentState = default(GamePadState);
        LeftThumbstick = new GamePadThumbStickInfo();
        RightThumbstick = new GamePadThumbStickInfo();
        LeftTrigger = new GamePadTriggerInfo();
        RightTrigger = new GamePadTriggerInfo();
    }

    internal void Update(GameTime gameTime)
    {
        PreviousState = CurrentState;
        CurrentState = GamePad.GetState(PlayerIndex);
        LeftThumbstick.Update(CurrentState.ThumbSticks.Left);
        RightThumbstick.Update(CurrentState.ThumbSticks.Right);
        LeftTrigger.Update(CurrentState.Triggers.Left);
        RightTrigger.Update(CurrentState.Triggers.Right);
        IsAttached = CurrentState.IsConnected;
        UpdateVibration(gameTime);
    }

    private void UpdateVibration(GameTime gameTime)
    {
        UpdateLeftMotorVibration(gameTime);
        UpdateRightMotorVibration(gameTime);
        UpdateLeftTriggerMotorVibration(gameTime);
        UpdateRightTriggerMotorVibration(gameTime);
        InternalVibrate();
    }

    private void InternalVibrate()
    {
        GamePad.SetVibration(PlayerIndex, _leftMotorSpeed, _rightMotorSpeed, _leftTriggerMotorSpeed, _rightTriggerMotorSpeed);
    }

    private void UpdateLeftMotorVibration(GameTime gameTime)
    {
        if (_leftMotorTimeRemaining > TimeSpan.Zero)
        {
            _leftMotorTimeRemaining -= gameTime.ElapsedGameTime;
            if (_leftMotorTimeRemaining <= TimeSpan.Zero)
            {
                _leftMotorSpeed = 0.0f;
            }
        }
    }

    private void UpdateRightMotorVibration(GameTime gameTime)
    {
        if (_rightMotorTimeRemaining > TimeSpan.Zero)
        {
            _rightMotorTimeRemaining -= gameTime.ElapsedGameTime;
            if (_rightMotorTimeRemaining <= TimeSpan.Zero)
            {
                _rightMotorSpeed = 0.0f;
            }
        }
    }

    private void UpdateLeftTriggerMotorVibration(GameTime gameTime)
    {
        if (_leftTriggerMotorTimeRemaining > TimeSpan.Zero)
        {
            _leftTriggerMotorTimeRemaining -= gameTime.ElapsedGameTime;
            if (_leftTriggerMotorTimeRemaining <= TimeSpan.Zero)
            {
                _leftTriggerMotorSpeed = 0.0f;
            }
        }
    }

    private void UpdateRightTriggerMotorVibration(GameTime gameTime)
    {
        if (_rightTriggerMotorTimeRemaining > TimeSpan.Zero)
        {
            _rightTriggerMotorTimeRemaining -= gameTime.ElapsedGameTime;
            if (_rightTriggerMotorTimeRemaining <= TimeSpan.Zero)
            {
                _rightTriggerMotorSpeed = 0.0f;
            }
        }
    }

    /// <summary>
    /// Returns a value that indicates whether the specified button of this
    /// gamepad is currently down.
    /// </summary>
    /// <param name="button">The button to check.</param>
    /// <returns>
    /// <see langword="true"/> if the specified button of this gamepad is
    /// currently down; otherwise, <see langword="false"/>. This returns
    /// <see langword="true"/> for every frame the button is down.
    /// </returns>
    public bool Check(Buttons button) => CurrentState.IsButtonDown(button);

    /// <summary>
    /// Returns a value that indicates whether the specified button of this
    /// gamepad was just pressed.
    /// </summary>
    /// <param name="button">The button to check.</param>
    /// <returns>
    /// <see langword="true"/> if the specified button of this gamepad was just
    /// pressed; otherwise, <see langword="false"/>. This only returns
    /// <see langword="true"/> on the first frame the button was pressed.
    /// </returns>
    public bool Pressed(Buttons button) => Check(button) && PreviousState.IsButtonUp(button);

    /// <summary>
    /// Returns a value that indicates whether the specified button of this
    /// gamepad was just  released.
    /// </summary>
    /// <param name="button">The button to check.</param>
    /// <returns>
    /// <see langword="true"/> if the specified button of this gamepad was just
    /// released; otherwise, <see langword="false"/>. This only returns
    /// <see langword="true"/> on the first frame the button was released.
    /// </returns>
    public bool Released(Buttons button) => CurrentState.IsButtonUp(button) && PreviousState.IsButtonDown(button);

    /// <summary>
    /// Sets the vibration speed and duration of the left motor of this gamepad.
    /// </summary>
    /// <param name="speed">The speed, between 0 and 1, of the motor.</param>
    /// <param name="duration">The duration the motor should vibrate.</param>
    public void LeftMotorVibrate(float speed, TimeSpan duration)
    {
        _leftMotorSpeed = speed;
        _leftMotorTimeRemaining = duration;
        InternalVibrate();
    }

    /// <summary>
    /// Sets the vibration speed and duration of the right motor of this gamepad.
    /// </summary>
    /// <param name="speed">The speed, between 0 and 1, of the motor.</param>
    /// <param name="duration">The duration the motor should vibrate.</param>
    public void RightMotorVibrate(float speed, TimeSpan duration)
    {
        _rightMotorSpeed = speed;
        _rightMotorTimeRemaining = duration;
        InternalVibrate();
    }

    /// <summary>
    /// Sets the vibration speed and duration of the left trigger motor of this
    /// gamepad.
    /// </summary>
    /// <param name="speed">The speed, between 0 and 1, of the motor.</param>
    /// <param name="duration">The duration the motor should vibrate.</param>
    public void LeftTriggerVibrate(float speed, TimeSpan duration)
    {
        _leftTriggerMotorSpeed = speed;
        _leftTriggerMotorTimeRemaining = duration;
        InternalVibrate();
    }

    /// <summary>
    /// Sets the vibration speed and duration of the right trigger motor of this
    /// gamepad.
    /// </summary>
    /// <param name="speed">The speed, between 0 and 1, of the motor.</param>
    /// <param name="duration">The duration the motor should vibrate.</param>
    public void RightTriggerVibrate(float speed, TimeSpan duration)
    {
        _rightTriggerMotorSpeed = speed;
        _rightTriggerMotorTimeRemaining = duration;
        InternalVibrate();
    }

    /// <summary>
    /// Sets the vibration speed and duration of all motors of this gamepad.
    /// </summary>
    /// <param name="speed">The speed, between 0 and 1, of the motors.</param>
    /// <param name="duration">The duration the motors should vibrate.</param>
    public void Vibrate(float speed, TimeSpan duration)
    {
        _leftMotorSpeed = _rightMotorSpeed = _leftTriggerMotorSpeed = _rightTriggerMotorSpeed = speed;
        _leftMotorTimeRemaining = _rightMotorTimeRemaining = _leftTriggerMotorTimeRemaining = _rightTriggerMotorTimeRemaining = duration;
        InternalVibrate();
    }

    /// <summary>
    /// Tells the left motor of this gamepad to stop vibrating.
    /// </summary>
    public void StopLeftMotorVibration()
    {
        _leftMotorSpeed = 0.0f;
        _leftMotorTimeRemaining = TimeSpan.Zero;
        InternalVibrate();
    }

    /// <summary>
    /// Tells the right motor of this gamepad to stop vibrating.
    /// </summary>
    public void StopRightMotorVibration()
    {
        _rightMotorSpeed = 0.0f;
        _rightMotorTimeRemaining = TimeSpan.Zero;
        InternalVibrate();
    }

    /// <summary>
    /// Tells the left trigger motor of this gamepad to stop vibrating.
    /// </summary>
    public void StopLeftTriggerMotorVibration()
    {
        _leftTriggerMotorSpeed = 0.0f;
        _leftTriggerMotorTimeRemaining = TimeSpan.Zero;
        InternalVibrate();
    }

    /// <summary>
    /// Tells the right trigger motor of this gamepad to stop vibrating.
    /// </summary>
    public void StopRightTriggerMotorVibration()
    {
        _rightTriggerMotorSpeed = 0.0f;
        _rightTriggerMotorTimeRemaining = TimeSpan.Zero;
        InternalVibrate();
    }

    /// <summary>
    /// Tells all motors of this gamepad to stop vibrating.
    /// </summary>
    public void StopVibration()
    {
        _leftMotorSpeed = _rightMotorSpeed = _leftTriggerMotorSpeed = _rightTriggerMotorSpeed = 0.0f;
        _leftMotorTimeRemaining = _rightMotorTimeRemaining = _leftTriggerMotorTimeRemaining = _rightTriggerMotorTimeRemaining = TimeSpan.Zero;
        InternalVibrate();
    }
}
