using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Client.Src.Screens;

namespace Client.Src.Components
{
    internal class Button : UserInterfaceComponent
    {
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public Action OnClick { get; set; }

        private readonly string text;
        private MouseState oldMouse;
        private MouseState currentMouse;
        private Color colour;
        private SpriteFont Font => Screen.FontRegular;

        public Button(string text, Vector2 position, Action onClickAction, Screen screen) : base(screen,position)
        {
            this.text = text;
            OnClick = onClickAction;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, colour);

            var x = Position.X + (Texture.Width / 2) - (Font.MeasureString(text).X / 2);
            var y = Position.Y + (Texture.Height / 2) - (Font.MeasureString(text).Y / 2);

            spriteBatch.DrawString(Font, text, new Vector2(x, y), Color.Black);
        }

        public override void Update(GameTime gameTime)
        {
            colour = Color.White;
            oldMouse = currentMouse;
            currentMouse = Mouse.GetState();
            if (currentMouse.X < Position.X + Texture.Width && currentMouse.X > Position.X && currentMouse.Y < Position.Y + Texture.Height && currentMouse.Y > Position.Y)
            {
                colour = Color.Yellow;
                if (currentMouse.LeftButton == ButtonState.Released && oldMouse.LeftButton == ButtonState.Pressed)
                {
                    OnClick?.Invoke();
                }
            }


        }

        public override void LoadContent()
        {
            Texture = Screen.Content.Load<Texture2D>("UI/button");
        }

    }
}
