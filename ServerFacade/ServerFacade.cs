using Server.Src;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServerFacadeNS
{
    public class ServerFacade
    {
        private const int maximumPlayers = 10;

        public void CreateAndStartServer(bool isMultiplayer)
        {
            //Thread serverThread = new Thread(() =>
            //{
            //    SynchronizationContext.SetSynchronizationContext(new SynchronizationContext());

                GameServer server = new GameServer(isMultiplayer ? maximumPlayers : 1);
                
                server.StartServer();

            //});
            //serverThread.Start();
            //serverThread.Name = "Server Thread";
        }

    }
}
