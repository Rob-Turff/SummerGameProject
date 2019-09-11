using Lidgren.Network;
using Client.Src.Common.Utilities;
using System.Collections.Generic;
using System;
using Common.Src;
using System.Linq;
using Common.Src.Packets;
using System.Threading;
using Common.Src.Packets.ClientToServer;
using Common.Src.Packets.ServerToClient;
using System.Diagnostics;

namespace Server.Src
{
    public class GameServer : GamePeer
    {
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private IServerPacketHandler packetHandler;


        public GameServer(int maxConnections)
        {
            NetPeerConfiguration config = GetDefaultNetPeerConfiguration();

            config.Port = GamePeer.DefaultPort;
            config.MaximumConnections = maxConnections;
            config.EnableMessageType(NetIncomingMessageType.ConnectionApproval);

            NetPeer = new NetServer(config);

            packetHandler = new Lobby(this);
        }

        public void StartServer()
        {
            NetPeer.Start();

            logger.Debug("Server - Started");

            NetPeer.RegisterReceivedCallback(new SendOrPostCallback(HandleMessage));

            logger.Debug("Server - Message Handler Registered");
        }

        internal void BroadcastMessage(IPacket messageContent)
        {
            NetOutgoingMessage msg = CreateMessage(messageContent);
            ((NetServer)NetPeer).SendToAll(msg, NetDeliveryMethod.ReliableOrdered);

            logger.Info(
                "Server - Message containing a " + messageContent.PacketType.ToString() + " " +
                "packet was sent to " + ((NetServer)NetPeer).ConnectionsCount + " " +
                "client(s)");
            //server.FlushSendQueue(); TODO: Check with Rob, think not necessary because autoflushsendqueue is true by default
        }

        private void HandleMessage(object peer)
        {
            logger.Info("Server - Message recieved");
            NetServer netServer = (NetServer)peer;
            NetIncomingMessage netIncomingMessage = netServer.ReadMessage();

            //logger.Info(netIncomingMessage);

            switch (netIncomingMessage.MessageType)
            {
                case NetIncomingMessageType.ConnectionApproval:
                    HandleConnectionApprovalMessage(netIncomingMessage);
                    break;
                case NetIncomingMessageType.Data:
                    HandleDataMessage(netIncomingMessage);
                    break;
                case NetIncomingMessageType.ErrorMessage:
                    logger.Error("Server - " + netIncomingMessage.ReadString());
                    break;
                case NetIncomingMessageType.StatusChanged:
                    HandleStatusChangedMessage(netIncomingMessage);
                    break;
                default:
                    logger.Error("Server - Unhandled type: " + netIncomingMessage.MessageType);
                    break;
            }

            NetPeer.Recycle(netIncomingMessage);
        }

        private void HandleConnectionApprovalMessage(NetIncomingMessage netIncomingMessage)
        {
            if (netIncomingMessage.ReadByte() == (byte)PacketType.PLAYER_JOIN)
            {
                logger.Debug("Server - Incoming Login");

                PlayerJoinPacket playerJoinPacket = new PlayerJoinPacket(netIncomingMessage);

                if (packetHandler.HandlePlayerJoinPacket(playerJoinPacket, netIncomingMessage.SenderConnection))
                    logger.Debug("Server - New connection was approved");
                else
                    logger.Debug("Server - New connection was denied");
            }
            else
                logger.Error("Unrecognised packet type received in Connection Approval Message");
        }

        private void HandleDataMessage(NetIncomingMessage netIncomingMessage)
        {
            PacketType packetType = (PacketType)netIncomingMessage.ReadByte();
            switch (packetType)
            {
                case PacketType.MATCH_START_REQUEST:
                    MatchStartRequestPacket matchStartRequestPacket = new MatchStartRequestPacket(netIncomingMessage);
                    packetHandler.HandleMatchStartRequestPacket(matchStartRequestPacket, netIncomingMessage.SenderConnection);
                    break;
                case PacketType.PLAYER_INPUT:
                    PlayerInputPacket playerInputPacket = new PlayerInputPacket(netIncomingMessage);
                    packetHandler.HandlePlayerInputPacket(playerInputPacket);
                    break;
                default:
                    logger.Error("Server - Unhandled network packet type: " + (byte)packetType);
                    break;
            }
        }

        private void HandleStatusChangedMessage(NetIncomingMessage inc)
        {
            logger.Debug("Server - " + inc.SenderConnection.ToString() + " status changed. " + inc.SenderConnection.Status);

            if (inc.SenderConnection.Status == NetConnectionStatus.Disconnected)
            {
                packetHandler.HandlePlayerDisconnect(inc.SenderConnection);
            }
            else if(inc.SenderConnection.Status == NetConnectionStatus.Connected)
            {
                packetHandler.HandlePlayerConnect(inc.SenderConnection);
            }
        }

    }

    interface IServerPacketHandler
    {
        bool HandlePlayerJoinPacket(PlayerJoinPacket playerJoinPacket, NetConnection senderConnection);

        void HandleMatchStartRequestPacket(MatchStartRequestPacket matchStartRequestPacket, NetConnection senderConnection);

        void HandlePlayerInputPacket(PlayerInputPacket playerInputPacket);

        void HandlePlayerDisconnect(NetConnection senderConnection);

        void HandlePlayerConnect(NetConnection senderConnection);
    }
}
