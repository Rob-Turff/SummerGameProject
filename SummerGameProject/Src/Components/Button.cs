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
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public Action OnClick { get; set; }

        private string text;
        private Texture2D texture;
        private MouseState oldMouse;
        private MouseState currentMouse;
        private Color colour;
        private SpriteFont font;

        public Button(string text, Texture2D texture, Vector2 ButtonPos, SpriteFont font)
        {
            this.text = text;
            this.texture = texture;
            this.font = font;

            // Centres the button
            Vector2 textureSize = new Vector2(texture.Width, texture.Height) / 2;
            this.Position = ButtonPos - textureSize;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Position, colour);

            if (font != null)
            {
                var x = (Position.X + (texture.Width / 2) - (font.MeasureString(text).X / 2));
                var y = (Position.Y + (texture.Height / 2) - (font.MeasureString(text).Y / 2));
                spriteBatch.DrawString(font, text, new Vector2(x, y), Color.Black);
            }
        }

        public override void Update(GameTime gameTime)
        {
            colour = Color.White;
            oldMouse = currentMouse;
            currentMouse = Mouse.GetState();
            if (currentMouse.X < Position.X + texture.Width && currentMouse.X > Position.X && currentMouse.Y < Position.Y + texture.Height && currentMouse.Y > Position.Y)
            {
                colour = Color.Yellow;
                if (currentMouse.LeftButton == ButtonState.Released && oldMouse.LeftButton == ButtonState.Pressed)
                {
                    logger.Error("Invoked button in main menu");
                    OnClick?.Invoke();
                }
            }


        }
    }
}
