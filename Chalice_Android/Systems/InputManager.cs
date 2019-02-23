using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;

using Chalice_Android.Entities;

namespace Chalice_Android.Systems
{
    public class InputManager
    {
        TouchCollection tc;
        public Cursor cursor;
        KeyboardState ks;
        bool inputFlag = true;
        Card selectedCard;

        public InputManager()
        {
            cursor = new Cursor();
        }

        public void Update(Game1 game)
        {
            UpdateTouch(game);

            ks = Keyboard.GetState();

            if (ks.IsKeyDown(Keys.D) && inputFlag)
            {
                inputFlag = false;
                game.Player1Hand.AddCards(game.Player1Deck.Deal(1));
            }

            if (ks.IsKeyUp(Keys.D))
            {
                inputFlag = true;
            }

            if (ks.IsKeyDown(Keys.Down))
            {
                inputFlag = false;
                game.rotationOrigin = game.rotationOrigin + new Vector2(0, 50);
            }

            if (ks.IsKeyDown(Keys.Up))
            {
                inputFlag = false;
                game.rotationOrigin = game.rotationOrigin + new Vector2(0, -50);
            }
        }

        public void UpdateTouch(Game1 game)
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

                    game.cards.ForEach(card =>
                    {
                        card.ZIndex = 0;
                        
                        
                        //if (touch.Position.X > card.Pos.X && touch.Position.X < card.Pos.X + card.Texture.Width * card.Scale.X
                        //    && touch.Position.Y > card.Pos.Y && touch.Position.Y < card.Pos.Y + card.Texture.Height * card.Scale.Y)
                        if(card.Collider.Contains(touch.Position))
                        {
                            if (!card.wasPlayed)
                            {
                                card.wasPlayed = true;
                                selectedCard = card;
                            }

                            cursor.HeldCard = card;
                            cursor.Active = true;
                            cursor.PickupPoint = card.Pos.ToPoint();
                            card.ZIndex = 1;
                            Cell cell = game.Board.GameGrid.Cells.Find(c => c.Occupant == card);
                            if (cell != null)
                            {
                                cell.Occupant = null;
                                cell.isOccupied = false;
                            }
                        }
                    });

                    if (selectedCard != null)
                    {
                        game.Player1Hand.RemoveCard(selectedCard);
                        selectedCard = null;
                    }

                    if (cursor.Active)
                    {
                        game.Player1Hand._CardList = game.Player1Hand._CardList.OrderBy(c => c.ZIndex).ToList();
                    }

                    cursor.Update(touch);

                    Console.WriteLine(touch.Position.ToString());

                    break;

                case TouchLocationState.Moved:

                    cursor.Update(touch);

                    break;

                case TouchLocationState.Released:

                    game.Board.GameGrid.Cells.ForEach(cell =>
                    {
                        if (cell.Rectangle.Contains(touch.Position.ToPoint()))
                        {
                            if (!cell.isOccupied && cursor.HeldCard != null && cursor.HeldCard._CardType == CardType.Minion)
                            {
                                cursor.HeldCard.Pos = cell.Rectangle.Location.ToVector2();
                                cell.isOccupied = true;
                                cell.Occupant = cursor.HeldCard;
                                cursor.Active = false;
                                cursor.HeldCard = null;
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
                        cursor.Active = false;
                        cursor.HeldCard = null;
                    }


                    cursor.Update(touch);

                    break;

                default: break;
            }
        }
    }

    public class Cursor
    {
        public Vector2 Position;
        public Point PickupPoint;
        public bool Active;
        public Card HeldCard;

        public void Update(TouchLocation tc)
        {
            Position = tc.Position;

            if(HeldCard != null) HeldCard.Pos = new Vector2(Position.X - (HeldCard.Texture.Width / 2) * HeldCard.Scale.X, Position.Y - (HeldCard.Texture.Height / 2) * HeldCard.Scale.Y);
        }

        public void Render(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(HeldCard.Texture, HeldCard.Pos, null, Color.White, 0f, Vector2.Zero, HeldCard.Scale, SpriteEffects.None, 0f);
        }
    }
}