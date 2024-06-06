using System.Collections.Generic;
using Domain;
using Domain.Enums;

namespace Game;
class RandomAgent : Player {
    //Inspired by a good friend of mine, Daniyal 
    //Nice tabletop playing skills

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