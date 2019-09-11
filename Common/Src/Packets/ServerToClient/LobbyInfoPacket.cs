using Lidgren.Network;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Common.Src.Packets.ServerToClient
{
    public class LobbyInfoPacket : IPacket
    {
        // Player names and player IDs in same order
        public List<Player> Players { get; private set; }
        public int HostIndex { get; private set; }
        
        public PacketType PacketType => PacketType.LOBBY_INFO;

        public LobbyInfoPacket(List<Player> players, int hostIndex)
        {
            Players = players;
            HostIndex = hostIndex;
        }

        public LobbyInfoPacket(NetIncomingMessage netIncomingMessage)
        {
            Decode(netIncomingMessage);
        }

        public void Encode(NetOutgoingMessage netOutgoingMessage)
        {
            netOutgoingMessage.Write(Players.Count);
            for (int i = 0; i < Players.Count; i++)
            {
                string name = Players[i].Name;
                Guid id = Players[i].ID;
                netOutgoingMessage.Write(name);
                netOutgoingMessage.Write(id.ToByteArray());
            } 
            netOutgoingMessage.Write(HostIndex);
        }

        public void Decode(NetIncomingMessage netIncomingMessage)
        {
            this.Players = new List<Player>();
            for (int i = 0, length = netIncomingMessage.ReadInt32(); i < length; i++)
            {
                Player player = new Player(netIncomingMessage.ReadString(), new Guid(netIncomingMessage.ReadBytes(16)));
                Players.Add(player);
            }
            this.HostIndex = netIncomingMessage.ReadInt32();
        }
    }

}
