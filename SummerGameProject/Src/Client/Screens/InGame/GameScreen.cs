using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SummerGameProject.Src.Client.Components.Player;
using SummerGameProject.Src.Components;
using SummerGameProject.Src.Components.Platforms;
using SummerGameProject.Src.Components.Player;

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

            menuOverlay = new InGameMenuScreen(game, this);

            PlayerAttributes playerAttributes = new PlayerAttributes("Player 1", new System.Guid());
            playerAttributes.position = new Vector2(ScreenWidth / 2, ScreenHeight / 2);
            Player player = new Player(playerAttributes, this);
            Components.Add(player);

            Platform floor = new GrassPlatform(new Vector2(0, 880), new Vector2(1f, 1f), this);
            Platform platform1 = new GrassPlatform(new Vector2(300, 650), new Vector2(0.2f, 0.2f), this);
            Platform platform2 = new GrassPlatform(new Vector2(1200, 650), new Vector2(0.2f, 0.2f), this);
            Platform wallLeft = new StoneWallPlatform(new Vector2(0, 380), new Vector2(1f, 1f), this);
            Platform wallRight = new StoneWallPlatform(new Vector2(1870, 380), new Vector2(1f, 1f), this);

            Components.Add(floor);
            Components.Add(platform1);
            Components.Add(platform2);
            Components.Add(wallLeft);
            Components.Add(wallRight);
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
