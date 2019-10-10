using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SummerGameProject.Src.Client.Components;
using SummerGameProject.Src.Client.Components.Player;
using SummerGameProject.Src.Client.Physics;
using SummerGameProject.Src.Client.Utilities;
using SummerGameProject.Src.Components;
using SummerGameProject.Src.Components.Platforms;
using SummerGameProject.Src.Components.Player;
using System.Collections.Generic;

namespace SummerGameProject.Src.Screens
{
    public class GameScreen : Screen
    {
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private bool wasEscapePressed = false;
        private InGameMenuScreen menuOverlay;
        private bool isMenuOverlayShowing;

        private PhysicsHandler physicsHandler;

        public GameScreen(MainGame game) : base(game)
        {
            SetMaxScreenSize();

            physicsHandler = new PhysicsHandler(this);

            menuOverlay = new InGameMenuScreen(game, this);

            World world = new World(this);

            Camera camera = new Camera(ScreenSize);
        }

        public void SetupGame()
        {
            if (game.GameData.isMultiplayer)
            {
                foreach (PlayerStats pa in game.GameData.players)
                {
                    components.Add(new Player(pa, this, game));
                }
            } else
            {
                PlayerStats playerAttributes = new PlayerStats("Player 1", new System.Guid(), true);
                playerAttributes.position = new Vector2(ScreenWidth / 2, ScreenHeight / 2);
                Player player = new Player(playerAttributes, this, game);
                components.Add(player);
            }

            LoadContent();
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
            isMenuOverlayShowing = CheckIfMenuToggled();
            
            if (isMenuOverlayShowing)
            {
                menuOverlay.Update(gameTime);
            }
            else
            {
                base.Update(gameTime);
                physicsHandler.Update(gameTime);
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

        private bool CheckIfMenuToggled()
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
