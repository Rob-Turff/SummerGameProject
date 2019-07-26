using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SummerGameProject.Src.Components;

namespace SummerGameProject.Src.GameStates
{
    public class MainMenuState : State
    {
        private MainGameClass game;
        private GraphicsDeviceManager graphics;
        private SpriteFont font;
        private ContentManager Content;
        private List<Component> components = new List<Component>();

        public MainMenuState(MainGameClass mainGameClass, GraphicsDeviceManager graphics, SpriteFont font, ContentManager Content)
        {
            this.game = mainGameClass;
            this.graphics = graphics;
            this.font = font;
            this.Content = Content;
            setupScreen();
        }

        private void setupScreen()
        {
            Texture2D buttonTexture = Content.Load<Texture2D>("UI/button");
            Button startGameBtn = new Button("Start Game", buttonTexture, 300, 300, font);
            components.Add(startGameBtn);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            foreach (var component in components)
                component.Draw(gameTime, spriteBatch);
            spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var component in components)
                component.Update(gameTime);
        }
    }
}
