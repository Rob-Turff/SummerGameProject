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
            Button startGameBtn = new Button("Start Game", buttonTexture, 300, 300, font)
            {
                OnClick = new Action(() => game.ChangeState(this))//TODO change "this" to PlayState 
            };
            components.Add(startGameBtn);
        }
    }
}
