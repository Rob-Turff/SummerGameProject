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
    class Platform : Component
    {
        private Texture2D texture;
        private Vector2 position;
        private Color colour;

        public Platform(Texture2D texture, Vector2 position, Color colour)
        {
            this.texture = texture;
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
