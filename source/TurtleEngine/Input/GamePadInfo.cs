using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace TurtleEngine.Input;

/// <summary>
///     Defines the information regarding gamepad input.
/// </summary>
public sealed class GamePadInfo
{
    private PlayerIndex _playerIndex;

    private float _leftMotorStrength = 0.0f;
    private float _rightMotorStrength = 0.0f;
    private float _leftTriggerMotorStrength = 0.0f;
    private float _rightTriggerMotorStrength = 0.0f;
    private TimeSpan _leftMotorTimeRemaining = TimeSpan.Zero;
    private TimeSpan _rightMotorTimeRemaining = TimeSpan.Zero;
    private TimeSpan _leftTriggerMotorTimeRemaining = TimeSpan.Zero;
    private TimeSpan _rightTriggerMotorTimeRemaining = TimeSpan.Zero;

    /// <summary>
    ///     Gets the state of input for this gamepad during the previous update cycle.
    /// </summary>
    /// <value>
    ///     A <see cref="Microsoft.Xna.Framework.Input.GamePadState"/> value that represents the state of gamepad input
    ///     input for this gamepad during the previous update cycle.
    /// </value>
    public GamePadState PreviousState { get; set; }

    /// <summary>
    ///     Gets the state of input for this gamepad during the current update cycle.
    /// </summary>
    /// <value>
    ///     A <see cref="Microsoft.Xna.Framework.Input.GamePadState"/> value that represents the state of gamepad input
    ///     input for this gamepad during the current update cycle.
    /// </value>
    public GamePadState CurrentState { get; set; }

    /// <summary>
    ///     Gets a value that indicates if this gamepad is currently attached.
    /// </summary>
    /// <value>
    ///     <see langword="true"/> if this gamepad is currently attached; otherwise, <see langword="false"/>.
    /// </value>
    public bool IsAttached { get; private set; }

    /// <summary>
    ///     Gets the <see cref="GamePadThumbstickInfo"/> related to the left thumbstick of this gamepad.
    /// </summary>
    /// <value>
    ///     An instance of the <see cref="GamePadThumbstickInfo"/> class containing information related to the left 
    ///     thumbstick of this gamepad.
    /// </value>
    public GamePadThumbstickInfo LeftThumbstick { get; private set; }

    /// <summary>
    ///     Gets the information related to the right thumbstick of this gamepad.
    /// </summary>
    /// <value>
    ///     An instance of the <see cref="GamePadThumbstickInfo"/> class containing information related to the right 
    ///     thumbstick of this gamepad.
    /// </value>
    public GamePadThumbstickInfo RightThumbstick { get; private set; }

    /// <summary>
    ///     Gets the information related to the left trigger of this gamepad.
    /// </summary>
    /// <value>
    ///     An instance of the <see cref="GamePadTriggerInfo"/> class containing information related to the left trigger
    ///     of this gamepad.
    /// </value>
    public GamePadTriggerInfo LeftTrigger { get; private set; }

    /// <summary>
    ///     Gets the information related to the right trigger of this gamepad.
    /// </summary>
    /// <value>
    ///     An instance of the <see cref="GamePadTriggerInfo"/> class containing information related to the right
    ///     trigger of this gamepad.
    /// </value>
    public GamePadTriggerInfo RightTrigger { get; private set; }

    /// <summary>
    ///     Initializes a new instance of the <see cref="GamePadInfo"/> class.
    /// </summary>
    /// <param name="playerIndex">
    ///     A <see cref="Microsoft.Xna.Framework.PlayerIndex"/> value that represents the player this
    ///     <see cref="GamePadInfo"/> is for.
    /// </param>
    public GamePadInfo(PlayerIndex playerIndex)
    {
        _playerIndex = playerIndex;
        PreviousState = new();
        CurrentState = GamePad.GetState(playerIndex);

        LeftThumbstick = new();
        RightThumbstick = new();
        LeftTrigger = new();
        RightTrigger = new();
    }

