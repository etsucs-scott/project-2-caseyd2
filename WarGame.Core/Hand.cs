using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.Generic;

namespace WarGame.Core
{
    // Represents a player's hand of cards
    public class Hand
    {
        // Queue to store cards in order
        private Queue<Card> cards = new Queue<Card>();

        // Returns the number of cards in hand
        public int Count()
        { return cards.Count; }

        // Adds one card to the hand
        public void AddCard(Card card)
        {
            cards.Enqueue(card);
        }

        // Plays the top card in the hand
        public Card PlayCard()
        { return cards.Dequeue(); }

        // Adds multiple cards (used after winning a round)
        public void AddCards(List<Card> newCards)
        {
            foreach (Card card in newCards)
            {
                cards.Enqueue(card);
            }
        }
    }
}

