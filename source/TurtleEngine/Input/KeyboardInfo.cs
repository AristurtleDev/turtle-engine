using Microsoft.Xna.Framework.Input;

namespace TurtleEngine.Input;

/// <summary>
///     Defines the information regarding keyboard input.
/// </summary>
public sealed class KeyboardInfo
{
    /// <summary>
    ///     Gets the state of keyboard input during the previous update cycle.
    /// </summary>
    /// <value>
    ///     A <see cref="Microsoft.Xna.Framework.Input.KeyboardState"/> value that represents the state of keyboard
    ///     input during the previous update cycle.
    /// </value>
    public KeyboardState PreviousState { get; private set; }

    /// <summary>
    ///     Gets the state of keyboard input during the current update cycle.
    /// </summary>
    /// <value>
    ///     A <see cref="Microsoft.Xna.Framework.Input.KeyboardState"/> value that represents the state of keyboard
    ///     input during the current update cycle.
    /// </value>
    public KeyboardState CurrentState { get; private set; }

    /// <summary>
    ///     Initializes a new instance of the <see cref="KeyboardInfo"/> class.
    /// </summary>
    public KeyboardInfo()
    {
        PreviousState = new();
        CurrentState = Keyboard.GetState();
    }

    /// <summary>
    ///     Updates the state of this instance of the <see cref="KeyboardInfo"/> class.
    /// </summary>
    /// <remarks>
    ///     This should only be called once per update cycle.
    /// </remarks>
    public void Update()
    {
        PreviousState = CurrentState;
        CurrentState = Keyboard.GetState();
    }

    /// <summary>
    ///     Returns a value that indicates if the specified keyboard key is currently pressed.
    /// </summary>
    /// <param name="key">
    ///     The <see cref="Microsoft.Xna.Framework.Input.Keys"/> value that represents the keyboard key to check.
    /// </param>
    /// <returns>
    ///     <see langword="true"/> if the specified keyboard key is currently pressed; otherwise, 
    ///     <see langword="false"/>.
    /// </returns>
    public bool KeyCheck(Keys key) => CurrentState.IsKeyDown(key);

    /// <summary>
    ///     Returns a value that indicates if the specified keyboard key was just pressed.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         This only returns <see langword="true"/> on the first frame the specified keyboard key was pressed.  
    ///     </para>
    ///     <para>
    ///         This means if the keyboard key is pressed down for repeated frames, it will only return 
    ///         <see langword="true"/> on the first frame of the event and <see langword="false"/> for subsequent 
    ///         frames.
    ///     </para>
    /// </remarks>
    /// <param name="key">
    ///     The <see cref="Microsoft.Xna.Framework.Input.Keys"/> value that represents the keyboard key to check.
    /// </param>
    /// <returns>
    ///     <see langword="true"/> if the specified keyboard key was just pressed; otherwise, <see langword="false"/>.
    /// </returns>
    public bool KeyJustPressed(Keys key) => KeyCheck(key) && PreviousState.IsKeyUp(key);

    /// <summary>
    ///     Returns a value that indicates if the specified keyboard key was just released.
    /// </summary>
    /// <param name="key">
    ///     <para>
    ///         This only returns <see langword="true"/> on the first frame the specified keyboard key was released.
    ///     </para>
    ///     <para>
    ///         This means if the keyboard key is not pressed down for repeated frames, it will only return
    ///         <see langword="true"/> for the first frame after it was released and <see langword="false"/> for
    ///         subsequent frames.
    ///     </para>
    /// </param>
    /// <returns></returns>
    public bool KeyJustReleased(Keys key) => !KeyCheck(key) && PreviousState.IsKeyDown(key);
}