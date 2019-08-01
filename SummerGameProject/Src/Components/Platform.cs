using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SummerGameProject.Src.Screens;

namespace SummerGameProject.Src.Components
{
    abstract class Platform : Component
    {
        protected Texture2D texture;
        protected Vector2 position;
        protected Color colour;

        public Platform(Vector2 position, Color colour,Screen screen) : base(screen)
        {
            this.position = position;
            this.colour = colour;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, colour);
        }

        public override void Update(GameTime gameTime)
        {
            
        }

    }
}
