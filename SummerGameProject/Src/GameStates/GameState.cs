using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummerGameProject.Src.GameStates
{
    /// <summary>
    /// Abstract class to create game states e.g. main menu
    /// </summary>
    public abstract class GameState
    {
        #region Fields

        protected MainGame game;

        #endregion

        #region Properties 

        public ContentManager Content { get; }

        #endregion

        #region Methods

        public void GameState()
        {
            this.Content = new ContentManager()
        }

        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);

        public abstract void Update(GameTime gameTime, KeyboardState keyboardState);
        
        #endregion
    }
}
