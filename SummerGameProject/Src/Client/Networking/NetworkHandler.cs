using Lidgren.Network;
using SummerGameProject.Src.Common;
using SummerGameProject.Src.Server.Networking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SummerGameProject.Src.Client.Networking
{
    public class NetworkHandler
    {
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private string ip = "localhost";
        private int port = 7777;
        private NetClient client;
        
        public GameServer gameServer { get; private set; }

        public void HostServer()
        {
            gameServer = new GameServer();
            Thread serverThread = new Thread(gameServer.StartServer);
            serverThread.Start();
            StartClient();
        }

        public void StartClient()
        {
            NetPeerConfiguration config = new NetPeerConfiguration("gameServer");
            client = new NetClient(config);
            client.Start();

            client.Connect(ip, port);

            while (true)
                ReadMessages();
        }

        public void sendMessage(NetworkCommands msgType, byte[] bytes)
        {
            NetOutgoingMessage msg = client.CreateMessage();
            msg.Write((byte)NetworkCommands.ADD_PLAYER);
            msg.Write(bytes);
            client.SendMessage(msg, NetDeliveryMethod.ReliableOrdered);
            client.FlushSendQueue();
        }

        private void ReadMessages()
        {
            NetIncomingMessage msg;
            while ((msg = client.ReadMessage()) != null)
            {
                switch (msg.MessageType)
                {
                    case NetIncomingMessageType.Data:
                        logger.Debug("Client received message: " + msg.ReadString());
                        break;
                    case NetIncomingMessageType.ErrorMessage:
                        logger.Error(msg.ReadString());
                        break;
                    case NetIncomingMessageType.StatusChanged:
                        if (msg.SenderConnection.Status == NetConnectionStatus.Connected)
                        {
                            logger.Debug("Connected to server " + msg.SenderConnection.Peer.Configuration.LocalAddress);
                            //TODO Implement
                        }
                        else if (msg.SenderConnection.Status == NetConnectionStatus.Disconnected)
                        {
                            logger.Debug("Disconnected from server " + msg.SenderConnection.Peer.Configuration.LocalAddress);
                            //TODO Implement
                        }
                        else
                            logger.Error("Connection Status net yet implemented: " + msg.SenderConnection.Status);
                        break;
                    default:
                        logger.Error("Unhandled type: " + msg.MessageType);
                        break;
                }
                client.Recycle(msg);
            }
        }
    }
}
