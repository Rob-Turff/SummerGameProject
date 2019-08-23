using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SummerGameProject.Src.Client.Screens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummerGameProject.Src.Screens
{
    public class ScreenManager
    {
        private readonly MenuScreen menuScreen;
        private readonly SettingScreen settingScreen;
        private readonly GameScreen gameScreen;
        private readonly MultiplayerScreen multiplayerScreen;
        private readonly LoginScreen loginScreen;
        private readonly LobbyScreen lobbyScreen;
        private GraphicsDeviceManager graphics;

        public Screen CurrentScreen { get; private set; }

        public enum ScreenEnum { GAME, MENU, SETTING, MULTIPLAYER, LOGIN, LOBBY };

        public ScreenManager(MainGame game, GraphicsDeviceManager graphics)
        {
            this.graphics = graphics;

            gameScreen = new GameScreen(game);
            menuScreen = new MenuScreen(game);
            settingScreen = new SettingScreen(game);
            multiplayerScreen = new MultiplayerScreen(game);
            loginScreen = new LoginScreen(game);
            lobbyScreen = new LobbyScreen(game);

            CurrentScreen = loginScreen;
            ChangeRes(CurrentScreen.ScreenWidth, CurrentScreen.ScreenHeight, CurrentScreen.IsFullScreen);
            game.IsMouseVisible = true;
        }

        public void ChangeScreen(ScreenEnum screenEnum)
        {
            // Unloads the content from the current screen when switching
            CurrentScreen.UnloadContent();

            switch (screenEnum)
            {
                case ScreenEnum.GAME:
                    gameScreen.LoadContent();
                    gameScreen.SetupGame(); //TODO Handle quiting to menu then returning back to game
                    CurrentScreen = gameScreen;
                    break;
                case ScreenEnum.MENU:
                    menuScreen.LoadContent();
                    CurrentScreen = menuScreen;
                    break;
                case ScreenEnum.SETTING:
                    settingScreen.LoadContent();
                    CurrentScreen = settingScreen;
                    break;
                case ScreenEnum.MULTIPLAYER:
                    multiplayerScreen.LoadContent();
                    CurrentScreen = multiplayerScreen;
                    break;
                case ScreenEnum.LOGIN:
                    loginScreen.LoadContent();
                    CurrentScreen = loginScreen;
                    break;
                case ScreenEnum.LOBBY:
                    lobbyScreen.LoadContent();
                    CurrentScreen = lobbyScreen;
                    break;
            }

            ChangeRes(CurrentScreen.ScreenWidth, CurrentScreen.ScreenHeight, CurrentScreen.IsFullScreen);
        }

        private void ChangeRes(int width, int height, bool fullscreen)
        {
            graphics.PreferredBackBufferWidth = width;
            graphics.PreferredBackBufferHeight = height;
            graphics.IsFullScreen = fullscreen;
            graphics.ApplyChanges();
        }
    }
}
