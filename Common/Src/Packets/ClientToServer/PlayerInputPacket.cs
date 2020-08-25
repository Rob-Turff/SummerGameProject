using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lidgren.Network;

namespace Common.Src.Packets.ClientToServer
{
    public class CharacterInputPacket : Packet
    {

        public override PacketType PacketType => PacketType.PLAYER_INPUT;

        public CharacterInputs Inputs { get; private set; }

        public CharacterInputPacket(CharacterInputs inputs)
        {
            this.Inputs = inputs;
        }

        public CharacterInputPacket(NetIncomingMessage netIncomingMessage)
        {
            Decode(netIncomingMessage);
        }

        public override void Encode(NetOutgoingMessage netOutgoingMessage)
        {
            netOutgoingMessage.Write((byte)Inputs);
        }

        protected override void Decode(NetIncomingMessage netIncomingMessage)
        {
            this.Inputs = (CharacterInputs)netIncomingMessage.ReadByte();
        }
    }


    [Flags]
    public enum CharacterInputs
    {
        NONE    = 0,
        JUMP    = 1,
        LEFT    = 2,
        RIGHT   = 4,
        //powers of 2 e.g. PUNCH = 8
    }
}
