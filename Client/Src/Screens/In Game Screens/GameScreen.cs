using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Client.Src.Components.Player;
using Client.Src.Components;
using System.Collections.Generic;
using Common.Src.Packets;
using System;
using Common.Src.Packets.ServerToClient;
using Common.Src.Entities;
using Client.Src.Components.Game_Components;

namespace Client.Src.Screens
{
    class GameScreen : Screen
    {
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private readonly ClientMatch clientMatch;
        private readonly List<DrawableGameObject> drawableGameObjects;
        private readonly InGameMenuScreen menuOverlay;

        private bool wasEscapePressed = false;
        private bool isMenuOverlayShowing = false;

        public GameScreen(Game1 game, ClientMatch clientMatch) : base(game)
        {
            ScreenWidth = 1920;
            ScreenHeight = 1080;

            this.clientMatch = clientMatch;
            this.drawableGameObjects = new List<DrawableGameObject>();

            clientMatch.DrawableEntityHasBeenAdded += ClientMatch_EntityHasBeenAdded;

            foreach (IDrawableEntity drawableEntity in clientMatch.DrawableEntities)
            {
                drawableGameObjects.Add(new DrawableGameObject(drawableEntity, this));
            }



            menuOverlay = new InGameMenuScreen(game, this);
        }

        private void ClientMatch_EntityHasBeenAdded(object sender, IDrawableEntity drawableEntity)
        {
            drawableGameObjects.Add(new DrawableGameObject(drawableEntity, this));
        }

        public void SetupGame()
        {
            //if (game.GameData.isMultiplayer)
            //{
            //    foreach (PlayerAttributes pa in game.GameData.players)
            //    {
            //        Components.Add(new Player(pa, this, game));
            //    }
            //}
            //else
            //{
            //    PlayerAttributes playerAttributes = new PlayerAttributes("Player 1", new System.Guid(), true);
            //    playerAttributes.position = new Vector2(ScreenWidth / 2, ScreenHeight / 2);
            //    Player player = new Player(playerAttributes, this, game);
            //    Components.Add(player);
            //}

            //LoadContent();
        }


        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (isMenuOverlayShowing)
            {
                menuOverlay.Draw(gameTime, spriteBatch);
            }

            spriteBatch.Begin();
            foreach (DrawableGameObject drawableGameObject in drawableGameObjects)
            {
                spriteBatch.Draw(drawableGameObject.Texture, drawableGameObject.Position, null, Color.White, 0f, Vector2.Zero, drawableGameObject.Scale, SpriteEffects.None, 0f);
            }
            spriteBatch.End();
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
            }

            clientMatch.Update(gameTime);
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
                    isOverlayShowing = !wasOverlayShowing;
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
