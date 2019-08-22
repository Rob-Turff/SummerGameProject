using Microsoft.Xna.Framework;
using SummerGameProject.Src.Client.Components.Player;
using SummerGameProject.Src.Components.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummerGameProject.Src.Client.Utilities
{
    public class GameDataStore
    {
        public string PlayerName;
        public Guid clientsPlayer;
        public List<PlayerAttributes> players = new List<PlayerAttributes>();

        public bool IsHost()
        {
            foreach (PlayerAttributes p in players)
            {
                if (clientsPlayer.Equals(p.playerID) && p.isHost)
                    return true;
            }
            return false;
        }
    }
}
