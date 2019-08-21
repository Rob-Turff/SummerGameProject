using Microsoft.Xna.Framework;
using SummerGameProject.Src.Client.Components;
using SummerGameProject.Src.Client.Networking;
using SummerGameProject.Src.Components;
using SummerGameProject.Src.Screens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SummerGameProject.Src.Screens.ScreenManager;

namespace SummerGameProject.Src.Client.Screens
{
    public class MultiplayerScreen : Screen
    {
        private NetworkHandler networkHandler;

        public MultiplayerScreen(MainGame game) : base(game)
        {
            ScreenWidth = 400;
            ScreenHeight = 500;
            IsFullScreen = false;

            networkHandler = new NetworkHandler(game);

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
            networkHandler.StartClient();
        }

        private void HostGame()
        {
            networkHandler.HostServer();
        }
    }
}
