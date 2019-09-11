
using Common.Src.Packets;
using Common.Src.Packets.ServerToClient;
using Lidgren.Network;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace Client.Src.Screens
{
    internal abstract class NetworkScreen : Screen , IClientPacketHandler
    {
        protected GameClient Client { get; }

        public NetworkScreen(Game1 game, GameClient gameClient) : base(game)
        {
            Debug.Assert(gameClient.IsConnected);

            this.Client = gameClient;
        }

        public override void Update(GameTime gameTime)
        {
            Client.Update(gameTime);
            base.Update(gameTime);
        }

        public abstract void HandleMatchStartedPacket(MatchStartedPacket matchStartedPacket);

        public abstract void HandleLobbyInfoPacket(LobbyInfoPacket clientNamesPacket);
    }
}