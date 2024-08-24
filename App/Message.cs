namespace App;

public static class Message {
    public const string Welcome = @"Welcome to Durak - The Card Game!

Durak is a classic Russian card game where the objective is to avoid being the last player with cards.

Currently supported commands:
1. start - Begin a new game of Durak.
2. restart - Restart the current game from the beginning.
3. exit - Exit the game.
4. 0 - End your turn. If you are defending and there are unbeaten cards, you will take them.

To play a card, enter the card's position in hand starting from 1. For example, if your hand looks as follows:
[♦ 6] [♦ 10] [♥ Q] [♣ K]
and you want to select [♥ Q], type 3. 
Alternatively, you could type card's value along side the first letter of its suit to play the card.
For example, 6d, 10D, QH, Kc are all valid selections for the hand above.

Enjoy the game and may the best player win!

Type 'start' to begin or 'exit' to quit.
";
    public const string EndGame = @"";
    public const string Move = @"";
    public const string Exit = @"The game was exited";
    public const string Restart = @"The game was exited";
}