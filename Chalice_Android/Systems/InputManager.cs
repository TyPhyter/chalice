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
        Cursor cursor;

        public InputManager()
        {
            cursor = new Cursor();
        }

        public void Update(Game1 game)
        {
            tc = TouchPanel.GetState();

            //foreach (TouchLocation tl in tc)
            //{
            //    Console.WriteLine(tl.ToString());
            //}

            TouchLocation touch = tc.FirstOrDefault();

            switch(touch.State)
            {
                case TouchLocationState.Pressed:

                    game.Player1Hand._CardList.ForEach(card =>
                    {
                        card.ZIndex = 0;

                        float cardWidth = card.Texture.Width * card.Scale.X;
                        float cardHeight = card.Texture.Height * card.Scale.Y;
                        if (touch.Position.X > card.Pos.X && touch.Position.X < card.Pos.X + card.Texture.Width * card.Scale.X
                            && touch.Position.Y > card.Pos.Y && touch.Position.Y < card.Pos.Y + card.Texture.Height * card.Scale.Y)
                        {
                            cursor.HeldCard = card;
                            cursor.Active = true;
                            card.ZIndex = 1;
                        }
                    });

                    if(cursor.Active)
                    {
                        game.Player1Hand._CardList = game.Player1Hand._CardList.OrderBy(c => c.ZIndex).ToList();
                    }

                    cursor.Update(touch);

                    break;

                case TouchLocationState.Moved:

                    cursor.Update(touch);

                    break;

                case TouchLocationState.Released:

                    cursor.Active = false;
                    cursor.HeldCard = null;
                    cursor.Update(touch);

                    break;

                default: break;
            }
            
        }
    }

    public class Cursor
    {
        public Vector2 Position;
        public bool Active;
        public Card HeldCard;

        public void Update(TouchLocation tc)
        {
            Position = tc.Position;

            if(HeldCard != null) HeldCard.Pos = new Vector2(Position.X - (HeldCard.Texture.Width / 2) * HeldCard.Scale.X, Position.Y - (HeldCard.Texture.Height / 2) * HeldCard.Scale.Y);
        }
    }
}