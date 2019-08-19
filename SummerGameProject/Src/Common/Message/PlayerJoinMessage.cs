using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummerGameProject.Src.Common.Message
{
    public class PlayerJoinMessage : Message
    {
        internal Guid guid;
        internal string name;

        public PlayerJoinMessage() : base(NetworkCommands.ADD_PLAYER) { }
    }
}
