using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;

using MonoGame.Extended;
using MonoGame.Extended.Tweening;

using Chalice_Android.Entities;

namespace Chalice_Android.Systems
{
    public class InputManager
    {
        TouchCollection tc;
        public Cursor cursor;
        KeyboardState ks;
        bool inputFlag = true;
        Keys trackingKey;
        Card selectedCard; // may not need this since cursor has HeldCard
        int timeHeld;
        Game1 _game;
        

        public InputManager(Game1 game)
        {
            _game = game;
            cursor = new Cursor(_game);
        }

        public void Update(Game1 game, GameTime gameTime)
        {
            UpdateTouch(game, gameTime);

            ks = Keyboard.GetState();

            if (ks.IsKeyDown(Keys.D) && inputFlag)
            {
                inputFlag = false;
                trackingKey = Keys.D;
                game.Player1Hand.AddCards(game.Player1Deck.Deal(1));
            }

            if (ks.IsKeyUp(Keys.D) && trackingKey == Keys.D)
            {
                inputFlag = true;
                trackingKey = Keys.None;
            }

            if (ks.IsKeyDown(Keys.Down))
            {
                game.Player1Hand.rotationOrigin = game.Player1Hand.rotationOrigin + new Vector2(0, 50);
                game.Player1Hand.UpdatePositions();
                Console.WriteLine(game.Player1Hand.rotationOrigin);
            }

            if (ks.IsKeyDown(Keys.Up))
            {
                inputFlag = false;
                game.Player1Hand.rotationOrigin = game.Player1Hand.rotationOrigin + new Vector2(0, -50);
                game.Player1Hand.UpdatePositions();
            }

            if (ks.IsKeyDown(Keys.Left))
            {
                game.Player1Hand.radiansPer = game.Player1Hand.radiansPer - .01f;
                game.Player1Hand.UpdatePositions();
                Console.WriteLine(game.Player1Hand.radiansPer);
            }

            if (ks.IsKeyDown(Keys.Right))
            {
                game.Player1Hand.radiansPer = game.Player1Hand.radiansPer + .01f;
                game.Player1Hand.UpdatePositions();
                Console.WriteLine(game.Player1Hand.radiansPer);
            }

            if (ks.IsKeyDown(Keys.X) && inputFlag)
            {
                inputFlag = false;
                trackingKey = Keys.X;
                game.basicEffectX = game.basicEffectX + 5;
                game.basicEffect.World = Matrix.CreateRotationX(MathHelper.ToRadians(game.basicEffectX)) * Matrix.CreateRotationY(MathHelper.ToRadians(game.basicEffectY)) * Matrix.CreateRotationZ(MathHelper.ToRadians(game.basicEffectZ)) * Matrix.Identity;
            }

            if (ks.IsKeyUp(Keys.X) && trackingKey == Keys.X)
            {
                inputFlag = true;
                trackingKey = Keys.None;
            }

            if (ks.IsKeyDown(Keys.Y) && inputFlag)
            {
                inputFlag = false;
                trackingKey = Keys.Y;
                game.basicEffectY = game.basicEffectY + 5;
                game.basicEffect.World = Matrix.CreateRotationX(MathHelper.ToRadians(game.basicEffectX)) * Matrix.CreateRotationY(MathHelper.ToRadians(game.basicEffectY)) * Matrix.CreateRotationZ(MathHelper.ToRadians(game.basicEffectZ)) * Matrix.Identity;
            }

            if (ks.IsKeyUp(Keys.Y) && trackingKey == Keys.Y)
            {
                inputFlag = true;
                trackingKey = Keys.None;
            }

            if (ks.IsKeyDown(Keys.Z) && inputFlag)
            {
                inputFlag = false;
                trackingKey = Keys.Z;
                game.basicEffectZ = game.basicEffectZ + 5;
                game.basicEffect.World = Matrix.CreateRotationX(MathHelper.ToRadians(game.basicEffectX)) * Matrix.CreateRotationY(MathHelper.ToRadians(game.basicEffectY)) * Matrix.CreateRotationZ(MathHelper.ToRadians(game.basicEffectZ)) * Matrix.Identity;
            }

            if (ks.IsKeyUp(Keys.Z) && trackingKey == Keys.Z)
            {
                inputFlag = true;
                trackingKey = Keys.None;
            }
        }

