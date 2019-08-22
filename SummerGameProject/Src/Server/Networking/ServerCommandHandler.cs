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
    public class ServerCommandHandler
    {
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private GameServer gameServer;
        private readonly MainGame game;

        public ServerCommandHandler(GameServer gameServer, MainGame game)
        {
            this.gameServer = gameServer;
            this.game = game;
        }

        public void PlayerJoined(NetIncomingMessage msg)
        {
            int msgLength = msg.ReadInt32();
            byte[] msgContents = msg.ReadBytes(msgLength);
            PlayerJoinMessage joinMsg = (PlayerJoinMessage) SerializationHandler.ByteArrayToObject(msgContents);
            PlayerAttributes player = new PlayerAttributes(joinMsg.name, joinMsg.playerID, joinMsg.isHost);
            game.GameData.players.Add(player);
            gameServer.clientList.Add(new ClientInfo(msg.SenderConnection, player));
            logger.Debug("Player with name " + player.playerName + " added");
            ClientPlayerUpdate();
        }

        public void StatusChanged(NetIncomingMessage msg)
        {
            if (msg.SenderConnection.Status == NetConnectionStatus.Connected)
            {
                logger.Debug(msg.SenderConnection.Peer.Configuration.LocalAddress + " has connected");

            }
            else if (msg.SenderConnection.Status == NetConnectionStatus.Disconnected)
            {
                logger.Debug(msg.SenderConnection.Peer.Configuration.LocalAddress + " has disconnected");
                // TODO Handle disconnect from server
            }
            else
                logger.Error("Connection Status net yet implemented: " + msg.SenderConnection.Status);
        }

        internal void StartGame()
        {
            gameServer.sendMsgToAll(new StartGameMessage());
        }

        private void ClientPlayerUpdate()
        {
            foreach (ClientInfo c in gameServer.clientList)
            {
                foreach (var p in game.GameData.players)
                {
                    if (!c.playerSent.Contains(p.playerID) && !c.playerInfo.isHost)
                    {
                        gameServer.sendMsg(new PlayerJoinMessage(p.playerName, p.playerID, p.isHost), c.clientConnection);
                        c.playerSent.Add(p.playerID);
                    }
                }
            }
        }
    }
}
