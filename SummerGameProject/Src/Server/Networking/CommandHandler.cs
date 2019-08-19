using Lidgren.Network;
using SummerGameProject.Src.Client.Components.Player;
using SummerGameProject.Src.Common.Message;
using SummerGameProject.Src.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummerGameProject.Src.Server.Networking
{
    public class CommandHandler
    {
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private GameServer gameServer;
        private readonly MainGame game;

        public CommandHandler(GameServer gameServer, MainGame game)
        {
            this.gameServer = gameServer;
            this.game = game;
        }

        public void PlayerJoined(NetIncomingMessage msg)
        {
            int msgLength = msg.ReadInt32();
            byte[] msgContents = msg.ReadBytes(msgLength);
            PlayerJoinMessage playerJoinMsg = (PlayerJoinMessage) SerializationHandler.ByteArrayToObject(msgContents);
            PlayerAttributes player = new PlayerAttributes(playerJoinMsg.name, playerJoinMsg.playerID);
            game.GameData.players.Add(player);
            logger.Debug("Player with name " + player.playerName + " added");
        }

        public void StatusChanged(NetIncomingMessage msg)
        {
            if (msg.SenderConnection.Status == NetConnectionStatus.Connected)
            {
                logger.Debug(msg.SenderConnection.Peer.Configuration.LocalAddress + " has connected");
                gameServer.clientList.Add(msg.SenderConnection.Peer);

            }
            else if (msg.SenderConnection.Status == NetConnectionStatus.Disconnected)
            {
                logger.Debug(msg.SenderConnection.Peer.Configuration.LocalAddress + " has disconnected");
                gameServer.clientList.Remove(msg.SenderConnection.Peer);
            }
            else
                logger.Error("Connection Status net yet implemented: " + msg.SenderConnection.Status);
        }
    }
}
