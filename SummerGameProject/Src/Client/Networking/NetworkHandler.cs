using Lidgren.Network;
using SummerGameProject.Src.Common;
using SummerGameProject.Src.Common.Message;
using SummerGameProject.Src.Common.Utilities;
using SummerGameProject.Src.Server.Networking;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SummerGameProject.Src.Client.Networking
{
    public class NetworkHandler
    {
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private readonly MainGame game;
        private string ip = "localhost";
        private int port = 7777;
        private NetClient client;

        public NetworkHandler(MainGame game)
        {
            this.game = game;
        }

        public GameServer gameServer { get; private set; }

        public void HostServer()
        {
            gameServer = new GameServer(game);
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

        public void sendMessage(Message msgContent)
        {
            NetOutgoingMessage msg = client.CreateMessage();
            msg.Write((byte)msgContent.command);
            byte[] bytes = SerializationHandler.ObjectToByteArray(msgContent);
            msg.Write(bytes.Length);
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
                            handleConnect();
                        }
                        else if (msg.SenderConnection.Status == NetConnectionStatus.Disconnected)
                        {
                            logger.Debug("Disconnected from server " + msg.SenderConnection.Peer.Configuration.LocalAddress);
                            handleDisconnect();
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

        private void handleConnect()
        {
            game.GameData.clientsPlayer = new Guid();
            sendMessage(new PlayerJoinMessage(game.GameData.PlayerName, game.GameData.clientsPlayer));
        }

        private void handleDisconnect()
        {

        }
    }
}
