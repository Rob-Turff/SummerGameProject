using Microsoft.Xna.Framework;
using System;

namespace SummerGameProject.Src.Client.Components.Player
{
    [Serializable]
    public class PlayerMove
    {
        public bool jumping;
        public bool movingLeft;
        public bool movingRight;
        private float xPos;
        private float yPos;

        public Vector2 Position
        {
            get
            {
                return new Vector2(xPos, yPos);
            }
        }

        public PlayerMove(float xPos, float yPos)
        {
            this.jumping = false;
            this.movingLeft = false;
            this.movingRight = false;
            this.xPos = xPos;
            this.yPos = yPos;
        }

        public PlayerMove(bool jumping, bool movingLeft, bool movingRight, float xPos, float yPos)
        {
            this.jumping = jumping;
            this.movingLeft = movingLeft;
            this.movingRight = movingRight;
            this.xPos = xPos;
            this.yPos = yPos;
        }

        public bool isSameDirection(PlayerMove obj)
        {
            return (this.jumping == obj.jumping && this.movingLeft == obj.movingLeft && this.movingRight == obj.movingRight);
        }
    }
}
