using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummerGameProject.Src.Common.Message
{
    [Serializable]
    public class PlayerJoinMessage : Message
    {
        internal Guid playerID;
        internal string name;

        public PlayerJoinMessage(string name, Guid playerID) : base(NetworkCommands.ADD_PLAYER) {
            this.name = name;
            this.playerID = playerID;
        }
    }
}
