using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummerGameProject.Src.Client.Components.Player
{
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
    }
}
