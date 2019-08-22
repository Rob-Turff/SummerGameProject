using Lidgren.Network;
using Microsoft.Xna.Framework;
using SummerGameProject.Src.Common;
using SummerGameProject.Src.Common.Message;
using SummerGameProject.Src.Common.Utilities;
using SummerGameProject.Src.Server.Networking;
using System;
using System.Threading;
using static SummerGameProject.Src.Screens.ScreenManager;

namespace SummerGameProject.Src.Client.Networking
{
    public class NetworkHandler
    {
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private readonly MainGame game;
        private ClientCommandHandler commandHandler;
        private string ip = "localhost";
        private int port = 7777;
        private NetClient client;
        private bool isHost = false;
        private bool isConnected = false;
        private bool gameStarted = false;
        private bool onGameScreen = false;

        public NetworkHandler(MainGame game)
        {
            this.game = game;
            commandHandler = new ClientCommandHandler(this, game);
        }

        public GameServer gameServer { get; private set; }

        public void HostServer()
        {
            isHost = true;
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

            Thread msgReadThread = new Thread(ReadMessages);
            msgReadThread.Start();

            waitForConnection();
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
            while (true) {
                while ((msg = client.ReadMessage()) != null)
                {
                    switch (msg.MessageType)
                    {
                        case NetIncomingMessageType.Data:
                            switch ((NetworkCommands)msg.ReadByte())
                            {
                                case NetworkCommands.ADD_PLAYER:
                                    logger.Debug("Client - received add player message");
                                    commandHandler.AddPlayer(msg);
                                    break;
                                case NetworkCommands.START_GAME:
                                    gameStarted = true;
                                    break;
                                default:
                                    logger.Error("Client - Unhandled network command type");
                                    break;
                            }
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
                                logger.Error("Client - Connection Status net yet implemented: " + msg.SenderConnection.Status);
                            break;
                        default:
                            logger.Error("Client - Unhandled type: " + msg.MessageType);
                            break;
                    }
                    client.Recycle(msg);
                }
                System.Threading.Thread.Sleep(10);
            }
        }

        internal void Update(GameTime gameTime)
        {
            if (gameStarted && !onGameScreen)
            {
                game.ScreenManager.ChangeScreen(ScreenEnum.GAME);
                onGameScreen = true;
            }
        }

        private void waitForConnection()
        {
            while (!isConnected)
            {
                System.Threading.Thread.Sleep(50);
                //TODO CONNECTING SCREEN?
            }

            game.ScreenManager.ChangeScreen(ScreenEnum.LOBBY);
        }

        private void handleConnect()
        {
            game.GameData.clientsPlayer = Guid.NewGuid();
            sendMessage(new PlayerJoinMessage(game.GameData.PlayerName, game.GameData.clientsPlayer, isHost));
            isConnected = true;
        }

        private void handleDisconnect()
        {

        }
    }
}
