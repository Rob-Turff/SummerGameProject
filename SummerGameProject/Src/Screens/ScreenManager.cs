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
        private MenuScreen menuScreen;
        private SettingScreen settingScreen;
        private GameScreen gameScreen;
        private InGameMenuScreen inGameMenuScreen;

        private GraphicsDeviceManager graphics;

        public Screen CurrentScreen { get; private set; }

        public bool ToggleMenuOverlay { set; get; } = false;
        public enum ScreenEnum { Game, Menu, Setting };

        public ScreenManager(MainGame game, GraphicsDeviceManager graphics)
        {
            this.graphics = graphics;

            gameScreen = new GameScreen(game);
            menuScreen = new MenuScreen(game);
            settingScreen = new SettingScreen(game);
            inGameMenuScreen = new InGameMenuScreen(game,gameScreen);

            CurrentScreen = menuScreen;
            ChangeRes(CurrentScreen.ScreenWidth, CurrentScreen.ScreenHeight, CurrentScreen.IsFullScreen);
            game.IsMouseVisible = true;
        }

        public void ChangeScreen(ScreenEnum screenEnum)
        {
            // Unloads the content from the current screen when switching (also unload inGameMenu if switching from game screen)
            if (CurrentScreen != null)
            {
                CurrentScreen.UnloadContent();
                if (CurrentScreen == gameScreen)
                {
                    inGameMenuScreen.UnloadContent();
                    ToggleMenuOverlay = false;
                }
            }

            switch (screenEnum)
            {
                case ScreenEnum.Game:
                    CurrentScreen = gameScreen;
                    CurrentScreen.LoadContent();
                    inGameMenuScreen.LoadContent();
                    break;
                case ScreenEnum.Menu:
                    CurrentScreen = menuScreen;
                    CurrentScreen.LoadContent();
                    break;
                case ScreenEnum.Setting:
                    CurrentScreen = settingScreen;
                    CurrentScreen.LoadContent();
                    break;
            }
            ChangeRes(CurrentScreen.ScreenWidth, CurrentScreen.ScreenHeight, CurrentScreen.IsFullScreen);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            CurrentScreen.Draw(gameTime, spriteBatch);

            if (ToggleMenuOverlay)
            {
                inGameMenuScreen.Draw(gameTime, spriteBatch);
            }
        }

        public void Update(GameTime gameTime)
        {
            CurrentScreen.Update(gameTime);

            if (ToggleMenuOverlay)
            {
                inGameMenuScreen.Update(gameTime);
            }
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
