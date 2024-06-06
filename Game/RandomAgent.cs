using System.Collections.Generic;
using Domain;
using Domain.Enums;

namespace Game;
class RandomAgent : Player {
    //Inspired by a good friend of mine, Daniyal 
    //Nice tabletop playing skills

    public override (EPlayerAction, Card?) ChooseMove(Game game) {
        List<Card?> validMoves = game.GetValidMoves();

        return (
            EPlayerAction.Move, 
            validMoves[game.Rand.Next(validMoves.Count-1)]
        );
    }
}