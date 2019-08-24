using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Client.Src.Screens;

namespace Client.Src.Components.Platforms
{
    class GrassPlatform : Platform
    {
        public GrassPlatform(Vector2 position, Vector2 scale, Screen screen) : base(position, Color.White, scale, screen)
        {
        }

        public override void LoadContent()
        {
            Texture = Screen.Content.Load<Texture2D>("Game/GroundV1");
        }
    }
}
