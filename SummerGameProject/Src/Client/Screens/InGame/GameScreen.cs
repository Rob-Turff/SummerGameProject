using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SummerGameProject.Src.Client.Components;
using SummerGameProject.Src.Client.Components.Player;
using SummerGameProject.Src.Client.Physics;
using SummerGameProject.Src.Client.Utilities;
using SummerGameProject.Src.Components;
using SummerGameProject.Src.Components.Platforms;
using SummerGameProject.Src.Components.Player;
using System;
using System.Collections.Generic;

namespace SummerGameProject.Src.Screens
{
    public class GameScreen : Screen
    {
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private bool wasEscapePressed = false;
        private InGameMenuScreen menuOverlay;
        private bool isMenuOverlayShowing;

        private PhysicsHandler physicsHandler;

        public GameScreen(MainGame game, bool useResScaling) : base(game, useResScaling)
        {
            SetMaxScreenSize();

            physicsHandler = new PhysicsHandler(this);

            menuOverlay = new InGameMenuScreen(game, this, false);

            World world = new World(this);

            Cam.Size = ScreenSize;
        }

        public void SetupGame()
        {
            if (game.GameData.isMultiplayer)
            {
                foreach (PlayerStats pa in game.GameData.players)
                {
                    components.Add(new Player(pa, this, game));
                }
            } else
            {
                Guid playerID = Guid.NewGuid();
                PlayerStats playerStats = new PlayerStats("Player 1", playerID, true);
                playerStats.position = new Vector2(ScreenWidth / 2, ScreenHeight / 2);
                Player player = new Player(playerStats, this, game);
                game.GameData.players.Add(playerStats);
                game.GameData.clientsPlayerID = playerID;
                components.Add(player);
            }

            foreach (Component c in components)
            {
                c.Position *= c.CombinedScale;
            }

            foreach (Entity e in entities)
            {
                e.Position *= e.CombinedScale;
            }
            LoadContent();
        }


        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (isMenuOverlayShowing)
            {
                menuOverlay.Draw(gameTime,spriteBatch);
            }
            base.Draw(gameTime, spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            isMenuOverlayShowing = CheckIfMenuToggled();
            
            if (isMenuOverlayShowing)
            {
                menuOverlay.Update(gameTime);
            }
            else
            {
                base.Update(gameTime);
                physicsHandler.Update(gameTime);
                CameraUpdate(gameTime);
            }
        }

        private void CameraUpdate(GameTime gameTime)
        {
            if (game.GameData.getClientsPlayer() != null)
                Cam.Position = game.GameData.getClientsPlayer().position - Cam.Size/2;

            foreach (var component in components) {
                Tuple<bool, Vector2> result = Cam.CalcScreenCoords(component.Position, component.Size);
                component.onScreen = result.Item1;
                component.ScreenPos = result.Item2;
            }

            foreach (var entity in entities)
            {
                Tuple<bool, Vector2> result = Cam.CalcScreenCoords(entity.Position, entity.Size);
                entity.onScreen = result.Item1;
                entity.ScreenPos = result.Item2;
            }
        }

        public override void LoadContent()
        {
            menuOverlay.LoadContent();
            base.LoadContent();
        }

        public override void UnloadContent()
        {
            menuOverlay.UnloadContent();
            base.UnloadContent();
        }

        private bool CheckIfMenuToggled()
        {
            bool wasOverlayShowing = isMenuOverlayShowing;
            bool isOverlayShowing = wasOverlayShowing;

            KeyboardState keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Escape))
            {
                if (wasEscapePressed == false)
                {
                    logger.Debug("Menu toggled in game screen");
                    isOverlayShowing = !isMenuOverlayShowing;
                    wasEscapePressed = true;
                }
            }
            else
            {
                wasEscapePressed = false;
            }

            return isOverlayShowing;
        }
    }
}
