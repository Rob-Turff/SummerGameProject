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
        public PlayerStats playerInfo;
        public List<Guid> playerSent = new List<Guid>();

        public ClientInfo(NetConnection clientConnection, PlayerStats playerInfo)
        {
            this.clientConnection = clientConnection;
            this.playerInfo = playerInfo;
        }
    }
}
