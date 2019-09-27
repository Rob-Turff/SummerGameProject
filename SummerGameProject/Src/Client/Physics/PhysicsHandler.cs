using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using SummerGameProject.Src.Client.Components;
using SummerGameProject.Src.Screens;

namespace SummerGameProject.Src.Client.Physics
{
    public class PhysicsHandler
    {
        #region Constants

        private const float gravityAcceleration = 200f;

        #endregion

        private readonly Screen screen;
        private float dragFactor;
        private float newXVelocity;
        private float newYVelocity;

        public PhysicsHandler(Screen screen)
        {
            this.screen = screen;
        }

        internal void Update(GameTime gameTime)
        {
            ApplyPhysics(gameTime);
            CheckCollisons(gameTime);
        }

        private void ApplyPhysics(GameTime gameTime)
        {
            float eTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            foreach (var entity in screen.entities)
            {
                entity.velocity.X += entity.horizontalAcceleration * eTime;
                entity.velocity.Y += gravityAcceleration * eTime + entity.verticalAcceleration * eTime;

                // Apply drag
                if (entity.isInAir)
                {
                    dragFactor = entity.airDrag;
                } else
                {
                    dragFactor = entity.groundFriction;
                }

                if (entity.velocity.X > 0)
                    newXVelocity = entity.velocity.X - (Square(entity.velocity.X) * dragFactor * eTime);
                else
                    newXVelocity = entity.velocity.X + (Square(entity.velocity.X) * dragFactor * eTime);

                if (entity.velocity.Y > 0)
                    newYVelocity = entity.velocity.Y - (Square(entity.velocity.Y) * dragFactor * eTime);
                else
                    newYVelocity = entity.velocity.Y + (Square(entity.velocity.Y) * dragFactor * eTime);

                entity.velocity = new Vector2(newXVelocity, newYVelocity);

                // Enforce max speed
                entity.velocity.X = MathHelper.Clamp(entity.velocity.X, -entity.maxHorzizontalSpeed, entity.maxHorzizontalSpeed);
                entity.velocity.Y = MathHelper.Clamp(entity.velocity.Y, -entity.maxVerticalSpeed, entity.maxVerticalSpeed);

                // Detect if player is falling
                if (entity.velocity.Y > 0)
                    entity.isInAir = true;

                entity.Position = entity.Position + entity.velocity * eTime;
            }
        }

        private void CheckCollisons(GameTime gameTime)
        {
            
        }

        private float Square(float value)
        {
            return value * value;
        }
    }
}
