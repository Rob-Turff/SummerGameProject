using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SummerGameProject.Src.Components.Sprites
{
    class Player : Component
    {
        #region Constants

        // Constants for controlling horizontal movement
        private const float moveAcceleration = 13000.0f;
        private const float maxMoveSpeed = 1750.0f;
        private const float groundDragFactor = 0.48f;
        private const float airDragFactor = 0.58f;

        // Constants for controlling vertical movement
        private const float gravityAcceleration = 3400.0f;
        private const float maxFallSpeed = 550.0f;
        private const float initalJumpSpeed = 2000.0f;
        private const float maxJumpTime = 0.3f;
        private const float jumpControlPower = 0.3f;

        #endregion

        private Vector2 velocity;

        private bool isOnGround = true; //TODO

        private float jumpTime;

        private Texture2D texture2D;

        public Player(Vector2 position)
        {

        }

        public override void Update(GameTime gameTime, KeyboardState keyboardState)
        {
            (float horizontalMovement, bool attemptJump) = HandleInput(keyboardState);

            ApplyPhysics(gameTime, horizontalMovement, attemptJump);
        }

        private (float, bool) HandleInput(KeyboardState keyboardState)
        {
            float horizontalMovement = 0;
            bool attemptJump = false;

            if (keyboardState.IsKeyDown(Keys.A))
                horizontalMovement = -1f;
            else if (keyboardState.IsKeyDown(Keys.D))
                horizontalMovement = 1f;

            if (keyboardState.IsKeyDown(Keys.W) || keyboardState.IsKeyDown(Keys.Space))
                attemptJump = true;

            return (horizontalMovement, attemptJump);
        }

        /// <summary>
        /// Updates the player's velocity and position based on input, gravity, etc
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="horizontalMovement"></param>
        /// <param name="attemptJump"></param>
        private void ApplyPhysics(GameTime gameTime, float horizontalMovement, bool attemptJump)
        {
            float elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            Vector2 previousPosition = Position;

            // Base velocity is a combination of horizontal movement control and
            // acceleration downward due to gravity
            velocity.X += horizontalMovement * moveAcceleration * elapsedTime;
            velocity.Y += gravityAcceleration * elapsedTime;

            velocity.Y = HandleJump(attemptJump, velocity.Y, elapsedTime);

            // Apply pseudo-drag horizontally
            if (isOnGround)
                velocity.X *= groundDragFactor;
            else
                velocity.X *= airDragFactor;

            // Prevent the player from running faster than their top speed
            velocity.X = MathHelper.Clamp(velocity.X, -maxMoveSpeed, maxMoveSpeed);

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
                    if (isOnGround)
                    {
                        //TODO animation start and sound

                        newVerticalVelocity = -initalJumpSpeed;
                        jumpTime += elapsedTime;
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

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            throw new NotImplementedException();
        }

        protected override void LoadContent()
        {
            texture2D = Content.Load
        }
    }
}
