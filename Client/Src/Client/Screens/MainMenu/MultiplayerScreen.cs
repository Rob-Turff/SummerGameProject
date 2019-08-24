using Microsoft.Xna.Framework;
using Client.Src.Client.Components;
using Client.Src.Client.Networking;
using Client.Src.Components;
using Client.Src.Screens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Client.Src.Screens.ScreenManager;

namespace Client.Src.Client.Screens
{
    public class MultiplayerScreen : Screen
    {
        public MultiplayerScreen(MainGame game) : base(game)
        {
            ScreenWidth = 400;
            ScreenHeight = 500;
            IsFullScreen = false;

            Button joinGameBtn = new Button("Join Game", new Vector2(0, 0), JoinGame, this);
            Button hostGameBtn = new Button("Host Game", new Vector2(0, 0), HostGame, this);

            Components.Add(joinGameBtn);
            Components.Add(hostGameBtn);
        }

        public override void LoadContent()
        {
            base.LoadContent();
            DistributeVertically(Components);
        }

        private void JoinGame()
        {
            game.networkHandler.StartClient();
        }

        private void HostGame()
        {
            game.networkHandler.HostServer();
        }
    }
}
