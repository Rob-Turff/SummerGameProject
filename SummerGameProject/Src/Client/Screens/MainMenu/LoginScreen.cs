using Microsoft.Xna.Framework;
using SummerGameProject.Src.Client.Components;
using SummerGameProject.Src.Components;
using SummerGameProject.Src.Screens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummerGameProject.Src.Client.Screens
{
    public class LoginScreen : Screen
    {
        private TextInputBox playerNameBox;

        public LoginScreen(MainGame game, bool useResScaling) : base(game, useResScaling)
        {
            ScreenWidth = 500;
            ScreenHeight = 150;
            IsFullScreen = false;

            playerNameBox = new TextInputBox("Enter Your Player Name", new Vector2(0, 0), this, game);
            playerNameBox.Position = new Vector2(50, 55);

            Action loginBtnAction = new Action(() => handleLogin());
            Button loginButton = new Button("Login", new Vector2(0, 0), loginBtnAction, this);
            loginButton.Position = new Vector2(300, 57);

            components.Add(playerNameBox);
            components.Add(loginButton);
        }

        public void handleLogin()
        {
            game.GameData.PlayerName = playerNameBox.enteredText;
            game.ScreenManager.ChangeScreen(ScreenManager.ScreenEnum.MENU);
        }
    }
}
