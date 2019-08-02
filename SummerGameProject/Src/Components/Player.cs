using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SummerGameProject.Src.Screens;

namespace SummerGameProject.Src.Components
{
    class Player : Component
    {
        #region Constants

        // Constants for controlling horizontal movement
        private const float moveAcceleration = 30.0f;
        private const float maxMoveSpeed = 1000.0f;
        private const float groundDragFactor = 0.9f;
        private const float airDragFactor = 0.9f;

        // Constants for controlling vertical movement
        private const float gravityAcceleration = 40.0f;
        private const float maxFallSpeed = 20.0f;
        private const float initalJumpSpeed = 10.0f;
        private const float maxJumpTime = 0.5f;
        private const float jumpControlPower = 0.3f;

        #endregion

        private Screen screen;

        private Texture2D texture;

        private bool isOnGround = true; //TODO

        private float jumpTime;

        private Vector2 velocity;

        public override Vector2 Position { get; set; }
        public override int Width => 100; //TODO sort these
        public override int Height => 200;

        public Player(Vector2 position,Screen screen) : base (screen)
        {
            this.Position = position;
        }

        public override void Update(GameTime gameTime)
        {
            (float horizontalMovement, bool attemptJump) = HandleInput();

            ApplyPhysics(gameTime, horizontalMovement, attemptJump);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Position, Color.White);
        }

        public override void LoadContent()
        {
            texture = screen.Content.Load<Texture2D>("ball");
        }

        #region Private Methods

        private (float, bool) HandleInput()
        {
            float horizontalMovement = 0;
            bool attemptJump = false;

            KeyboardState keyboardState = Keyboard.GetState();

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
            velocity.Y = MathHelper.Clamp(velocity.Y, -maxFallSpeed, maxFallSpeed);

            Position = previousPosition + velocity;
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
    }
}
