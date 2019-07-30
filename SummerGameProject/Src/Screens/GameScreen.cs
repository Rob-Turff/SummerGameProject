using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SummerGameProject.Src.Components.Sprites;

namespace SummerGameProject.Src.Screens
{
    class GameScreen : Screen
    {

        public GameScreen(MainGame game, GraphicsDeviceManager graphics) : base(game, graphics)
        {
        }

        public override void LoadContent()
        {
            Texture2D floorTexture = Content.Load<Texture2D>("Game/GroundV1");
            Platform floor = new Platform(floorTexture, new Vector2(0, graphics.PreferredBackBufferHeight - floorTexture.Height), Color.White);
            components.Add(floor);
        }

        public override void UnloadContent()
        {
            Content.Unload();
        }
    }
}
