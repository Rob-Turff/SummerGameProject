using Common.Src;
using Lidgren.Network;
using mattmc3.Common.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Src
{
    public class GameServer
    {
        private const int maxPlayers = 4;

        public bool IsMultiplayer { get; }

        public GameServer(bool isMultiplayer)
        {
            IsMultiplayer = IsMultiplayer;
        }

        public void Start()
        {
            ServerLobby serverLobby = new ServerLobby(IsMultiplayer ? maxPlayers : 1);

            serverLobby.StartMatchRequestedByHost += ServerLobby_StartMatchRequestedByHost;

            serverLobby.Start();
        }

        private void ServerLobby_StartMatchRequestedByHost(object sender, (NetServer, OrderedDictionary<NetConnection,Player>) e)
        {
            NetServer netServer = e.Item1;
            OrderedDictionary<NetConnection, Player> playerConnections = e.Item2;

            ServerMatch serverMatch = new ServerMatch(netServer, playerConnections);
            serverMatch.Start();
        }
    }
}
