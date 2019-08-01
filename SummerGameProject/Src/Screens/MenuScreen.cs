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

namespace SummerGameProject.Src.Screens
{
    public class MenuScreen : Screen
    {

        public MenuScreen(MainGame mainGame, GraphicsDeviceManager graphics) : base (mainGame,graphics)
        {

        }

        public override void LoadContent()
        {
            Texture2D buttonTexture = Content.Load<Texture2D>("UI/button");

            Vector2 playButtonPos = new Vector2(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight / 2 - (float)(buttonTexture.Height * 0.75));
            Vector2 settingsButtonPos = new Vector2(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight / 2 + (float)(buttonTexture.Height * 0.75));

            Button playGameBtn = new Button("Start Game", buttonTexture, playButtonPos, game.Font);
            Button settingsBtn = new Button("Settings", buttonTexture, settingsButtonPos, game.Font);

            playGameBtn.OnClick = new Action(() => game.ScreenManager.ChangeScreen(ScreenManager.ScreenEnum.Game));
            settingsBtn.OnClick = new Action(() => game.ScreenManager.ChangeScreen(ScreenManager.ScreenEnum.Setting));
            Components.Add(playGameBtn);
            Components.Add(settingsBtn);
        }
    }
}
