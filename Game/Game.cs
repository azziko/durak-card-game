using Domain;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Game;

class Game{
    private Random rand = new Random((int)(DateTime.Now.Ticks));

    private Bout bout;
    private List<Player> players;
    private Deck deck;
    private List<Card> discardPile = new List<Card>();
    private int activePlayer = 0;

    public Game(List<Player> _players){
        players = _players;
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
                    return (false, $"card {card.ToString()} does not match any Value on board");
                }
            }
        } else {
            Card cardtoBeat = bout.DefendingCards.Last();

            if(card.Suit == deck.Tramp){
                if(
                    cardtoBeat.Suit == deck.Tramp &&
                    cardtoBeat.Val > card.Val
                ){
                    return(false, $"{cardtoBeat.ToString()} is not beatable by {card.ToString()}");
                } else {
                    return(true, "");
                }
            } else {
                if(cardtoBeat.Suit == deck.Tramp){
                    return(false, $"{cardtoBeat.ToString()} is not beatable by {card.ToString()}");
                }

                if(cardtoBeat.Suit != card.Suit){
                    return(false, $"{cardtoBeat.ToString()} and {card.ToString()} don't much the suits");
                }

                if(cardtoBeat.Val > card.Val){
                    return(false, $"{cardtoBeat.ToString()} has higher value than {card.ToString()}");
                }

                return (true, "");
            }
        }

        return (false, "Unknown move");
    }

    private void startNewGame(){
        deck = new Deck();
        bout = new Bout();

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
            activePlayer = rand.Next(players.Count);
        }
    }

    public void Move(Card? card){
        try{
            (bool isValid, string message)= isValidMove(card);
            if(!isValid){
                throw new InvalidMoveException(message);
            }

            if(!makeMove(card)){
                throw new InvalidMoveException($"No {card.ToString()} in your hand");
            }
        }
        catch (InvalidMoveException exceptInv){
            Console.WriteLine($"Error: {exceptInv.Message}. Please, choose another play");
        }
    }

    private void nextPlayerClockWise(int n){
        activePlayer = (activePlayer + n) % players.Count;
    }

    private bool makeMove(Card? card){
        Player currentPlayer = bout.isAttackersTurn() ? players[activePlayer] : players[(activePlayer + 1)%players.Count]; 

        if(card == null){
            //TODO: draw cards to players
            bool wasAttackersTurn = bout.isAttackersTurn();
            List<Card> cardsCleared = bout.ClearBout();

            if(wasAttackersTurn){
                discardPile.AddRange(cardsCleared);
                nextPlayerClockWise(1);
            } else {
                currentPlayer.AddCards(cardsCleared);
                nextPlayerClockWise(2);
            }

            return true;
        }

        bool wasRemoved = currentPlayer.RemoveCard(card);
        if(!wasRemoved){
            return false;
        }
        bout.PlayCard(card);

        return true;
    }

    public List<Card> GetValidMoves(){
        List<Card> validMoves = new List<Card>();
        Player currentPlayer = bout.isAttackersTurn() ? players[activePlayer] : players[(activePlayer + 1)%players.Count];

        foreach(Card card in currentPlayer.GetCards()){
            (bool isValid, string mes) = isValidMove(card);
            if(isValid){
                validMoves.Add(card);
            }
        }
        
        return validMoves;
    }
}