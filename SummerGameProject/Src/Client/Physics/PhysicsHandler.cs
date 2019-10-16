using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using SummerGameProject.Src.Client.Components;
using SummerGameProject.Src.Components;
using SummerGameProject.Src.Screens;

namespace SummerGameProject.Src.Client.Physics
{
    public class PhysicsHandler
    {
        #region Constants

        private const float gravityAcceleration = 200f;

        #endregion

        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private readonly Screen screen;
        private float dragFactor;
        private float newXVelocity;
        private float newYVelocity;
        private Queue<Component> collidedComponents = new Queue<Component>();

        public PhysicsHandler(Screen screen)
        {
            this.screen = screen;
        }

        internal void Update(GameTime gameTime)
        {
            ApplyPhysics(gameTime);
            CheckCollisons(gameTime);

            while (collidedComponents.Count != 0)
            {
                var comp = collidedComponents.Dequeue();
                comp.OnCollide();
            }
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
            foreach (Entity e in screen.entities)
            {
                IEnumerable<Entity> entitiesBarThis = screen.entities.Except(new List<Entity> { e });

                foreach (var e2 in entitiesBarThis)
                {
                    CompareHitboxes(e, e2);
                }

                foreach (var e2 in screen.components)
                {
                    CompareHitboxes(e, e2);
                }
            }
        }

        private void CompareHitboxes(Entity e, Component e2)
        {
            bool collided = false;

            // Collided Bottom
            if (e.Hitbox.Bottom <= e2.Hitbox.Bottom && e.Hitbox.Bottom >= e2.Hitbox.Top && e.Hitbox.IntersectsWith(e2.Hitbox))
            {
                e.Position = new Vector2(e.Position.X, e2.Hitbox.Top - e.Hitbox.Height - 0.001f);
                e.velocity.Y = 0;
                collided = true;
            }

            // Collided top
            if (e.Hitbox.Top >= e2.Hitbox.Top && e.Hitbox.Top <= e2.Hitbox.Bottom && e.Hitbox.IntersectsWith(e2.Hitbox))
            {
                e.Position = new Vector2(e.Position.X, e2.Hitbox.Bottom + 0.001f);
                e.velocity.Y = 0;
                collided = true;
            }

            // Collided left
            if (e.Hitbox.Left <= e2.Hitbox.Right && e.Hitbox.Left >= e2.Hitbox.Left && e.Hitbox.IntersectsWith(e2.Hitbox))
            {
                e.Position = new Vector2(e2.Hitbox.Right + 0.001f, e.Position.Y);
                e.velocity.X = 0;
                collided = true;
            }

            // Collided right
            if (e.Hitbox.Right >= e2.Hitbox.Left && e.Hitbox.Right <= e2.Hitbox.Right && e.Hitbox.IntersectsWith(e2.Hitbox))
            {
                e.Position = new Vector2(e2.Hitbox.Left - e.Hitbox.Width - 0.001f, e.Position.Y);
                e.velocity.X = 0;
                collided = true;
            }

            if (collided)
            {
                collidedComponents.Enqueue(e);
                collidedComponents.Enqueue(e2);
            }
        }

        private float Square(float value)
        {
            return value * value;
        }
    }
}
