using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lidgren.Network;

namespace Common.Src.Packets.ClientToServer
{
    public class MatchStartRequestPacket : IPacket
    {
        public PacketType PacketType => PacketType.MATCH_START_REQUEST;

        public MatchStartRequestPacket()
        {
        }

        public MatchStartRequestPacket(NetIncomingMessage netIncomingMessage)
        {
            Decode(netIncomingMessage);
        }

        public void Encode(NetOutgoingMessage netOutgoingMessage)
        {
            // No data associated with this packet yet
        }

        public void Decode(NetIncomingMessage netIncomingMessage)
        {
            // No data associated with this packet yet
        }
    }
}
