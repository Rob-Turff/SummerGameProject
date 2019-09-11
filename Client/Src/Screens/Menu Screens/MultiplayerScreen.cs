using Microsoft.Xna.Framework;
using Client.Src.Components;
using System;
using ServerFacadeNS;
using Common.Src;
using System.Threading;

namespace Client.Src.Screens
{
    public class MultiplayerScreen : Screen
    {
        public MultiplayerScreen(Game1 game) : base(game)
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
            throw new NotImplementedException();
        }

        private void HostGame()
        {
            (new ServerFacade()).CreateAndStartServer(true);
            GameClient client = new GameClient(Game.PlayerName);
            client.Connect("localhost", GamePeer.DefaultPort);

            LobbyScreen lobbyScreen = new LobbyScreen(Game, client, true);
            client.RegisterPacketHandler(lobbyScreen);
            Game.ScreenManager.CurrentScreen = lobbyScreen;
        }
    }
}
