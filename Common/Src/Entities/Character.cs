using Common.Src.Packets.ClientToServer;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Microsoft.Xna.Framework;
using Common.Src;

namespace Common.Src.Entities
{
    // NOTE: We aren't using the same vector 2 as in the client project as to do so would require adding a reference to the monogame dll
    public class Character : ICollidableEntity, IMovableCharacter, IDrawableEntity
    {
        public string ImageName => "Player";
        public float Scale => 1;

        public Character(Player player,Vector2 position)
        {
            Player = player;
            Position = position;

            PlayerInputs = PlayerInputs.NONE;
            Velocity = new Vector2(0, 0);
            JumpTime = 0;
            IsOnGround = false;
        }
        
        public Player Player { get; }
        public PlayerInputs PlayerInputs { get; set; }

        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }

        public float JumpTime { get; set; }
        public bool IsOnGround { get; set; }

        public float Width => 72;
        public float Height => 106;
        public RectangleF HitBox => new RectangleF(Position.X, Position.Y, Width, Height);

    }
}