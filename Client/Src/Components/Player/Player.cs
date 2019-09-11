using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Client.Src.Components.Player;
using Client.Src.Screens;
using Client.Src.Utilities;
using System;

namespace Client.Src.Components.Player
{
    public class Player : Component
    {
        private readonly Screen screen;
        private readonly Game1 game;
        //private PlayerMovementHandler movementHandler;
        private PlayerAttributes playerAttributes;
        private Animation animation;
        private AnimationHandler animationHandler;
        private int moveAnimationFrames = 9;

        //public override Vector2 Position { get => playerAttributes.position; set => playerAttributes.position = value; }

        public Player(PlayerAttributes playerAttributes, Screen screen, Game1 game) : base(screen)
        {
            this.playerAttributes = playerAttributes;
            this.screen = screen;
            this.game = game;
            animation = new Animation(moveAnimationFrames);
            animationHandler = new AnimationHandler(animation, this);
            //this.movementHandler = new PlayerMovementHandler(this, screen.Components, animationHandler, playerAttributes, game);
        }

        public override void Update(GameTime gameTime)
        {
            //movementHandler.Update(gameTime);
            animationHandler.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //animationHandler.MovingLeft = playerAttributes.currentMove.movingLeft;
            animationHandler.Draw(spriteBatch);
        }

        public override void LoadContent()
        {
            Texture = screen.Content.Load<Texture2D>("Game/Player");
            animation.Texture = screen.Content.Load<Texture2D>("Game/WizardSpriteSheet");
        }

    }
}
