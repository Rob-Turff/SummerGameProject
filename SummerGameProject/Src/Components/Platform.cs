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
        protected Color colour;


        public override Vector2 Position { get; set; }
        public override int Width => texture.Width;
        public override int Height => texture.Height;


        public Platform(Vector2 position, Color colour,Screen screen) : base(screen)
        {
            Position = position;
            this.colour = colour;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Position, colour);
        }

        public override void Update(GameTime gameTime)
        {
            
        }

    }
}
