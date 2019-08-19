using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SummerGameProject.Src.Client.Utilities;
using SummerGameProject.Src.Components;
using SummerGameProject.Src.Screens;
using System;

namespace SummerGameProject.Src.Client.Components
{
    public class TextInputBox : Component
    {
        private string text;
        public string enteredText { get; set; } = "";

        private Color colour;
        private readonly Screen screen;
        private readonly Game game;

        private SpriteFont font => Screen.Font;

        private MouseState oldMouse;
        private MouseState currentMouse;
        private bool clicked = false;

        private KeyboardState oldKeyboard;
        private KeyboardState currentKeyboard;


        public TextInputBox(string text, Vector2 position, Screen screen, Game game) : base(screen)
        {
            this.text = text;
            this.Position = position;
            this.screen = screen;
            this.game = game;
            game.Window.TextInput += TextInputHandler;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, null, Color.White, 0f, Vector2.Zero, Scale, SpriteEffects.None, 0f);

            float x, y;
            if (clicked || enteredText != "")
            {
                x = Position.X + (Texture.Width / 2) - (font.MeasureString(enteredText).X / 2);
                y = Position.Y + (Texture.Height / 2) - (font.MeasureString(enteredText).Y / 2);
                spriteBatch.DrawString(font, enteredText, new Vector2(x, y), Color.Black);
            }
            else {
                x = Position.X + (Texture.Width / 2) - (font.MeasureString(text).X / 2);
                y = Position.Y + (Texture.Height / 2) - (font.MeasureString(text).Y / 2);
                spriteBatch.DrawString(font, text, new Vector2(x, y), Color.Black);
            }
        }

        public override void LoadContent()
        {
            //TODO Make text input box texture
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
                    if (enteredText.Length > 0)
                    {
                        enteredText = enteredText.Remove(enteredText.Length - 1);
                    }
                }
                else
                {
                    enteredText += character;
                }
            }
        }
    }
}
