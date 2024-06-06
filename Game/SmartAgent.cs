using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Domain;
using Domain.Enums;

namespace Game;
class SmartAgent : Player {
    public override (EPlayerAction, Card?) ChooseMove(Game game) {
        List<Card?> validMoves = game.GetValidMoves();
        
        return (EPlayerAction.Move, null);
    }
}