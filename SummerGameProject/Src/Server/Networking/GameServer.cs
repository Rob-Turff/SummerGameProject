using Lidgren.Network;
using System.Collections.Generic;

namespace SummerGameProject.Src.Server.Networking
{
    public class GameServer
    {
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static List<NetPeer> clientList;

        public GameServer()
        {
            clientList = new List<NetPeer>();
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

        private static void ReadMessages(NetServer server)
        {
            NetIncomingMessage msg;
            while ((msg = server.ReadMessage()) != null)
            {
                switch (msg.MessageType)
                {
                    case NetIncomingMessageType.Data:
                        logger.Debug("Server received message: " + msg.ReadString());
                        break;
                    case NetIncomingMessageType.ErrorMessage:
                        logger.Error(msg.ReadString());
                        break;
                    case NetIncomingMessageType.StatusChanged:
                        if (msg.SenderConnection.Status == NetConnectionStatus.Connected)
                        {
                            logger.Debug(msg.SenderConnection.Peer.Configuration.LocalAddress + " has connected");
                            clientList.Add(msg.SenderConnection.Peer);

                        }
                        else if (msg.SenderConnection.Status == NetConnectionStatus.Disconnected)
                        {
                            logger.Debug(msg.SenderConnection.Peer.Configuration.LocalAddress + " has disconnected");
                            clientList.Remove(msg.SenderConnection.Peer);
                        }
                        else
                            logger.Error("Connection Status net yet implemented: " + msg.SenderConnection.Status);
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
