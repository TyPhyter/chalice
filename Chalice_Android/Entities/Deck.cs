using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chalice_Android.Entities
{
    public class Deck
    {
        public List<Card> _CardList { get; private set; }
        public int InitialSize;

        public Deck(List<Card> list)
        {
            _CardList = list;
            InitialSize = list.Count;
        }

        private static Random rng = new Random();

        public void Shuffle()
        {
            List<Card> list = _CardList;

            // Fisher-Yates shuffle
            int n = list.Count;

            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                Card value = list[k];
                list[k] = list[n];
                list[n] = value;
                // store index in deck for lookup later
                list[n]._InitialIndex = list.Count - n; 
            }

            _CardList = list;
        }

        public List<Card> Deal(int quantity)
        {
            List<Card> outCards = new List<Card>();
            Stack<Card> deck = new Stack<Card>(_CardList);

            if (quantity > deck.Count) quantity = deck.Count;
            for(int i = 0; i < quantity; i++)
            {
                Card card = deck.Pop();
                card.isActive = true;
                card.HandId = InitialSize - _CardList.Count;
                outCards.Add(card);
            }

            _CardList = deck.ToList();

            return outCards;
        }
    }
}