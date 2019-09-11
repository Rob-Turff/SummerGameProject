using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Src;
using Common.Src.Packets.ClientToServer;
using Common.Src.Packets.ServerToClient;
using Lidgren.Network;

namespace Server.Src
{
    class Lobby : IServerPacketHandler
    {
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private readonly GameServer gameServer;

        private List<PlayerConnection> playerConnections;
        private PlayerConnection hostConnection;

        // This is set when a player join packet is received but the value is only added to playerConnections once the status of that connection has changed to Connected
        private PlayerConnection cachedPlayerConnection;

        public Lobby(GameServer gameServer)
        {
            this.gameServer = gameServer;
            this.playerConnections = new List<PlayerConnection>();
        }

        public bool HandlePlayerJoinPacket(PlayerJoinPacket playerJoinPacket, NetConnection senderConnection)
        {
            // For now always aprove login, maybe later require some authentication data is sent in player join packet
            bool approveLogin = true;

            if (approveLogin)
            {
                senderConnection.Approve();

                Player player = new Player(playerJoinPacket.Name);

                cachedPlayerConnection = new PlayerConnection(player, senderConnection);
            }
            else
            {
                senderConnection.Deny();
            }
            return approveLogin;
        }

        public void HandleMatchStartRequestPacket(MatchStartRequestPacket matchStartRequestPacket, NetConnection senderConnection)
        {
            if (senderConnection == hostConnection.Connection)
            {
                throw new NotImplementedException();
            }
            else
            {
                logger.Error("Server Lobby - Non-host attempted to start the game");
            }
        }

        public void HandlePlayerInputPacket(PlayerInputPacket playerInputPacket)
        {
            throw new NotImplementedException();
        }

        public void HandlePlayerDisconnect(NetConnection senderConnection)
        {
            PlayerConnection playerConnection
                    = playerConnections.SingleOrDefault
                      (playerConnections => playerConnections.Connection == senderConnection);

            // Find disconnected player and remove them
            if (playerConnection != null && playerConnections.Remove(playerConnection))
            {
                logger.Info("Server Lobby - Player removed");
            }
            else
            {
                logger.Error("Server Lobby - Failed to remove player specified by given NetConnection");
            }
        }

        public void HandlePlayerConnect(NetConnection senderConnection)
        {
            if (senderConnection == cachedPlayerConnection.Connection)
            {
                playerConnections.Add(cachedPlayerConnection);

                if (cachedPlayerConnection.Connection.RemoteEndPoint.Address.Equals(new System.Net.IPAddress(new byte[] { 127, 0, 0, 1 })))
                {
                    hostConnection = cachedPlayerConnection;
                }

                BroadcastLobbyInfo();
            }
        }

        private void BroadcastLobbyInfo()
        {
            List<Player> players = new List<Player>(playerConnections.Select(playerConnection => playerConnection.Player));
            int hostIndex = playerConnections.IndexOf(hostConnection);

            LobbyInfoPacket lobbyInfoPacket = new LobbyInfoPacket(players, hostIndex);
            gameServer.BroadcastMessage(lobbyInfoPacket);
        }
    }
}
