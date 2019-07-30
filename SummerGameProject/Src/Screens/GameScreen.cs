using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SummerGameProject.Src.Screens
{
    class GameScreen : Screen
    {
        public GameScreen(MainGame game, GraphicsDeviceManager graphics) : base(game, graphics)
        {
        }

        public override void LoadContent()
        {
            Texture2D ground = Content.Load<Texture2D>("Game/GroundV1");
            components.Add(ground);
        }

        public override void UnloadContent()
        {
            Content.Unload();
        }
    }
}
