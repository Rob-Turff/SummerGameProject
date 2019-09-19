using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lidgren.Network;

namespace Common.Src.Packets.ClientToServer
{
    public class PlayerJoinPacket : Packet
    {
        public override PacketType PacketType => PacketType.PLAYER_JOIN;

        public Player Player { get; private set; }
        public string Password { get; private set; }

        public PlayerJoinPacket(Player player,string password)
        {
            this.Player = player;
            this.Password = password;
        }

        public PlayerJoinPacket(NetIncomingMessage netIncomingMessage)
        {
            Decode(netIncomingMessage);
        }

        public override void Encode(NetOutgoingMessage netOutgoingMessage)
        {
            netOutgoingMessage.Write(Player.Name);
            netOutgoingMessage.Write(Player.ID.ToByteArray());
            netOutgoingMessage.Write(Password);
        }

        protected override void Decode(NetIncomingMessage netIncomingMessage)
        {
            string name = netIncomingMessage.ReadString();
            Guid id = new Guid(netIncomingMessage.ReadBytes(16));

            this.Player = new Player(name,id);
            this.Password = netIncomingMessage.ReadString();
        }
    }
}
