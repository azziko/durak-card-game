using Domain.Enums;

namespace Domain;

class Card{
    public ECardSuit Suit { get; set; }
    public ECardValue Val { get; set; }
    public int Score { get; set; }

    public override string ToString(){
        string val ="";
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

        switch (Val){
            case ECardValue.Jack:
                val = "J";
                break;
            case ECardValue.Queen:
                val = "Q";
                break;
            case ECardValue.King: 
                val = "K";
                break;
            case ECardValue.Ace:
                val = "A";
                break;
            default:
                val = ((int)Val).ToString();
                break;
        }

        return $"{val}{suit}";
    }
}