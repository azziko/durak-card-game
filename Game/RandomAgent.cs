using System.Collections.Generic;
using Domain;
using Domain.Enums;

namespace Game;
class RandomAgent : Player {
    public override (EPlayerAction, Card?) ChooseMove(Game game) {
        List<Card?> validMoves = game.GetValidMoves();
        if(validMoves.Count > 1) {
            return (
                EPlayerAction.Move, 
                validMoves[game.Rand.Next(1, validMoves.Count)]
            );
        } else {
            return (EPlayerAction.Move, null);
        }
    }
}