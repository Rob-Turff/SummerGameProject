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
    class PlayerSprite : Sprite
    {
        private float horizontalMovement;
        private bool attemptJump;

        public Vector2 Velocity { get; }

        public Vector2 Acceleration { get; }

        public PlayerSprite(Texture2D texture, Vector2 position) : base(texture, position)
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
    }
}
