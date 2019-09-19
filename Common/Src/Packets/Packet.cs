using Lidgren.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Common.Src
{
    public abstract class Packet
    {
        public abstract PacketType PacketType { get; }

        public abstract void Encode(NetOutgoingMessage netOutgoingMessage);

        protected abstract void Decode(NetIncomingMessage netIncomingMessage);

        protected static void EncodePlayer(NetOutgoingMessage netOutgoingMessage, Player player)
        {
            netOutgoingMessage.Write(player.Name);
            netOutgoingMessage.Write(player.ID.ToByteArray());
        }

        protected static Player DecodePlayer(NetIncomingMessage netIncomingMessage)
        {
            return new Player(netIncomingMessage.ReadString(), new Guid(netIncomingMessage.ReadBytes(16)));
        }

        protected static void EncodePlayerList(NetOutgoingMessage netOutgoingMessage, List<Player> players)
        {
            netOutgoingMessage.Write(players.Count);
            for (int i = 0; i < players.Count; i++)
            {
                EncodePlayer(netOutgoingMessage, players[i]);
            }
        }

        protected static List<Player> DecodePlayerList(NetIncomingMessage netIncomingMessage)
        {
            List<Player> players = new List<Player>();
            for (int i = 0, length = netIncomingMessage.ReadInt32(); i < length; i++)
            {
                players.Add(DecodePlayer(netIncomingMessage));
            }
            return players;
        }
    }
}
