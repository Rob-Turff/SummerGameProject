using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SummerGameProject.Src.Screens;
using SummerGameProject.Src.Utilities;
using System;

namespace SummerGameProject.Src.Components.Player
{
    public class Player : Component
    {
        private Screen screen;
        private string playerName;
        private PlayerMovementHandler movementHandler;
        private Animation animation;
        private AnimationHandler animationHandler;

        public override Vector2 Position { get => movementHandler.Position; set => movementHandler.Position = value; }

        public Player(Vector2 position, Screen screen, string name)
        {
            this.screen = screen;
            this.movementHandler = new PlayerMovementHandler(this, screen.Components, animationHandler);
            this.Position = position;
            this.playerName = name;
        }


        public override void Update(GameTime gameTime)
        {
            movementHandler.Update(gameTime);
            animationHandler.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            animationHandler.MovingLeft = movementHandler.IsPlayerMovingLeft;
            animationHandler.Draw(spriteBatch);
        }

        public void LoadContent()
        {
            Texture = screen.Content.Load<Texture2D>("Game/WizardSpriteSheet");
            animation = new Animation(7, 0.5f, Texture);
            animationHandler = new AnimationHandler(animation, this);
        }

    }
}
