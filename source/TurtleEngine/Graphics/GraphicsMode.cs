namespace TurtleEngine;

/// <summary>
///     Defines the values that represent the graphics mode to render at.
/// </summary>
public enum GraphicsMode
{
    /// <summary>
    ///     Defines that graphics will render using the independent screen resolution mode and force letter or pillar
    ///     boxes when the virtual rendering resolution and actual rendering resolution are not 1:1
    /// </summary>
    IndependentScreenResolution,

    /// <summary>
    ///     Defines that the graphics will render stretched when the virtual rendering resolution and actual rendering
    ///     resolution are not 1:1 in order to fill the entire game window.
    /// </summary>
    Stretch
}