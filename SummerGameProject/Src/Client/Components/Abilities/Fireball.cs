using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SummerGameProject.Src.Client.Components.Player;
using SummerGameProject.Src.Client.Utilities;
using SummerGameProject.Src.Components;
using SummerGameProject.Src.Screens;
using SummerGameProject.Src.Utilities;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummerGameProject.Src.Client.Components
{
    public class Fireball : Ability
    {
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private int moveAnimationFrames = 4;

        public Fireball(Screen screen, PlayerStats playerAttributes, MouseState mouseState) : base(screen, playerAttributes, mouseState)
        {
            animation = new Animation(moveAnimationFrames);
            animationHandler = new AnimationHandler(animation, this, screen);
            animation.FrameSpeed = 0.25f;
            LoadContent();
            animationHandler.Play();
            Screen.entities.Add(this);
            SetInitialVelocity(800);
            SetPositionOffset();
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            animationHandler.Draw(spriteBatch);
        }

        public override void LoadContent()
        {
            Texture = Screen.Content.Load<Texture2D>("Game/Fireball");
            animation.Texture = Screen.Content.Load<Texture2D>("Game/FireballSpriteSheet");
        }

        public override void Update(GameTime gameTime)
        {
            AngleForward();
            animationHandler.Update(gameTime);
        }

        public override void OnCollide()
        {
            // Do exploding stuff
            Screen.entities.Remove(this);
        }
    }
}
