using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lidgren.Network;
using Microsoft.Xna.Framework;

namespace Common.Src.Packets.ServerToClient
{
    public class WorldStatePacket : ServerToClientPacket
    {
        public override PacketType PacketType => PacketType.WORLD_STATE;

        public List<CharacterData> CharacterDataList;

        public WorldStatePacket(List<CharacterData> characterDataList)
        {
            this.CharacterDataList = characterDataList;
        }

        public WorldStatePacket(NetIncomingMessage netIncomingMessage)
        {
            Decode(netIncomingMessage);
        }

        public override void Encode(NetOutgoingMessage netOutgoingMessage)
        {
            EncodeCharacterDataList(netOutgoingMessage,CharacterDataList);
        }

        protected override void Decode(NetIncomingMessage netIncomingMessage)
        {
            CharacterDataList = DecodeCharacterDataCollection(netIncomingMessage);
        }

    }

    public struct CharacterData
    {
        public Guid ID { get; }

        public Vector2 Position { get; set; }

        public Vector2 Velocity { get; set; }

        public CharacterData(Guid id, Vector2 position, Vector2 velocity)
        {
            ID = id;
            Position = position;
            Velocity = velocity;
        }
    }
}
