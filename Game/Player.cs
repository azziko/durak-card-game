using System.Collections.Generic;
using Domain;

namespace Game;

abstract class Player{
    protected List<Card> hand = new List<Card>();
    public abstract Card? ChooseMove(Game game);

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