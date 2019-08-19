using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SummerGameProject.Src.Components;
using SummerGameProject.Src.Components.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummerGameProject.Src.Utilities
{
    public class AnimationHandler
    {
        public bool MovingLeft { get; set; } = false;

        public float Scale { get; set; } = 1f;

        private readonly Component component;
        public Animation animation { get; private set; }
        private float timer = 0f;
        private bool doUpdate = false;

        public AnimationHandler(Animation animation, Component component)
        {
            this.animation = animation;
            this.component = component;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Rectangle sourceRectangle = new Rectangle(animation.CurrentFrame * animation.FrameWidth, 0, animation.FrameWidth, animation.Texture.Height);
            if (MovingLeft)
                spriteBatch.Draw(animation.Texture, component.Position, sourceRectangle, Color.White, 0f, Vector2.Zero, Scale, SpriteEffects.FlipHorizontally, 0f);
            else
                spriteBatch.Draw(animation.Texture, component.Position, sourceRectangle, Color.White, 0f, Vector2.Zero, Scale, SpriteEffects.None, 0f);
        }

        public void Stop()
        {
            timer = 0f;
            animation.CurrentFrame = 0;
            doUpdate = false;
        }

        public void Play()
        {
            doUpdate = true;
        }

        public void Update(GameTime gameTime)
        {
            if (doUpdate)
                timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (timer >= animation.FrameSpeed)
            {
                timer = 0f;
                animation.CurrentFrame++;

                if (animation.CurrentFrame >= animation.NumFrames)
                    animation.CurrentFrame = 0;
            }
        }
    }
}
