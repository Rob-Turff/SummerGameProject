using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Client.Src.Components;
using Client.Src.Screens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Src.Components
{
    public class Label : Component
    {
        private readonly string text;

        public override float Width => IsTextBold ? Screen.FontBold.MeasureString(text).X : Screen.FontRegular.MeasureString(text).X;
        public override float Height => IsTextBold ? Screen.FontBold.MeasureString(text).Y : Screen.FontRegular.MeasureString(text).Y;

        public bool IsTextBold { get; set; }

        public Label(string text, Vector2 position, Screen screen) : base(screen, position)
        {
            this.text = text;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (IsTextBold)
            {
                spriteBatch.DrawString(Screen.FontBold, text, Position, Color.Black);
            }
            else
            {
                spriteBatch.DrawString(Screen.FontRegular, text, Position, Color.Black);
            }
        }

        public override void LoadContent()
        {
        }

        public override void Update(GameTime gameTime)
        {
        }
    }
}
