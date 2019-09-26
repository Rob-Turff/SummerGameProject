using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using SummerGameProject.Src.Common.Message;
using SummerGameProject.Src.Screens;
using SummerGameProject.Src.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummerGameProject.Src.Client.Components.Player
{
    public class PlayerInputHandler
    {
        private enum AbilityType { Fireball};
        private PlayerStats playerStats;
        private MouseState oldMouseState;
        private MouseState mouseState;
        // TODO let player change this
        private AbilityType currentAbility = AbilityType.Fireball;

        private Vector2 Position { get { return playerStats.position; } set { playerStats.position = value; } }
        private readonly GameScreen screen;
        private readonly AnimationHandler animationHandler;
        private readonly MainGame game;

        public PlayerInputHandler(GameScreen screen, PlayerStats playerAttributes, AnimationHandler animationHandler, MainGame game)
        {
            this.playerStats = playerAttributes;
            this.screen = screen;
            this.animationHandler = animationHandler;
            this.game = game;
        }

        internal void Update(GameTime gameTime)
        {
            if (playerStats.playerID == game.GameData.clientsPlayerID)
                HandleInput();
        }

        private void HandleInput()
        {
            PlayerMove playerMove = new PlayerMove(Position.X, Position.Y);

            KeyboardState keyboardState = Keyboard.GetState();
            oldMouseState = mouseState;
            mouseState = Mouse.GetState();

            if (keyboardState.IsKeyDown(Keys.A))
            {
                animationHandler.Play();
                playerMove.movingLeft = true;
            }
            else if (keyboardState.IsKeyDown(Keys.D))
            {
                animationHandler.Play();
                playerMove.movingRight = true;
            }
            else
                animationHandler.Stop();

            if (keyboardState.IsKeyDown(Keys.W) || keyboardState.IsKeyDown(Keys.Space))
            {
                playerMove.jumping = true;
                // TODO Play jumping animation
            }

            if (mouseState.LeftButton == ButtonState.Pressed && oldMouseState.LeftButton == ButtonState.Released)
            {
                switch (currentAbility)
                {
                    case AbilityType.Fireball:
                        screen.entities.Add(new Fireball(screen, playerStats, mouseState));
                        break;
                }
            }

            if (!playerMove.isSameDirection(playerStats.currentMove) && game.GameData.isMultiplayer)
            {
                game.networkHandler.sendMessage(new PlayerMoveMessage(playerMove, playerStats.playerID));
            }

            playerStats.currentMove = playerMove;
        }
    }
}
