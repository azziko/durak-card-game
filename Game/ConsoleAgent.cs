using System.Collections.Generic;
using Domain;
using Domain.Enums;

namespace Game;
class ConsoleAgent : Player {
    public override (EPlayerAction, Card?) ChooseMove(Game game) {
        //TODO: Polling the player for a move until valid one is chosen
        //Remark: don't print anything, it's View's responsibility

        return (EPlayerAction.Move, null);
    }
}