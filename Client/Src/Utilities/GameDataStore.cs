using Microsoft.Xna.Framework;
using Client.Src.Components.Player;
using System;
using System.Collections.Generic;

namespace Client.Src.Utilities
{
    public class GameDataStore
    {
        public string PlayerName;
        public Guid clientsPlayerID;
        public List<PlayerAttributes> players = new List<PlayerAttributes>();
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
            foreach (PlayerAttributes p in players)
            {
                if (clientsPlayerID.Equals(p.playerID) && p.isHost)
                    return true;
            }
            return false;
        }

        public PlayerAttributes GetPlayer(Guid ID)
        {
            foreach (PlayerAttributes p in players)
            {
                if (p.playerID == ID)
                    return p;
            }

            return null;
        }

        public PlayerAttributes GetClientsPlayer()
        {
            return GetPlayer(clientsPlayerID);
        }

        public Vector2 GetSpawnPos()
        {
            return spawnPositions[spawnPosIndex++];
        }
    }
}
