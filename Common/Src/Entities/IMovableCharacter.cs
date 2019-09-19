using System.Drawing;
using System.Numerics;
using Common.Src.Packets.ClientToServer;
using Microsoft.Xna.Framework;

namespace Common.Src.Entities
{
    public interface IMovableCharacter : ICollidableEntity
    {
        PlayerInputs PlayerInputs { get; set; }

        Vector2 Velocity { get; set; }
        Vector2 Position { get; set; }

        float Width { get; }
        float Height { get; }

        float JumpTime { get; set; }
        bool IsOnGround { get; set; }
    }
}