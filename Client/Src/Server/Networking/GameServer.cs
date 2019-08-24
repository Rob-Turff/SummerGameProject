using Lidgren.Network;
using Client.Src.Common;
using Client.Src.Common.Message;
using Client.Src.Common.Utilities;
using System.Collections.Generic;

namespace Client.Src.Server.Networking
{
    public class GameServer
    {
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private readonly MainGame game;
        internal List<ClientInfo> clientList;
        private ServerCommandHandler commandHandler;
        private NetServer server;

        public GameServer(MainGame game)
        {
            this.game = game;
            clientList = new List<ClientInfo>();
            commandHandler = new ServerCommandHandler(this, game);
        }

        public void StartServer()
        {
            NetPeerConfiguration config = new NetPeerConfiguration("gameServer");
            config.Port = 27015;
            server = new NetServer(config);
            server.Start();

            logger.Debug("Server Started");

            while (true)
                ReadMessages();
        }

        private void ReadMessages()
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
                                logger.Debug("Server - received add player message");
                                commandHandler.PlayerJoined(msg);
                                break;
                            case NetworkCommands.START_GAME:
                                commandHandler.StartGame();
                                break;
                            case NetworkCommands.MOVE_PLAYER:
                                commandHandler.MovePlayer(msg);
                                break;
                            default:
                                logger.Error("Server - Unhandled network command type");
                                break;
                        }
                        break;
                    case NetIncomingMessageType.ErrorMessage:
                        logger.Error("Server - " + msg.ReadString());
                        break;
                    case NetIncomingMessageType.StatusChanged:
                        commandHandler.StatusChanged(msg);
                        break;
                    default:
                        logger.Error("Server - Unhandled type: " + msg.MessageType);
                        break;
                }
                server.Recycle(msg);
            }
            System.Threading.Thread.Sleep(10);
        }

        internal void sendMsg(Message msgContent, NetConnection recipient)
        {
            NetOutgoingMessage msg = prepareMsg(msgContent);
            server.SendMessage(msg, recipient, NetDeliveryMethod.ReliableOrdered);
            server.FlushSendQueue();
        }

        internal void sendMsgToAll(Message msgContent)
        {
            NetOutgoingMessage msg = prepareMsg(msgContent);
            server.SendToAll(msg, NetDeliveryMethod.ReliableOrdered);
            server.FlushSendQueue();
        }

        private NetOutgoingMessage prepareMsg(Message msgContent)
        {
            NetOutgoingMessage msg = server.CreateMessage();
            msg.Write((byte)msgContent.command);
            byte[] bytes = SerializationHandler.ObjectToByteArray(msgContent);
            msg.Write(bytes.Length);
            msg.Write(bytes);
            return msg;
        }
    }
}
