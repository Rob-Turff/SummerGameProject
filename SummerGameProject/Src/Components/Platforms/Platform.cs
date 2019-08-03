using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SummerGameProject.Src.Screens;

namespace SummerGameProject.Src.Components.Platforms
{
    abstract class Platform : Component
    {
        protected Color colour;

        public override Vector2 Position { get; set; }

        public Platform(Vector2 position, Color colour, float scale, Screen screen) : base(screen)
        {
            this.Position = position;
            this.colour = colour;
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
