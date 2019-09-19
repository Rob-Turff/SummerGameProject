using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Common.Src.Entities.Platforms
{
    abstract public class Platform : CollidableEntity, IDrawableEntity
    {
        private readonly float width;
        private readonly float height;

        public abstract string ImageName { get; }
        public float Scale { get; set; } = 1;

        public override float Width => Scale * width;
        public override float Height => Scale * height;

        public Platform(Size size, Vector2 position)
        {
            Position = position;

            width = size.Width;
            height = size.Height;
        }
    }
}
