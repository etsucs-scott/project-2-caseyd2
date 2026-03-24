using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarGame.Core
{
    // Represents a deck of cards
    public class Deck
    {
        // Stack to hold all cards
        private Stack<Card> cards = new Stack<Card>();

        // Creates and prepares the deck
        public Deck()
        {
            BuildDeck();
            Shuffle();

        }

        // Builds a standard 52 card deck
        private void BuildDeck()
        {
            foreach (Suit suit in Enum.GetValues(typeof(Suit)))
            {
                foreach (Rank rank in Enum.GetValues(typeof(Rank)))
                {
                    cards.Push(new Card(suit, rank));
                }
            }
        }

        // Randomly shuffles the deck
        public void Shuffle()
        {
            Random random = new Random();
            List<Card> temp = new List<Card>(cards);
            cards.Clear();

            while (temp.Count > 0)
            {
                int index = random.Next(temp.Count);
                cards.Push(temp[index]);
                temp.RemoveAt(index);
            }
        }

        // Draws the top card from the deck
        public Card DrawCard()
        {
            return cards.Pop();
        }

        // Returns how many cards are left
        public int Count()
        {
            return cards.Count;
        }
    }
}

