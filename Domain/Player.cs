using System.Collections.Generic;

namespace Domain;

class Player{
    private List<Card> hand = new List<Card>();

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