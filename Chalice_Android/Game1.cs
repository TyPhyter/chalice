using System;
using System.Collections.Generic;

using Xamarin; 
using Xamarin.Android;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Content;

using MonoGame.Extended;
using MonoGame.Extended.Animations;
using MonoGame.Extended.Animations.SpriteSheets;
using MonoGame.Extended.TextureAtlases;
//using MonoGame.Extended.Tiled;
using MonoGame.Extended.ViewportAdapters;

using Chalice_Android.Entities;
using Chalice_Android.Components;
using Chalice_Android.Cards;


namespace Chalice_Android
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private Camera2D _camera;

        KeyboardState keyboardState;
        private int camSpeed = 200;

        Texture2D _background;
        List<Card> Cards;

        Deck Player1Deck;
        Hand Player1Hand;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = true;
            graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            graphics.ApplyChanges();

            if(TouchPanel.GetCapabilities().IsConnected)
            {
                Console.WriteLine("**********TOUCH INPUT CONNECTED**********");
            }  

            Content.RootDirectory = "Content";
        }

        protected override void OnActivated(object sender, EventArgs args)
        {
            Window.Title = "Chalice";
            base.OnActivated(sender, args);
        }

        protected override void OnDeactivated(object sender, EventArgs args)
        {
            Window.Title = "Chalice (PAUSED)";
            base.OnDeactivated(sender, args);
        }

        protected override void Initialize()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Minion boar1 = new Boar(Content);
            boar1.Pos = new Vector2(100, 100);
            boar1.Scale = new Vector2(0.25f, 0.25f);

            Cards = new List<Card>
            {
                boar1
            };

            Player1Deck = new Deck(new List<Card>
            {
                new Boar(Content),
                new Boar(Content),
                new Boar(Content),
                new Boar(Content),
                new Boar(Content),
                //new Minion2(Content),
                //new Minion2(Content),
                //new Minion2(Content),
                //new Minion2(Content),
                //new Minion2(Content)
            });

            Player1Deck.Shuffle(); // make shuffle take iterations as param

            Player1Deck._CardList.ForEach(c => Console.WriteLine(c.Name));

            Player1Hand = new Hand();
            Player1Hand.Position = new Vector2(100, 100);
            Player1Hand.AddCards(Player1Deck.Deal(5));

            var viewportAdapter = new BoxingViewportAdapter(Window, GraphicsDevice, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
            _camera = new Camera2D(viewportAdapter);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            //should probably move texture to a component and load it in a system here
            _background = Content.Load<Texture2D>("board_skeleton");
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
            //brick.Texture.Dispose();
        }

        protected override void Update(GameTime gameTime)
        {
            if (IsActive)
            {
                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                    Exit();

                //float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

                //keyboardState = Keyboard.GetState();

                //HandleInputCamera(keyboardState, deltaTime);

                TouchCollection tc = TouchPanel.GetState();
                foreach (TouchLocation tl in tc)
                {
                    Console.WriteLine(tl.ToString());
                    Cards[0].Pos = tl.Position;
                }

                base.Update(gameTime);
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            Matrix transformMatrix = _camera.GetViewMatrix();

            spriteBatch.Begin(transformMatrix: _camera.GetViewMatrix(), samplerState: SamplerState.PointClamp);

                spriteBatch.Draw(_background, Vector2.Zero, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);

                Player1Hand.Render(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }

        public bool HandleInputCamera(KeyboardState keyboard, float deltaTime)
        {

            if (keyboardState.IsKeyDown(Keys.W))
            {
                _camera.Move(new Vector2(0, -camSpeed * deltaTime));
            }
            if (keyboardState.IsKeyDown(Keys.S))
            {
                _camera.Move(new Vector2(0, camSpeed * deltaTime));
            }
            if (keyboardState.IsKeyDown(Keys.A))
            {
                _camera.Move(new Vector2(-camSpeed * deltaTime, 0));
            }
            if (keyboardState.IsKeyDown(Keys.D))
            {
                _camera.Move(new Vector2(camSpeed * deltaTime, 0));
            }
            if (keyboardState.IsKeyDown(Keys.E))
            {
                _camera.ZoomIn(1 * deltaTime);
            }
            if (keyboardState.IsKeyDown(Keys.Q))
            {
                _camera.ZoomOut(1 * deltaTime);
            }
            return true;
        }
    }
}