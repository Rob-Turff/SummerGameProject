using Lidgren.Network;
using SummerGameProject.Src.Client.Components.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummerGameProject.Src.Server.Networking
{
    public class ClientInfo
    {
        public NetConnection clientConnection;
        public PlayerAttributes playerInfo;
        public List<Guid> playerSent = new List<Guid>();

        public ClientInfo(NetConnection clientConnection, PlayerAttributes playerInfo)
        {
            this.clientConnection = clientConnection;
            this.playerInfo = playerInfo;
        }
    }
}
