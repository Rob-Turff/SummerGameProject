//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Graphics;
//using Microsoft.Xna.Framework.Input;
//using Client.Src.Screens;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Drawing;

//namespace Client.Src.Components
//{
//    /// <summary>
//    /// Abstract class to create components of the game e.g. buttons or sprites
//    /// </summary>
//    public abstract class Component
//    {
//        protected Screen Screen { get; }

//        public Texture2D Texture { get; set; }
//        public Vector2 Scale { get; set; } = new Vector2(1f, 1f);
    
//        public virtual Vector2 Position { get; set; }
        
//        public virtual float Width { get => Texture.Width * Scale.X; }
//        public virtual float Height { get => Texture.Height * Scale.Y; }

//        public Component(Screen screen, Vector2 position)
//        {
//            Screen = screen;
//            Position = position;
//        }

//        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);

//        public abstract void Update(GameTime gameTime);

//        public abstract void LoadContent();
//    }
//}
