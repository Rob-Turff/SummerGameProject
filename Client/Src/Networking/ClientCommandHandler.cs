//using Lidgren.Network;
//using Microsoft.Xna.Framework;
//using Client.Src.Components.Player;
//using Client.Src.Common.Utilities;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using static Client.Src.Screens.ScreenManager;

//namespace Client.Src.Networking
//{
//    public class ClientCommandHandler
//    {
//        private NetworkHandler networkHandler;
//        private GameClient game;

//        public ClientCommandHandler(NetworkHandler networkHandler, GameClient game)
//        {
//            this.networkHandler = networkHandler;
//            this.game = game;
//        }

//        internal void AddPlayer(NetIncomingMessage msg)
//        {
//            int msgLength = msg.ReadInt32();
//            byte[] msgContents = msg.ReadBytes(msgLength);
//            PlayerJoinMessage joinMsg = (PlayerJoinMessage)SerializationHandler.ByteArrayToObject(msgContents);
//            PlayerAttributes player = new PlayerAttributes(joinMsg.name, joinMsg.playerID, joinMsg.isHost, new Vector2(joinMsg.posX, joinMsg.posY));
//            game.GameData.players.Add(player);
//        }

//        internal void MovePlayer(NetIncomingMessage msg)
//        {
//            int msgLength = msg.ReadInt32();
//            byte[] msgContents = msg.ReadBytes(msgLength);
//            PlayerMoveMessage moveMsg = (PlayerMoveMessage)SerializationHandler.ByteArrayToObject(msgContents);
//            PlayerMove move = moveMsg.move;
//            if (game.GameData.clientsPlayerID != moveMsg.playerID)
//            {
//                game.GameData.getPlayer(moveMsg.playerID).currentMove = move;
//            }
//        }
//    }
//}
