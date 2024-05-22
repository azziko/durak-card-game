using Domain;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Game;

class Game{
    public Random Rand = new Random((int)(DateTime.Now.Ticks));

    private Bout bout;
    private List<Player> players;
    private Deck deck;
    private List<Card> discardPile = new List<Card>();
    private int activePlayer = 0;

    public Game(List<Player> _players){
        players = _players;
        deck = new Deck();
        bout = new Bout();
        
        startNewGame();
    }

    private (bool, string) isValidMove(Card? card){
        if(card == null){
            if(
                bout.isAttackersTurn() &&
                bout.AttackingCards.Count == 0
            ) return (false, "Attack is obligatory");

            return (true, "");
        }
        
        if(bout.isAttackersTurn()){
            if(bout.AttackingCards.Count == 0){
                return (true, "");
            } else {
                if(
                    (bout.AttackingCards.Find(c => c.Val == card.Val) != null) ||
                    (bout.DefendingCards.Find(c => c.Val == card.Val) != null)
                ){
                    return (true, "");
                } else {
                    return (false, $"card {card} does not match any Value on board");
                }
            }
        } else {
            Card cardtoBeat = bout.DefendingCards.Last();

            if(card.Suit == deck.Tramp){
                if(
                    cardtoBeat.Suit == deck.Tramp &&
                    cardtoBeat.Val > card.Val
                ){
                    return(false, $"{cardtoBeat} is not beatable by {card}");
                } else {
                    return(true, "");
                }
            } else {
                if(cardtoBeat.Suit == deck.Tramp){
                    return(false, $"{cardtoBeat} is not beatable by {card}");
                }

                if(cardtoBeat.Suit != card.Suit){
                    return(false, $"{cardtoBeat} and {card} don't much the suits");
                }

                if(cardtoBeat.Val > card.Val){
                    return(false, $"{cardtoBeat} has higher value than {card}");
                }

                return (true, "");
            }
        }
    }

    private void startNewGame(){
        Card? lowestTramp = null;
        for(int i = 0; i < players.Count; i++){
            players[i].AddCards(deck.Draw(6));
            Card? lowestTrampDealt = players[i].GetCards().Where(card => card.Suit == deck.Tramp)
                                    .OrderBy(card => card.Val)
                                    .FirstOrDefault();

            if(lowestTrampDealt != null){
                if(
                    lowestTramp != null &&
                    lowestTrampDealt.Val < lowestTramp.Val
                ){
                    lowestTramp = lowestTrampDealt;
                } else {
                    lowestTramp = lowestTrampDealt;
                }
            }
        }

        if(lowestTramp == null){
            activePlayer = Rand.Next(players.Count);
        }
    }

    public void Move(){
        Player currentPlayer = bout.isAttackersTurn() ? players[activePlayer] : players[(activePlayer + 1)%players.Count]; 
        Card? card = currentPlayer.ChooseMove(this);

        try{
            (bool isValid, string message)= isValidMove(card);
            if(!isValid){
                throw new InvalidMoveException(message);
            }

            if(!makeMove(card)){
                if(card == null){
                    throw new InvalidMoveException($"Something went wrong");
                } else {
                    throw new InvalidMoveException($"No {card} in your hand");
                }
            }
        }
        catch (InvalidMoveException exceptInv){
            Console.WriteLine($"Error: {exceptInv.Message}. Please, choose another play");
        }
    }

    private void refillHands(){
        for(int i = 0; i < players.Count; i++){
            Player currentPlayer = players[(activePlayer + i)%players.Count];
            int cardsInHand = currentPlayer.CountCards();

            if(cardsInHand < 6){
                currentPlayer.AddCards(deck.Draw(6 - cardsInHand));
            }
        }
    }

    private void nextPlayerClockWise(int n){
        activePlayer = (activePlayer + n) % players.Count;
    }

    private bool makeMove(Card? card){
        Player currentPlayer = bout.isAttackersTurn() ? players[activePlayer] : players[(activePlayer + 1)%players.Count]; 

        if(card == null){
            bool wasAttackersTurn = bout.isAttackersTurn();
            List<Card> cardsCleared = bout.ClearBout();

            if(wasAttackersTurn){
                discardPile.AddRange(cardsCleared);
                nextPlayerClockWise(1);
            } else {
                currentPlayer.AddCards(cardsCleared);
                nextPlayerClockWise(2);
            }

            refillHands();

            return true;
        }

        bool wasRemoved = currentPlayer.RemoveCard(card);
        if(!wasRemoved){
            return false;
        }
        bout.PlayCard(card);

        return true;
    }

    public List<Card?> GetValidMoves(){
        List<Card?> validMoves = new List<Card?>{null};
        Player currentPlayer = bout.isAttackersTurn() ? players[activePlayer] : players[(activePlayer + 1)%players.Count];

        foreach(Card card in currentPlayer.GetCards()){
            (bool isValid, string _) = isValidMove(card);
            if(isValid){
                validMoves.Add(card);
            }
        }
        
        return validMoves;
    }
}