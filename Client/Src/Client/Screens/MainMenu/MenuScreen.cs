using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Client.Src.Components;

namespace Client.Src.Screens
{
    public class MenuScreen : Screen
    {
        public MenuScreen(MainGame game) : base(game)
        {
            ScreenWidth = 400;
            ScreenHeight = 500;
            IsFullScreen = false;

            Action playGameBtnAction = new Action(() => game.ScreenManager.ChangeScreen(ScreenManager.ScreenEnum.GAME));
            Action settingsBtnAction = new Action(() => game.ScreenManager.ChangeScreen(ScreenManager.ScreenEnum.SETTING));
            Action multiplayerBtnAction = new Action(() => game.ScreenManager.ChangeScreen(ScreenManager.ScreenEnum.MULTIPLAYER));

            Button singlePlayerButton = new Button("SinglePlayer", new Vector2(0,0), playGameBtnAction, this);
            Button settingsBtn = new Button("Settings", new Vector2(0,0), settingsBtnAction, this);
            Button multiplayerBtn = new Button("Multiplayer", new Vector2(0, 0), multiplayerBtnAction, this);

            Components.Add(singlePlayerButton);
            Components.Add(settingsBtn);
            Components.Add(multiplayerBtn);
        }

        public override void LoadContent()
        {
            base.LoadContent();

            DistributeVertically(Components);
        }

    }
}
