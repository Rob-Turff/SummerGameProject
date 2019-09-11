using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lidgren.Network;

namespace Common.Src.Packets.ClientToServer
{
    public class PlayerJoinPacket : IPacket
    {
        public string Name { get; private set; }

        public PacketType PacketType => PacketType.PLAYER_JOIN;

        public PlayerJoinPacket(string name)
        {
            this.Name = name;
        }

        public PlayerJoinPacket(NetIncomingMessage netIncomingMessage)
        {
            Decode(netIncomingMessage);
        }

        public void Encode(NetOutgoingMessage netOutgoingMessage)
        {
            netOutgoingMessage.Write(Name);
        }

        public void Decode(NetIncomingMessage netIncomingMessage)
        {
            this.Name = netIncomingMessage.ReadString();
        }
    }
}
