using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Src.Entities;
using Common.Src.Entities.Platforms;
using Microsoft.Xna.Framework;

namespace Common.Src.Stages
{
    public class DefaultStage : IStage
    {
        public StageIdentifier StageIdentifier => StageIdentifier.DEFAULT;

        public List<Platform> Terrain { get; }
        public List<Vector2> CharacterSpawnPositions { get; }

        public DefaultStage()
        {
            Terrain = new List<Platform>();

            Platform floor = new GrassPlatform(new Vector2(0, 880));
            Platform platform1 = new GrassPlatform(new Vector2(300, 650)) { Scale = 0.2f };
            Platform platform2 = new GrassPlatform(new Vector2(1200, 650)) { Scale = 0.2f };
            Platform wallLeft = new StoneWall(new Vector2(0, 380));
            Platform wallRight = new StoneWall(new Vector2(1870, 380));

            Terrain.Add(floor);
            Terrain.Add(platform1);
            Terrain.Add(platform2);
            Terrain.Add(wallLeft);
            Terrain.Add(wallRight);

            CharacterSpawnPositions = new List<Vector2>() { new Vector2(75, 680), new Vector2(885, 680), new Vector2(1035, 680), new Vector2(75, 680) };
        }



    }
}
