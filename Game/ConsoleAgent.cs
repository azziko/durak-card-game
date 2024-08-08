using Domain;
using Domain.Enums;

namespace Game;
class ConsoleAgent : Player {
    override public (EPlayerAction, Card?) ChooseMove(Game game) {
        List<Card> hand = game.GetHandActivePlayer();
        while(true){
            Console.Write("Choose a move (#): ");
            string input = Console.ReadLine()!;

            if(input == "exit"){
                return (EPlayerAction.Exit, null);
            }

            if(input == "skip"){
                return (EPlayerAction.Move, null);
            }

            if(Int32.TryParse(input, out int cardPos)){
                if(cardPos == 0){
                    return(EPlayerAction.Move, null);
                }

                if(cardPos > 0 && cardPos < hand.Count + 1){
                    return (EPlayerAction.Move, hand[cardPos-1]);
                } else {
                    Console.WriteLine($"{cardPos} is not in the range of cards in hand ({1}-{hand.Count})");
                }
            } else {
                Console.WriteLine($"{input} is not a valid command");
            }
        }
    }
}