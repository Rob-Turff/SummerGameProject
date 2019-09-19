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
    public interface IStage
    {
        StageIdentifier StageIdentifier { get; }

        List<Platform> Terrain { get; }
        List<Vector2> CharacterSpawnPositions { get; }
    }
}
