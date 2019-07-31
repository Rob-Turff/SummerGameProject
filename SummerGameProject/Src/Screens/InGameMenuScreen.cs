using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SummerGameProject.Src.Components;

namespace SummerGameProject.Src.Screens
{
    class InGameMenuScreen : Screen
    {
        public InGameMenuScreen(MainGame game,GameScreen gameScreen) : base(game)
        {
            ScreenWidth = gameScreen.ScreenWidth;
            ScreenHeight = gameScreen.ScreenHeight;
            IsFullScreen = gameScreen.IsFullScreen;
        }

        public override void LoadContent()
        {
            Texture2D buttonTexture = Content.Load<Texture2D>("UI/button");

            Vector2 quitGamePos     = new Vector2(ScreenWidth / 2, ScreenHeight / 2 - (float)(buttonTexture.Height * 0.75));
            Vector2 quitToMenuPos   = new Vector2(ScreenWidth / 2, ScreenHeight / 2 + (float)(buttonTexture.Height * 0.75));
            Vector2 SettingsPos     = new Vector2(ScreenWidth / 2, ScreenHeight / 2 + (float)(buttonTexture.Height * 2.25));

            Button quitGameBtn      = new Button("Quit Game", buttonTexture, quitGamePos, game.Font);
            Button quitToMenuBtn    = new Button("Quit to Menu", buttonTexture, quitToMenuPos, game.Font);
            Button settingsBtn      = new Button("Settings", buttonTexture, SettingsPos, game.Font);

            quitGameBtn.OnClick     = new Action(() => game.Exit());
            quitToMenuBtn.OnClick   = new Action(() => game.ScreenManager.ChangeScreen(ScreenManager.ScreenEnum.Menu));
            settingsBtn.OnClick     = new Action(() => game.ScreenManager.ChangeScreen(ScreenManager.ScreenEnum.Setting));

            components.Add(quitGameBtn);
            components.Add(quitToMenuBtn);
            components.Add(settingsBtn);
        }
    }
}
