using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SummerGameProject.Src.Client.Components.Player;
using SummerGameProject.Src.Client.Utilities;
using SummerGameProject.Src.Components;
using SummerGameProject.Src.Screens;
using SummerGameProject.Src.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummerGameProject.Src.Client.Components
{
    public abstract class Ability : Entity
    {
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private readonly Screen screen;
        protected PlayerStats playerStats;
        protected MouseState mouseState;
        protected Animation animation;
        protected AnimationHandler animationHandler;

        public Ability(Screen screen, PlayerStats playerAttributes, MouseState mouseState) : base(screen)
        {
            this.screen = screen;
            this.playerStats = playerAttributes;
            this.mouseState = mouseState;
        }

        /// <summary>
        /// Calculates the velocity vector of the ability needed to achieve target speed in the direction of the mouse cursor
        /// For reference 3000 is the max player move speed
        /// </summary>
        /// <param name="targetSpeed"></param>
        protected void SetInitialVelocity(float targetSpeed)
        {
            Vector2 mousePos = screen.Cam.CalcWorldCoords(mouseState.Position.ToVector2());
            float angle = playerStats.player.GetAngleToCentre(mousePos);
            velocity = new Vector2((float)-(Math.Sin(angle) * targetSpeed), (float)(Math.Cos(angle) * targetSpeed));
            logger.Debug("New Ability created with initial velocity: " + velocity + " ----- using mouse coords: " + mousePos + " ------ and player location: " + playerStats.position);
        }

        protected void SetPositionOffset()
        {
            Vector2 vUnitVector = velocity;
            vUnitVector.Normalize();
            Position = playerStats.position + vUnitVector * Math.Max(Width, Height) * 1.5f;
            logger.Debug("Ability position offset: " + vUnitVector * Math.Max(Width, Height) * 1.5f);
        }

        /// <summary>
        /// Calculates the angle of the animation so that it faces the direction of travel
        /// </summary>
        protected void AngleForward()
        {
            animation.Angle = (float) Math.Atan2(velocity.Y, velocity.X);
        }
    }
}
