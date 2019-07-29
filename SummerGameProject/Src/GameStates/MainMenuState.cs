﻿using System;
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
        private MainGame game;
        private GraphicsDeviceManager graphics;
        private SpriteFont font;
        private ContentManager Content;
        private List<Component> components = new List<Component>();

        public MainMenuState(MainGame mainGame, GraphicsDeviceManager graphics, SpriteFont font, ContentManager Content)
        {
            this.game = mainGame;
            this.graphics = graphics;
            this.font = font;
            this.Content = Content;
            setupScreen();
        }

        private void setupScreen()
        {
            Texture2D buttonTexture = Content.Load<Texture2D>("UI/button");
            Button startGameBtn = new Button("Start Game", buttonTexture, 300, 300, font);
            startGameBtn.OnClick = new Action(() => game.changeState(this));
            components.Add(startGameBtn);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            foreach (var component in components)
                component.Draw(gameTime, spriteBatch);
            spriteBatch.End();
        }

        public override void Update(GameTime gameTime, KeyboardState keyboardState)
        {
            foreach (var component in components)
                component.Update(gameTime, keyboardState);
        }
    }
}
