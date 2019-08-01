using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummerGameProject.Src.Components
{
    /// <summary>
    /// Abstract class to create components of the game e.g. buttons or sprites
    /// </summary>
    public abstract class Component
    {
        public Vector2 Position { get; set; }
        public Texture2D Texture { get; set; }
        public float Scale { get; set; } = 1f;
        public Rectangle hitbox
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, (int)(Texture.Width * Scale), (int)(Texture.Height * Scale));
            }
        }

        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);

        public abstract void Update(GameTime gameTime);
    }
}