    /// <summary>
    ///     Updates the state of this instance of the <see cref="GamePadInfo"/> class.
    /// </summary>
    /// <remarks>
    ///     This should only be called once per update cycle.
    /// </remarks>
    public void Update(GameTime gameTime)
    {
        PreviousState = CurrentState;
        CurrentState = GamePad.GetState(_playerIndex);
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
        SetVibration();
    }

    private void UpdateLeftMotorVibration(GameTime gameTime)
    {
        if (_leftMotorTimeRemaining > TimeSpan.Zero)
        {
            _leftMotorTimeRemaining -= gameTime.ElapsedGameTime;
            if (_leftMotorTimeRemaining <= TimeSpan.Zero)
            {
                _leftMotorStrength = 0.0f;
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
                _rightMotorStrength = 0.0f;
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
                _leftTriggerMotorStrength = 0.0f;
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
                _rightTriggerMotorStrength = 0.0f;
            }
        }
    }

    /// <summary>
    ///     Returns a value that indicates if the specified gamepad button is currently pressed.
    /// </summary>
    /// <param name="button">
    ///     The <see cref="Microsoft.Xna.Framework.Input.Buttons"/> value that represents the gamepad button to check.
    /// </param>
    /// <returns>
    ///     <see langword="true"/> if the gamepad button is pressed; otherwise, <see langword="false"/>.  
    /// </returns>
    public bool ButtonCheck(Buttons button) => CurrentState.IsButtonDown(button);

    /// <summary>
    ///     Returns a value that indicates if the specified gamepad button was just pressed.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         This only return <see langword="true"/> on the first frame the specified gamepad button was pressed.
    ///     </para>
    ///     <para>
    ///         This  means if the gamepad button is being held down, it will only return <see langword="true"/> for the
    ///         first frame of the event and <see langword="false"/> for repeated frames.
    ///     </para>
    /// </remarks>
    /// <param name="button">
    ///     The <see cref="Microsoft.Xna.Framework.Input.Buttons"/> value that represents the gamepad button to check.
    /// </param>
    /// <returns>
    ///     <see langword="true"/> if the specified gamepad button was just pressed; otherwise, <see langword="false"/>.
    /// </returns>
    public bool ButtonJustPressed(Buttons button) => ButtonCheck(button) && PreviousState.IsButtonUp(button);

    /// <summary>
    ///     Returns a value that indicates if the specified gamepad button was just released.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         This only return <see langword="true"/> on the first frame the specified gamepad button was released.
    ///     </para>
    ///     <para>
    ///         This means if the specified gamepad button -was- pressed and then released, this will only return
    ///         <see langword="true"/> during the first frame of the release event and <see langword="false"/> for
    ///         subsequent frames.
    ///     </para>
    /// </remarks>
    /// <param name="button">
    ///     The <see cref="Microsoft.Xna.Framework.Input.Buttons"/> value that represents the gamepad button to check.
    /// </param>
    /// <returns>
    ///     <see langword="true"/> if the specified gamepad button was just released; otherwise, 
    ///     <see langword="false"/>.
    /// </returns>
    public bool ButtonJustReleased(Buttons button) => CurrentState.IsButtonUp(button) && PreviousState.IsButtonDown(button);

    /// <summary>
    ///     Set the vibration of the left motor in this gamepad.
    /// </summary>
    /// <param name="strength">
    ///     The speed of the left motor, between <c>0.0f</c> and <c>1.0f</c>.
    /// </param>
    /// <param name="time">
    ///     The amount of time this motor should vibrate.
    /// </param>
    public void SetLeftMotorVibration(float strength, TimeSpan time)
    {
        _leftMotorStrength = strength;
        _leftMotorTimeRemaining = time;
        SetVibration();
    }

    /// <summary>
    ///     Set the vibration of the right motor in this gamepad.
    /// </summary>
    /// <param name="strength">
    ///     The speed of the right motor, between <c>0.0f</c> and <c>1.0f</c>.
    /// </param>
    /// <param name="time">
    ///     The amount of time this motor should vibrate.
    /// </param>
    public void SetRightMotorVibration(float strength, TimeSpan time)
    {
        _rightMotorStrength = strength;
        _rightMotorTimeRemaining = time;
        SetVibration();
    }

