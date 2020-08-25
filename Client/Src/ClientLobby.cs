using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Src;
using Common.Src.Packets.ClientToServer;
using Common.Src.Packets.ServerToClient;
using Lidgren.Network;
using Microsoft.Xna.Framework;

namespace Client.Src
{
    internal class ClientLobby : NetClientHandler
    {
        internal event EventHandler<(Player, bool)> PlayerHasJoined;
        internal event EventHandler<Player> PlayerHasLeft;
        internal event EventHandler<(NetClient, MatchStartedPacket)> MatchHasStarted;

        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private List<Player> players = new List<Player>();

        public ClientLobby(NetClient netClient) : base(netClient)
        {
            this.NetClient = netClient;
        }

        public override string ToString() => "Client Lobby";

        protected override void HandleLobbyInfoPacket(LobbyInfoPacket lobbyInfoPacket)
        {
            int i = 0;
            bool isHost = false;

            // If new player detected fire player joined event
            foreach (Player player in lobbyInfoPacket.Players)
            {
                if (!players.Contains(player))
                {
                    if (i == lobbyInfoPacket.HostIndex)
                        isHost = true;

                    PlayerHasJoined.Invoke(this, (player, isHost));
                    players.Add(player);
                }
                i++;
            }
            // If existing player missing fire player left event
            foreach (Player player in players)
            {
                if (!lobbyInfoPacket.Players.Contains(player))
                {
                    PlayerHasLeft.Invoke(this, player);
                    players.Remove(player);
                }
            }
        }

        protected override void HandleMatchStartedPacket(MatchStartedPacket matchStartedPacket)
        {
            logger.Info(ToString() + ": Match started packet recieved!");
            MatchHasStarted.Invoke(this, (NetClient, matchStartedPacket));
        }

        internal void SendMatchStartRequestPacket()
        {
            SendMessageToServer(new MatchStartRequestPacket());
        }

        protected override void HandleWorldStatePacket(WorldStatePacket worldStatePacket)
        {
            logger.Error(ToString() + ": World state packet received!");
        }

        protected override void RunProcesses(GameTime gameTime) { }
    }
}
