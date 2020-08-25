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
    public abstract class NetServerHandler : NetPeerHandler
    {
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private bool keepRunning = true;

        protected NetServer NetServer { get; set; }

        /// <summary>
        /// How many times the server loop should run a second
        /// </summary>
        protected abstract int TickRate { get; }

        public void Start()
        {
            logger.Debug(ToString() + ": Started");

            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();

            while (keepRunning)
            {

                HandleMessages(NetServer);

                stopwatch.Stop();

                RunProcesses(stopwatch.Elapsed);

                stopwatch.Restart();
                Thread.Sleep(1000 / TickRate);
            }
        }


        public void Stop()
        {
            keepRunning = false;
        }

        protected void BroadcastMessage(Packet messageContent)
        {
            NetOutgoingMessage netOutgoingMessage = CreateMessage(messageContent, NetServer);

            NetServer.SendToAll(netOutgoingMessage, NetDeliveryMethod.ReliableOrdered);

            logger.Info(
                "Server - Message containing a " + messageContent.PacketType.ToString() + " " +
                "packet was sent to " + NetServer.ConnectionsCount + " " +
                "client(s)");

            //server.FlushSendQueue(); TODO: Check with Rob, think not necessary because autoflushsendqueue is true by default
        }

        #region Override Methods

        protected override void HandleConnectionApprovalMessage(NetIncomingMessage netIncomingMessage)
        {
            if (netIncomingMessage.ReadByte() == (byte)PacketType.PLAYER_JOIN)
            {
                logger.Debug(ToString() + ": Incoming Login");

                PlayerJoinPacket playerJoinPacket = new PlayerJoinPacket(netIncomingMessage);

                HandlePlayerJoinPacket(playerJoinPacket, netIncomingMessage.SenderConnection);
            }
            else
                logger.Error(ToString() + ": Unrecognised packet type received in Connection Approval Message");
        }

        protected override void HandleDataMessage(NetIncomingMessage netIncomingMessage)
        {
            PacketType packetType = (PacketType)netIncomingMessage.ReadByte();
            switch (packetType)
            {
                case PacketType.MATCH_START_REQUEST:
                    MatchStartRequestPacket matchStartRequestPacket = new MatchStartRequestPacket(netIncomingMessage);
                    HandleMatchStartRequestPacket(matchStartRequestPacket, netIncomingMessage.SenderConnection);
                    break;
                case PacketType.PLAYER_INPUT:
                    CharacterInputPacket playerInputPacket = new CharacterInputPacket(netIncomingMessage);
                    HandlePlayerInputPacket(playerInputPacket, netIncomingMessage.SenderConnection);
                    break;
                default:
                    logger.Error(ToString() + ":  Unhandled network packet type: " + (byte)packetType);
                    break;
            }
        }

        protected override void HandleStatusChangedMessage(NetIncomingMessage netIncomingMessage)
        {
            logger.Debug(ToString() + ": " + netIncomingMessage.SenderConnection.ToString() + " status changed to " + netIncomingMessage.SenderConnection.Status);

            if (netIncomingMessage.SenderConnection.Status == NetConnectionStatus.Disconnected)
            {
                HandlePlayerDisconnect(netIncomingMessage.SenderConnection);
            }
            else if (netIncomingMessage.SenderConnection.Status == NetConnectionStatus.Connected)
            {
                HandlePlayerConnect(netIncomingMessage.SenderConnection);
            }
        }

        #endregion

        #region Abstract Methods

        protected abstract void RunProcesses(TimeSpan elapsed);

        protected abstract void HandlePlayerJoinPacket(PlayerJoinPacket playerJoinPacket, NetConnection senderConnection);

        protected abstract void HandleMatchStartRequestPacket(MatchStartRequestPacket matchStartRequestPacket, NetConnection senderConnection);

        protected abstract void HandlePlayerInputPacket(CharacterInputPacket playerInputPacket, NetConnection senderConnection);

        protected abstract void HandlePlayerConnect(NetConnection senderConnection);

        protected abstract void HandlePlayerDisconnect(NetConnection senderConnection);

        #endregion
    }

}
