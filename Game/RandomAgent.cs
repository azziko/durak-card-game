using System.Collections.Generic;
using Domain;
using Domain.Enums;

namespace Game;
class RandomAgent : Player {
    public override (EPlayerAction, Card?) ChooseMove(Game game) {
        List<Card?> validMoves = game.GetValidMoves();

        return (
            EPlayerAction.Move, 
            validMoves[game.Rand.Next(validMoves.Count)]
        );
    }
}