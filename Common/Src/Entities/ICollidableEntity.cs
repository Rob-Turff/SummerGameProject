using System.Drawing;

namespace Common.Src.Entities
{
    public interface ICollidableEntity
    {
        RectangleF HitBox { get; }
    }
}