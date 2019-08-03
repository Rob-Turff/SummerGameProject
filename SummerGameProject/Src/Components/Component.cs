using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SummerGameProject.Src.Screens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace SummerGameProject.Src.Components
{
    /// <summary>
    /// Abstract class to create components of the game e.g. buttons or sprites
    /// </summary>
    public abstract class Component
    {
        protected Screen Screen { get; }

        public Texture2D Texture { get; set; }
        public float Scale { get; set; } = 1f;

        public abstract Vector2 Position { get; set; }
        public float Width => Texture.Width * Scale;
        public float Height => Texture.Height * Scale;

        public RectangleF Hitbox
        {
            get
            {
                return new RectangleF(Position.X, Position.Y, Width, Height);
            }
        }

        public Component(Screen screen)
        {
            Screen = screen;
        }

        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);

        public abstract void Update(GameTime gameTime);

        public abstract void LoadContent();
    }
}
