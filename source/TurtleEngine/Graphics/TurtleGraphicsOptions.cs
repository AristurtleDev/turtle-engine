/* ----------------------------------------------------------------------------
MIT License

Copyright (c) 2023 Christopher Whitley

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
---------------------------------------------------------------------------- */

using Microsoft.Xna.Framework.Graphics;

namespace TurtleEngine.Graphics;

/// <summary>
///     Defines the values used to initialize an instance of the  <see cref="Graphics"/> class
/// </summary>
/// <param name="SynchronizeWithVerticalRetrace">
///     Indicates whether vsync should be used.
/// </param>
/// <param name="PreferMultiSampling">
///     Indicates whether multi-sampling should be used for the back buffer.
/// </param>
/// <param name="GraphicsProfile">
///     Indicates the <see cref="Microsoft.Xna.Framework.Graphics.GraphicsProfile"/> value to use.
/// </param>
/// <param name="PreferredBackBufferFormat">
///     indicates the desired <see cref="Microsoft.Xna.Framework.Graphics.SurfaceFormat"/> value to use.
/// </param>
/// <param name="PreferredDepthStencilFormat">
///     Indicates the desired <see cref="Microsoft.Xna.Framework.Graphics.DepthFormat"/> value to use.
/// </param>
/// <param name="AllowUserResizeWindow">
///     Indicates whether users should be able to resize the game window.
/// </param>
/// <param name="IsMouseVisible">
///     Indicates whether the mouse should be visible on the game window.
/// </param>
public record TurtleGraphicsOptions(bool SynchronizeWithVerticalRetrace = true,
                                    bool PreferMultiSampling = false,
                                    GraphicsProfile GraphicsProfile = GraphicsProfile.HiDef,
                                    SurfaceFormat PreferredBackBufferFormat = SurfaceFormat.Color,
                                    DepthFormat PreferredDepthStencilFormat = DepthFormat.Depth24Stencil8,
                                    bool AllowUserResizeWindow = false,
                                    bool IsMouseVisible = true);
