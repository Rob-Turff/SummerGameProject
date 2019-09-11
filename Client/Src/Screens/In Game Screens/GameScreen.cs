using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Client.Src.Components.Player;
using Client.Src.Components;
using Client.Src.Components.Platforms;
using System.Collections.Generic;
using Common.Src.Packets;
using System;
using Common.Src.Packets.ServerToClient;

namespace Client.Src.Screens
{
    class GameScreen : NetworkScreen
    {
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private bool wasEscapePressed = false;

        private InGameMenuScreen menuOverlay;
        private bool isMenuOverlayShowing;

        public GameScreen(Game1 game, GameClient gameClient) : base(game, gameClient)
        {
            ScreenWidth = 1920;
            ScreenHeight = 1080;

            menuOverlay = new InGameMenuScreen(game, this);

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

        public void SetupGame()
        {
            //if (game.GameData.isMultiplayer)
            //{
            //    foreach (PlayerAttributes pa in game.GameData.players)
            //    {
            //        Components.Add(new Player(pa, this, game));
            //    }
            //}
            //else
            //{
            //    PlayerAttributes playerAttributes = new PlayerAttributes("Player 1", new System.Guid(), true);
            //    playerAttributes.position = new Vector2(ScreenWidth / 2, ScreenHeight / 2);
            //    Player player = new Player(playerAttributes, this, game);
            //    Components.Add(player);
            //}

            //LoadContent();
        }


        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (isMenuOverlayShowing)
            {
                menuOverlay.Draw(gameTime, spriteBatch);
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
                    isOverlayShowing = !wasOverlayShowing;
                    wasEscapePressed = true;
                }
            }
            else
            { 
                wasEscapePressed = false;
            }

            return isOverlayShowing;
        }

        public override void HandleMatchStartedPacket(MatchStartedPacket matchStartedPacket)
        {
            logger.Error("Match started packet recieved when the match has already started");
        }

        public override void HandleLobbyInfoPacket(LobbyInfoPacket clientNamesPacket)
        {
            logger.Error("Lobby information packet recieved when the match has already started");
        }
    }
}
