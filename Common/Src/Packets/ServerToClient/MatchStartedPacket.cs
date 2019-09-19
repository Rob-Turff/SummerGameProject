using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Src.Stages;
using Lidgren.Network;

namespace Common.Src.Packets.ServerToClient
{
    public class MatchStartedPacket : Packet
    {
        public override PacketType PacketType => PacketType.MATCH_STARTED;

        public StageIdentifier StageIdentifier { get; private set; }
        public List<Player> Players { get; private set; }

        public MatchStartedPacket(StageIdentifier stageIdentifier, List<Player> players)
        {
            StageIdentifier = stageIdentifier;
            Players = players;
        }

        public MatchStartedPacket(NetIncomingMessage netIncomingMessage)
        {
            Decode(netIncomingMessage);
        }

        public override void Encode(NetOutgoingMessage netOutgoingMessage)
        {
            netOutgoingMessage.Write((byte)StageIdentifier);
            EncodePlayerList(netOutgoingMessage, Players);
        }

        protected override void Decode(NetIncomingMessage netIncomingMessage)
        {
            StageIdentifier = (StageIdentifier)netIncomingMessage.ReadByte();
            Players = DecodePlayerList(netIncomingMessage);
        }
    }
}
