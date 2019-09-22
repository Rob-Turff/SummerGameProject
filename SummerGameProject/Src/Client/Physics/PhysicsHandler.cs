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

        private const float gravityAcceleration = 100f;

        #endregion

        private readonly Screen screen;
        private float dragFactor;

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
            float elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            foreach (var entity in screen.entities)
            {
                entity.velocity.X += entity.horizontalAcceleration * elapsedTime;
                entity.velocity.Y += gravityAcceleration * elapsedTime + entity.verticalAcceleration * elapsedTime;

                // Apply drag
                if (entity.isInAir)
                {
                    dragFactor = entity.airDrag;
                } else
                {
                    dragFactor = entity.groundFriction;
                }

                entity.velocity.X *= dragFactor;
                entity.velocity.Y *= dragFactor;

                // Enforce max speed
                entity.velocity.X = MathHelper.Clamp(entity.velocity.X, -entity.maxHorzizontalSpeed, entity.maxHorzizontalSpeed);
                entity.velocity.Y = MathHelper.Clamp(entity.velocity.Y, -entity.maxVerticalSpeed, entity.maxVerticalSpeed);

                // Detect if player is falling
                if (entity.velocity.Y > 0)
                    entity.isInAir = true;

                entity.Position = entity.Position + entity.velocity * elapsedTime;
            }
        }

        private void CheckCollisons(GameTime gameTime)
        {
            
        }
    }
}
