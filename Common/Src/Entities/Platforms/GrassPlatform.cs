using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Common.Src.Entities.Platforms
{
    class GrassPlatform : Platform
    {
        public override string ImageName => "GroundV1";

        public GrassPlatform(Vector2 position,float scale = 1) : base(Common.Properties.Resource1.GroundV1.Size, position)
        {
            Scale = scale;
        }

    }
}
