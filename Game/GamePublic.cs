using Domain;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Game;

partial class Game{
    public Random Rand = new Random((int)(DateTime.Now.Ticks));

    public Game(List<Player> _players){
        players = _players;
        deck = new Deck();
        bout = new Bout();
        
        startNewGame();
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

        return action;
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