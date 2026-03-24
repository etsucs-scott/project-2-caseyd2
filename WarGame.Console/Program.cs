using System;
using System.Collections.Generic;
using WarGame.Core;

namespace WarGame.ConsoleApp
{
    // Runs the game in the console
    class Program
    {

    // Starts the program and handles the input and output
        static void Main(string[] args)
      
            {
            // Stores the number of players
                int numberOfplayers;
            // Keep asking until input is valid
                while (true)
                {
                    Console.WriteLine("Enter number of players (2-4):");

                // try to convert input to an integer
                    if (int.TryParse(Console.ReadLine(), out numberOfplayers))
                    {
                    // Check if number is valid and in the range
                        if (numberOfplayers >= 2 && numberOfplayers <= 4)
                        {
                            break;
                        }
                    }
                    // Show error if invalid
                    Console.WriteLine("Invalid input.");
                }

                // Creates a new game with the selected number of players
                WarGame.Core.WarGame game = new WarGame.Core.WarGame(numberOfplayers);
                // Keeps track of the round number
                int round = 1;
                // Run the game until a winner is found
                while (true)
                {
                // Round header
                    Console.WriteLine();
                    Console.WriteLine("--- Round " + round + "---");

                // Play one round and get the winner
                    string winner = game.PlayRound();
                    Console.WriteLine("Winner: " + winner);

                // Show each player's card for this round
                foreach (var p in game.LastPlayedCards)
                {
                    Console.WriteLine(p.Key + ": " + p.Value);
                }
                // If there is a tie, show which players are tied
                if (game.lastRoundWasTie)
                {
                    Console.WriteLine("Tie between: " + string.Join(" , ", game.LastTiePlayers));
                }

                // Show tiebreaker cards used
                if (game.LastTieBreakercards.Count > 0)
                {
                    Console.Write("Tiebreaker: ");
                    foreach (var p in game.LastTieBreakercards)
                    {
                        Console.Write(p.Key + ": " + p.Value + " | ");
                    }

                    Console.WriteLine();
                }
                // Gets all the players names
                    List<string> players = game.GetPlayerNames();
                // Show each players card count
                    foreach (string player in players)
                    {
                        Console.WriteLine(player + " cards: " + game.GetCardCount(player));
                    }
                    // IF only one player remains, end the game
                    if (game.GetActivePlayerCount() == 1)
                    {
                        Console.WriteLine();
                        Console.WriteLine("Game winner: " + game.GetGameWinner());
                        break;
                    }
                    // If round limit is reached, determine the winner by card count
                    if (round >= 10000)
                    {
                        string topPlayer = players[0];
                        bool isTie = false;
                    // Find player with the most cards
                        foreach (string player in players)
                        {
                            if (game.GetCardCount(player) > game.GetCardCount(topPlayer))
                            {
                                topPlayer = player;
                                isTie = false;
                            }
                            else if (player != topPlayer && game.GetCardCount(player) == game.GetCardCount(topPlayer))
                            {
                            isTie = true;
                            }
                        }

                        // Show result if round limit is hit
                        Console.WriteLine();
                        if (isTie)
                        {
                            Console.WriteLine("Round limit reached. Game is a draw.");
                        }
                        else
                        {
                            Console.WriteLine("Round limit reached. Winner: " + topPlayer);
                        }
                        break;
                    }
                    // Wait for the next round
                    Console.WriteLine("Press any key for next round...");
                    Console.ReadKey();
                // Start the next round
                    round++;
                }
            }
        }
    }

