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
        private PlayerMovementHandler movementHandler;
        private Animation animation;
        private AnimationHandler animationHandler;
        private int moveAnimationFrames = 9;

        public override Vector2 Position { get => movementHandler.Position; set => movementHandler.Position = value; }

        public Player(Vector2 position, Screen screen) : base(screen)
        {
            this.screen = screen;
            animation = new Animation(moveAnimationFrames);
            animationHandler = new AnimationHandler(animation, this);
            this.movementHandler = new PlayerMovementHandler(this, screen.Components, animationHandler);
            this.Position = position;
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

        public override void LoadContent()
        {
            Texture = screen.Content.Load<Texture2D>("Game/Player");
            animation.Texture = screen.Content.Load<Texture2D>("Game/WizardSpriteSheet");
        }

    }
}
