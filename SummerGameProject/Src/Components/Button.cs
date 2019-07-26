using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SummerGameProject.Src.Components
{
    class Button : Component
    {
        public int ButtonX { get; set; }
        public int ButtonY { get; set; }

        public Action OnClick { get; set; }

        private string text;
        private Texture2D texture;
        private MouseState oldMouse;
        private MouseState currentMouse;
        private Color colour;
        private SpriteFont font;

        public Button(string text, Texture2D texture, int ButtonX, int ButtonY, SpriteFont font)
        {
            this.text = text;
            this.texture = texture;
            this.ButtonX = ButtonX;
            this.ButtonY = ButtonY;
            this.font = font;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Vector2(ButtonX, ButtonY), colour);

            if (font != null)
            {
                var x = (ButtonX + (texture.Width / 2) - (font.MeasureString(text).X / 2));
                var y = (ButtonY + (texture.Height / 2) - (font.MeasureString(text).Y / 2));
                spriteBatch.DrawString(font, text, new Vector2(x, y), Color.Black);
            }
        }

        public override void Update(GameTime gameTime)
        {
            colour = Color.White;
            oldMouse = currentMouse;
            currentMouse = Mouse.GetState();
            if (currentMouse.X < ButtonX + texture.Width && currentMouse.X > ButtonX && currentMouse.Y < ButtonY + texture.Height && currentMouse.Y > ButtonY)
            {
                colour = Color.Yellow;
                if (currentMouse.LeftButton == ButtonState.Released && oldMouse.LeftButton == ButtonState.Pressed)
                {
                    OnClick?.Invoke();
                }
            }


        }
    }
}
