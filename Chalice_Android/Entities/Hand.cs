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
            float radiansPer = _CardList.Count > 1 ? .5f / (_CardList.Count - 1) : 0;
            float startingRotation = _CardList.Count > 1 ? -0.25f : 0f;
            //int startingX = (int)Position.X - 600;
            //int startingY = (int)Position.Y - 200;
            int radius = (int)(rotationOrigin.Y - Position.Y);
            for (int i = 0; i < _CardList.Count; i++)
            {
                float zRotation = startingRotation + (radiansPer * i);

                float cardX = radius * (float)Math.Sin(zRotation) + rotationOrigin.X;

                //float cosinOp = (float)Math.Cos(2f * (float)Math.PI + zRotation);

                float cardY = rotationOrigin.Y - radius * (float)Math.Cos(2f * (float)Math.PI + zRotation); // subtracting because the y axis is inverted/in the fourth quadrant

                _CardList[i].Pos = new Vector2(cardX, cardY);
                _CardList[i].ZIndex = i;
                _CardList[i].Rotation3D = new Vector3(0, 0, zRotation);
            }
        }
    }
}