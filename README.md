# Classic Russian card game - Durak

## How to start playing:
- [Game specifications](#game-specifications)
- [Running the game](#running-the-game)
- [Interface](#interface)

### Game specifications:
To get familiar with the rules, I recommend reading the [Wiki](https://en.wikipedia.org/wiki/Durak) page of the game first. Having in mind you are familiar with the rules, here are the following specifications of the implementation: 
- There are 36 cards in the deck initially, each player gets 6 fresh cards from the top of the shuffled deck. 
- First player is determined based on the lowest trump card in each player's hand. If none, first player is chosen randomly.
- After each successful round the attacking player draws first up to 6 cards, followed by everybody else clockwise.
- The trump is revealed after shuffling the deck and it remains the bottom card till the next shuffle.
- Classical Attack/Defense variaton, i.e common variation called Perevodnoy ("transferrable" or "reversible") is not supported.
- 2 player variation, you play agains the smart bot.
- No draws. First player to empty their hand while no cards in the decks are left wins.

### Running the game:
After you install the game, move into root repository of the project and run 
```shell 
dotnet run
```
You will be met with the welcoming message + some explanation on how to make a move.

### Interface:
The interface is fairly simple. Each succesfull move you get to see the new state. All the open info are separated from each other by the new line, and the bout is in the middle of the each new print. If the bout is initially empty, it means that this is your turn to attack. Otherwise, you are defending.

In order to play the card from your hand, write its position in your hand, starting from 1(the most left card). If you want to take no action, type 0.

Here is an example of what a board state could look like:
```
=========================================
Trump Card: [♣ 10]

Your Hand: [♣ A] [♠ J] [♥ 9] [♣ K]
Deck size: 24 cards

Bout:
-----------------------------------------
Attacking Cards: [♣ 6] [♦ 6]
Defending Cards: [♣ J] [♦ 8]
-----------------------------------------

Opponent's Cards: 4
```

In this particular example we have attacked with 2 sixes and our opponent succesfully defended. From now on we can either attack with more cards([♠ J] is a valid attack) or pass typing 0.

## For developers:
- [Briefly on the strucure](#briefly-on-the-structure)
- [Bots](#bots)
- [Running bots simulation](#running-bots-simulation)

### Briefly on the strucure:
This is a dependency free project written with two main entities being the Game and Domain. 
#### Game:
Game contains all the game definitions, players' behaviour and state. The main class Game is separated into two partial classes to distinguish public and private implementations. While it is not necessary, it makes it much easier to read the code.

#### Domain:
Domain's role is to prepare all the necessary building blocks of the game, i.e cards, enums and the deck that are independent of the game implementation. 

### Bots:
There are currently 2 bots in the game. First being RandomAgent, which serves as a dummy to compare the other bots' implementation.

#### SmartAgent:
The more interesting bot is of course the SmartAgent, which utilizes the rules of the game to play optimally. Its main strategy is to use heuristic function, which analyzes the hand. Based on the analyzis it chooses the move that keeps the hand as string as possible for the turns to come.

The heuristic function of the bot takes into account the card's value, whether it is a trump, proportion of cards with non-trump suit and finally the number of cards that share value.

##### Carrot:
The hand is rewarded extra points for having more cards of the same value, because we can easily overwhelm our opponent with the massive attack no matter what their response is.

##### Stick:
The hand receives penalty for having disproportional amount of cards with some suit different from trump because even if we manage to withstand one attack, our hand is vulnerable to cards of different suits. 

All the numbers for evaluations are experementally chosen. 

### Running bots simulation:
To run the simulation between the SmartAgent and RandomAgent use the following command when firing up the application:
``` shell
dotnet run -- sim N
```

Where sim signifies that we want to run simulation and **N** being the number of simulations you wish to run.
When all games were succesfully played out by the bots, you get to see the total number of games won by each bot.