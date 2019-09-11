using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Src
{
    public enum PacketType
    {
        PLAYER_JOIN,
        MATCH_START_REQUEST,
        PLAYER_INPUT,
        LOBBY_INFO,
        MATCH_STARTED,
        WORLD_STATE
    }
}
