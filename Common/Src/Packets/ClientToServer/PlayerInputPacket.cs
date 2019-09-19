using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lidgren.Network;

namespace Common.Src.Packets.ClientToServer
{
    public class PlayerInputPacket : Packet
    {

        public override PacketType PacketType => PacketType.PLAYER_INPUT;

        public PlayerInputs Inputs { get; private set; }

        public PlayerInputPacket(PlayerInputs inputs)
        {
            this.Inputs = inputs;
        }

        public PlayerInputPacket(NetIncomingMessage netIncomingMessage)
        {
            Decode(netIncomingMessage);
        }

        public override void Encode(NetOutgoingMessage netOutgoingMessage)
        {
            netOutgoingMessage.Write((byte)Inputs);
        }

        protected override void Decode(NetIncomingMessage netIncomingMessage)
        {
            this.Inputs = (PlayerInputs)netIncomingMessage.ReadByte();
        }
    }


    [Flags]
    public enum PlayerInputs
    {
        NONE    = 0,
        JUMP    = 1,
        LEFT    = 2,
        RIGHT   = 4,
        //powers of 2 e.g. PUNCH = 8
    }
}
