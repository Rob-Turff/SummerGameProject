using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SummerGameProject.Src.Screens;
using System;

namespace SummerGameProject.Src.Components.Player
{
    public class Player : Component
    {
        private Screen screen;
        private PlayerMovementHandler movementHandler;

        public override Vector2 Position { get => movementHandler.Position; set => movementHandler.Position = value; }

        public Player(Vector2 position, Screen screen) : base(screen)
        {
            this.screen = screen;
            this.movementHandler = new PlayerMovementHandler(this, screen.Components);
            this.Position = position;
        }

        public override void Update(GameTime gameTime)
        {
            movementHandler.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (movementHandler.IsPlayerMovingLeft)
            {
                spriteBatch.Draw(Texture, Position, null, Color.White, 0f, Vector2.Zero, Scale, SpriteEffects.FlipHorizontally, 0f);
            }
            else
            {
                spriteBatch.Draw(Texture, Position, null, Color.White, 0f, Vector2.Zero, Scale, SpriteEffects.None, 0f);
            }
        }

        public override void LoadContent()
        {
            Texture = screen.Content.Load<Texture2D>("Game/Player");
        }

    }
}
