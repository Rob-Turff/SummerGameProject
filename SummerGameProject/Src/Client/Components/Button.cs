﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SummerGameProject.Src.Screens;

namespace SummerGameProject.Src.Components
{
    public class Button : Component
    {
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public Action OnClick { get; set; }

        private readonly string text;
        private MouseState oldMouse;
        private MouseState currentMouse;
        private Color colour;
        private SpriteFont font => Screen.Font;

        public override Vector2 Position { get; set; }

        public Button(string text, Vector2 position, Action onClickAction, Screen screen) : base(screen)
        {
            this.text = text;
            OnClick = onClickAction;
            this.Position = position;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, colour);

            var x = Position.X + (Texture.Width / 2) - (font.MeasureString(text).X / 2);
            var y = Position.Y + (Texture.Height / 2) - (font.MeasureString(text).Y / 2);

            spriteBatch.DrawString(font, text, new Vector2(x, y), Color.Black);
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
