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
        public MultiplayerScreen(MainGame game, bool useResScaling) : base(game, useResScaling)
        {
            ScreenWidth = 400;
            ScreenHeight = 500;
            IsFullScreen = false;

            Button joinGameBtn = new Button("Join Game", new Vector2(0, 0), JoinGame, this);
            Button hostGameBtn = new Button("Host Game", new Vector2(0, 0), HostGame, this);

            components.Add(joinGameBtn);
            components.Add(hostGameBtn);
        }

        public override void LoadContent()
        {
            base.LoadContent();
            DistributeVertically(components);
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
