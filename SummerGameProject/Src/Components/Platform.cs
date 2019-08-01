using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SummerGameProject.Src.Components
{
    public class Platform : Component
    {
        private Color colour;

        public Platform(Texture2D texture, Vector2 position, Color colour)
        {
            this.Texture = texture;
            this.Position = position;
            this.colour = colour;
        }

        public Platform(Texture2D texture, Vector2 position, Color colour, float scale) : this(texture, position, colour)
        {
            this.Scale = scale;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, null, colour, 0f, Vector2.Zero, Scale, SpriteEffects.None, 0f);
        }

        public override string ToString()
        {
            return base.ToString() + ".at: " + Position;
        }

        public override void Update(GameTime gameTime)
        {
            
        }
    }
}
