using System.Collections.Generic;
using Domain;

namespace Game;
class RandomAgent : Player {
    public override Card? ChooseMove(Game game) {
        List<Card?> validMoves = game.GetValidMoves();

        return validMoves[game.Rand.Next(validMoves.Count)];
    }
}