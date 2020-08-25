using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Src.Stages;
using Lidgren.Network;

namespace Common.Src.Packets.ServerToClient
{
    public class MatchStartedPacket : ServerToClientPacket
    {
        public override PacketType PacketType => PacketType.MATCH_STARTED;

        public StageIdentifier StageIdentifier { get; private set; }
        public List<CharacterData> CharacterDataList { get; private set; }

        public MatchStartedPacket(StageIdentifier stageIdentifier, List<CharacterData> characterDataList)
        {
            StageIdentifier = stageIdentifier;
            CharacterDataList = characterDataList;
        }

        public MatchStartedPacket(NetIncomingMessage netIncomingMessage)
        {
            Decode(netIncomingMessage);
        }

        public override void Encode(NetOutgoingMessage netOutgoingMessage)
        {
            netOutgoingMessage.Write((byte)StageIdentifier);
            EncodeCharacterDataList(netOutgoingMessage, CharacterDataList);
        }

        protected override void Decode(NetIncomingMessage netIncomingMessage)
        {
            StageIdentifier = (StageIdentifier)netIncomingMessage.ReadByte();
            CharacterDataList = DecodeCharacterDataCollection(netIncomingMessage);
        }


    }
}
