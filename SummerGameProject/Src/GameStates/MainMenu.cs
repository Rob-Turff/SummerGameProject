using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SummerGameProject.Src.Components;

namespace SummerGameProject.Src.GameStates
{
    public class MainMenuState : GameState
    {

        public MainMenuState(MainGame mainGame, GraphicsDeviceManager graphics, SpriteFont font) : base (mainGame,graphics)
        {
            SetupScreen();
        }

        private void SetupScreen()
        {
            Texture2D buttonTexture = Content.Load<Texture2D>("UI/button");

            Vector2 playButtonPos = new Vector2(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight / 2 - (float)(buttonTexture.Height * 0.75));
            Vector2 settingsButtonPos = new Vector2(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight / 2 + (float)(buttonTexture.Height * 0.75));

            Button playGameBtn = new Button("Start Game", buttonTexture, playButtonPos, font);
            Button settingsBtn = new Button("Settings", buttonTexture, settingsButtonPos, font);

            playGameBtn.OnClick = new Action(() => game.ChangeState(this));
            settingsBtn.OnClick = new Action(() => game.ChangeState(this));
            components.Add(playGameBtn);
            components.Add(settingsBtn);
        }
    }
}
