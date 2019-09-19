using Lidgren.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Common.Src
{
    public abstract class NetPeerHandler
    {
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static NetOutgoingMessage CreateMessage(Packet packet, NetPeer netPeer)
        {
            NetOutgoingMessage netOutgoingMessage = netPeer.CreateMessage();

            netOutgoingMessage.Write((byte)packet.PacketType);

            packet.Encode(netOutgoingMessage);

            return netOutgoingMessage;
        }

        protected void HandleMessages(NetPeer netPeer)
        {
            NetIncomingMessage msg;
            while ((msg = netPeer.ReadMessage()) != null)
            {

                switch (msg.MessageType)
                {
                    case NetIncomingMessageType.ConnectionApproval:
                        HandleConnectionApprovalMessage(msg);
                        break;
                    case NetIncomingMessageType.Data:
                        HandleDataMessage(msg);
                        break;
                    case NetIncomingMessageType.ErrorMessage:
                        logger.Error(ToString() + ": Error message recieved: " + msg.ReadString());
                        break;
                    case NetIncomingMessageType.WarningMessage:
                        logger.Warn(ToString() + ": Warning message recieved: " + msg.ReadString());
                        break;
                    case NetIncomingMessageType.StatusChanged:
                        HandleStatusChangedMessage(msg);
                        break;
                    default:
                        logger.Error(ToString() + ": Unhandled message type recieved: " + msg.MessageType);
                        break;
                }

                netPeer.Recycle(msg);

            }

        }

        protected abstract void HandleStatusChangedMessage(NetIncomingMessage netIncomingMessage);
        protected abstract void HandleDataMessage(NetIncomingMessage netIncomingMessage);
        protected abstract void HandleConnectionApprovalMessage(NetIncomingMessage netIncomingMessage);

        public abstract override string ToString();
    }
}
