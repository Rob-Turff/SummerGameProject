using Common.Src.Packets.ClientToServer;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Common.Src.Entities;
using Common.Src.Entities.Platforms;

namespace Common.Src
{
    public class CharacterMovementSystem
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

        private readonly List<IMovableCharacter> movableCharacters;
        private readonly List<Platform> platforms;

        public CharacterMovementSystem(List<IMovableCharacter> movableCharacters, List<Platform> platforms)
        {
            this.movableCharacters = movableCharacters;
            this.platforms = platforms;
        }

        public void Update(TimeSpan elapsed)
        {
            for (int i = 0, length = movableCharacters.Count; i < length; i++)
            {
                List<ICollidableEntity> collidableEntities = new List<ICollidableEntity>(platforms);

                collidableEntities.AddRange(movableCharacters.Where(character => character != movableCharacters[i]));

                UpdateCharacter(movableCharacters[i], collidableEntities, elapsed);
            }
        }

        /// <summary>
        /// Updates the character's velocity and position based on input, gravity, collisions etc
        /// </summary>
        private void UpdateCharacter(IMovableCharacter character, List<ICollidableEntity> collidableEntities, TimeSpan elapsedTimeSpan)
        {
            float elapsedTime = (float)elapsedTimeSpan.TotalSeconds;

            float newVelocityX = character.Velocity.X;
            float newVelocityY = character.Velocity.Y;
            float newJumpTime;

            PlayerInputs playerInputs = character.PlayerInputs;

            // Update horizontal velocity according to player input
            if (playerInputs.HasFlag(PlayerInputs.LEFT))
                newVelocityX -= moveAcceleration * elapsedTime;
            if (playerInputs.HasFlag(PlayerInputs.RIGHT))
                newVelocityX += moveAcceleration * elapsedTime;

            // Update vertical velocity according to acceleration due to gravity
            newVelocityY += gravityAcceleration * elapsedTime;

            // Handle jumping logic
            bool jumpButtonPressed = playerInputs.HasFlag(PlayerInputs.JUMP);
            (newVelocityY, newJumpTime) = HandleJump(jumpButtonPressed, character.JumpTime, character.IsOnGround, newVelocityY, elapsedTime);

            // Apply pseudo-drag horizontally
            newVelocityX *= (character.IsOnGround ? groundDragFactor : airDragFactor);

            // Prevent the player from running faster than their top speed
            newVelocityX = Clamp(newVelocityX, -maxMoveSpeed, maxMoveSpeed);
            newVelocityY = Clamp(newVelocityY, -initalJumpSpeed, maxFallSpeed);

            // Change the player's position, velocity, jumptime and isonground according to whether or not they collide
            HandleCollisions(character, collidableEntities, newVelocityX, newVelocityY, newJumpTime, elapsedTime);
        }

        private (float, float) HandleJump(bool jumpButtonPressed, float jumpTime, bool isOnGround, float verticalSpeed, float elapsedTime)
        {
            float newVerticalSpeed;
            float newJumpTime;

            // If the player is pressing the jump button
            if (jumpButtonPressed)
            {
                // If not already jumping and on ground then begin jump
                if (jumpTime == 0.0f)
                {
                    if (isOnGround)
                    {
                        //TODO animation start and sound

                        newVerticalSpeed = -initalJumpSpeed;
                        newJumpTime = jumpTime + elapsedTime;
                    }
                    else
                    {
                        newVerticalSpeed = verticalSpeed;
                        newJumpTime = 0.0f;
                    }
                }
                // If already jumping
                else
                {
                    float timeLeft = maxJumpTime - jumpTime;

                    // If time left on jump set vertical velocity according to power curve
                    if (timeLeft > 0)
                    {
                        newVerticalSpeed = -initalJumpSpeed * (float)Math.Pow(timeLeft / maxJumpTime, jumpControlPower);
                        newJumpTime = jumpTime + elapsedTime;
                    }
                    // If no time left then cancel jump and leave velocity unchanged
                    else
                    {
                        newVerticalSpeed = verticalSpeed;
                        newJumpTime = 0.0f;
                    }

                }
            }
            else
            {
                // Continue not jumping or cancel jump in progress
                newVerticalSpeed = verticalSpeed;
                newJumpTime = 0.0f;
            }

            return (newVerticalSpeed, newJumpTime);
        }

        private void HandleCollisions(IMovableCharacter character, List<ICollidableEntity> collidableComponents, float velocityX, float velocityY, float jumpTime, float elapsedTime)
        {
            // By default don't do anything to jumptime unless the player touches the ground or hits their head
            character.JumpTime = jumpTime;
            // By default assume player isn't on ground unless they collide on the bottom
            character.IsOnGround = false;
            // By default assume players new velocity doesn't change due to collisions
            character.Velocity = new Vector2(velocityX, velocityY);

            // Move character to predicted Y position
            character.Position = new Vector2(character.Position.X, character.Position.Y + velocityY * elapsedTime);

            bool goingDown = velocityY > 0;

            foreach (var component in collidableComponents)
            {
                // Check if we collide with anything due to downwards movement
                if (goingDown && character.HitBox.IntersectsWith(component.HitBox))
                {
                    character.Position = new Vector2(character.Position.X, component.HitBox.Top - character.HitBox.Height);
                    character.Velocity = new Vector2(character.Velocity.X, 0);
                    character.JumpTime = 0;
                    character.IsOnGround = true;
                }

                // Check if we collide with anything due to upwards movement
                if (!goingDown && character.HitBox.IntersectsWith(component.HitBox))
                {
                    character.Position = new Vector2(character.Position.X, component.HitBox.Bottom);
                    character.Velocity = new Vector2(character.Velocity.X, 0);
                    character.JumpTime = 0;
                }
            }

            // Move position to predicted X position
            character.Position = new Vector2(character.Position.X + velocityX * elapsedTime, character.Position.Y);

            bool goingLeft = velocityX < 0;

            foreach (var component in collidableComponents)
            {
                // Check if we collide with anything due to leftwards movement
                if (goingLeft && character.HitBox.IntersectsWith(component.HitBox))
                {
                    character.Position = new Vector2(component.HitBox.Right, character.Position.Y);
                    character.Velocity = new Vector2(0, character.Velocity.Y);
                }

                // Check if we collide with anything due to rightwards movement
                if (!goingLeft && character.HitBox.IntersectsWith(component.HitBox))
                {
                    character.Position = new Vector2(component.HitBox.Left - character.HitBox.Width, character.Position.Y);
                    character.Velocity = new Vector2(0, character.Velocity.Y);
                }
            }

            // Set player inputs to none now that they've been dealt with
            character.PlayerInputs = PlayerInputs.NONE;
        }

        private float Clamp(float number, float min, float max) => Math.Max(min, Math.Min(number, max));
    }
}
