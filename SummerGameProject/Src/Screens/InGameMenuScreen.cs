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
        private Button quitGameBtn;
        private Button quitToMenuBtn;
        private Button settingsBtn;
        public InGameMenuScreen(MainGame game, GameScreen gameScreen) : base(game)
        {
            ScreenWidth = gameScreen.ScreenWidth;
            ScreenHeight = gameScreen.ScreenHeight;
            IsFullScreen = gameScreen.IsFullScreen;

            Action quitGameBtnAction = new Action(() => game.Exit());
            Action quitToMenuBtnAction = new Action(() => game.ScreenManager.ChangeScreen(ScreenManager.ScreenEnum.Menu));
            Action settingsBtnAction = new Action(() => game.ScreenManager.ChangeScreen(ScreenManager.ScreenEnum.Setting));

            quitGameBtn = new Button("Quit Game", new Vector2(0,0), quitGameBtnAction, this);
            quitToMenuBtn = new Button("Quit to Menu", new Vector2(0,0), quitToMenuBtnAction, this);
            settingsBtn = new Button("Settings", new Vector2(0,0), settingsBtnAction, this);

            components.Add(quitGameBtn);
            components.Add(quitToMenuBtn);
            components.Add(settingsBtn);
        }

        public override void LoadContent()
        {
            base.LoadContent();
            DistributeVertically(new List<Button> { quitGameBtn, quitToMenuBtn, settingsBtn });
        }
    }
}
