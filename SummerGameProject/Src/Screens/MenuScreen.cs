using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SummerGameProject.Src.Components;

namespace SummerGameProject.Src.Screens
{
    public class MenuScreen : Screen
    {
        public MenuScreen(MainGame game) : base(game)
        {
            ScreenWidth = 400;
            ScreenHeight = 500;
            IsFullScreen = false;

            Action playGameBtnAction = new Action(() => game.ScreenManager.ChangeScreen(ScreenManager.ScreenEnum.Game));
            Action settingsBtnAction = new Action(() => game.ScreenManager.ChangeScreen(ScreenManager.ScreenEnum.Setting));

            Button playGameBtn = new Button("Start Game", new Vector2(0,0), playGameBtnAction, this);
            Button settingsBtn = new Button("Settings", new Vector2(0,0), settingsBtnAction, this);

            components.Add(playGameBtn);
            components.Add(settingsBtn);
        }

        public override void LoadContent()
        {
            base.LoadContent();

            DistributeVertically(components);
        }

    }
}
