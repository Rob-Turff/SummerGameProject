using Common.Src.Entities.Platforms;
using Microsoft.Xna.Framework;

namespace Common.Src.Entities.Platforms
{
    internal class StoneWall : Platform
    {
        public override string ImageName => "Stonewall";

        public StoneWall(Vector2 position) : base(Common.Properties.Resource1.Stonewall.Size, position) { }

    }
}