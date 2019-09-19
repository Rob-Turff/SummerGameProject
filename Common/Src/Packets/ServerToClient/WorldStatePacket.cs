using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lidgren.Network;
using Microsoft.Xna.Framework;

namespace Common.Src.Packets.ServerToClient
{
    public class WorldStatePacket : Packet
    {
        public override PacketType PacketType => PacketType.WORLD_STATE;

        public List<(Player, Vector2)> PlayerPositions;

        public WorldStatePacket(List<(Player, Vector2)> playerPositions)
        {
            this.PlayerPositions = playerPositions;
        }

        public WorldStatePacket(NetIncomingMessage netIncomingMessage)
        {
            Decode(netIncomingMessage);
        }

        public override void Encode(NetOutgoingMessage netOutgoingMessage)
        {
            netOutgoingMessage.Write(PlayerPositions.Count);
            foreach ((Player, Vector2) pair in PlayerPositions)
            {
                Player player = pair.Item1;
                EncodePlayer(netOutgoingMessage, player);
                Vector2 position = pair.Item2;
                netOutgoingMessage.Write(position.X);
                netOutgoingMessage.Write(position.Y);
            }
        }

        protected override void Decode(NetIncomingMessage netIncomingMessage)
        {
            this.PlayerPositions = new List<(Player, Vector2)>();
            for (int i = 0, length = netIncomingMessage.ReadInt32(); i < length; i++)
            {
                PlayerPositions.Add((
                    DecodePlayer(netIncomingMessage),
                    new Vector2(netIncomingMessage.ReadFloat(), netIncomingMessage.ReadFloat()
                )));
            }
        }

    }
}
