using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SummerGameProject.Src.Client.Components;
using SummerGameProject.Src.Client.Components.Player;
using SummerGameProject.Src.Screens;
using SummerGameProject.Src.Utilities;
using System;

namespace SummerGameProject.Src.Components.Player
{
    public class Player : Entity
    {
        private readonly GameScreen screen;
        private readonly MainGame game;
        private PlayerMovementHandler movementHandler;
        private PlayerInputHandler inputHandler;
        private PlayerStats playerAttributes;
        private Animation animation;
        private AnimationHandler animationHandler;
        private int moveAnimationFrames = 9;

        public override Vector2 Position { get => playerAttributes.position; set => playerAttributes.position = value; }

        public Player(PlayerStats playerStats, GameScreen screen, MainGame game) : base(screen)
        {
            this.playerAttributes = playerStats;
            this.screen = screen;
            this.game = game;
            playerStats.movementHandler = movementHandler;
            playerStats.player = this;
            animation = new Animation(moveAnimationFrames);
            animationHandler = new AnimationHandler(animation, this, screen);
            movementHandler = new PlayerMovementHandler(this, screen.components, animationHandler, playerStats, game);
            inputHandler = new PlayerInputHandler(screen, playerStats, animationHandler, game);
        }

        public override void Update(GameTime gameTime)
        {
            inputHandler.Update(gameTime);
            movementHandler.Update(gameTime);
            animationHandler.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            animationHandler.MovingLeft = playerAttributes.currentMove.movingLeft;
            animationHandler.Draw(spriteBatch);
        }

        public override void LoadContent()
        {
            Texture = screen.Content.Load<Texture2D>("Game/Player");
            animation.Texture = screen.Content.Load<Texture2D>("Game/WizardSpriteSheet");
        }

        public override void OnCollide()
        {
            // Do Something
        }
    }
}
