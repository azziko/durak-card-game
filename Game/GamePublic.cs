using Domain;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Game;

partial class Game{
    public Random Rand = new Random((int)(DateTime.Now.Ticks));
    public Card Tramp;

    public Game(List<Player> _players){
        players = _players;
        deck = new Deck();
        bout = new Bout();
        Tramp = deck.Tramp;
        
        startNewGame();
    }

    public int GetDeckSize(){
        return deck.CardsLeft();
    }

    public Bout GetBout(){
        return bout;
    }

    public bool IsActiveBot(){
        Player currentPlayer = bout.isAttackersTurn() ? players[activePlayer] : players[(activePlayer + 1)%players.Count]; 

        if(currentPlayer is RandomAgent || currentPlayer is SmartAgent)
            return true;

        return false;
    }

    public EPlayerAction Move(){
        Player currentPlayer = bout.isAttackersTurn() ? players[activePlayer] : players[(activePlayer + 1)%players.Count]; 
        (EPlayerAction action, Card? card) = currentPlayer.ChooseMove(this);

        if(action == EPlayerAction.Exit){
            return action;
        } else if(action == EPlayerAction.Restart){
            deck = new Deck();
            bout = new Bout();
        
            startNewGame();
            return action;
        }

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

        if(winner > -1){
            Console.WriteLine($"Player {players[winner].GetType()} won!");
            return EPlayerAction.Exit;
        }

        return action;
    }

    public List<Card> GetHandActivePlayer(){        
        Player currentPlayer = bout.isAttackersTurn() ? players[activePlayer] : players[(activePlayer + 1)%players.Count];

        return currentPlayer.GetCards();
    }

    public int CountCardsOpponent(){
        Player opponentPlayer = bout.isAttackersTurn() ? players[(activePlayer + 1)%players.Count] : players[activePlayer];

        return opponentPlayer.CountCards();
    }

    public List<Card?> GetValidMoves(){
        //null is a valid move and counts as turn skip
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
