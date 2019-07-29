using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SummerGameProject.Src.Components.Sprites
{
    public abstract class Sprite : Component
    {
        private Texture2D texture;
        private float depth;
        private Vector2 origin;

        public Sprite(Texture2D texture, Vector2 position)
        {
            this.texture = texture;
            this.Position = position;
            Color = Color.White;
            origin = new Vector2(texture.Width / 2, texture.Height / 2);
            Scale = Vector2.One;
            depth = 0f;
        }

        public Vector2 Position { get; set; }
        public Vector2 Scale { get; set; }
        public Color Color { get; set; }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Position, null, Color, 0f, origin, Scale, SpriteEffects.None, depth);
        }

    }
}
