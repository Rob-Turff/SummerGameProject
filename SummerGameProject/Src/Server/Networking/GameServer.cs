using Lidgren.Network;
using SummerGameProject.Src.Common;
using System.Collections.Generic;

namespace SummerGameProject.Src.Server.Networking
{
    public class GameServer
    {
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private readonly MainGame game;
        internal List<NetPeer> clientList;
        private CommandHandler commandHandler;

        public GameServer(MainGame game)
        {
            this.game = game;
            clientList = new List<NetPeer>();
            commandHandler = new CommandHandler(this, game);
        }

        public void StartServer()
        {
            NetPeerConfiguration config = new NetPeerConfiguration("gameServer");
            config.Port = 7777;
            NetServer server = new NetServer(config);
            server.Start();

            logger.Debug("Server Started");

            while (true)
                ReadMessages(server);
        }

        private void ReadMessages(NetServer server)
        {
            NetIncomingMessage msg;
            while ((msg = server.ReadMessage()) != null)
            {
                switch (msg.MessageType)
                {
                    case NetIncomingMessageType.Data:
                        switch ((NetworkCommands) msg.ReadByte())
                        {
                            case NetworkCommands.ADD_PLAYER:
                                logger.Debug("Server received add player message");
                                commandHandler.PlayerJoined(msg);
                                break;
                            default:
                                logger.Error("Unhandled network command type");
                                break;
                        }
                        break;
                    case NetIncomingMessageType.ErrorMessage:
                        logger.Error(msg.ReadString());
                        break;
                    case NetIncomingMessageType.StatusChanged:
                        commandHandler.StatusChanged(msg);
                        break;
                    default:
                        logger.Error("Unhandled type: " + msg.MessageType);
                        break;
                }
                server.Recycle(msg);
            }
        }
    }
}
