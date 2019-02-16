using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Chalice_Android.Entities
{
    public class Hand
    {
        public List<Card> _CardList;
        public Vector2 Position;

        public Hand()
        {
            _CardList = new List<Card>();
        }

        public Hand(List<Card> list)
        {
            _CardList = list;
            UpdatePositions();
        }

        public void Render (SpriteBatch sb)
        {
            _CardList.ForEach(card =>
            {
                sb.Draw(card.Texture, card.Pos, null, Color.White, 0f, Vector2.Zero, card.Scale.X, SpriteEffects.None, 0f);
            });
        }

        public void AddCards (List<Card> cards)
        {
            _CardList.AddRange(cards);
            UpdatePositions();
        }

        public void RemoveCards (List<Card> cards)
        {
            cards.ForEach(card =>
            {
                _CardList.Remove(card);
            });
            UpdatePositions();
        }

        public void RemoveCard (Card card)
        {
            _CardList.Remove(card);
            UpdatePositions();
        }

        public void UpdatePositions()
        {
            for (int i = 0; i < _CardList.Count; i++)
            {
                _CardList[i].Pos = new Vector2(Position.X + (i * 150), Position.Y);
            }
        }
    }
}