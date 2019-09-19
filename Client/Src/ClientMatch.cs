using Common.Src;
using Common.Src.Entities;
using Common.Src.Entities.Platforms;
using Common.Src.Packets.ClientToServer;
using Common.Src.Packets.ServerToClient;
using Common.Src.Stages;
using Lidgren.Network;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Client.Src.Screens
{
    internal class ClientMatch : NetClientHandler
    {
        public event EventHandler<IDrawableEntity> DrawableEntityHasBeenAdded;

        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private readonly List<Platform> platforms;
        private readonly List<Character> characters;

        public List<IDrawableEntity> DrawableEntities
        {
            get
            {
                List<IDrawableEntity> drawableEntities = new List<IDrawableEntity>(characters);
                drawableEntities.AddRange(platforms);
                return drawableEntities;
            }
        }


        public ClientMatch(NetClient netClient, MatchStartedPacket matchStartedPacket) : base(netClient)
        {
            characters = new List<Character>();
            platforms = new List<Platform>();

            IStage stage = StageFactory.GetStage(matchStartedPacket.StageIdentifier);

            foreach (Platform platform in stage.Terrain)
            {
                platforms.Add(platform);
            }

            int i = 0;
            foreach (Player player in matchStartedPacket.Players)
            {
                characters.Add(new Character(player, stage.CharacterSpawnPositions[i++]));
            }
        }

        public override string ToString() => "Client Match";

        protected override void HandleLobbyInfoPacket(LobbyInfoPacket lobbyInfoPacket)
        {
            logger.Error(ToString() + ": Lobby information packet received!");
        }

        protected override void HandleMatchStartedPacket(MatchStartedPacket matchStartedPacket)
        {
            logger.Error(ToString() + ": Match started packet received!");
        }

        protected override void HandleWorldStatePacket(WorldStatePacket worldStatePacket)
        {
            foreach ((Player player, Vector2 position) in worldStatePacket.PlayerPositions)
            {
                characters.First(character => character.Player == player).Position = position;
            }
        }

        protected override void RunProcesses(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();

            PlayerInputs playerInputs = PlayerInputs.NONE;

            if (keyboardState.IsKeyDown(Keys.W) || keyboardState.IsKeyDown(Keys.Space))
                playerInputs |= PlayerInputs.JUMP;
            if (keyboardState.IsKeyDown(Keys.A))
                playerInputs |= PlayerInputs.LEFT;
            else if (keyboardState.IsKeyDown(Keys.D))
                playerInputs |= PlayerInputs.RIGHT;

            if (playerInputs != PlayerInputs.NONE)
            {
                PlayerInputPacket playerInputPacket = new PlayerInputPacket(playerInputs);

                SendPlayerInputs(playerInputPacket);
            }

        }

        private void SendPlayerInputs(PlayerInputPacket playerInputPacket)
        {
            SendMessageToServer(playerInputPacket);
        }
    }
}