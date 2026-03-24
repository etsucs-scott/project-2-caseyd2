using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarGame.Core
{
    // Represents a single playing card
        public class Card : IComparable<Card>
        {
        // Card suit
        public Suit Suit { get; set; }
        // Card rank
        public Rank Rank { get; set; }

        //Creates a card with a suit and a rank
        public Card(Suit suit, Rank rank)
        {
            Suit = suit;
            Rank = rank;
        }

        // Compares cards by rank (Ace high)
        public int CompareTo(Card other)
        {
            return Rank.CompareTo(other.Rank);
        }

        // Returns card description in text form
        public override string ToString()
        {
            return $"{Rank} of {Suit}";
        }
    }
}

