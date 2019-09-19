using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Src.Entities
{
    public abstract class CollidableEntity : Entity, ICollidableEntity
    {
        public Vector2 Position { get; set; }
        public abstract float Width { get; }
        public abstract float Height { get; }

        public RectangleF HitBox
        {
            get
            {
                return new RectangleF(Position.X, Position.Y, Width, Height);
            }
        }
    }
}
