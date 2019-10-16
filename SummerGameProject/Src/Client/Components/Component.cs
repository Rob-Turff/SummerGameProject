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
        private Vector2 defaultRes = new Vector2(1920, 1080);

        protected Screen Screen { get; }
        public Texture2D Texture { get; set; }

        public Vector2 CombinedScale { get => BaseScale * ResScale; }
        public Vector2 BaseScale { get; set; } = new Vector2(1f, 1f);
        public Vector2 ResScale { get; set; } = new Vector2(1f, 1f);

        public bool onScreen { get; set; }
        public Vector2 ScreenPos { get; set; }

        public virtual Vector2 Position { get; set; }
        public virtual float Width { get => Texture.Width * CombinedScale.X; set => Width = value; }
        public virtual float Height { get => Texture.Height * CombinedScale.Y; set => Width = value; }
        public virtual Vector2 Size { get { return new Vector2(Width, Height); } }


        public Component(Screen screen)
        {
            this.Screen = screen;
            if (Screen.UseResScaling)
                CalcResScale(screen.ScreenSize);
        }

        public Component(Screen screen, Vector2 position)
        {
            this.Screen = screen;
            this.Position = position;
            if (Screen.UseResScaling)
                CalcResScale(screen.ScreenSize);
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
            return new Vector2(Position.X * ResScale.X + Width / 2, Position.Y * ResScale.X + Height / 2);
        }

        /// <summary>
        /// Returns the angle between the centre of this component and the obj (in radians)
        /// </summary>
        /// <param name="objPos"></param>
        /// <returns></returns>
        public float GetAngleToCentre(Vector2 objPos)
        {
            Vector2 centre = GetCentreCoord();
            objPos = ScaleToRes(objPos);
            logger.Debug("Obj x: " + centre.X + " Mouse x: " + objPos.X);
            return (float) (Math.Atan2((centre.Y - objPos.Y), (centre.X - objPos.X)) + Math.PI/2);
        }

        private void CalcResScale(Vector2 resolution)
        {
            ResScale = new Vector2(resolution.X / defaultRes.X, resolution.Y / defaultRes.Y);
        }

        protected Vector2 ScaleToRes(Vector2 coords)
        {
            return coords * ResScale;
        }

        public virtual void OnCollide()
        {
            // Do Nothing by default
        }

        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);

        public abstract void Update(GameTime gameTime);

        public abstract void LoadContent();
    }
}
