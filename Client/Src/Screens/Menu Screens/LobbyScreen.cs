using Microsoft.Xna.Framework;
using Client.Src.Components;
using Client.Src.Screens;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Lidgren.Network;
using Common.Src.Packets;
using Common.Src.Packets.ServerToClient;
using System.Linq;
using Common.Src;

namespace Client.Src.Screens
{
    internal class LobbyScreen : Screen
    {
        private readonly List<PlayerLabel> playerLabels = new List<PlayerLabel>();
        private readonly Button startGameBtn;
        private readonly bool amHosting;
        private readonly ClientLobby clientLobby;
        private readonly int distanceBetweenLabels = 30;
        private Vector2 nextLabelPosition;

        public LobbyScreen(Game1 game, ClientLobby clientLobby, bool amHosting) : base(game)
        {
            ScreenWidth = 400;
            ScreenHeight = 500;
            IsFullScreen = false;

            this.clientLobby = clientLobby;
            this.amHosting = amHosting;

            this.nextLabelPosition = new Vector2(ScreenWidth / 2, ScreenHeight / 10);

            clientLobby.PlayerHasJoined += LobbyScreen_PlayerHasJoined;
            clientLobby.PlayerHasLeft += LobbyScreen_PlayerHasLeft;
            clientLobby.MatchHasStarted += LobbyScreen_MatchHasStarted;

            if (amHosting)
            {
                startGameBtn = new Button("Start Game", new Vector2(ScreenWidth / 2, ScreenHeight * 4 / 5), () => clientLobby.SendMatchStartRequestPacket(), this)
                {
                    CentreOnPosition = true
                };
                UIComponents.Add(startGameBtn);
            }
        }


        public override void Update(GameTime gameTime)
        {
            clientLobby.Update(gameTime);
            base.Update(gameTime);
        }

        private void LobbyScreen_PlayerHasJoined(object sender, (Player, bool) tuple)
        {
            Player player = tuple.Item1;
            bool isHost = tuple.Item2;

            Label label = new Label(player.Name, nextLabelPosition, this)
            {
                CentreOnPosition = true
            };

            if (isHost)
                label.IsTextBold = true;

            UIComponents.Add(label);
            playerLabels.Add(new PlayerLabel(player, label));


        }

        private void LobbyScreen_PlayerHasLeft(object sender, Player removedPlayer)
        {
            bool afterRemovedLabel = false;

            for (int i = 0, length = playerLabels.Count; i < length; i++)
            {
                PlayerLabel playerLabel = playerLabels[i];
                Player player = playerLabel.Player;
                Label label = playerLabel.Label;

                if (player == removedPlayer)
                {
                    UIComponents.Remove(label);
                    playerLabels.Remove(playerLabel);
                    afterRemovedLabel = true;
                    i--;
                    continue;
                }

                // Move each label after the removed label up
                if (afterRemovedLabel == true)
                {
                    label.Position = new Vector2(label.Position.X, label.Position.Y - distanceBetweenLabels);
                }
            }

            nextLabelPosition = new Vector2(nextLabelPosition.X, nextLabelPosition.Y - distanceBetweenLabels);
        }

        private void LobbyScreen_MatchHasStarted(object sender, (NetClient netClient, MatchStartedPacket matchStartedPacket) pair)
        {
            GameScreen gameScreen = new GameScreen(Game, new ClientMatch(pair.netClient, pair.matchStartedPacket));
            Game.ScreenManager.CurrentScreen = gameScreen;
        }


    }

    internal class PlayerLabel
    {
        public Player Player { get; }
        public Label Label { get; }

        public PlayerLabel(Player player, Label label)
        {
            Player = player;
            Label = label;
        }
    }
}
