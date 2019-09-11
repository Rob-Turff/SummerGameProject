using Lidgren.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Common.Src
{
    public interface IPacket
    {
        PacketType PacketType { get; }

        void Encode(NetOutgoingMessage netOutgoingMessage);

        void Decode(NetIncomingMessage netIncomingMessage);
    }
}
