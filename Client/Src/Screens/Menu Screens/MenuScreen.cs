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
using Lidgren.Network;

namespace Client.Src.Screens
{
    internal class MenuScreen : Screen
    {
        public MenuScreen(Game1 game) : base(game)
        {
            ScreenWidth = 400;
            ScreenHeight = 500;
            IsFullScreen = false;

            //Action singleplayerBtnAction = new Action(() => StartSinglePlayer());
            Action startGameBtnAction = new Action(() => game.ScreenManager.ChangeToSavedScreen<MultiplayerScreen>());
            Action settingsBtnAction = new Action(() => game.ScreenManager.ChangeToSavedScreen<SettingScreen>());
            Action quitBtnAction = new Action(() => game.Exit());

            //Button singleplayerBtn = new Button("Singleplayer", new Vector2(0, 0), singleplayerBtnAction, this)
            //{ CentreOnPosition = true };
            Button startGameBtn = new Button("Multiplayer", new Vector2(0, 0), startGameBtnAction, this)
            { CentreOnPosition = true };
            Button settingsBtn = new Button("Settings", new Vector2(0, 0), settingsBtnAction, this)
            { CentreOnPosition = true };
            Button quitBtn = new Button("Quit", new Vector2(0, 0), quitBtnAction, this)
            { CentreOnPosition = true };

            //UIComponents.Add(singleplayerBtn);
            UIComponents.Add(startGameBtn);
            UIComponents.Add(settingsBtn);
            UIComponents.Add(quitBtn);
        }


        public override void LoadContent()
        {
            base.LoadContent();

            DistributeVertically(UIComponents);
        }

        //private void StartSinglePlayer()
        //{
        //    (new ServerFacade()).CreateAndStartServer(false);
        //    NetClient client = new NetClient(NetworkSettings.DefaultNetPeerConfiguration);
        //    client.Connect("localhost", NetworkSettings.DefaultPort);
        //    Game.ScreenManager.CurrentScreen = new GameScreen(Game,client);//TODO 
        //}
    }
}
