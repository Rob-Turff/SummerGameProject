using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SummerGameProject.Src.Components;
using Microsoft.Xna.Framework.Input;
using SummerGameProject.Src.Components.Player;

namespace SummerGameProject.Src.Screens
{
    class GameScreen : Screen
    {
        Player player;
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private bool wasEscapePressed = false;

        public GameScreen(MainGame game, GraphicsDeviceManager graphics) : base(game, graphics)
        {
            player = new Player(new Vector2(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight / 2), this, "Player1");
            Components.Add(player);
        }

        public void Initialize()
        {

        }

        public override void LoadContent()
        {
            player.LoadContent();
            Texture2D floorTexture = Content.Load<Texture2D>("Game/GroundV1");
            Platform floor = new Platform(floorTexture, new Vector2(0, graphics.PreferredBackBufferHeight - floorTexture.Height), Color.White);
            Vector2 platfrom1Pos = new Vector2(300, 650);
            Platform platform1 = new Platform(floorTexture, platfrom1Pos, Color.White, 0.2f);
            Vector2 platfrom2Pos = new Vector2(1200, 650);
            Platform platform2 = new Platform(floorTexture, platfrom2Pos, Color.White, 0.2f);

            Texture2D wallTexture = Content.Load<Texture2D>("Game/Stonewall");
            Vector2 wallLeftPos = new Vector2(0, graphics.PreferredBackBufferHeight - wallTexture.Height - floorTexture.Height);
            Platform wallLeft = new Platform(wallTexture, wallLeftPos, Color.White);
            Vector2 wallRightPos = new Vector2(graphics.PreferredBackBufferWidth - wallTexture.Width, graphics.PreferredBackBufferHeight - wallTexture.Height - floorTexture.Height);
            Platform wallRight = new Platform(wallTexture, wallRightPos, Color.White);

            Components.Add(floor);
            Components.Add(platform1);
            Components.Add(platform2);
            Components.Add(wallLeft);
            Components.Add(wallRight);
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
