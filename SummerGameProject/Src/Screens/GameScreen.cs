using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SummerGameProject.Src.Components;

namespace SummerGameProject.Src.Screens
{
    class GameScreen : Screen
    {
        Player player;

        public GameScreen(MainGame game, GraphicsDeviceManager graphics) : base(game, graphics)
        {
            player = new Player(new Vector2(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight / 2),this);
            components.Add(player);
        }

        public void Initialize()
        {
            
        }

        public override void LoadContent()
        {
            player.LoadContent();
        }

        public override void UnloadContent()
        {
            Content.Unload();
        }
    }
}
