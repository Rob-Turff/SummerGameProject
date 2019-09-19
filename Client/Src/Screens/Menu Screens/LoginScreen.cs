using Microsoft.Xna.Framework;
using Client.Src.Components;
using Client.Src.Screens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Src;

namespace Client.Src.Screens
{
    internal class LoginScreen : Screen
    {
        private TextInputBox playerNameBox;

        public LoginScreen(Game1 game) : base(game)
        {
            ScreenWidth = 500;
            ScreenHeight = 150;
            IsFullScreen = false;

            playerNameBox = new TextInputBox("Enter Your Player Name", new Vector2(0, 0), this, game);
            playerNameBox.Position = new Vector2(50, 55);

            Action loginBtnAction = new Action(() => HandleLogin());
            Button loginButton = new Button("Login", new Vector2(0, 0), loginBtnAction, this);
            loginButton.Position = new Vector2(300, 57);

            UIComponents.Add(playerNameBox);
            UIComponents.Add(loginButton);
        }

        private void HandleLogin()
        {
            Game.Player = new Player(playerNameBox.EnteredText, Guid.NewGuid());
            Game.ScreenManager.ChangeToSavedScreen<MenuScreen>();
        }
    }
}
