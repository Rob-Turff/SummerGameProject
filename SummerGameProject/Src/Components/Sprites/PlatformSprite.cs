using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SummerGameProject.Src.Components.Sprites
{
    class PlatformSprite : Sprite
    {
        public PlatformSprite(Texture2D texture, Vector2 position) : base(texture, position)
        {
        }

        public override void Update(GameTime gameTime, KeyboardState keyboardState)
        {
            
        }
    }
}
