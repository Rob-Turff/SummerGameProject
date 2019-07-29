using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SummerGameProject.Src.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummerGameProject.Src.Screens
{
    /// <summary>
    /// Abstract class to create game states e.g. main menu
    /// </summary>
    public abstract class Screen
    {
        #region Fields

        protected List<Component> components = new List<Component>();

        protected readonly MainGame game;
        protected readonly GraphicsDeviceManager graphics;
        protected readonly SpriteFont font;

        #endregion

        #region Properties

        public ContentManager Content { get; }

        #endregion

        #region Methods

        public Screen(MainGame game,GraphicsDeviceManager graphics)
        {
            this.game = game;
            this.graphics = graphics;
            this.font = game.Font;
            this.Content = new ContentManager(game.Services,game.Content.RootDirectory);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            foreach (var component in components)
                component.Draw(gameTime, spriteBatch);
            spriteBatch.End();
        }

        public void Update(GameTime gameTime, KeyboardState keyboardState)
        {
            foreach (var component in components)
                component.Update(gameTime);
        }

        #endregion
    }
}
