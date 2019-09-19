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
    internal abstract class NetClientHandler : NetPeerHandler
    {
        #region Fields

        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #endregion

        #region Properties

        protected NetClient NetClient { get; set; }

        #endregion

        #region Constructor

        protected NetClientHandler(NetClient netClient)
        {
            Debug.Assert(netClient.ServerConnection != null);
            NetClient = netClient;
        }

        #endregion

        #region Methods

        public void Update(GameTime gameTime)
        {
            HandleMessages(NetClient);
            RunProcesses(gameTime);
        }

        protected abstract void RunProcesses(GameTime gameTime);

        protected void SendMessageToServer(Packet messageContent)
        {
            NetOutgoingMessage netOutgoingMessage = CreateMessage(messageContent, NetClient);

            NetClient.SendMessage(netOutgoingMessage,NetDeliveryMethod.ReliableOrdered);
        }
        
        protected override void HandleConnectionApprovalMessage(NetIncomingMessage netIncomingMessage)
        {
            throw new NotImplementedException(ToString() + ": Client's should never recieve a connection approval message");
        }

        protected override void HandleDataMessage(NetIncomingMessage netIncomingMessage)
        {
            PacketType packetType = (PacketType)netIncomingMessage.ReadByte();
            switch (packetType)
            {
                case PacketType.MATCH_STARTED:
                    MatchStartedPacket matchStartedPacket = new MatchStartedPacket(netIncomingMessage);
                    HandleMatchStartedPacket(matchStartedPacket);
                    break;
                case PacketType.LOBBY_INFO:
                    LobbyInfoPacket lobbyInfoPacket = new LobbyInfoPacket(netIncomingMessage);
                    HandleLobbyInfoPacket(lobbyInfoPacket);
                    break;
                case PacketType.WORLD_STATE:
                    WorldStatePacket worldStatePacket = new WorldStatePacket(netIncomingMessage);
                    HandleWorldStatePacket(worldStatePacket);
                    break;
                default:
                    logger.Error(ToString() + ": Unhandled network packet type: " + packetType);
                    break;
            }
        }


        protected override void HandleStatusChangedMessage(NetIncomingMessage netIncomingMessage)
        {
            logger.Debug(ToString() + ": " + netIncomingMessage.SenderConnection.ToString() 
                + " status changed to " + netIncomingMessage.SenderConnection.Status);
        }

        protected abstract void HandleLobbyInfoPacket(LobbyInfoPacket lobbyInfoPacket);

        protected abstract void HandleMatchStartedPacket(MatchStartedPacket matchStartedPacket);

        protected abstract void HandleWorldStatePacket(WorldStatePacket worldStatePacket);

        #endregion
    }
}