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
    internal class LobbyScreen : NetworkScreen
    {
        private readonly Dictionary<Guid,Label> playerLabels = new Dictionary<Guid, Label>();
        private readonly Button startGameBtn;
        private readonly bool amHosting;
        private Vector2 nextLabelPosition;

        public LobbyScreen(Game1 game, GameClient gameClient, bool amHosting) : base(game, gameClient)
        {
            ScreenWidth = 400;
            ScreenHeight = 500;
            IsFullScreen = false;

            this.amHosting = amHosting;
            this.nextLabelPosition = new Vector2(ScreenWidth / 2, ScreenHeight / 10);

            if (this.amHosting)
            {
                startGameBtn = new Button("Start Game", new Vector2(ScreenWidth / 2, ScreenHeight * 4 / 5), () => Client.SendMatchStartRequestPacket(), this)
                {
                    CentreOnPosition = true
                };
                Components.Add(startGameBtn);
            }
        }


        public override void HandleMatchStartedPacket(MatchStartedPacket matchStartedPacket)
        {
            Client.UnregisterPacketHandler();
            GameScreen gameScreen = new GameScreen(Game, Client);
            Client.RegisterPacketHandler(gameScreen);
            Game.ScreenManager.CurrentScreen = gameScreen;
        }

        public override void HandleLobbyInfoPacket(LobbyInfoPacket lobbyInfoPacket)
        {
            // Add labels for players without a label
            for (int i = 0, length = lobbyInfoPacket.Players.Count; i < length; i++)
            {
                Player player = lobbyInfoPacket.Players[i];

                if (!playerLabels.ContainsKey(player.ID))
                {
                    Label label = new Label(player.Name, nextLabelPosition, this)
                    {
                        CentreOnPosition = true
                    };

                    if (i == lobbyInfoPacket.HostIndex)
                        label.IsTextBold = true;

                    playerLabels.Add(player.ID, label);
                    Components.Add(label);

                    nextLabelPosition.Y += 40;
                }
            }

            // Remove labels for players who have left
            for (int i = 0, length = playerLabels.Keys.Count; i < length; i++)
            {
                Guid playerID = playerLabels.Keys.ElementAt(i);

                if (lobbyInfoPacket.Players.Count(player => player.ID == playerID) == 0)
                {
                    Components.Remove(playerLabels[playerID]);
                    playerLabels.Remove(playerID);
                    nextLabelPosition.Y -= 40;
                }
            }
        }

    }
}
