using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SummerGameProject.Src.Components;
using Microsoft.Xna.Framework.Input;

namespace SummerGameProject.Src.Screens
{
    class GameScreen : Screen
    {
        Player player;
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private bool wasEscapePressed = false;

        public GameScreen(MainGame game, GraphicsDeviceManager graphics) : base(game, graphics)
        {
            player = new Player(new Vector2(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight / 2),this);
            components.Add(player);
        }

        public void Initialize()
        {
            
        }

        public override void LoadContent()
        {
            player.LoadContent();
            Texture2D floorTexture = Content.Load<Texture2D>("Game/GroundV1");
            Platform floor = new Platform(floorTexture, new Vector2(0, graphics.PreferredBackBufferHeight - floorTexture.Height), Color.White);
            components.Add(floor);
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Escape))
            {
                if (wasEscapePressed == false)
                {
                    logger.Debug("Menu toggled in game screen");
                    game.ScreenManager.ToggleMenuOverlay = !game.ScreenManager.ToggleMenuOverlay;
                    wasEscapePressed = true;
                }
            }
            else
            {
                wasEscapePressed = false;
            }

            // Potential Issue: Might update twice in one cycle
            base.Update(gameTime);
        }
    }
}
