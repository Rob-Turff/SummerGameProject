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
        private MainGame game;
        private GraphicsDeviceManager graphics;

        private MenuScreen menuScreen;
        private SettingScreen settingScreen;
        private GameScreen gameScreen;
        private InGameMenuScreen inGameMenuScreen;

        public Screen CurrentScreen { get; private set; }

        public bool ToggleMenuOverlay { set; get; } = false;
        public enum ScreenEnum { Game, Menu, Setting };

        public ScreenManager(MainGame game, GraphicsDeviceManager graphics)
        {
            this.game = game;
            this.graphics = graphics;

            gameScreen = new GameScreen(game, graphics);
            menuScreen = new MenuScreen(game, graphics);
            settingScreen = new SettingScreen(game, graphics);
            inGameMenuScreen = new InGameMenuScreen(game, graphics);

            ChangeScreen(ScreenEnum.Menu);
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
                    changeRes(1920, 1080, true);
                    CurrentScreen = gameScreen;
                    CurrentScreen.LoadContent();
                    inGameMenuScreen.LoadContent();
                    break;
                case ScreenEnum.Menu:
                    changeRes(400, 500, false);
                    CurrentScreen = menuScreen;
                    CurrentScreen.LoadContent();
                    break;
                case ScreenEnum.Setting:
                    changeRes(400, 500, false);
                    CurrentScreen = settingScreen;
                    CurrentScreen.LoadContent();
                    break;
            }
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

        private void changeRes(int width, int height, bool fullscreen)
        {
            graphics.PreferredBackBufferWidth = width;
            graphics.PreferredBackBufferHeight = height;
            graphics.IsFullScreen = fullscreen;
            graphics.ApplyChanges();
        }

    }
}
