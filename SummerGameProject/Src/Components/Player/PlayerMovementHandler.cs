﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummerGameProject.Src.Components.Player
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

        private bool isInAir = false;
        private float jumpTime = 0;

        private Vector2 velocity = new Vector2(0, 0);
        private Vector2 position;

        public Vector2 Position { get => position; set => position = value; }
        public bool IsPlayerMovingLeft { get; set; } = false;

        public PlayerMovementHandler(Player player, List<Component> components)
        {
            this.player = player;
            this.components = components;
        }

        public void Update(GameTime gameTime)
        {
            (float horizontalMovement, bool attemptJump) = HandleInput();

            ApplyPhysics(gameTime, horizontalMovement, attemptJump);
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
            velocity.X += horizontalMovement * moveAcceleration * elapsedTime;
            velocity.Y += gravityAcceleration * elapsedTime;

            velocity.Y = HandleJump(attemptJump, velocity.Y, elapsedTime);

            // Apply pseudo-drag horizontally
            if (isInAir)
                velocity.X *= groundDragFactor;
            else
                velocity.X *= airDragFactor;

            // Prevent the player from running faster than their top speed
            velocity.X = MathHelper.Clamp(velocity.X, -maxMoveSpeed, maxMoveSpeed);
            velocity.Y = MathHelper.Clamp(velocity.Y, -initalJumpSpeed, maxFallSpeed);

            HandleCollisions(elapsedTime);
        }

        private void HandleCollisions(float elapsedTime)
        {

            // Move position to predicted Y position
            position.Y += velocity.Y * elapsedTime;

            IEnumerable<Component> componentsBarPlayer = components.Except(new List<Component> { player });

            foreach (var component in componentsBarPlayer)
            {
                // Collided Bottom
                if (player.Hitbox.Bottom <= component.Hitbox.Bottom && player.Hitbox.Bottom >= component.Hitbox.Top && player.Hitbox.IntersectsWith(component.Hitbox))
                {
                    position.Y = component.Hitbox.Top - player.Hitbox.Height - 0.001f;
                    velocity.Y = 0;
                    jumpTime = 0;
                    isInAir = false;
                    logger.Debug("The bottom of the player collided with " + component + " at: " + Position + " new velocity = " + velocity);
                }

                // Collided top
                if (player.Hitbox.Top >= component.Hitbox.Top && player.Hitbox.Top <= component.Hitbox.Bottom && player.Hitbox.IntersectsWith(component.Hitbox))
                {
                    position.Y = component.Hitbox.Bottom + 0.001f;
                    velocity.Y = 0;
                    jumpTime = 0;
                    logger.Debug("The top of the player collided with " + component + " at: " + Position + " new velocity = " + velocity);
                }
            }


            // Move position to predicted X position
            position.X += velocity.X * elapsedTime;

            foreach (var component in componentsBarPlayer)
            {
                // Collided left
                if (player.Hitbox.Left <= component.Hitbox.Right && player.Hitbox.Left >= component.Hitbox.Left && player.Hitbox.IntersectsWith(component.Hitbox))
                {
                    position.X = component.Hitbox.Right + 0.001f;
                    velocity.X = 0;
                    logger.Debug("The left of the player collided with " + component + " at: " + Position + " new velocity = " + velocity);
                }

                // Collided right
                if (player.Hitbox.Right >= component.Hitbox.Left && player.Hitbox.Right <= component.Hitbox.Right && player.Hitbox.IntersectsWith(component.Hitbox))
                {
                    position.X = component.Hitbox.Left - player.Hitbox.Width - 0.001f;
                    velocity.X = 0;
                    logger.Debug("The right of the player collided with " + component + " at: " + Position + " new velocity = " + velocity);
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

        private (float, bool) HandleInput()
        {
            float horizontalMovement = 0;
            bool attemptJump = false;

            KeyboardState keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.A))
            {
                horizontalMovement = -1f;
                IsPlayerMovingLeft = true;
            }
            else if (keyboardState.IsKeyDown(Keys.D))
            {
                horizontalMovement = 1f;
                IsPlayerMovingLeft = false;
            }

            if (keyboardState.IsKeyDown(Keys.W) || keyboardState.IsKeyDown(Keys.Space))
                attemptJump = true;

            return (horizontalMovement, attemptJump);
        }
    }
}
