// using Microsoft.Xna.Framework;
// using Microsoft.Xna.Framework.Content;
// using Microsoft.Xna.Framework.Graphics;

// using TurtleEngine.Graphics;
// using TurtleEngine.Input;

// namespace TurtleEngine;

// public abstract class Scene
// {
//     public TurtleInput Input { get; }
//     public TurtleGraphics Graphics { get; }
//     public TurtleTime Time { get; }

//     public RenderTarget2D? RenderTarget;
//     public bool Paused;
//     public ContentManager Content;

//     public Scene(Engine engine)
//     {
//         Input = engine.Input;
//         Graphics = engine.Graphics;
//         Time = engine.Time;
//     }
// }