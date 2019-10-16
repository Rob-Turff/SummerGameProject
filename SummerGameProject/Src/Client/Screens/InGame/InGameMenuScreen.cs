using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SummerGameProject.Src.Components;

namespace SummerGameProject.Src.Screens
{
    class InGameMenuScreen : Screen
    {
        public InGameMenuScreen(MainGame game, GameScreen gameScreen, bool useResScaling) : base(game, useResScaling)
        {
            ScreenWidth = gameScreen.ScreenWidth;
            ScreenHeight = gameScreen.ScreenHeight;
            IsFullScreen = gameScreen.IsFullScreen;

            Action quitGameBtnAction = new Action(() => game.Exit());
            Action quitToMenuBtnAction = new Action(() => game.ScreenManager.ChangeScreen(ScreenManager.ScreenEnum.MENU));
            Action settingsBtnAction = new Action(() => game.ScreenManager.ChangeScreen(ScreenManager.ScreenEnum.SETTING));

            Button quitGameBtn = new Button("Quit Game", new Vector2(0,0), quitGameBtnAction, this);
            Button quitToMenuBtn = new Button("Quit to Menu", new Vector2(0,0), quitToMenuBtnAction, this);
            Button settingsBtn = new Button("Settings", new Vector2(0,0), settingsBtnAction, this);

            components.Add(quitGameBtn);
            components.Add(quitToMenuBtn);
            components.Add(settingsBtn);
        }

        public override void LoadContent()
        {
            base.LoadContent();
            DistributeVertically(components);
        }
    }
}
