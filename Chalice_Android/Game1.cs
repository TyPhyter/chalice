using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

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

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = true;
            graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            graphics.ApplyChanges();

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
            boar1._Pos = new Vector2(100, 100);
            boar1._Scale = new Vector2(0.25f, 0.25f);

            Cards = new List<Card>
            {
                boar1
            };

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

                float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

                keyboardState = Keyboard.GetState();

                HandleInputCamera(keyboardState, deltaTime);

                //mapRenderer.Update(map, gameTime);

                base.Update(gameTime);
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            Matrix transformMatrix = _camera.GetViewMatrix();

            spriteBatch.Begin(transformMatrix: _camera.GetViewMatrix(), samplerState: SamplerState.PointClamp);

            spriteBatch.Draw(_background, position: Vector2.Zero);

            Cards.ForEach(card =>
            {
                spriteBatch.Draw(card._Texture, position: card._Pos, scale: card._Scale);
            });

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