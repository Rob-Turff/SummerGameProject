using Microsoft.Xna.Framework;
using SummerGameProject.Src.Common;
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
        public PlayerMove currentMove = new PlayerMove();
        public bool isHost = false;

        public PlayerAttributes(string playerName, Guid playerID, bool isHost)
        {
            this.playerName = playerName;
            this.playerID = playerID;
            this.isHost = isHost;
        }
    }
}
