using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Client.Src.Client.Components.Player;
using Client.Src.Common.Message;
using Client.Src.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Src.Components.Player
{
    class PlayerMovementHandler
    {
        #region Constants

        // Constants for controlling horizontal movement
        private const float maxMoveSpeed = 3000.0f;
        private const float moveAcceleration = maxMoveSpeed; // So it takes one second to accelerate to full speed
        private const float groundDragFactor = 0.9f;
        private const float airDragFactor = 0.9f;

        // Constants for controlling vertical movement
        private const float maxFallSpeed = 1600.0f;
        private const float gravityAcceleration = maxFallSpeed * 2; // So it takes 0.5 second to accelerate to terminal velocity
        private const float initalJumpSpeed = 800.0f;
        private const float maxJumpTime = 0.4f;
        private const float jumpControlPower = 0.4f;

        #endregion

        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private Player player;
        private List<Component> components;
        private AnimationHandler animationHandler;
        private PlayerAttributes playerAttributes;
        private readonly MainGame game;
        private bool isInAir = false;
        private float jumpTime = 0;

        public PlayerMovementHandler(Player player, List<Component> components, AnimationHandler animationHandler, PlayerAttributes playerAttributes, MainGame game)
        {
            this.player = player;
            this.components = components;
            this.animationHandler = animationHandler;
            this.playerAttributes = playerAttributes;
            this.game = game;
        }

        public void Update(GameTime gameTime)
        {
            if (playerAttributes.playerID == game.GameData.clientsPlayerID)
                HandleInput();

            float horizontalMovement = 0f;

            if (playerAttributes.currentMove.movingLeft == true)
                horizontalMovement = -1f;
            if (playerAttributes.currentMove.movingRight == true)
                horizontalMovement = 1f;

            ApplyPhysics(gameTime, horizontalMovement, playerAttributes.currentMove.jumping);
        }

        /// <summary>
        /// Updates the player's velocity and position based on input, gravity, collisions etc
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="horizontalMovement"></param>
        /// <param name="attemptJump"></param>
        private void ApplyPhysics(GameTime gameTime, float horizontalMovement, bool attemptJump)
        {
            float elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Base velocity is a combination of horizontal movement control and
            // acceleration downward due to gravity
            playerAttributes.velocity.X += horizontalMovement * moveAcceleration * elapsedTime;
            playerAttributes.velocity.Y += gravityAcceleration * elapsedTime;

            playerAttributes.velocity.Y = HandleJump(attemptJump, playerAttributes.velocity.Y, elapsedTime);


            // Apply pseudo-drag horizontally
            if (isInAir)
            {
                playerAttributes.velocity.X *= groundDragFactor;
                // Stop moving animation if falling/jumping
                animationHandler.Stop();
            }
            else
                playerAttributes.velocity.X *= airDragFactor;

            // Prevent the player from running faster than their top speed
            playerAttributes.velocity.X = MathHelper.Clamp(playerAttributes.velocity.X, -maxMoveSpeed, maxMoveSpeed);
            playerAttributes.velocity.Y = MathHelper.Clamp(playerAttributes.velocity.Y, -initalJumpSpeed, maxFallSpeed);

            HandleCollisions(elapsedTime);

            // Detect if player is falling
            if (playerAttributes.velocity.Y > 0)
                isInAir = true;

            animationHandler.animation.FrameSpeed = Math.Abs(30 / playerAttributes.velocity.X);
        }

        private void HandleCollisions(float elapsedTime)
        {

            // Move position to predicted Y position
            playerAttributes.position.Y += playerAttributes.velocity.Y * elapsedTime;

            IEnumerable<Component> componentsBarPlayer = components.Except(new List<Component> { player });

            foreach (var component in componentsBarPlayer)
            {
                // Collided Bottom
                if (player.Hitbox.Bottom <= component.Hitbox.Bottom && player.Hitbox.Bottom >= component.Hitbox.Top && player.Hitbox.IntersectsWith(component.Hitbox))
                {
                    playerAttributes.position.Y = component.Hitbox.Top - player.Hitbox.Height - 0.001f;
                    playerAttributes.velocity.Y = 0;
                    jumpTime = 0;
                    isInAir = false;
                }

                // Collided top
                if (player.Hitbox.Top >= component.Hitbox.Top && player.Hitbox.Top <= component.Hitbox.Bottom && player.Hitbox.IntersectsWith(component.Hitbox))
                {
                    playerAttributes.position.Y = component.Hitbox.Bottom + 0.001f;
                    playerAttributes.velocity.Y = 0;
                    jumpTime = 0;
                }
            }


            // Move position to predicted X position
            playerAttributes.position.X += playerAttributes.velocity.X * elapsedTime;

            foreach (var component in componentsBarPlayer)
            {
                // Collided left
                if (player.Hitbox.Left <= component.Hitbox.Right && player.Hitbox.Left >= component.Hitbox.Left && player.Hitbox.IntersectsWith(component.Hitbox))
                {
                    playerAttributes.position.X = component.Hitbox.Right + 0.001f;
                    playerAttributes.velocity.X = 0;
                }

                // Collided right
                if (player.Hitbox.Right >= component.Hitbox.Left && player.Hitbox.Right <= component.Hitbox.Right && player.Hitbox.IntersectsWith(component.Hitbox))
                {
                    playerAttributes.position.X = component.Hitbox.Left - player.Hitbox.Width - 0.001f;
                    playerAttributes.velocity.X = 0;
                }
            }
        }


        private float HandleJump(bool jumpButtonPressed, float currentVerticalVelocity, float elapsedTime)
        {
            float newVerticalVelocity = currentVerticalVelocity;

            // If the player is pressing the jump button
            if (jumpButtonPressed)
            {
                // If not already jumping and on ground then begin jump
                if (jumpTime == 0.0f)
                {
                    if (!isInAir)
                    {
                        //TODO animation start and sound

                        newVerticalVelocity = -initalJumpSpeed;
                        jumpTime += elapsedTime;
                        isInAir = true;
                    }

                }
                // If already jumping
                else
                {
                    float timeLeft = maxJumpTime - jumpTime;

                    // If time left on jump set vertical velocity according to power curve
                    if (timeLeft > 0)
                    {
                        newVerticalVelocity = -initalJumpSpeed * (float)Math.Pow(timeLeft / maxJumpTime, jumpControlPower);
                        jumpTime += elapsedTime;
                    }
                    // If no time left then cancel jump and leave velocity unchanged
                    else
                    {
                        jumpTime = 0.0f;
                    }

                }
            }
            else
            {
                // Continue not jumping or cancel jump in progress
                jumpTime = 0.0f;
            }

            return newVerticalVelocity;
        }

        private void HandleInput()
        {
            PlayerMove playerMove = new PlayerMove();

            float horizontalMovement = 0;

            KeyboardState keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.A))
            {
                animationHandler.Play();
                playerMove.movingLeft = true;
            }
            else if (keyboardState.IsKeyDown(Keys.D))
            {
                animationHandler.Play();
                playerMove.movingRight = true;
            }
            else
                animationHandler.Stop(); 

            if (keyboardState.IsKeyDown(Keys.W) || keyboardState.IsKeyDown(Keys.Space))
            {
                playerMove.jumping = true;
                // TODO Play jumping animation
            }

            if (playerMove != playerAttributes.currentMove && game.GameData.isMultiplayer)
            {
                game.networkHandler.sendMessage(new PlayerMoveMessage(playerMove, playerAttributes.playerID));
            }

            playerAttributes.currentMove = playerMove;
        }
    }
}
