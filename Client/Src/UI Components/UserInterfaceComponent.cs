using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.Src.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Client.Src.Components
{
    internal abstract class UserInterfaceComponent
    {
        private Vector2 position;

        protected Screen Screen { get; }
        protected Texture2D Texture { get; set; }

        public bool CentreOnPosition { get; set; }
        public Vector2 Position
        {
            get
            {
                if (CentreOnPosition)
                {
                    return new Vector2(position.X - Width / 2, position.Y - Height / 2);
                }
                return position;
            }
            set
            {
                position = value;
            }
        }

        public Vector2 Scale { get; set; } = new Vector2(1f, 1f);
        public virtual float Width { get => Texture.Width * Scale.X; }
        public virtual float Height { get => Texture.Height * Scale.Y; }


        protected UserInterfaceComponent(Screen screen, Vector2 position)
        {
            Screen = screen;
            Position = position;
        }

        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);

        public abstract void Update(GameTime gameTime);

        public abstract void LoadContent();

    }
}
