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

        #endregion

        #region Properties

        public ContentManager Content { get; }
        public int ScreenWidth { get; protected set; }
        public int ScreenHeight { get; protected set; }
        public bool IsFullScreen { get; protected set; }
        public SpriteFont Font { get => game.Font; }

        #endregion

        #region Methods

        public Screen(MainGame game)
        {
            this.game = game;
            this.Content = new ContentManager(game.Services, game.Content.RootDirectory);
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            foreach (var component in components)
                component.Draw(gameTime, spriteBatch);
            spriteBatch.End();
        }

        public virtual void Update(GameTime gameTime)
        {
            foreach (var component in components)
                component.Update(gameTime);
        }

        public virtual void LoadContent()
        {
            foreach (var component in components)
            {
                component.LoadContent();
            }
        }

        public virtual void UnloadContent()
        {
            Content.Unload();
        }

        protected void DistributeVertically(List<Button> listOfButtons)
        {
            bool IsEvenNumber = listOfButtons.Count % 2 == 0;

            // Assume all same height and width
            int height = listOfButtons[0].Height;
            int width = listOfButtons[0].Width;

            if (IsEvenNumber)
            {
                for (int i = 0, length = listOfButtons.Count; i < length; i++)
                {
                    Button ithButton = listOfButtons[i];
                    ithButton.ButtonPos = new Vector2(
                        ScreenWidth / 2 - ithButton.Width / 2, // Centre horizontally
                        ScreenHeight / 2 - 2 * (ithButton.Height * (length / 2 - i))  // Distribute Vertically
                        );
                }
            }
            else
            {
                for (int i = 0, length = listOfButtons.Count; i < length; i++)
                {
                    Button ithButton = listOfButtons[i];
                    ithButton.ButtonPos = new Vector2(
                        ScreenWidth / 2 - ithButton.Width / 2, // Centre horizontally
                        ScreenHeight / 2 - 2 * ithButton.Height * ((length - 1) / 2 - i) // Distribute Vertically
                        );
                }
            }

        }

        #endregion
    }
}
