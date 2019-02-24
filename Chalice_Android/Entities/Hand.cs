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
        public Vector2 rotationOrigin;
        public int TotalCardsThisGame = 0;

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
            for(int i = 0; i < cards.Count; i++)
            {
                cards[i].HandId = TotalCardsThisGame;
                TotalCardsThisGame++;
            }
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
            if (_CardList.Count == 0) return;

            if (_CardList.Count == 1)
            {
                _CardList.First().Pos = Position;
                _CardList.First().Rotation3D.Z = 0f;
                return;
            }

            float radiansPer = .25f;

            float startingRotation = -1 * ((_CardList.Count - 1) * radiansPer / 2);
            
            int radius = (int)(rotationOrigin.Y - Position.Y);

            _CardList = _CardList.OrderBy(c => c.HandId).ToList();

            for (int i = 0; i < _CardList.Count; i++)
            {
                float zRotation = startingRotation + (radiansPer * i);

                float cardX = radius * (float)Math.Sin(zRotation) + rotationOrigin.X;

                float cardY = rotationOrigin.Y - radius * (float)Math.Cos(2f * (float)Math.PI + zRotation); // subtracting because the y axis is inverted/in the fourth quadrant

                _CardList[i].Pos = new Vector2(cardX, cardY);
                _CardList[i].ZIndex = i;
                _CardList[i].Rotation3D = new Vector3(0, 0, zRotation);
            }
        }
    }
}