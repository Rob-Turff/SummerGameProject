using Client.Src.Client.Components.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Src.Common.Message
{
    [Serializable]
    public class PlayerMoveMessage : Message
    {
        public readonly PlayerMove move;
        public readonly Guid playerID;

        public PlayerMoveMessage(PlayerMove move, Guid playerID) : base(NetworkCommands.MOVE_PLAYER)
        {
            this.move = move;
            this.playerID = playerID;
        }
    }
}
