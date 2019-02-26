using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

using Xamarin; 
using Xamarin.Android;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Content;

using MonoGame.Extended;
using MonoGame.Extended.TextureAtlases;
//using MonoGame.Extended.Tiled;
using MonoGame.Extended.ViewportAdapters;
using MonoGame.Extended.Tweening;

using Chalice_Android.Entities;
using Chalice_Android.Components;
using Chalice_Android.Cards;
using Chalice_Android.Systems;


namespace Chalice_Android
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        InputManager inputManager;
        public Tweener tweener;

        public BasicEffect basicEffect;
        public int basicEffectX = 0;
        public int basicEffectY = 0;
        public int basicEffectZ = 0;

        KeyboardState keyboardState;
        private int camSpeed = 200;

        Texture2D _background;
        public GameBoard Board;
        public Deck Player1Deck;
        public Hand Player1Hand;

        List<IRenderable> renderables;
        public List<Card> cards;
        public Vector2 rotationOrigin;
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
                inputManager = new InputManager(this);
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

            tweener = new Tweener();

            renderables = new List<IRenderable>();

            cards = new List<Card>();

            Board = new GameBoard();
            
            Player1Deck = new Deck(new List<Card>
            {
                new Boar(Content),
                new Boar(Content),
                new Boar(Content),
                new Boar(Content),
                new Boar(Content),
                new Boar(Content),
                new Boar(Content),
                new Boar(Content),
                new Boar(Content),
                new Possessed_Neophyte(Content),
                new Possessed_Neophyte(Content),
                new Possessed_Neophyte(Content),
                new Possessed_Neophyte(Content),
                new Possessed_Neophyte(Content)
            });

            Player1Deck.Shuffle(); // make shuffle take iterations as param

            cards.AddRange(Player1Deck._CardList);

            renderables.AddRange(Player1Deck._CardList);

            Player1Deck._CardList.ForEach(c => Console.WriteLine(c.Name));

            Player1Hand = new Hand();
            Player1Hand.Position = new Vector2((graphics.GraphicsDevice.Viewport.Width / 2f), graphics.GraphicsDevice.Viewport.Height - 350);
            Player1Hand.rotationOrigin = Player1Hand.Position + (Vector2.UnitY * 1600);
            rotationOrigin = Player1Hand.Position;
            Player1Hand.AddCards(Player1Deck.Deal(3));

            basicEffect = new BasicEffect(GraphicsDevice)
            {
                TextureEnabled = true,
                VertexColorEnabled = true,
            };

            Viewport viewport = GraphicsDevice.Viewport;

            Matrix projection = Matrix.CreateOrthographicOffCenter(0, viewport.Width, viewport.Height, 0, 0, 1);
            Matrix halfPixelOffset = Matrix.CreateTranslation(-0.5f, -0.5f, 0);

            basicEffect.World = Matrix.Identity * Matrix.CreateTranslation(0, 0, -0.5f);
            basicEffect.View = Matrix.Identity;
            basicEffect.Projection = halfPixelOffset * projection;

            basicEffect.TextureEnabled = true;
            basicEffect.VertexColorEnabled = true;

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

                inputManager.Update(this, gameTime);

                tweener.Update(gameTime.GetElapsedSeconds());

                cards.ForEach(card => card.Update(gameTime.GetElapsedSeconds()));

                base.Update(gameTime);
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
                //move this into the GameBoard and render it there
                spriteBatch.Draw(_background, Vector2.Zero, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                Board.GameGrid.Cells.Where(c => c.Occupant != null).Select(c => c.Occupant).OrderBy(r => r.ZIndex).ToList().ForEach(r => { if (r.isActive) r.Render(spriteBatch, rotationOrigin); });
            //renderables.OrderBy(r => r.ZIndex).ToList().ForEach(r => { if (r.isActive) r.Render(spriteBatch, rotationOrigin); } );

            spriteBatch.End();

            spriteBatch.Begin(0, null, null, null, null, basicEffect);
            
                Player1Hand._CardList.OrderBy(r => r.ZIndex).ThenBy(r => r.HandId).ToList().ForEach(r => { if (r.isActive) r.Render(spriteBatch, rotationOrigin); });
                

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}