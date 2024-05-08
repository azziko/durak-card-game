using System.Collections.Generic;

namespace Domain;

class Player{
    private List<Card> hand = new List<Card>();

    public List<Card> GetCards(){
        return hand;
    }

    public void AddCards(List<Card> cardsToAdd){
        hand.AddRange(cardsToAdd);
    }
}