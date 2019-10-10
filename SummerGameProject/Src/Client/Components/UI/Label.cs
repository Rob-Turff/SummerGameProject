using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SummerGameProject.Src.Components;
using SummerGameProject.Src.Screens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummerGameProject.Src.Client.Components
{
    public class Label : Component
    {
        private readonly string text;
        private readonly Game game;
        public override float Width { get; set; }
        public override float Height { get; set; }

        public Label(string text, Vector2 position, Screen screen) : base(screen, position)
        {
            this.text = text;
            this.Width = Screen.Font.MeasureString(text).X;
            this.Height = Screen.Font.MeasureString(text).Y;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Screen.Font, text, Position, Color.Black);
        }

        public override void LoadContent()
        {
        }

        public override void Update(GameTime gameTime)
        {
        }
    }
}
