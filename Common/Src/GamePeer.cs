using Lidgren.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Src
{
    public abstract class GamePeer
    {
        private const string appIdentifier = "SummerGameProject";

        protected NetPeer NetPeer { get; set; }

        public static int DefaultPort => 27015;

        protected NetPeerConfiguration GetDefaultNetPeerConfiguration()
        {
            NetPeerConfiguration config = new NetPeerConfiguration(appIdentifier);
            return config;
        }

        protected void SendMesssage(IPacket messageContent, NetConnection recipient)
        {
            NetOutgoingMessage msg = CreateMessage(messageContent);
            NetPeer.SendMessage(msg, recipient, NetDeliveryMethod.ReliableOrdered);
            //server.FlushSendQueue();
            //TODO: Check with Rob, think not necessary because autoflushsendqueue is true by default
        }

        protected NetOutgoingMessage CreateMessage(IPacket packet)
        {
            NetOutgoingMessage message = NetPeer.CreateMessage();

            message.Write((byte)packet.PacketType);

            packet.Encode(message);

            return message;
        }
    }
}