        public void UpdateTouch(Game1 game, GameTime gameTime)
        {
            tc = TouchPanel.GetState();

            //foreach (TouchLocation tl in tc)
            //{
            //    Console.WriteLine(tl.ToString());
            //}

            TouchLocation touch = tc.FirstOrDefault();

            switch (touch.State)
            {
                case TouchLocationState.Pressed:

                    if(cursor.Status == Cursor.CursorStatus.Zoomed)
                    {
                        cursor.HeldCard.Pos = cursor.PickupPoint.ToVector2();
                        cursor.HeldCard.Scale = new Vector2(0.25f, 0.25f);
                        cursor.Status = Cursor.CursorStatus.Empty;
                        cursor.Active = false;
                        cursor.HeldCard = null;

                        game.Player1Hand.UpdatePositions();
                        return;
                    }

                    game.Player1Hand._CardList.ForEach(card =>
                    {
                        card.ZIndex = 0;
                        
                        
                        //if (touch.Position.X > card.Pos.X && touch.Position.X < card.Pos.X + card.Texture.Width * card.Scale.X
                        //    && touch.Position.Y > card.Pos.Y && touch.Position.Y < card.Pos.Y + card.Texture.Height * card.Scale.Y)
                        if(card.Collider.Contains(touch.Position))
                        {
                            if (!card.wasPlayed)
                            {
                                selectedCard = card;
                                timeHeld = (int)gameTime.TotalGameTime.TotalMilliseconds;

                                // AFRICA: Move this to a routine in Cursor
                                cursor.HeldCard = card;
                                cursor.Active = true;
                                cursor.Status = Cursor.CursorStatus.Staged;
                                cursor.PickupPoint = card.Pos.ToPoint();

                                Cell cell = game.Board.GameGrid.Cells.Find(c => c.Occupant == card);
                                if (cell != null)
                                {
                                    cell.Occupant = null;
                                    cell.isOccupied = false;
                                }
                            }
                        }
                    });

                    if (cursor.HeldCard != null)
                    {
                        cursor.HeldCard.ZIndex = 1;
                        cursor.HeldCard.Rotation3D.Z = 0f;
                        cursor.HeldCard.Pos = cursor.HeldCard.Pos + (Vector2.UnitY * -500);
                        cursor.HeldCard.Scale = new Vector2(0.35f, 0.35f);
                    }
                    else
                    {
                        game.Player1Hand.AddCards(game.Player1Deck.Deal(1));
                    }

                    cursor.Update(touch);

                    Console.WriteLine(touch.Position.ToString());

                    break;

                case TouchLocationState.Moved:

                    cursor.Update(touch);

                    break;

                case TouchLocationState.Released:

                    if (cursor.HeldCard == null || cursor.Status == Cursor.CursorStatus.Zoomed) return;

                    if (gameTime.TotalGameTime.TotalMilliseconds - timeHeld <= 200)
                    {
                        cursor.HeldCard.Pos = new Vector2(game.GraphicsDevice.Viewport.Width / 2, game.GraphicsDevice.Viewport.Height / 2);
                        cursor.HeldCard.Scale = Vector2.One;
                        cursor.HeldCard.ZIndex = 1;
                        cursor.Status = Cursor.CursorStatus.Zoomed;
                        return;
                    }

                    game.Board.GameGrid.Cells.ForEach(cell =>
                    {
                        if (cell.Rectangle.Contains(touch.Position.ToPoint()))
                        {
                            if (!cell.isOccupied && cursor.HeldCard != null && cursor.HeldCard._CardType == CardType.Minion)
                            {
                                // AFRICA: Move this stuff to routines in their associated classes
                                cursor.HeldCard.Pos = cell.Rectangle.Location.ToVector2() + new Vector2(cell.Rectangle.Width / 2, cell.Rectangle.Height / 2);
                                _game.tweener.TweenTo(cursor.HeldCard, c => c.Scale, Vector2.One * 0.25f, 0.1f, 0f);
                                cursor.HeldCard.wasPlayed = true;
                                cell.isOccupied = true;
                                cell.Occupant = cursor.HeldCard;
                                cursor.Status = Cursor.CursorStatus.Empty;
                                cursor.Active = false;
                                cursor.HeldCard = null;
                                game.Player1Hand.RemoveCard(selectedCard);
                                selectedCard = null;
                            }
                            else
                            {
                                // check artifact stuff
                            }
                        }
                    });

                    if (cursor.Active)
                    {
                        cursor.HeldCard.Pos = cursor.PickupPoint.ToVector2();
                        _game.tweener.TweenTo(cursor.HeldCard, c => c.Scale, Vector2.One * 0.25f, 0.1f, 0f);
                        cursor.Status = Cursor.CursorStatus.Empty;
                        cursor.Active = false;
                        cursor.HeldCard = null;

                        game.Player1Hand.UpdatePositions();
                    }

                    cursor.Update(touch);

                    break;

                default: break;
            }
        }
    }

    public class Cursor
    {
        public Game game;
        public Vector2 Position;
        public Vector2 PrevPosition;
        public Point PickupPoint;
        public bool Active;
        public Card HeldCard;
        public CursorStatus Status;
        Game1 _game;

        public Cursor(Game1 game)
        {
            _game = game;
        }

        public void Update(TouchLocation tc)
        {
            PrevPosition = Position;
            Position = tc.Position;

            Vector2 velocity = new Vector2(Math.Min(Math.Abs(Position.X - PrevPosition.X), 30), Math.Min(Math.Abs(Position.Y - PrevPosition.Y), 30));

            if (HeldCard != null && Position.Y <= HeldCard.Pos.Y && Status != CursorStatus.Grabbed)
            {
                Status = CursorStatus.Grabbed;
                HeldCard.Scale = new Vector2(0.25f, 0.25f);
            }
            
            if (HeldCard != null && Status == CursorStatus.Grabbed)
            {
                HeldCard.Pos = Position;
                
                _game.tweener.TweenTo(HeldCard, c => c.Scale, (Vector2.One * 0.25f) - (velocity * .0025f), 0.1f, 0f);
     
            }
        }

        public void Render(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(HeldCard.Texture, HeldCard.Pos, null, Color.White, 0f, Vector2.Zero, HeldCard.Scale, SpriteEffects.None, 0f);
        }

        public enum CursorStatus
        {
            Empty,
            Staged,
            Grabbed,
            Zoomed
        }
    }
}