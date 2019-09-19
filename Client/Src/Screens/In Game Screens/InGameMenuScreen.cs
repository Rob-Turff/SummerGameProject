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

            Button quitGameBtn = new Button("Quit Game", new Vector2(0, 0), quitGameBtnAction, this)
            { CentreOnPosition = true };
            Button quitToMenuBtn = new Button("Quit to Menu", new Vector2(0, 0), quitToMenuBtnAction, this)
            { CentreOnPosition = true };
            Button settingsBtn = new Button("Settings", new Vector2(0, 0), settingsBtnAction, this)
            { CentreOnPosition = true };

            UIComponents.Add(quitGameBtn);
            UIComponents.Add(quitToMenuBtn);
            UIComponents.Add(settingsBtn);
        }

        public override void LoadContent()
        {
            base.LoadContent();

            DistributeVertically(UIComponents);
        }
    }
}
