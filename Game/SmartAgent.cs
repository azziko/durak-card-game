using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Domain;
using Domain.Enums;

namespace Game;
class SmartAgent : Player {
    public override (EPlayerAction, Card?) ChooseMove(Game game) {
        List<Card> hand = game.GetHandActivePlayer();
        
        return (EPlayerAction.Move, null);
    }
}