using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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

        private GraphicsDeviceManager graphics;

        public Screen CurrentScreen { get; private set; }

        public enum ScreenEnum { Game, Menu, Setting };

        public ScreenManager(MainGame game, GraphicsDeviceManager graphics)
        {
            this.graphics = graphics;

            gameScreen = new GameScreen(game);
            menuScreen = new MenuScreen(game);
            settingScreen = new SettingScreen(game);

            CurrentScreen = menuScreen;
            ChangeRes(CurrentScreen.ScreenWidth, CurrentScreen.ScreenHeight, CurrentScreen.IsFullScreen);
            game.IsMouseVisible = true;
        }

        public void ChangeScreen(ScreenEnum screenEnum)
        {
            // Unloads the content from the current screen when switching
            CurrentScreen.UnloadContent();

            switch (screenEnum)
            {
                case ScreenEnum.Game:
                    gameScreen.LoadContent();
                    CurrentScreen = gameScreen;
                    break;
                case ScreenEnum.Menu:
                    menuScreen.LoadContent();
                    CurrentScreen = menuScreen;
                    break;
                case ScreenEnum.Setting:
                    settingScreen.LoadContent();
                    CurrentScreen = settingScreen;
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
