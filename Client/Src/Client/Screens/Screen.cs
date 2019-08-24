using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Client.Src.Components;
using System.Collections.Generic;

namespace Client.Src.Screens
{
    /// <summary>
    /// Abstract class to create game states e.g. main menu
    /// </summary>
    public abstract class Screen
    {
        #region Fields

        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public List<Component> Components { get; set; } = new List<Component>();

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
            foreach (var component in Components)
                component.Draw(gameTime, spriteBatch);
            spriteBatch.End();
        }

        public virtual void Update(GameTime gameTime)
        {
            foreach (var component in Components)
                component.Update(gameTime);
        }

        public virtual void LoadContent()
        {
            foreach (var component in Components)
            {
                component.LoadContent();
            }
        }

        public virtual void UnloadContent()
        {
            Content.Unload();
        }

        protected void DistributeVertically(List<Component> listOfComponents)
        {
            bool IsEvenNumber = listOfComponents.Count % 2 == 0;

            if (IsEvenNumber)
            {
                for (int i = 0, length = listOfComponents.Count; i < length; i++)
                {
                    Component ithButton = listOfComponents[i];
                    ithButton.Position = new Vector2(
                        ScreenWidth / 2 - ithButton.Width / 2, // Centre horizontally
                        ScreenHeight / 2 - 1.75f * (ithButton.Height * (length / 2 - i))  // Distribute Vertically
                        );
                }
            }
            else
            {
                for (int i = 0, length = listOfComponents.Count; i < length; i++)
                {
                    Component ithButton = listOfComponents[i];
                    ithButton.Position = new Vector2(
                        ScreenWidth / 2 - ithButton.Width / 2, // Centre horizontally
                        ScreenHeight / 2 - 1.75f * ithButton.Height * ((length - 1) / 2 - i) // Distribute Vertically
                        );
                }
            }

        }

        protected void DistributeHorizontally(List<Component> listOfComponents, float spacing)
        {
            bool IsEvenNumber = listOfComponents.Count % 2 == 0;

            if (IsEvenNumber)
            {
                for (int i = 0, length = listOfComponents.Count; i < length; i++)
                {
                    Component ithButton = listOfComponents[i];
                    ithButton.Position = new Vector2(
                        ScreenWidth / 2 - (1.75f + spacing) * (ithButton.Width * (length / 2 - i)),  // Distribute horizontally
                        ScreenHeight / 2 - ithButton.Height / 2 // Centre Vertically
                        );
                }
            }
            else
            {
                for (int i = 0, length = listOfComponents.Count; i < length; i++)
                {
                    Component ithButton = listOfComponents[i];
                    ithButton.Position = new Vector2(
                        ScreenWidth / 2 - (1.75f + spacing) * ithButton.Width * ((length - 1) / 2 - i), // Distribute horizontally
                        ScreenHeight / 2 - ithButton.Height / 2 // Centre Vertically
                        );
                }
            }
        }
        #endregion
    }
}
