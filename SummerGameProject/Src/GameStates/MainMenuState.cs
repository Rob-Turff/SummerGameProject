using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SummerGameProject.Src.Components;

namespace SummerGameProject.Src.GameStates
{
    public class MainMenuState : State
    {
        private MainGameClass game;
        private GraphicsDeviceManager graphics;
        private List<Component> components;

        public MainMenuState(MainGameClass mainGameClass, GraphicsDeviceManager graphics)
        {
            this.game = mainGameClass;
            this.graphics = graphics;
            components = new List<Component>();
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
