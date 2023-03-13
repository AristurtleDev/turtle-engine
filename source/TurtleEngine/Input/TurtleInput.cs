using Microsoft.Xna.Framework;

namespace TurtleEngine.Input;

/// <summary>
///     Provides information on the state if input devices.
/// </summary>
public sealed class TurtleInput
{
    /// <summary>
    ///     Gets or Sets a value that indicates if this instance of the <see cref="TurtleInput"/> class is enabled.
    /// </summary>
    /// <remarks>
    ///     When <see langword="false"/>, the <see cref="Update(GameTime)"/> method for this instance of the 
    ///     <see cref="TurtleInput"/> class will not be called.
    /// </remarks>
    /// <value>
    ///     <see langword="true"/> if this instance fo the <see cref="TurtleInput"/> class is enabled; otherwise,
    ///     <see langword="false"/>.
    /// </value>
    public bool Enabled { get; set; } = true;

    /// <summary>
    ///     Gets the state of keyboard input.
    /// </summary>
    /// <value>
    ///     An instance of the <see cref="KeyboardInfo"/> class containing the state of keyboard input.
    /// </value>
    public KeyboardInfo Keyboard { get; }

    /// <summary>
    ///     Gets the state of mouse input.
    /// </summary>
    /// <value>
    ///     An instance of the <see cref="MouseInfo"/> class containing the state of mouse input.
    /// </value>
    public MouseInfo Mouse { get; }

    /// <summary>
    ///     Gets the state of input for each gamepad.
    /// </summary>
    /// <value>
    ///     An array of the <see cref="GamePadInfo"/> classes containing the state of input for each gamepad.
    /// </value>
    public GamePadInfo[] GamePads { get; }

    /// <summary>
    ///     Creates a new instance of the <see cref="TurtleInput"/> class.
    /// </summary>
    public TurtleInput()
    {
        Keyboard = new();
        Mouse = new();
        GamePads = new GamePadInfo[4];
        for (int i = 0; i < 4; i++)
        {
            GamePads[i] = new((PlayerIndex)i);
        }
    }

    /// <summary>
    ///     Updates this instance of the <see cref="TurtleInput"/> class.
    /// </summary>
    /// <param name="gameTime">
    ///     A snapshot of the game timing values.
    /// </param>
    public void Update(GameTime gameTime)
    {
        if (!Enabled)
        {
            return;
        }

        Keyboard.Update();
        Mouse.Update();
        for (int i = 0; i < GamePads.Length; i++)
        {
            GamePads[i].Update(gameTime);
        }
    }
}