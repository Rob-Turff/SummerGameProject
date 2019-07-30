using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SummerGameProject.Src.Components.Sprites;

namespace SummerGameProject.Src.Screens
{
    class GameScreen : Screen
    {
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private KeyboardState keyboardState;
        private bool escPressed = false;

        public GameScreen(MainGame game, GraphicsDeviceManager graphics) : base(game, graphics)
        {
        }

        public override void LoadContent()
        {
            Texture2D floorTexture = Content.Load<Texture2D>("Game/GroundV1");
            Platform floor = new Platform(floorTexture, new Vector2(0, graphics.PreferredBackBufferHeight - floorTexture.Height), Color.White);
            components.Add(floor);
        }

        public override void Update(GameTime gameTime)
        {
            keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Escape))
            {
                escPressed = true;
            } 
            else if (escPressed)
            {
                logger.Debug("Menu toggled in game screen");
                game.ScreenManager.ToggleMenuOverlay = !game.ScreenManager.ToggleMenuOverlay;
                escPressed = false;
            }

            // Potential Issue: Might update twice in one cycle
            base.Update(gameTime);
        }
    }
}
