using Client;
using Server.Src;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Container
{
    /// <summary>
    /// This class simply is used to facilitate starting a GameServer from within GameClient, without the GameClient project knowing about the GameServer project 
    /// </summary>
    public class Container : IServerStarter
    {

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            new Container();
        }

        public Container()
        {
            using (var game = new GameClient(this))
            {
                game.Run();
            }
        }

        public void HostServer()
        {
            Thread serverThread = new Thread(() =>
            {
                GameServer server = new GameServer();
                server.StartServer();
            });
        }

    }
}
