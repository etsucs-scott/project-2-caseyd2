using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace WarGame.Core
{
    // Handles the main game logic for War
    public class WarGame
    {
        // Stores each player and their hand
        private Dictionary<string, Hand> playerHands = new Dictionary<string, Hand>();
        // Holds cards played during a round 
        private List<Card> pot = new List<Card>();

        // Cards played the most in the most recent round
        public Dictionary<string, Card> LastPlayedCards { get; private set; } = new Dictionary<string, Card>();
        // True if the last round had a tie
        public bool lastRoundWasTie { get; private set; } = false;
        // Cards played during a tiebreaker
        public Dictionary<string, Card> LastTieBreakercards { get; private set; } = new Dictionary<string, Card>(); 
        // Players involved in the last tie
        public List<string> LastTiePlayers { get; private set; } = new List<string>();

        // Creates players and deals the cards for the round
        public WarGame(int numberOfPlayers)
        {
            for (int i = 1; i <= numberOfPlayers; i++)
            {
                playerHands.Add($"Player {i}", new Hand());
            }
            DealCards();
        }

        // Deals all cards evenly to the players
        private void DealCards()
        {
            Deck deck = new Deck();
            List<string> players = new List<string>(playerHands.Keys);
            int playerIndex = 0;

            while (deck.Count() > 0)
            {
                Card card = deck.DrawCard();
                playerHands[players[playerIndex]].AddCard(card);

                playerIndex++;
                // Loop back to first player
                if (playerIndex >= players.Count)
                    playerIndex = 0;
            }
        }
        // Returns players who still have cards
        private List<string> GetActivePlayers()
        {
            List<string> activePlayers = new List<string>();
            foreach (string playerName in playerHands.Keys)
            {
                if (playerHands[playerName].Count() > 0)
                    activePlayers.Add(playerName);
            }
            return activePlayers;
        }

        // Gets how many cards a player has
        public int GetCardCount(string playerName)
        {
            return playerHands[playerName].Count();
        }

        // Returns all player names
        public List<string> GetPlayerNames()
        {
            return new List<string>(playerHands.Keys);
        }

        // Returns number of players still in the game
        public int GetActivePlayerCount()
        {
            return GetActivePlayers().Count;
        }

        // Returns the winner if only one player is left in the round
        public string GetGameWinner()
        {
            List<string> activePlayers = GetActivePlayers();

            if (activePlayers.Count == 1)
                return activePlayers[0];

            return "";
        }

        // Plays one round of the game and determines the winner
        public string PlayRound()
        {
            // Reset round tracking
            LastPlayedCards.Clear();
            LastTieBreakercards.Clear();
            LastTiePlayers.Clear();
            lastRoundWasTie = false;

            // Get players still in the game
            List<string> activePlayers = GetActivePlayers();
            if (activePlayers.Count == 0)
                return "No winner";

            if (activePlayers.Count == 1)
                return activePlayers[0];

            // Start with all active players 
            List<string> tiedPlayers = new List<string>(activePlayers);

            // Keep playing rounds untill a winner is found
            while (true)
            {
                // Stores cards played this round
                Dictionary<string, Card> playedCards = new Dictionary<string, Card>();
               
                foreach (string playerName in tiedPlayers)
                {
           
                    if (playerHands[playerName].Count() > 0)
                    {
                        Card card = playerHands[playerName].PlayCard();
                        playedCards.Add(playerName, card);
                        pot.Add(card);
                    }
                }

                // If no cards were played, it is a draw
                if (playedCards.Count == 0)
                {
                    pot.Clear();
                    return "Draw";
                }

                // Save cards from the first play
                if (LastPlayedCards.Count == 0)
                {
                    foreach (var p in playedCards)
                        LastPlayedCards[p.Key] = p.Value;
                }
                else
                {
                    // Save cards from tiebreaker rounds
                    LastTieBreakercards.Clear();
                    foreach (var p in playedCards)
                        LastTieBreakercards[p.Key] = p.Value;
                }

                // Find the highest card played
                Card highestCard = playedCards.Values.First();

                foreach (var card in playedCards.Values)
                {
                    if (card.CompareTo(highestCard) > 0)
                        highestCard = card;
                }

                // Find players who played the highest card
                List<string> roundWinners = new List<string>();

                foreach (var p in playedCards)
                {
                    if (p.Value.CompareTo(highestCard) == 0)
                        roundWinners.Add(p.Key);
                }

                // If more than one winner, it's a tie
                if (roundWinners.Count > 1)
                {
                    lastRoundWasTie = true;
                    LastTiePlayers = new List<string>(roundWinners);
                }

                // If one winner, give them all cards in the pot
                if (roundWinners.Count == 1)
                { 
                    string winner = roundWinners[0];
                    playerHands[winner].AddCards(pot);
                    pot.Clear();
                    return winner;
                }

                // Reset tied players for next round 
                tiedPlayers.Clear();

                // Only keep players who are still tied and have cards 
                foreach (string playerName in roundWinners)
                {
                    if (playerHands[playerName].Count() > 0)
                        tiedPlayers.Add(playerName);
                }

                // If only one player remains after a tie, they are the winner
                if (tiedPlayers.Count == 1)
                {
                    string winner = tiedPlayers[0];
                    playerHands[winner].AddCards(pot);
                    pot.Clear();
                    return winner;
                }

                // If no players left, it is a draw
                if (tiedPlayers.Count == 0)
                {
                    pot.Clear();
                    return "Draw";
                }
            }
        }
    }
}

