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
        public Guid clientsPlayerID;
        public List<PlayerStats> players = new List<PlayerStats>();
        public bool isMultiplayer = false;

        private List<Vector2> spawnPositions = new List<Vector2>();
        private int spawnPosIndex = 0;

        public GameDataStore()
        {
            //TODO TEMP auto generate these
            spawnPositions.Add(new Vector2(400, 400));
            spawnPositions.Add(new Vector2(800, 400));
            spawnPositions.Add(new Vector2(1200, 400));
            spawnPositions.Add(new Vector2(1600, 400));
        }

        public bool IsHost()
        {
            foreach (PlayerStats p in players)
            {
                if (clientsPlayerID.Equals(p.playerID) && p.isHost)
                    return true;
            }
            return false;
        }

        public PlayerStats getPlayer(Guid ID)
        {
            foreach (PlayerStats p in players)
            {
                if (p.playerID == ID)
                    return p;
            }

            return null;
        }

        public PlayerStats getClientsPlayer()
        {
            return getPlayer(clientsPlayerID);
        }

        public Vector2 getSpawnPos()
        {
            return spawnPositions[spawnPosIndex++];
        }
    }
}
