using Common.Src;
using Lidgren.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Src
{
    class PlayerConnection
    {
        public Player Player { get; }
        public NetConnection Connection { get; }

        public PlayerConnection(Player player, NetConnection connection)
        {
            Player = player;
            Connection = connection;
        }
    }
}
