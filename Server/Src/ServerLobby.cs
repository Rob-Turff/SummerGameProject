using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Common.Src;
using Common.Src.Packets.ClientToServer;
using Common.Src.Packets.ServerToClient;
using Lidgren.Network;
using mattmc3.Common.Collections.Generic;

namespace Server.Src
{
    internal class ServerLobby : NetServerHandler
    {
        public event EventHandler<(NetServer, OrderedDictionary<NetConnection,Player>)> StartMatchRequestedByHost;

        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private OrderedDictionary<NetConnection,Player> connectionToPlayerMap = new OrderedDictionary<NetConnection,Player>();
        private NetConnection hostConnection;
        private (NetConnection connection,Player player) cachedConnectionAndPlayer;

        protected override int TickRate => 10;

        public ServerLobby(int maxConnections) : base()
        {
            NetServer = new NetServer(GetDefaultNetServerConfiguration(maxConnections));
            NetServer.Start();
        }

        public override string ToString() => "Server Lobby";

        protected override void RunProcesses(TimeSpan elapsed)
        {
            BroadcastLobbyInfo();
        }

        protected override void HandlePlayerJoinPacket(PlayerJoinPacket playerJoinPacket, NetConnection senderConnection)
        {
            string password = playerJoinPacket.Password;
            Player player = playerJoinPacket.Player;

            if (password == "Rob is in fact handsome Squidward")
            {
                // This is set now so that once the connection's status is connected, this can be added to playerConnections
                cachedConnectionAndPlayer = (senderConnection,player);

                senderConnection.Approve();

                logger.Info(ToString() + ": " + player.Name + "'s connection approval request was approved");
            }
            else
            {
                senderConnection.Deny();

                logger.Info(ToString() + ": " + player.Name + "'s connection approval request was denied");
            }
        }

        protected override void HandleMatchStartRequestPacket(MatchStartRequestPacket matchStartRequestPacket, NetConnection senderConnection)
        {
            if (senderConnection == hostConnection)
            {
                logger.Info(ToString() + ": Recieved valid match start request");
                StartMatchRequestedByHost.Invoke(this, (NetServer, connectionToPlayerMap));
                this.Stop();
            }
            else
            {
                logger.Error(ToString() + ": Non-host attempted to start the game");
            }
        }

        protected override void HandlePlayerInputPacket(PlayerInputPacket playerInputPacket, NetConnection senderConnection)
        {
            logger.Error(ToString() + ": Player input packet received!");
        }

        protected override void HandlePlayerDisconnect(NetConnection senderConnection)
        {
            // Find disconnected player and remove them
            if (connectionToPlayerMap.Remove(senderConnection))
            {
                logger.Info(ToString() + ": Player removed");
            }
            else
            {
                logger.Error(ToString() + ": Failed to remove player specified by given NetConnection");
            }
        }

        protected override void HandlePlayerConnect(NetConnection senderConnection)
        {
            if (senderConnection == cachedConnectionAndPlayer.connection)
            {
                connectionToPlayerMap.Add(cachedConnectionAndPlayer.connection,cachedConnectionAndPlayer.player);

                if (cachedConnectionAndPlayer.connection.RemoteEndPoint.Address.Equals(new System.Net.IPAddress(new byte[] { 127, 0, 0, 1 })))
                {
                    hostConnection = senderConnection;
                }

                 BroadcastLobbyInfo();
            }
        }

        private void BroadcastLobbyInfo()
        {
            List<Player> players = new List<Player>(connectionToPlayerMap.Values);
            int hostIndex = hostConnection != null ? connectionToPlayerMap.IndexOf(hostConnection) : -1;

            LobbyInfoPacket lobbyInfoPacket = new LobbyInfoPacket(players, hostIndex);
            BroadcastMessage(lobbyInfoPacket);
        }

        private static NetPeerConfiguration GetDefaultNetServerConfiguration(int maxConnections)
        {
            NetPeerConfiguration config = NetworkSettings.DefaultNetPeerConfiguration;

            config.Port = NetworkSettings.DefaultPort;
            config.MaximumConnections = maxConnections;
            config.EnableMessageType(NetIncomingMessageType.ConnectionApproval);

            return config;
        }

    }
}
