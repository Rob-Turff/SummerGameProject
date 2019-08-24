using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Src.Client.Components.Player
{
    [Serializable]
    public class PlayerMove
    {
        public bool jumping;
        public bool movingLeft;
        public bool movingRight;

        public PlayerMove()
        {
            this.jumping = false;
            this.movingLeft = false;
            this.movingRight = false;
        }

        public PlayerMove(bool jumping, bool movingLeft, bool movingRight)
        {
            this.jumping = jumping;
            this.movingLeft = movingLeft;
            this.movingRight = movingRight;
        }

        public static bool operator ==(PlayerMove obj1, PlayerMove obj2)
        {
            return (obj1.jumping == obj2.jumping && obj1.movingLeft == obj2.movingLeft && obj1.movingRight == obj2.movingRight);
        }

        public static bool operator !=(PlayerMove obj1, PlayerMove obj2)
        {
            return !(obj1.jumping == obj2.jumping && obj1.movingLeft == obj2.movingLeft && obj1.movingRight == obj2.movingRight);
        }
    }
}
