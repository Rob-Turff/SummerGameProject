using Lidgren.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Src
{
    public class NetworkSettings
    {
        private const string appIdentifier = "SummerGameProject";
        public static int DefaultPort => 27015;
        public static NetPeerConfiguration DefaultNetPeerConfiguration => new NetPeerConfiguration(appIdentifier);
    }
}
