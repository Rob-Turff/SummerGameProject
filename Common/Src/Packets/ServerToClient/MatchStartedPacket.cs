using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lidgren.Network;

namespace Common.Src.Packets.ServerToClient
{
    public class MatchStartedPacket : IPacket
    {
        public PacketType PacketType => PacketType.MATCH_STARTED;

        public MatchStartedPacket()
        {
        }

        public MatchStartedPacket(NetIncomingMessage netIncomingMessage)
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
