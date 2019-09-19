using Lidgren.Network;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Common.Src.Packets.ServerToClient
{
    public class LobbyInfoPacket : Packet
    {
        // Player names and player IDs in same order
        public List<Player> Players { get; private set; }
        public int HostIndex { get; private set; }
        
        public override PacketType PacketType => PacketType.LOBBY_INFO;

        public LobbyInfoPacket(List<Player> players, int hostIndex)
        {
            Players = players;
            HostIndex = hostIndex;
        }

        public LobbyInfoPacket(NetIncomingMessage netIncomingMessage)
        {
            Decode(netIncomingMessage);
        }

        public override void Encode(NetOutgoingMessage netOutgoingMessage)
        {
            EncodePlayerList(netOutgoingMessage,Players);
            netOutgoingMessage.Write(HostIndex);
        }

        protected override void Decode(NetIncomingMessage netIncomingMessage)
        {
            this.Players = DecodePlayerList(netIncomingMessage);
            this.HostIndex = netIncomingMessage.ReadInt32();
        }
    }

}
