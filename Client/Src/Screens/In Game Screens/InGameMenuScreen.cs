using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Client.Src.Components;

namespace Client.Src.Screens
{
    class InGameMenuScreen : Screen
    {
        public InGameMenuScreen(Game1 game, GameScreen gameScreen) : base(game)
        {
            ScreenWidth = gameScreen.ScreenWidth;
            ScreenHeight = gameScreen.ScreenHeight;
            IsFullScreen = gameScreen.IsFullScreen;

            Action quitGameBtnAction = new Action(() => game.Exit());
            Action quitToMenuBtnAction = new Action(() => game.ScreenManager.ChangeToSavedScreen<MenuScreen>());
            Action settingsBtnAction = new Action(() => game.ScreenManager.ChangeToSavedScreen<SettingScreen>());

            Button quitGameBtn = new Button("Quit Game", new Vector2(0,0), quitGameBtnAction, this);
            Button quitToMenuBtn = new Button("Quit to Menu", new Vector2(0,0), quitToMenuBtnAction, this);
            Button settingsBtn = new Button("Settings", new Vector2(0,0), settingsBtnAction, this);

            Components.Add(quitGameBtn);
            Components.Add(quitToMenuBtn);
            Components.Add(settingsBtn);
        }

        public override void LoadContent()
        {
            base.LoadContent();

            DistributeVertically(Components);
        }
    }
}
