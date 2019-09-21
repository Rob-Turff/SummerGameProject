using Microsoft.Xna.Framework;
using SummerGameProject.Src.Client.Components;
using SummerGameProject.Src.Common.Message;
using SummerGameProject.Src.Components;
using SummerGameProject.Src.Screens;
using System;
using System.Collections.Generic;

namespace SummerGameProject.Src.Client.Screens
{
    public class LobbyScreen : Screen
    {
        private List<Label> playerLabels = new List<Label>();
        private List<Guid> labelsCreated = new List<Guid>();
        private bool buttonAdded = false;
        private Button startGameBtn;

        public LobbyScreen(MainGame game) : base(game)
        {
            ScreenWidth = 400;
            ScreenHeight = 500;
            IsFullScreen = false;

            startGameBtn = new Button("Start Game", new Vector2(0, 0), startGame, this);
            components.Add(startGameBtn);
        }

        private void updateConnectedPlayers()
        {
            foreach (var p in game.GameData.players)
            {
                if (!labelsCreated.Contains(p.playerID))
                {
                    if (p.isHost)
                        components.Add(new Label(p.playerName + " : Host", new Vector2(), this));
                    else
                        components.Add(new Label(p.playerName, new Vector2(), this));

                    labelsCreated.Add(p.playerID);
                    DistributeVertically(components);
                }
            }
        }

        private void startGame()
        {
            game.networkHandler.sendMessage(new StartGameMessage());
        }

        public override void LoadContent()
        {
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            updateConnectedPlayers();

            if (!game.GameData.IsHost())
            {
                components.Remove(startGameBtn);
                DistributeVertically(components);
            }

            base.Update(gameTime);
        }
    }
}
