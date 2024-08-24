using Domain.Enums;

namespace Domain;

class Card{
    public ECardSuit Suit { get; set; }
    public ECardValue Val { get; set; }
    public int Score { get; set; }

    public override string ToString(){
        string val;
        string suit ="";
        switch (Suit){
            case ECardSuit.Diamonds:
                suit = "♦";
                break;
            case ECardSuit.Clubs:
                suit = "♣";
                break;
            case ECardSuit.Hearts:
                suit = "♥";
                break;
            case ECardSuit.Spades:
                suit = "♠";
                break;
        }

        val = GetCardValueShort();

        return $"{suit} {val}";
    }

    public string GetCardSuitShort(){
        return Suit switch {
            ECardSuit.Spades => "S",
            ECardSuit.Hearts => "H",
            ECardSuit.Diamonds => "D",
            ECardSuit.Clubs => "C",
            _ => "?"
        };
    } 

    public string GetCardValueShort(){
        return Val switch {
            ECardValue.Ace => "A",
            ECardValue.King => "K",
            ECardValue.Queen => "Q",
            ECardValue.Jack => "J",
            _ => ((int)Val).ToString()
        };
    }
}