using Lidgren.Network;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Src.Packets.ServerToClient
{
    public abstract class ServerToClientPacket : Packet
    {
        private static void EncodeCharacterData(NetOutgoingMessage netOutgoingMessage, CharacterData characterData)
        {
            netOutgoingMessage.Write(characterData.ID.ToByteArray());
            netOutgoingMessage.Write(characterData.Position.X);
            netOutgoingMessage.Write(characterData.Position.Y);
            netOutgoingMessage.Write(characterData.Velocity.X);
            netOutgoingMessage.Write(characterData.Velocity.Y);
        }

        private static CharacterData DecodeCharacterData(NetIncomingMessage netIncomingMessage)
        {
            Guid id = new Guid(netIncomingMessage.ReadBytes(16));
            Vector2 position = new Vector2(netIncomingMessage.ReadFloat(), netIncomingMessage.ReadFloat());
            Vector2 velocity = new Vector2(netIncomingMessage.ReadFloat(), netIncomingMessage.ReadFloat());
            return new CharacterData(id, position, velocity);
        }

        protected static void EncodeCharacterDataList(NetOutgoingMessage netOutgoingMessage,IList<CharacterData> characterDataCollection)
        {
            netOutgoingMessage.Write(characterDataCollection.Count());
            foreach (CharacterData characterData in characterDataCollection)
            {
                EncodeCharacterData(netOutgoingMessage, characterData);
            }
        }

        protected static List<CharacterData> DecodeCharacterDataCollection(NetIncomingMessage netIncomingMessage)
        {
            List<CharacterData> characterDataList = new List<CharacterData>();

            for (int i = 0, length = netIncomingMessage.ReadInt32(); i < length; i++)
            {
                characterDataList.Add(DecodeCharacterData(netIncomingMessage));
            }

            return characterDataList;
        }


    }
}
