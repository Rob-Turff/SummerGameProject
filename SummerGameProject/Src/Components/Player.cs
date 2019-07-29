using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SummerGameProject.Src.Components.Sprites
{
    class Player : Component
    {
        private float horizontalMovement;
        private bool attemptJump;

        public Vector2 Velocity { get; }

        public Vector2 Acceleration { get; }

        public Player(Vector2 position)
        {
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();
            HandleInput(keyboardState);
        }

        private void HandleInput(KeyboardState keyboardState)
        {
            if (keyboardState.IsKeyDown(Keys.A))
                horizontalMovement = 1f;
            else if (keyboardState.IsKeyDown(Keys.D))
                horizontalMovement = 1f;

            if (keyboardState.IsKeyDown(Keys.W) || keyboardState.IsKeyDown(Keys.Space))
                attemptJump = true;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            throw new NotImplementedException();
        }
    }
}
