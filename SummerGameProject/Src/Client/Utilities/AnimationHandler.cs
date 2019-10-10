using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SummerGameProject.Src.Components;
using SummerGameProject.Src.Screens;
using System;

namespace SummerGameProject.Src.Utilities
{
    public class AnimationHandler
    {
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public bool MovingLeft { get; set; } = false;

        public float Scale { get; set; } = 1f;

        private readonly Component component;
        private readonly Screen screen;

        public Animation animation { get; private set; }
        private float timer = 0f;
        private bool doUpdate = false;

        public AnimationHandler(Animation animation, Component component, Screen screen)
        {
            this.animation = animation;
            this.component = component;
            this.screen = screen;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (component.onScreen)
            {
                Rectangle sourceRectangle = new Rectangle(animation.CurrentFrame * animation.FrameWidth, 0, animation.FrameWidth, animation.Texture.Height);
                if (MovingLeft)
                    spriteBatch.Draw(animation.Texture, component.ScreenPos, sourceRectangle, Color.White, animation.Angle, Vector2.Zero, Scale, SpriteEffects.FlipHorizontally, 0f);
                else
                    spriteBatch.Draw(animation.Texture, component.ScreenPos, sourceRectangle, Color.White, animation.Angle, Vector2.Zero, Scale, SpriteEffects.None, 0f);
            }

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
