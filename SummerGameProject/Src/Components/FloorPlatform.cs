using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SummerGameProject.Src.Screens;

namespace SummerGameProject.Src.Components
{
    class FloorPlatform : Platform
    {
        public FloorPlatform(Screen screen) : base(new Vector2(0,0), Color.White, screen)
        {
        }

        public override void LoadContent()
        {
            texture = Screen.Content.Load<Texture2D>("Game/GroundV1");

            position = new Vector2(0, Screen.ScreenHeight - texture.Height);
        }
    }
}