    /// <summary>
    ///     Set the vibration of the left trigger motor in this gamepad.
    /// </summary>
    /// <param name="strength">
    ///     The speed of the left trigger motor, between <c>0.0f</c> and <c>1.0f</c>.
    /// </param>
    /// <param name="time">
    ///     The amount of time this motor should vibrate.
    /// </param>
    public void SetLeftTriggerMotorVibration(float strength, TimeSpan time)
    {
        _leftTriggerMotorStrength = strength;
        _leftTriggerMotorTimeRemaining = time;
        SetVibration();
    }

    /// <summary>
    ///     Set the vibration of the right trigger motor in this gamepad.
    /// </summary>
    /// <param name="strength">
    ///     The speed of the right trigger motor, between <c>0.0f</c> and <c>1.0f</c>.
    /// </param>
    /// <param name="time">
    ///     The amount of time this motor should vibrate.
    /// </param>
    public void SetRightTriggerMotorVibration(float strength, TimeSpan time)
    {
        _rightTriggerMotorStrength = strength;
        _rightTriggerMotorTimeRemaining = time;
        SetVibration();
    }

    /// <summary>
    ///     Sets the vibration of all motors in this gamepad.
    /// </summary>
    /// <param name="strength">
    ///     The speed of the motors, between <c>0.0f</c> and <c>1.0f</c>
    /// </param>
    /// <param name="time">
    ///     The amount of time the motors should vibrate.
    /// </param>
    public void SetVibration(float strength, TimeSpan time)
    {
        _leftMotorStrength = _rightMotorStrength = _leftTriggerMotorStrength = _rightTriggerMotorStrength = strength;
        _leftMotorTimeRemaining = _rightMotorTimeRemaining = _leftTriggerMotorTimeRemaining = _rightTriggerMotorTimeRemaining = time;
        SetVibration();
    }

    private void SetVibration()
    {
        GamePad.SetVibration(_playerIndex, _leftMotorStrength, _rightMotorStrength, _leftTriggerMotorStrength, _rightTriggerMotorStrength);
    }

    /// <summary>
    ///     Stops vibration of the left motor in this gamepad.
    /// </summary>
    public void StopLeftMotorVibration()
    {
        _leftMotorStrength = 0.0f;
        _leftMotorTimeRemaining = TimeSpan.Zero;
        SetVibration();
    }

    /// <summary>
    ///     Stops vibration of the right motor in this gamepad.
    /// </summary>
    public void StopRightMotorVibration()
    {
        _rightMotorStrength = 0.0f;
        _rightMotorTimeRemaining = TimeSpan.Zero;
        SetVibration();
    }

    /// <summary>
    ///     Stops vibration of the left trigger motor in this gamepad.
    /// </summary>
    public void StopLeftTriggerMotorVibration()
    {
        _leftTriggerMotorStrength = 0.0f;
        _leftTriggerMotorTimeRemaining = TimeSpan.Zero;
        SetVibration();
    }

    /// <summary>
    ///     Stops vibration of the right trigger motor in this gamepad.
    /// </summary>
    public void StopRightTriggerMotorVibration()
    {
        _rightTriggerMotorStrength = 0.0f;
        _rightTriggerMotorTimeRemaining = TimeSpan.Zero;
        SetVibration();
    }

    /// <summary>
    ///     Stops vibration of all motors in this gamepad.
    /// </summary>
    public void StopVibration()
    {
        _leftMotorStrength = _rightMotorStrength = _leftTriggerMotorStrength = _rightTriggerMotorStrength = 0.0f;
        _leftMotorTimeRemaining = _rightMotorTimeRemaining = _leftTriggerMotorTimeRemaining = _rightTriggerMotorTimeRemaining = TimeSpan.Zero;
        SetVibration();
    }
}