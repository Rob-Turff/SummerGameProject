using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Src.Entities
{
    public interface IDrawableEntity
    {
        string ImageName { get; }
        float Scale { get; }
        float Width { get; }
        float Height { get; }
        Vector2 Position { get; }
    }
}
