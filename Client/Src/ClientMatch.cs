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

            foreach (CharacterData characterData in matchStartedPacket.CharacterDataList)
            {
                characters.Add(new Character(characterData.ID, characterData.Position));
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
            foreach (CharacterData characterData in worldStatePacket.CharacterDataList)
            {
                Character characterOfInterest = characters.First(character => character.ID == characterData.ID);
                characterOfInterest.Position = characterData.Position;
                characterOfInterest.Velocity = characterData.Velocity;
            }
        }

        protected override void RunProcesses(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();

            CharacterInputs playerInputs = CharacterInputs.NONE;

            if (keyboardState.IsKeyDown(Keys.W) || keyboardState.IsKeyDown(Keys.Space))
                playerInputs |= CharacterInputs.JUMP;
            if (keyboardState.IsKeyDown(Keys.A))
                playerInputs |= CharacterInputs.LEFT;
            else if (keyboardState.IsKeyDown(Keys.D))
                playerInputs |= CharacterInputs.RIGHT;

            if (playerInputs != CharacterInputs.NONE)
            {
                CharacterInputPacket playerInputPacket = new CharacterInputPacket(playerInputs);

                SendPlayerInputs(playerInputPacket);
            }

        }

        private void SendPlayerInputs(CharacterInputPacket playerInputPacket)
        {
            SendMessageToServer(playerInputPacket);
        }
    }
}