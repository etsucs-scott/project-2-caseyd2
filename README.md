# War Game Simulation

## Overview
This program simulates the card game War in the console.

This project is split into two parts.
    - WarGame.Core contains all the game logic
	- WarGame.Console runs the program and prints the output

## How to Run
Run the program in Visual studio using start or in the terminal.

## How it works
- The program asks for the number of players (2-4)
- A 52 card deck is created, shuffled and dealt evenly
- Each round:
	- players reveal one card
	- The highest card wins
	- All cards go into a pot
- If there is a tie:
	- Only tied players play agin
	- The winner takes the entire pot
- The game continues until:
	- One player has all the cards or 10,000 rounds are reached

## Output
Each round shows:
- The round number
- Each player's card
- The winner of the round
- Any ties and tiebreakers
- Current card counts

## Notes
- Only rank matters
- Ace is the highest card
- Cards are added to the back of the winner's hand

## GitHub
This project was completed for project 2 and submitted via GitHub Classroom.

