using System.Collections.Generic;
using System.Linq;
using System;
using Domain.Enums;

namespace Domain;

class Deck {
    private List<Card> cards;
    private Random rand = new Random((int)DateTime.Now.Ticks);
    public ECardSuit Tramp;

    public Deck(){
        cards = new List<Card>();

        foreach(ECardValue _val in Enum.GetValues(typeof(ECardValue))){
            foreach(ECardSuit _suit in Enum.GetValues(typeof(ECardSuit))){
                cards.Add(new Card(){
                    Suit = _suit,
                    Val = _val,
                    Score = (int)_val,
                });
            }
        }

        shuffle();
    }

    public List<Card> Draw(int n){
        var drawnCards = cards.Take(n).ToList();
        cards.RemoveAll(x => drawnCards.Contains(x));

        return drawnCards;
    }

    private void shuffle(){
        //Fisher shuffling algorithm
        for(int i = cards.Count-1; i > 0; i--){
            int randCardIndex = rand.Next(i+1);
            
            Card temp = cards[i];
            cards[i] = cards[randCardIndex];
            cards[randCardIndex] = temp;
        }

        Tramp = cards.Last().Suit;
    }
}