using Client.Src.Screens;
using Common.Src;
using Common.Src.Packets.ClientToServer;
using Common.Src.Packets.ServerToClient;
using Lidgren.Network;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Client
{
    internal class GameClient : GamePeer
    {
        #region Fields

        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private readonly string playerName;
        private IClientPacketHandler packetHandler;

        #endregion

        #region Properties

        public bool IsConnected
        {
            get
            {
                return NetPeer.ConnectionsCount == 0;
            }
        }
        public bool IsInLobby { get; set; }

        #endregion

        #region Constructor

        public GameClient(string playerName)
        {
            this.playerName = playerName;

            NetPeer = new NetClient(GetDefaultNetPeerConfiguration());
            NetPeer.Start();
        }

        #endregion

        #region Methods

        public void Connect(string host, int port)
        {
            NetOutgoingMessage netOutgoingMessage = CreateMessage(new PlayerJoinPacket(playerName));
            NetConnection x = NetPeer.Connect(host, port, netOutgoingMessage);
        }

        public void Update(GameTime gameTime)
        {
            NetIncomingMessage msg;
            while ((msg = NetPeer.ReadMessage()) != null)
            {
                HandleMessage(msg);
            }
        }

        public void RegisterPacketHandler(IClientPacketHandler packetHandler)
        {
            if (this.packetHandler == null)
            {
                this.packetHandler = packetHandler;
            }
            else
            {
                throw new InvalidOperationException("Can't register a new packet handler whilst a packet handler is already registered");
            }
        }

        public void UnregisterPacketHandler()
        {
            packetHandler = null;
        }

        private void HandleMessage(NetIncomingMessage netIncomingMessage)
        {
            logger.Info("Client - Message recieved");

            switch (netIncomingMessage.MessageType)
            {
                case NetIncomingMessageType.Data:
                    PacketType packetType = (PacketType)netIncomingMessage.ReadByte();
                    switch (packetType)
                    {
                        case PacketType.MATCH_STARTED:
                            logger.Info("Client - MatchStarted receieved");
                            MatchStartedPacket matchStartedPacket = new MatchStartedPacket(netIncomingMessage);
                            packetHandler.HandleMatchStartedPacket(matchStartedPacket);
                            break;

                        case PacketType.LOBBY_INFO:
                            logger.Info("Client - LobbyInfoPacket receieved");
                            LobbyInfoPacket clientNamesPacket = new LobbyInfoPacket(netIncomingMessage);
                            packetHandler.HandleLobbyInfoPacket(clientNamesPacket);
                            break;

                        default:
                            logger.Error("Client - Unhandled network packet type: " + packetType);
                            break;
                    }
                    break;
                case NetIncomingMessageType.ErrorMessage:
                    logger.Error("Client - " + netIncomingMessage.ReadString());
                    break;
                case NetIncomingMessageType.StatusChanged:
                    HandleStatusChanged(netIncomingMessage);
                    break;
                default:
                    logger.Error("Client - Unhandled type: " + netIncomingMessage.MessageType);
                    break;
            }

            NetPeer.Recycle(netIncomingMessage);
        }

        internal void SendMatchStartRequestPacket()
        {
            SendMesssage(new MatchStartRequestPacket(), ((NetClient)NetPeer).ServerConnection);
        }

        private void HandleStatusChanged(NetIncomingMessage inc)
        {
            logger.Debug("Client - " + inc.SenderConnection.ToString() + " status changed. " + inc.SenderConnection.Status);
        }

        #endregion
    }

    internal interface IClientPacketHandler
    {
        void HandleLobbyInfoPacket(LobbyInfoPacket clientNamesPacket);
        void HandleMatchStartedPacket(MatchStartedPacket matchStartedPacket);
    }
}