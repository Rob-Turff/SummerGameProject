﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummerGameProject.Src.Screens
{
    public class ScreenManager
    {
        public Screen CurrentScreen { get; private set; }
        public enum ScreenEnum { Game, Menu, Setting };

        private GameScreen gameScreen;
        private MenuScreen menuScreen;
        private SettingScreen settingScreen;
        private MainGame game;
        private GraphicsDeviceManager graphics;

        public ScreenManager(MainGame game, GraphicsDeviceManager graphics)
        {
            this.game = game;
            this.graphics = graphics;

            gameScreen = new GameScreen(game, graphics);
            menuScreen = new MenuScreen(game, graphics);
            settingScreen = new SettingScreen(game, graphics);
        }

        public void ChangeScreen(ScreenEnum screenEnum)
        {
            if (CurrentScreen != null)
                CurrentScreen.UnloadContent();

            switch (screenEnum)
            {
                case ScreenEnum.Game:
                    changeRes(1920, 1080, false);
                    CurrentScreen = gameScreen;
                    CurrentScreen.LoadContent();
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
        }

        private void changeRes(int width, int height, bool fullscreen) {
            graphics.PreferredBackBufferWidth = width;
            graphics.PreferredBackBufferHeight = height;
            graphics.IsFullScreen = fullscreen;
            graphics.ApplyChanges();
        }
    }
}
