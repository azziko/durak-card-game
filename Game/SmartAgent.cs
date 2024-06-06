using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using Domain;
using Domain.Enums;

namespace Game;
class SmartAgent : Player {
    int TrumpBonus = 1500;
    int unbalancedHandPenalty = 900;
    double[] multipleBonus = {0.0, 0.0, 0.5, 0.75, 1.25};
    public override (EPlayerAction, Card?) ChooseMove(Game game) {
        List<Card?> validMoves = game.GetValidMoves();
        Bout bout = game.GetBout();
        int deckSize = game.GetDeckSize();

        //Attacking with trumps early game is bad
        if(bout.isAttackersTurn() && bout.AttackingCards.Count > 0){
            if(
                !validMoves.Any(card => card != null && card.Suit != game.Trump.Suit) &&
                deckSize > 12
            ){
                return(EPlayerAction.Move, null);
            }
        }

        //It's beneficial to take good cards early game
        if(!bout.isAttackersTurn() && bout.AttackingCards.Count < 3){
            int boutScore = 0;
            foreach(Card card in bout.AttackingCards){
                boutScore += card.Score;
                if(card.Suit == game.Trump.Suit){
                    boutScore += 1500;
                }
            }

            if(
                boutScore >= 1500 &&
                deckSize > 12
            ){
                return(EPlayerAction.Move, null);
            }
        }

        Card? bestMove = null;
        int currentBest = int.MinValue;
        foreach(Card? move in validMoves){
            if(move == null){
                continue;
            }

            List<Card> _hand = hand.Where(card => card != move).ToList();
            int moveEval = evalHand(game.Trump, _hand);
            
            if(moveEval >= currentBest){
                currentBest = moveEval;
                bestMove = move;
            }
        }

        return (EPlayerAction.Move, bestMove);
    }

    private int evalHand(Card Trump, List<Card> _hand){
        int eval = 0;
        int[] cardsByValue = new int[(int)ECardValue.Ace - (int)ECardValue.Six + 1]; 
        int[] cardsBySuit = new int[4]; 

        //Independent card score
        foreach(Card card in _hand){
            eval += card.Score;

            if(card.Suit == Trump.Suit){
                eval += TrumpBonus;
            }

            cardsByValue[(int)card.Val - (int)ECardValue.Six]++;
            cardsBySuit[(int)card.Suit]++;
        }

        //Bonus for multiple cards of the same value 
        foreach(ECardValue _val in Enum.GetValues(typeof(ECardValue))){
            eval += (int)_val * (int)multipleBonus[cardsByValue[(int)_val - (int)ECardValue.Six]];
        }

        //Penalty for unbalanced non-Trump cards
        double avgNonTrump = 0;
        foreach(ECardSuit _suit in Enum.GetValues(typeof(ECardSuit))){
            if(_suit == Trump.Suit)
                continue;

            avgNonTrump += cardsBySuit[(int)_suit];
        }

        avgNonTrump /= 3;
        foreach(ECardSuit _suit in Enum.GetValues(typeof(ECardSuit))){
            if(_suit == Trump.Suit)
                continue;
            
            double scalar = Math.Abs((cardsBySuit[(int)_suit] - avgNonTrump) / avgNonTrump);
            eval -= (int)(unbalancedHandPenalty * scalar);
        }

        return eval;
    }
}