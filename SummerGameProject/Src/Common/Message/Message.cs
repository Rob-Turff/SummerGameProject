using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummerGameProject.Src.Common.Message
{
    [Serializable]
    public abstract class Message
    {
        public readonly NetworkCommands command;

        internal Message(NetworkCommands command)
        {
            this.command = command;
        }
    }
}
