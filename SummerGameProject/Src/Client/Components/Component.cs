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
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        protected Screen Screen { get; }

        public Texture2D Texture { get; set; }
        public Vector2 Scale { get; set; } = new Vector2(1f, 1f);

        public virtual Vector2 Position { get; set; }
        public virtual float Width { get => Texture.Width * Scale.X; set => Width = value; }
        public virtual float Height { get => Texture.Height * Scale.Y; set => Width = value; }


        public Component(Screen screen)
        {
            this.Screen = screen;
        }

        public Component(Screen screen, Vector2 position)
        {
            this.Screen = screen;
            this.Position = position;
        }

        public RectangleF Hitbox
        {
            get
            {
                return new RectangleF(Position.X, Position.Y, Width, Height);
            }
        }

        public Vector2 GetCentreCoord()
        {
            return new Vector2(Position.X + Width / 2, Position.Y + Height / 2);
        }

        /// <summary>
        /// Returns the angle between obj1 and obj2 (in radians)
        /// </summary>
        /// <param name="obj1Pos"></param>
        /// <param name="obj2Pos"></param>
        /// <returns></returns>
        public float GetAngleToCentre(Vector2 obj1Pos, Vector2 obj2Pos)
        {
            return (float) Math.Atan((obj2Pos.Y - obj1Pos.Y) / (obj2Pos.X - obj1Pos.X));
        }

        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);

        public abstract void Update(GameTime gameTime);

        public abstract void LoadContent();
    }
}
