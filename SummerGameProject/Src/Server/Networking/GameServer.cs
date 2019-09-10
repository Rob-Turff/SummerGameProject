using Lidgren.Network;
using SummerGameProject.Src.Common;
using SummerGameProject.Src.Common.Message;
using SummerGameProject.Src.Common.Utilities;
using System.Collections.Generic;

namespace SummerGameProject.Src.Server.Networking
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

        /// <summary>
        /// Starts the server and initiates the messsage reading thread
        /// </summary>
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

        /// <summary>
        /// Continously reads and handles messages from the client
        /// </summary>
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

        /// <summary>
        /// Sends the message content to the desired recipient using the Reliable Ordered delivery type
        /// </summary>
        /// <param name="msgContent"></param>
        /// <param name="recipient"></param>
        internal void sendMsg(Message msgContent, NetConnection recipient)
        {
            NetOutgoingMessage msg = prepareMsg(msgContent);
            server.SendMessage(msg, recipient, NetDeliveryMethod.ReliableOrdered);
            server.FlushSendQueue();
        }

        /// <summary>
        /// Sends the message content to every connected client using the reliable ordered delivery type
        /// </summary>
        /// <param name="msgContent"></param>
        internal void sendMsgToAll(Message msgContent)
        {
            NetOutgoingMessage msg = prepareMsg(msgContent);
            server.SendToAll(msg, NetDeliveryMethod.ReliableOrdered);
            server.FlushSendQueue();
        }

        /// <summary>
        /// Converts the message into a sendable format
        /// </summary>
        /// <param name="msgContent"></param>
        /// <returns></returns>
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
