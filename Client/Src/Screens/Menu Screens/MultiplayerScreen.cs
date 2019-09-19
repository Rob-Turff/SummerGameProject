using Microsoft.Xna.Framework;
using Client.Src.Components;
using System;
using ServerFacadeNS;
using Common.Src;
using System.Threading;
using Lidgren.Network;
using Common.Src.Packets.ClientToServer;

namespace Client.Src.Screens
{
    internal class MultiplayerScreen : Screen
    {
        public MultiplayerScreen(Game1 game) : base(game)
        {
            ScreenWidth = 400;
            ScreenHeight = 500;
            IsFullScreen = false;

            Button joinGameBtn = new Button("Join Game", new Vector2(0, 0), JoinGame, this)
            { CentreOnPosition = true };
            Button hostGameBtn = new Button("Host Game", new Vector2(0, 0), HostGame, this)
            { CentreOnPosition = true };

            UIComponents.Add(joinGameBtn);
            UIComponents.Add(hostGameBtn);
        }


        public override void LoadContent()
        {
            base.LoadContent();

            DistributeVertically(UIComponents);
        }


        private void JoinGame()
        {
            throw new NotImplementedException();
        }

        private void HostGame()
        {
            (new ServerFacade()).CreateAndStartServer(true);

            NetClient netClient = new NetClient(NetworkSettings.DefaultNetPeerConfiguration);
            netClient.Start();

            NetOutgoingMessage netOutgoingMessage = NetPeerHandler.CreateMessage(new PlayerJoinPacket(Game.Player, "Rob is in fact handsome Squidward"), netClient);
            netClient.Connect("localhost", NetworkSettings.DefaultPort, netOutgoingMessage);

            while (netClient.ServerConnection == null)
            {
                Thread.Sleep(100);
            }

            ClientLobby clientLobby = new ClientLobby(netClient);

            LobbyScreen lobbyScreen = new LobbyScreen(Game, clientLobby, true);

            Game.ScreenManager.CurrentScreen = lobbyScreen;
        }
    }
}
