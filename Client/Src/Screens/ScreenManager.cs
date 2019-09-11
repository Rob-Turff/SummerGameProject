using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Client.Src.Screens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Src.Screens
{
    internal class ScreenManager
    {
        //private MenuScreen menuScreen;
        //private SettingScreen settingScreen;
        //private readonly GameScreen gameScreen;
        //private PlayGameScreen playGameScreen;
        //private LoginScreen loginScreen;
        //private LobbyScreen lobbyScreen;

        private GraphicsDeviceManager graphics;

        private Screen currentScreen;
        private Dictionary<Type, Screen> savedScreens; // Screens that can be reused

        public Screen CurrentScreen
        {
            get => currentScreen;
            set
            {
                currentScreen.UnloadContent();
                currentScreen = value;
                currentScreen.LoadContent();
                ChangeRes(currentScreen.ScreenWidth, currentScreen.ScreenHeight, currentScreen.IsFullScreen);
            }
        }


        public ScreenManager(Game1 game, GraphicsDeviceManager graphics)
        {
            this.graphics = graphics;

            savedScreens = new Dictionary<Type, Screen>()
            {
                {typeof(MenuScreen),new MenuScreen(game)},
                {typeof(SettingScreen),new SettingScreen(game) },
                {typeof(LoginScreen),new LoginScreen(game) },
                {typeof(MultiplayerScreen),new MultiplayerScreen(game) },
            };

            currentScreen = savedScreens[typeof(LoginScreen)];
            ChangeRes(currentScreen.ScreenWidth, currentScreen.ScreenHeight, currentScreen.IsFullScreen);
        }

        public void ChangeToSavedScreen<T>() where T : Screen
        {
            if (savedScreens.ContainsKey(typeof(T)))
            {
                CurrentScreen = savedScreens[typeof(T)];
            }
            else
            {
                throw new KeyNotFoundException("A screen of type " + typeof(T) + "is not contained in the savedScreens dictionary");
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
