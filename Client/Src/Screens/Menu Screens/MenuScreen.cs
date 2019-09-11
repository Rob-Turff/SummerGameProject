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
using ServerFacadeNS;
using Common.Src;

namespace Client.Src.Screens
{
    public class MenuScreen : Screen
    {
        public MenuScreen(Game1 game) : base(game)
        {
            ScreenWidth = 400;
            ScreenHeight = 500;
            IsFullScreen = false;

            Action singleplayerBtnAction = new Action(() => StartSinglePlayer());
            Action multiplayerBtnAction = new Action(() => game.ScreenManager.ChangeToSavedScreen<MultiplayerScreen>());
            Action settingsBtnAction = new Action(() => game.ScreenManager.ChangeToSavedScreen<SettingScreen>());
            Action quitBtnAction = new Action(() => game.Exit());

            Button singleplayerBtn = new Button("Singleplayer", new Vector2(0,0), singleplayerBtnAction, this);
            Button multiplayerGameBtn = new Button("Multiplayer", new Vector2(0,0), multiplayerBtnAction, this);
            Button settingsBtn = new Button("Settings", new Vector2(0,0), settingsBtnAction, this);
            Button quitBtn = new Button("Quit", new Vector2(0, 0), quitBtnAction, this);

            Components.Add(singleplayerBtn);
            Components.Add(multiplayerGameBtn);
            Components.Add(settingsBtn);
            Components.Add(quitBtn);
        }


        public override void LoadContent()
        {
            base.LoadContent();

            DistributeVertically(Components);
        }

        private void StartSinglePlayer()
        {
            (new ServerFacade()).CreateAndStartServer(false);
            GameClient client = new GameClient(Game.PlayerName);
            client.Connect("localhost", GamePeer.DefaultPort);
            Game.ScreenManager.CurrentScreen = new GameScreen(Game, client);
        }
    }
}
