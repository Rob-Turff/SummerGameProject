using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Src.Common.Message
{
    [Serializable]
    public class StartGameMessage : Message
    {
        public StartGameMessage() : base(NetworkCommands.START_GAME)
        {
        }
    }
}
