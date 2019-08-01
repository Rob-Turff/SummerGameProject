using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SummerGameProject.Src.Screens;

namespace SummerGameProject.Src.Components
{
    public class Button : Component
    {
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public Vector2 ButtonPos { get; set; }
        public Action OnClick { get; set; }

        private string text;
        private Texture2D texture;
        private MouseState oldMouse;
        private MouseState currentMouse;
        private Color colour;
        private SpriteFont font => Screen.Font;

        private int height = -1;
        private int width = -1;

        public int Height
        {
            get
            {
                if (height == -1)
                {
                    height = texture.Height;
                }
                return height;
            }
        }
        public int Width
        {
            get
            {
                if (width == -1)
                {
                    width = texture.Width;
                }
                return width;
            }
        }


        public Button(string text, Vector2 buttonPos, Action onClickAction, Screen screen) : base(screen)
        {
            this.text = text;
            OnClick = onClickAction;
            this.ButtonPos = buttonPos;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, ButtonPos, colour);

            var x = ButtonPos.X + (texture.Width / 2) - (font.MeasureString(text).X / 2);
            var y = ButtonPos.Y + (texture.Height / 2) - (font.MeasureString(text).Y / 2);

            spriteBatch.DrawString(font, text, new Vector2(x, y), Color.Black);
        }

        public override void Update(GameTime gameTime)
        {
            colour = Color.White;
            oldMouse = currentMouse;
            currentMouse = Mouse.GetState();
            if (currentMouse.X < ButtonPos.X + texture.Width && currentMouse.X > ButtonPos.X && currentMouse.Y < ButtonPos.Y + texture.Height && currentMouse.Y > ButtonPos.Y)
            {
                colour = Color.Yellow;
                if (currentMouse.LeftButton == ButtonState.Released && oldMouse.LeftButton == ButtonState.Pressed)
                {
                    logger.Error("Invoked button in main menu");
                    OnClick?.Invoke();
                }
            }


        }

        public override void LoadContent()
        {
            texture = Screen.Content.Load<Texture2D>("UI/button");
            // Centres the button
            this.ButtonPos = ButtonPos - new Vector2(Width, Height) / 2;
        }
    }
}
