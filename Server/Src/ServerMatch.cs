using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Common.Src;
using Common.Src.Packets.ClientToServer;
using Common.Src.Packets.ServerToClient;
using Lidgren.Network;
using Microsoft.Xna.Framework;
using mattmc3.Common.Collections.Generic;
using Common.Src.Stages;
using Common.Src.Entities;

namespace Server.Src
{
    internal class ServerMatch : NetServerHandler
    {
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private readonly Dictionary<NetConnection, Character> connectionToCharacterMap;
        private readonly IStage stage;
        private readonly CharacterMovementSystem characterMovementSystem;

        protected override int TickRate => 100;

        public ServerMatch(NetServer netServer, OrderedDictionary<NetConnection, Player> connectionToPlayerMap)
        {
            // Make sure each playerconnection's connection corresponds to a connection of the netserver
            Debug.Assert(new HashSet<NetConnection>(netServer.Connections).SetEquals(connectionToPlayerMap.Keys));

            this.NetServer = netServer;

            this.connectionToCharacterMap = new Dictionary<NetConnection, Character>();

            this.stage = StageFactory.GetStage(StageIdentifier.DEFAULT);

            int i = 0;

            foreach (NetConnection netConnection in connectionToPlayerMap.Keys)
            {
                Guid characterID = Guid.NewGuid();
                Vector2 characterSpawnPos = stage.CharacterSpawnPositions[i];

                connectionToCharacterMap.Add(
                    netConnection,
                    new Character(characterID,characterSpawnPos)
                );
                i++;
            }

            this.characterMovementSystem = new CharacterMovementSystem(new List<IMovableCharacter>(connectionToCharacterMap.Values), stage.Terrain);

            BroadcastMatchStarted();
        }

        public override string ToString() => "Server Match";

        protected override void RunProcesses(TimeSpan elapsed)
        {
            characterMovementSystem.Update(elapsed);
            BroadcastWorldState();
        }

        protected override void HandlePlayerInputPacket(CharacterInputPacket playerInputPacket, NetConnection senderConnection)
        {
            logger.Info(ToString() + ": Player input packet recieved!");

            connectionToCharacterMap[senderConnection].CharacterInputs = playerInputPacket.Inputs;
        }

        protected override void HandlePlayerDisconnect(NetConnection senderConnection)
        {
            throw new System.NotImplementedException();
        }

        protected override void HandlePlayerJoinPacket(PlayerJoinPacket playerJoinPacket, NetConnection senderConnection)
        {
            string playerName = playerJoinPacket.Player.Name;

            // Don't allow players to join match in progress
            senderConnection.Deny();

            logger.Info(ToString() + ": " + playerName + "'s connection approval request was denied");
        }

        protected override void HandleMatchStartRequestPacket(MatchStartRequestPacket matchStartRequestPacket, NetConnection senderConnection)
        {
            logger.Error(ToString() + ": Match start request packet received!");
        }

        protected override void HandlePlayerConnect(NetConnection senderConnection)
        {
            logger.Error(ToString() + ": A player has connected during a match!");
        }

        private void BroadcastMatchStarted()
        {
            StageIdentifier stageID = stage.StageIdentifier;
            List<CharacterData> characterDataList = ExtractCharacterData(connectionToCharacterMap.Values.ToList());

            MatchStartedPacket matchStartedPacket = new MatchStartedPacket(stageID, characterDataList);

            BroadcastMessage(matchStartedPacket);
        }

        private void BroadcastWorldState()
        {
            List<CharacterData> characterDataList = ExtractCharacterData(connectionToCharacterMap.Values.ToList());

            WorldStatePacket worldStatePacket = new WorldStatePacket(characterDataList);

            BroadcastMessage(worldStatePacket);
        }

        private static List<CharacterData> ExtractCharacterData(List<Character> characters)
        {
            List<CharacterData> characterDataList = new List<CharacterData>();

            foreach (Character character in characters)
            {
                characterDataList.Add(new CharacterData(character.ID, character.Position, character.Velocity));
            }

            return characterDataList;
        }
    }
}