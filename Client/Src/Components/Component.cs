using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Client.Src.Screens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Client.Src.Components
{
    /// <summary>
    /// Abstract class to create components of the game e.g. buttons or sprites
    /// </summary>
    public abstract class Component
    {
        private Vector2 position;
        protected Screen Screen { get; }

        public Texture2D Texture { get; set; }
        public Vector2 Scale { get; set; } = new Vector2(1f, 1f);

        public bool CentreOnPosition { get; set; }
        public Vector2 Position
        {
            get
            {
                if (CentreOnPosition)
                {
                    return new Vector2(position.X - Width / 2, position.Y + Height / 2);
                }
                return position;
            }
            set
            {
                position = value;
            }
        }
        public virtual float Width { get => Texture.Width * Scale.X; }
        public virtual float Height { get => Texture.Height * Scale.Y; }

        public RectangleF Hitbox
        {
            get
            {
                return new RectangleF(Position.X, Position.Y, Width, Height);
            }
        }

        public Component(Screen screen)
        {
            this.Screen = screen;
        }

        public Component(Screen screen, Vector2 position)
        {
            this.Screen = screen;
            this.Position = position;
        }

        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);

        public abstract void Update(GameTime gameTime);

        public abstract void LoadContent();

        private void CentreOn(Vector2 position)
        {
            Position = new Vector2(position.X - Width / 2, position.Y + Height / 2);
        }
    }
}
