using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Client.Src.Utilities;
using Client.Src.Screens;
using Lidgren.Network;
using System;
using Common.Src;
using ServerFacadeNS;

namespace Client
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        private SpriteBatch spriteBatch;

        internal SpriteFont FontRegular { get; private set; }
        internal SpriteFont FontBold { get; private set; }
        internal Player Player { get; set; }
        internal ScreenManager ScreenManager { get; }

        public Game1()
        {
            GraphicsDeviceManager graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            IsMouseVisible = true;

            ScreenManager = new ScreenManager(this, graphics);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            FontRegular = Content.Load<SpriteFont>("UI/ArialRegular");
            FontBold = Content.Load<SpriteFont>("UI/ArialBold");

            ScreenManager.CurrentScreen.LoadContent();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here

            ScreenManager.CurrentScreen.UnloadContent();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            ScreenManager.CurrentScreen.Update(gameTime);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            ScreenManager.CurrentScreen.Draw(gameTime, spriteBatch);

            base.Draw(gameTime);
        }

        //internal void HostAndJoinServer()
        //{
        //    serverFacade.CreateAndStartServer();
        //    JoinServer("localhost", NetworkSettings.Port);
        //}

        //internal void JoinServer(string host, int port)
        //{
        //    NetClient.Start();
        //    NetOutgoingMessage netOutgoingMessage = NetClient.CreateMessage();

        //    NetClient.Connect(host, port,);
        //}
    }
}
