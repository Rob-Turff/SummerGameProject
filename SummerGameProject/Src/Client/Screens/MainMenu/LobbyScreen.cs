using Microsoft.Xna.Framework;
using SummerGameProject.Src.Client.Components;
using SummerGameProject.Src.Client.Components.Player;
using SummerGameProject.Src.Screens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummerGameProject.Src.Client.Screens
{
    public class LobbyScreen : Screen
    {
        private List<Label> playerLabels = new List<Label>();
        private List<Guid> labelsCreated = new List<Guid>();

        public LobbyScreen(MainGame game) : base(game)
        {
            ScreenWidth = 400;
            ScreenHeight = 500;
            IsFullScreen = false;
        }

        private void updateConnectedPlayers()
        {
            foreach (var p in game.GameData.players)
            {
                if (!labelsCreated.Contains(p.playerID))
                {
                    if (p.isHost)
                        Components.Add(new Label(p.playerName + " : Host", new Vector2(), this));
                    else
                        Components.Add(new Label(p.playerName, new Vector2(), this));

                    labelsCreated.Add(p.playerID);
                    DistributeVertically(Components);
                }
            }
        }

        public override void LoadContent()
        {
            base.LoadContent();

        }

        public override void Update(GameTime gameTime)
        {
            updateConnectedPlayers();
            base.Update(gameTime);
        }
    }
}
