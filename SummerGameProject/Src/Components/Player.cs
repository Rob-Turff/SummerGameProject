using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SummerGameProject.Src.Screens;
using System;

namespace SummerGameProject.Src.Components
{
    public class Player : Component
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

        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private Screen screen;

        private string playerName;

        private bool isOnGround = true; //TODO

        private float jumpTime;

        private Vector2 velocity;

        public Player(Vector2 position, Screen screen, string name)
        {
            this.Position = position;
            this.screen = screen;
            this.playerName = name;
        }

        public void setVelocity(Vector2 velocity)
        {
            this.velocity = velocity;
        }

        public override void Update(GameTime gameTime)
        {
            (float horizontalMovement, bool attemptJump) = HandleInput();

            ApplyPhysics(gameTime, horizontalMovement, attemptJump);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Color.White);
        }

        public void LoadContent()
        {
            Texture = screen.Content.Load<Texture2D>("Game/Player");
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

            // Collision Detection
            foreach (var obj in screen.Components)
            {
                if (obj != this)
                {
                    // Collided Bottom
                    Position = new Vector2(previousPosition.X, previousPosition.Y + velocity.Y);
                    if (hitbox.Bottom <= obj.hitbox.Bottom && hitbox.Bottom >= obj.hitbox.Top && hitbox.Intersects(obj.hitbox))
                    {
                        velocity.Y = 0;
                        logger.Debug("The bottom of " + playerName + " collided with " + obj + " at: " + Position + " new velocity = " + velocity);
                        isOnGround = true;
                    }
                    // Collided top
                    if (hitbox.Top >= obj.hitbox.Top && hitbox.Top <= obj.hitbox.Bottom && hitbox.Intersects(obj.hitbox))
                    {
                        velocity.Y = 0;
                        logger.Debug("The top of " + playerName + " collided with " + obj + " at: " + Position + " new velocity = " + velocity);
                    }
                    Position = new Vector2(previousPosition.X + velocity.X, previousPosition.Y);
                    // Collided left
                    if (hitbox.Left <= obj.hitbox.Right && hitbox.Left >= obj.hitbox.Left && hitbox.Intersects(obj.hitbox))
                    {
                        velocity.X = 0;
                        logger.Debug("The left of " + playerName + " collided with " + obj + " at: " + Position + " new velocity = " + velocity);
                    }
                    // Collided right
                    if (hitbox.Right >= obj.hitbox.Left && hitbox.Right <= obj.hitbox.Right && hitbox.Intersects(obj.hitbox))
                    {
                        velocity.X = 0;
                        logger.Debug("The right of " + playerName + " collided with " + obj + " at: " + Position + " new velocity = " + velocity);
                    }
                }
            }
            Position = previousPosition + velocity;
        }

        private bool willCollideHorizontal(Component obj)
        {
            // Collided left
            if (hitbox.Left <= obj.hitbox.Right && hitbox.Right >= obj.hitbox.Left)
            {
                return true;
            }
            // Collided right
            else if (hitbox.Right <= obj.hitbox.Left && hitbox.Left >= obj.hitbox.Right)
            {
                return true;
            }
            return false;
        }

        private bool willCollideVertical(Component obj)
        {
            // Collided Bottom
            if (hitbox.Top <= obj.hitbox.Bottom && hitbox.Bottom >= obj.hitbox.Top)
            {
                return true;
            }

            // Collided top
            if (hitbox.Bottom <= obj.hitbox.Top && hitbox.Top >= obj.hitbox.Bottom)
            {
                return true;
            }
            return false;
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
                        isOnGround = false;
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

        #endregion
    }
}
