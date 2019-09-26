using Microsoft.Xna.Framework;
using SummerGameProject.Src.Common;
using SummerGameProject.Src.Components.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummerGameProject.Src.Client.Components.Player
{
    public class PlayerAttributes
    {
        public Guid playerID;
        public string playerName;
        public Vector2 velocity = new Vector2(0, 0);
        public Vector2 position = new Vector2(0, 0);
        public PlayerMove currentMove = new PlayerMove(0, 0);
        public PlayerMovementHandler movementHandler;
        public bool isHost = false;

        public PlayerAttributes(string playerName, Guid playerID, bool isHost)
        {
            this.playerName = playerName;
            this.playerID = playerID;
            this.isHost = isHost;
        }

        public PlayerAttributes(string playerName, Guid playerID, bool isHost, Vector2 position)
        {
            this.playerName = playerName;
            this.playerID = playerID;
            this.isHost = isHost;
            this.position = position;
        }
    }
}
