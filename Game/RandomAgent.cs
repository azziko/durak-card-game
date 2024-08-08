using System.Collections.Generic;
using Domain;
using Domain.Enums;

namespace Game;
class RandomAgent : Agent {
    public override (EPlayerAction, Card?) ChooseMove(Game game) {
        List<Card?> validMoves = game.GetValidMoves();

        int randomSkip = game.Rand.Next(100);
        if(randomSkip < 20 && validMoves.Any(move => move == null)){
            return(EPlayerAction.Move, null);
        } else {
            if(validMoves.Count > 1){
                validMoves.Remove(null);
            }
        }

        return (
            EPlayerAction.Move, 
            validMoves[game.Rand.Next(validMoves.Count-1)]
        );
    }
}