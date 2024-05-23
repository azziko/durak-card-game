using System.Collections.Generic;
using Domain;
using Domain.Enums;

namespace Game;

abstract class Player{
    protected List<Card> hand = new List<Card>();
    
    //Player should not know anything about the game, except open info.
    //However, it is being passed for simplicity of implementation sake.
    //TODO: change it, so the Player receives a clone state.
    public abstract (EPlayerAction, Card?) ChooseMove(Game game);

    public List<Card> GetCards(){
        return hand;
    }

    public int CountCards(){
        return hand.Count;
    }

    public void AddCards(List<Card> cardsToAdd){
        hand.AddRange(cardsToAdd);
    }

    public bool RemoveCard(Card card){
        return hand.Remove(card);
    }
}