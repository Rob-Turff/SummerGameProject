using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SummerGameProject.Src.Screens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummerGameProject.Src.Components.Platforms
{
    public class StoneWallPlatform : Platform
    {
        public StoneWallPlatform(Vector2 position, Vector2 scale, Screen screen) : base(position, Color.White, scale, screen)
        {
        }

        public override void LoadContent()
        {
            Texture = Screen.Content.Load<Texture2D>("Game/Stonewall");
        }
    }
}
