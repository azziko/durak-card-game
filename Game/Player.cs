using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Domain;
using Domain.Enums;

namespace Game;

abstract class Player{
    protected List<Card> hand = new List<Card>();
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

    public void ClearHand(){
        hand = new List<Card>();
    }

    public bool RemoveCard(Card card){
        return hand.Remove(card);
    }
}

abstract class Agent : Player{}