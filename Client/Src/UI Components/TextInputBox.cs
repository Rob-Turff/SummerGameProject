using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Client.Src.Utilities;
using Client.Src.Components;
using Client.Src.Screens;
using System;

namespace Client.Src.Components
{
    internal class TextInputBox : UserInterfaceComponent
    {
        private readonly string text;

        private Color colour;
        private readonly Game game;

        private MouseState oldMouse;
        private MouseState currentMouse;
        private bool clicked = false;

        private SpriteFont Font => Screen.FontRegular;
        public string EnteredText { get; set; } = "";

        public TextInputBox(string text, Vector2 position, Screen screen, Game game) : base(screen,position)
        {
            this.text = text;
            this.game = game;
            game.Window.TextInput += TextInputHandler;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, null, Color.White, 0f, Vector2.Zero, Scale, SpriteEffects.None, 0f);

            float x, y;
            if (clicked || EnteredText != "")
            {
                x = Position.X + (Texture.Width / 2) - (Font.MeasureString(EnteredText).X / 2);
                y = Position.Y + (Texture.Height / 2) - (Font.MeasureString(EnteredText).Y / 2);
                spriteBatch.DrawString(Font, EnteredText, new Vector2(x, y), Color.Black);
            }
            else {
                x = Position.X + (Texture.Width / 2) - (Font.MeasureString(text).X / 2);
                y = Position.Y + (Texture.Height / 2) - (Font.MeasureString(text).Y / 2);
                spriteBatch.DrawString(Font, text, new Vector2(x, y), Color.Black);
            }
        }

        public override void LoadContent()
        {
            Texture = Screen.Content.Load<Texture2D>("UI/TextBox");
        }

        public override void Update(GameTime gameTime)
        {
            colour = Color.White;
            oldMouse = currentMouse;
            currentMouse = Mouse.GetState();
            if (currentMouse.X < Position.X + Texture.Width && currentMouse.X > Position.X && currentMouse.Y < Position.Y + Texture.Height && currentMouse.Y > Position.Y)
            {
                if (currentMouse.LeftButton == ButtonState.Released && oldMouse.LeftButton == ButtonState.Pressed)
                {
                    clicked = !clicked;
                }
            }
        }

        internal void TextInputHandler(object sender, TextInputEventArgs args)
        {
            var pressedKey = args.Key;
            var character = args.Character;
            if (clicked)
            {
                if (pressedKey == Keys.Back)
                {
                    if (EnteredText.Length > 0)
                    {
                        EnteredText = EnteredText.Remove(EnteredText.Length - 1);
                    }
                }
                else
                {
                    EnteredText += character;
                }
            }
        }
    }
}
