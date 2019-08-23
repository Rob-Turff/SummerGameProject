using Lidgren.Network;
using Microsoft.Xna.Framework;
using SummerGameProject.Src.Client.Components.Player;
using SummerGameProject.Src.Common.Message;
using SummerGameProject.Src.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SummerGameProject.Src.Screens.ScreenManager;

namespace SummerGameProject.Src.Client.Networking
{
    public class ClientCommandHandler
    {
        private NetworkHandler networkHandler;
        private MainGame game;

        public ClientCommandHandler(NetworkHandler networkHandler, MainGame game)
        {
            this.networkHandler = networkHandler;
            this.game = game;
        }

        internal void AddPlayer(NetIncomingMessage msg)
        {
            int msgLength = msg.ReadInt32();
            byte[] msgContents = msg.ReadBytes(msgLength);
            PlayerJoinMessage joinMsg = (PlayerJoinMessage)SerializationHandler.ByteArrayToObject(msgContents);
            PlayerAttributes player = new PlayerAttributes(joinMsg.name, joinMsg.playerID, joinMsg.isHost, new Vector2(joinMsg.posX, joinMsg.posY));
            game.GameData.players.Add(player);
        }

        internal void MovePlayer(NetIncomingMessage msg)
        {
            int msgLength = msg.ReadInt32();
            byte[] msgContents = msg.ReadBytes(msgLength);
            PlayerMoveMessage moveMsg = (PlayerMoveMessage)SerializationHandler.ByteArrayToObject(msgContents);
            PlayerMove move = moveMsg.move;
            if (game.GameData.clientsPlayerID != moveMsg.playerID)
            {
                game.GameData.getPlayer(moveMsg.playerID).currentMove = move;
            }
        }
    }
}
