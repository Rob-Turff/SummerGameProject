using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SummerGameProject.Src.Components;

namespace SummerGameProject.Src.Screens
{
    class GameScreen : Screen
    {
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private bool wasEscapePressed = false;

        private InGameMenuScreen menuOverlay;
        private bool isMenuOverlayShowing;

        public GameScreen(MainGame game) : base(game)
        {
            ScreenWidth = 1920;
            ScreenHeight = 1080;
            IsFullScreen = true;

            menuOverlay = new InGameMenuScreen(game, this);
            Platform floor = new FloorPlatform(this);
            components.Add(floor);
        }


        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (isMenuOverlayShowing)
            {
                menuOverlay.Draw(gameTime,spriteBatch);
            }
            base.Draw(gameTime, spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            isMenuOverlayShowing = checkIfMenuToggled();
            
            if (isMenuOverlayShowing)
            {
                menuOverlay.Update(gameTime);
            }
            else
            {
                // Potential Issue: Might update twice in one cycle
                base.Update(gameTime);
            }
        }

        public override void LoadContent()
        {
            menuOverlay.LoadContent();
            base.LoadContent();
        }

        public override void UnloadContent()
        {
            menuOverlay.UnloadContent();
            base.UnloadContent();
        }

        private bool checkIfMenuToggled()
        {
            bool wasOverlayShowing = isMenuOverlayShowing;
            bool isOverlayShowing = wasOverlayShowing;

            KeyboardState keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Escape))
            {
                if (wasEscapePressed == false)
                {
                    logger.Debug("Menu toggled in game screen");
                    isOverlayShowing = !isMenuOverlayShowing;
                    wasEscapePressed = true;
                }
            }
            else
            {
                wasEscapePressed = false;
            }

            return isOverlayShowing;
        }
    }
}
