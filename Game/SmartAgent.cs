using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using Domain;
using Domain.Enums;

namespace Game;
class SmartAgent : Player {
    int trampBonus = 1000;
    int unbalancedHandPenalty = 200;
    int handCountPenalty = 300;
    double[] multipleBonus = {0.0, 0.0, 0.5, 0.75, 1.25};
    public override (EPlayerAction, Card?) ChooseMove(Game game) {
        List<Card?> validMoves = game.GetValidMoves();
        Card? bestMove = null;
        int currentBest = int.MinValue;

        //Todo: Eval skipping too;
        foreach(Card? move in validMoves){
            if(move == null){
                //TODO
                continue;
            }

            List<Card> _hand = hand.Where(card => card != move).ToList();
            int moveEval = evalHand(game.Tramp, game.GetDeckSize(), _hand);
            
            if(moveEval >= currentBest){
                currentBest = moveEval;
                bestMove = move;
            }
        }

        return (EPlayerAction.Move, bestMove);
    }

    private int evalHand(Card tramp, int cardsLeft, List<Card> _hand){
        int eval = 0;
        int[] cardsByValue = new int[(int)ECardValue.Ace - (int)ECardValue.Six + 1]; 
        int[] cardsBySuit = new int[4]; 

        //Independent card score
        foreach(Card card in _hand){
            eval += card.Score;

            if(card.Suit == tramp.Suit){
                eval += trampBonus;
            }

            cardsByValue[(int)card.Val - (int)ECardValue.Six]++;
            cardsBySuit[(int)card.Suit]++;
        }

        //Bonus for multiple cards of the same value 
        foreach(ECardValue _val in Enum.GetValues(typeof(ECardValue))){
            eval += (int)_val * (int)multipleBonus[cardsByValue[(int)_val - (int)ECardValue.Six]];
        }

        //Penalty for unbalanced non-tramp cards
        double avgNonTramp = 0;
        foreach(ECardSuit _suit in Enum.GetValues(typeof(ECardSuit))){
            if(_suit == tramp.Suit)
                continue;

            avgNonTramp += cardsBySuit[(int)_suit];
        }

        avgNonTramp /= 3;
        foreach(ECardSuit _suit in Enum.GetValues(typeof(ECardSuit))){
            if(_suit == tramp.Suit)
                continue;
            
            double scalar = Math.Abs((cardsBySuit[(int)_suit] - avgNonTramp) / avgNonTramp);
            eval -= (int)(unbalancedHandPenalty * scalar);
        }

        //Penalty for #cards in hand
        if(cardsLeft <= 10){
            eval -= handCountPenalty * _hand.Count;
        }

        return eval;
    }
}